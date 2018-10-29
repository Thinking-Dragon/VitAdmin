using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelChambre
    {
        #region GET

        public static List<Chambre> GetChambres(string idDepartement, string extends = "")
        {
            List<Chambre> chambres = new List<Chambre>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                List<int> idChambres = new List<int>();

                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT idChambre _id, nom " +
                        "FROM Chambres " +
                        "WHERE idDepartement = {0}",
                        idDepartement
                    ), lecteur =>
                    {
                        chambres.Add( new Chambre { _identifiant = int.Parse(lecteur.GetString("_id")), Numero = lecteur.GetString("nom") } );
                        if (extends != "")
                            idChambres.Add(int.Parse(lecteur.GetString("_id")));
                    }
                );

                for(int i = 0; i < idChambres.Count; ++i)
                {
                    if (extends.ToLower().Contains("lits"))
                        chambres[i].Lits = new ObservableCollection<Lit>(DataModelLit.GetLits(idChambres[i]));
                    if (extends.ToLower().Contains("equipements"))
                        chambres[i].Equipements = new ObservableCollection<Equipement>(DataModelEquipement.GetEquipements(idChambres[i]));

                }
            }

            return chambres;
        }

        public static int GetIdChambre(string numero)
        {
            int resultat = -1;

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT idChambre _id " +
                        "FROM Chambres " +
                        "WHERE nom = '{0}'",
                        numero
                    ), lecteur => resultat = int.Parse(lecteur.GetString("_id"))
                );
            }

            return resultat;
        }

        #endregion

        #region POST

        public static void PostChambre(int idDepartement, Chambre chambre)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "INSERT INTO Chambres (nom, idDepartement) " +
                        "VALUES ('{0}', {1}) ",
                        chambre.Numero, idDepartement
                    )
                );

                int idChambre = GetIdChambre(chambre.Numero);

                if(idChambre != -1)
                {
                    DataModelLit.PostLits(idChambre, new List<Lit>(chambre.Lits));
                    DataModelEquipement.PostEquipementsChambres(idChambre, new List<Equipement>(chambre.Equipements));
                }
            }
        }

        #endregion

        #region PUT

        public static void PutChambre(Chambre chambre)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE Chambres " +
                        "SET nom = '{0}' " +
                        "WHERE idChambre = {1}",
                        chambre.Numero, chambre._identifiant
                    )
                );

                DataModelLit.PutLits(chambre._identifiant, new List<Lit>(chambre.Lits));
                DataModelEquipement.DeleteEquipementsChambre(chambre._identifiant);
                DataModelEquipement.PostEquipementsChambres(chambre._identifiant, new List<Equipement>(chambre.Equipements));
            }
        }

        public static void PutChambres(int idDepartement, List<Chambre> chambres)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                List<Chambre> chambresExistantes = GetChambres(idDepartement.ToString());
                List<Chambre> chambresASupprimer = new List<Chambre>();

                foreach (var chambreExistante in chambresExistantes)
                    if (!chambres.Exists(c => c._identifiant == chambreExistante._identifiant))
                        chambresASupprimer.Add(chambreExistante);

                foreach (var chambreASupprimer in chambresASupprimer)
                {
                    chambresExistantes.Remove(chambreASupprimer);
                    DeleteChambre(chambreASupprimer);
                }

                foreach (var chambre in chambres)
                {
                    if (!chambresExistantes.Exists(c => c._identifiant == chambre._identifiant))
                        PostChambre(idDepartement, chambre);
                    else
                        PutChambre(chambre);
                }
            }
        }

        #endregion

        #region DELETE

        public static void DeleteChambre(Chambre chambre)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "DELETE FROM Chambres " +
                        "WHERE idChambre = {0}",
                        chambre._identifiant
                    )
                );

                if (chambre.Lits != null && chambre.Lits.Count > 0)
                    DataModelLit.DeleteLits(chambre._identifiant);
                if (chambre.Equipements != null && chambre.Equipements.Count > 0)
                    DataModelEquipement.DeleteEquipementsChambre(chambre._identifiant);
            }
        }

        #endregion
    }
}
