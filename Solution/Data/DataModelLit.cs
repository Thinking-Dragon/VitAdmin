using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelLit
    {
        #region GET

        public static List<Lit> GetLits(int idChambre)
        {
            List<Lit> lits = new List<Lit>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT l.idLit _id, l.numero, el.nom etat " +
                        "FROM Lits l " +
                        "JOIN EtatsLits el ON l.idEtatLit = el.idEtatLit " +
                        "WHERE idChambre = {0}",
                        idChambre
                    ), lecteur => lits.Add(
                        new Lit
                        {
                            _identifiant = int.Parse(lecteur.GetString("_id")),
                            Numero = lecteur.GetString("numero"),
                            EtatLit = (EtatLit) Enum.Parse(typeof(EtatLit), lecteur.GetString("etat"))
                        }
                    )
                );
            }

            return lits;
        }

        public static List<Lit> GetLitsDepartement(Departement departement)
        {
            List<Lit> lits = new List<Lit>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT idLit _id, l.numero NumLit, el.nom etat, c.Nom chNom, d.Nom depNom " +
                        "FROM Lits l " +
                        "JOIN EtatsLits el ON l.idEtatLit = el.idEtatLit " +
                        "JOIN Chambres c ON c.idChambre = l.idChambre " +
                        "JOIN Departements d ON d.idDepartement = c.idDepartement " +
                        "WHERE d.Nom = '" + departement.Nom + "' "
                    ), lecteur => lits.Add(
                        new Lit
                        {
                            _identifiant = int.Parse(lecteur.GetString("_id")),
                            Numero = lecteur.GetString("NumLit"),
                            EtatLit = (EtatLit)Enum.Parse(typeof(EtatLit), lecteur.GetString("etat")),
                            Chambre = new Chambre
                            {
                                Numero = lecteur.GetString("chNom"),
                                UnDepartement = new Departement
                                {
                                    Nom = lecteur.GetString("depNom")
                                }
                            }
                            
                            
                            
                        }
                    )
                );
            }

            return lits;
        }
        #endregion

        #region POST

        public static void PostLit(int idChambre, Lit lit)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "INSERT INTO `vitadmin_bd_main`.`lits` (`idLit`, `idCitoyen`, `idChambre`, `idEtatLit`, `numero`) VALUES (NULL, NULL, '{0}', (SELECT idEtatLit FROM EtatsLits WHERE nom = '{1}'), '{2}');",
                        /*"INSERT INTO Lits (idChambre, idEtatLit, numero) " +
                        "VALUES ( " +
                        "   {0}, " +
                        "   (SELECT idEtatLit FROM EtatsLits WHERE nom = '{1}'), " +
                        "   numero = '{2}'" +
                        ")",*/
                        idChambre, lit.EtatLit.ToString("g"), lit.Numero
                    )
                );
            }
        }

        public static void PostLits(int idChambre, List<Lit> lits)
        {
            foreach (var lit in lits)
                PostLit(idChambre, lit);
        }

        #endregion

        #region PUT

        public static void PutLit(Lit lit)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE Lits " +
                        "SET idEtatLit = (SELECT idEtatLit FROM EtatsLits WHERE nom = '{0}') " +  // Le numéro ne devrait pas pouvoir être modifié.
                        "WHERE idLit = {1}",
                        lit.EtatLit.ToString("g"), lit._identifiant
                    )
                );
            }
        }

        public static void PutLits(int idChambre, List<Lit> lits)
        {
            List<Lit> litsExistants = GetLits(idChambre);
            List<Lit> litsASupprimer = new List<Lit>();

            foreach (var litExistant in litsExistants)
                if (!lits.Exists(l => l._identifiant == litExistant._identifiant))
                    litsASupprimer.Add(litExistant);

            foreach (var litASupprimer in litsASupprimer)
            {
                DeleteLit(litASupprimer);
                litsExistants.Remove(litASupprimer);
            }

            foreach (var lit in lits)
            {
                if (!litsExistants.Exists(l => l._identifiant == lit._identifiant))
                    PostLit(idChambre, lit);
                else
                    PutLit(lit);
            }
        }

        public static void PutLitCitoyen(Lit lit, Citoyen citoyen)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE Lits " +
                        "SET idCitoyen = (SELECT idCitoyen FROM citoyens WHERE numAssuranceMaladie = '{0}') " + 
                        "WHERE idLit = {1}",
                        citoyen.AssMaladie, lit._identifiant
                    )
                );
            }
        }

        #endregion

        #region DELETE

        public static void DeleteLits(int idChambre)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "DELETE FROM Lits " +
                        "WHERE idChambre = {0}",
                        idChambre
                    )
                );
            }
        }

        public static void DeleteLit(Lit lit)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "DELETE FROM Lits " +
                        "WHERE idLit = {0}",
                        lit._identifiant
                    )
                );
            }
        }

        #endregion
    }
}
