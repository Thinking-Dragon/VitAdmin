using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelTraitement
    {
        #region GET

        public static int GetIdTraitement(string nom)
        {
            int idTraitement = -1;

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT t.idTraitement _id " +
                        "FROM Traitements t " +
                        "JOIN Departements d ON d.idDepartement = t.idDepartement " +
                        "WHERE t.nom = '{0}'",
                        nom
                    ), lecteur => idTraitement = int.Parse(lecteur.GetString("_id"))
                );
            }

            return idTraitement;
        }

        public static Traitement GetTraitement(string nom, bool expand = false)
        {
            Traitement traitement = null;

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT t.idTraitement _id, t.nom traitement, d.nom departement, d.abreviation dept " +
                        "FROM Traitements t " +
                        "JOIN Departements d ON d.idDepartement = t.idDepartement " +
                        "WHERE t.nom = '{0}'",
                        nom
                    ), lecteur =>
                    {
                        traitement = new Traitement
                        {
                            IdTraitment = int.Parse(lecteur.GetString("_id")),
                            Nom = lecteur.GetString("traitement"),
                            DepartementAssocie = new Departement
                            {
                                Nom = lecteur.GetString("departement"),
                                Abreviation = lecteur.GetString("dept")
                            }
                        };
                    }
                );
                if (expand && traitement != null && traitement.IdTraitment >= 0)
                    traitement.EtapesTraitement = new ObservableCollection<Etape>(DataModelEtape.GetEtapes(traitement.IdTraitment, true));
            }

            return traitement;
        }

        public static List<Traitement> GetTraitements(bool expand = false)
        {
            List<Traitement> traitements = new List<Traitement>();
            List<int> idTraitements = new List<int>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT t.idTraitement _id, t.nom traitement, d.nom departement, d.abreviation dept " +
                    "FROM Traitements t " +
                    "JOIN Departements d ON d.idDepartement = t.idDepartement", lecteur =>
                    {
                        Traitement traitement = new Traitement
                        {
                            IdTraitment = int.Parse(lecteur.GetString("_id")),
                            Nom = lecteur.GetString("traitement"),
                            DepartementAssocie = new Departement
                            {
                                Nom = lecteur.GetString("departement"),
                                Abreviation = lecteur.GetString("dept")
                            }
                        };
                        
                        traitements.Add(traitement);
                    }
                );
                if (expand)
                    for (int i = 0; i < traitements.Count; i++)
                        traitements[i].EtapesTraitement = new ObservableCollection<Etape>(DataModelEtape.GetEtapes(traitements[i].IdTraitment, true));
            }

            return traitements;
        }

        #endregion

        #region POST

        public static void PostTraitement(Traitement traitement)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "INSERT INTO Traitements (nom, idDepartement) " +
                        "VALUES ( " +
                        "   '{0}', " +
                        "   (SELECT idDepartement FROM Departements WHERE abreviation = '{1}') " +
                        ")",
                        traitement.Nom, traitement.DepartementAssocie.Abreviation
                    )
                );

                int idTraitement = -1;
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT t.idTraitement " +
                        "FROM Traitements t " +
                        "JOIN Departements d ON d.idDepartement = t.idDepartement " +
                        "WHERE t.nom = '{0}' AND d.abreviation = '{1}'",
                        traitement.Nom, traitement.DepartementAssocie.Abreviation
                    ), lecteur => idTraitement = int.Parse(lecteur.GetString("idTraitement"))
                );
                if (idTraitement >= 0)
                    DataModelEtape.PostEtapes(new List<Etape>(traitement.EtapesTraitement), idTraitement);
            }
        }

        #endregion

        #region PUT

        public static void PutTraitement(Traitement traitement)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                if (traitement.IdTraitment >= 0)
                {
                    ConnexionBD.Instance().ExecuterRequete(
                        String.Format(
                            "UPDATE Traitements " +
                            "SET nom = '{0}', idDepartement = (SELECT idDepartement FROM Departements WHERE abreviation = '{1}') " +
                            "WHERE idTraitement = {2}",
                            traitement.Nom, traitement.DepartementAssocie.Abreviation, traitement.IdTraitment
                        )
                    );
                    DataModelEtape.DeleteEtapes(traitement.IdTraitment);
                    DataModelEtape.PostEtapes(new List<Etape>(traitement.EtapesTraitement), traitement.IdTraitment);
                }
            }
        }

        public static void PutTraitements(List<Traitement> traitements)
        {
            /*
            if (ConnexionBD.Instance().EstConnecte())
            {
                List<Traitement> traitementsExistants = GetTraitements();

                for (int i = 0; i < traitementsExistants.Count; i++)
                {
                    int idTraitement = GetIdTraitement(traitementsExistants[i].Nom);
                    DataModelEtape.DeleteEtapes(idTraitement);

                    Traitement traitementNouveau = traitements.Find(t => t.Nom == traitementsExistants[i].Nom);
                    DataModelEtape.PostEtapes(new List<Etape>(traitementNouveau.EtapesTraitement), idTraitement);
                }

                List<Traitement> AAjouter = new List<Traitement>();

                for (int i = 0; i < traitements.Count; i++)
                {
                    bool existe = false;
                    for (int j = 0; j < traitementsExistants.Count; j++)
                        if (traitements[i].Nom == traitementsExistants[j].Nom)
                            existe = true;
                    if (!existe)
                        AAjouter.Add(traitements[i]);
                }

                for (int i = 0; i < AAjouter.Count; i++)
                    PostTraitement(AAjouter[i]);
            }
            */

            /*
            if(ConnexionBD.Instance().EstConnecte())
            {
                List<int> idTraitements = new List<int>();
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT idTraitement " +
                    "FROM Traitements", lecteur => idTraitements.Add(int.Parse(lecteur.GetString("idTraitement")))
                );
                foreach (int idTraitement in idTraitements)
                    DeleteTraitement(idTraitement);
                foreach (Traitement traitement in traitements)
                    PostTraitement(traitement);
            }
            */

            #region Version plus rapide, mais duplicant des données.
            if(ConnexionBD.Instance().EstConnecte())
            {
                List<Traitement> traitementsExistants = GetTraitements(true);
                
                for (int i = 0; i < traitementsExistants.Count; ++i)
                {
                    bool aSupprimer = true;
                    for (int j = 0; j < traitements.Count; ++j)
                        if (traitementsExistants[i].IdTraitment == traitements[j].IdTraitment)
                            aSupprimer = false;
                    if(aSupprimer)
                    {
                        DeleteTraitement(traitementsExistants[i]);
                        traitementsExistants.Remove(traitementsExistants[i]);
                    }
                }

                for(int i = 0; i < traitements.Count; ++i)
                {
                    bool aAjouter = true;
                    for (int j = 0; j < traitementsExistants.Count; ++j)
                        if (traitements[i].IdTraitment == traitementsExistants[j].IdTraitment)
                            aAjouter = false;
                    if(aAjouter)
                        PostTraitement(traitements[i]);
                }

                for (int i = 0; i < traitementsExistants.Count; ++i)
                    PutTraitement(traitementsExistants[i]);
            }
            #endregion
        }

        #endregion

        #region PATCH



        #endregion

        #region DELETE

        public static void DeleteTraitement(int idTraitement)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                if (idTraitement >= 0)
                {
                    DataModelEtape.DeleteEtapes(idTraitement);

                    ConnexionBD.Instance().ExecuterRequete(
                        String.Format(
                            "DELETE FROM Traitements " +
                            "WHERE idTraitement = {0}",
                            idTraitement
                        )
                    );
                }
            }
        }

        public static void DeleteTraitement(Traitement traitement)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                int idTraitement = -1;

                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT t.idTraitement " +
                        "FROM Traitements t " +
                        "JOIN Departements d ON d.idDepartement = t.idDepartement " +
                        "WHERE t.nom = '{0}' AND d.abreviation = '{1}'",
                        traitement.Nom, traitement.DepartementAssocie.Abreviation
                    ), lecteur => idTraitement = int.Parse(lecteur.GetString("idTraitement"))
                );

                DeleteTraitement(idTraitement);
            }
        }

        #endregion
    }
}
