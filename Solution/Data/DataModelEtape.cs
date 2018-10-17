using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelEtape
    {
        public static List<Etape> GetEtapes(int idTraitement, bool expand = false)
        {
            List<Etape> etapes = new List<Etape>();
            List<int> idEtapes = new List<int>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT idEtape _id, description nom " +
                        "FROM Etapes " +
                        "WHERE idTraitement = {0}",
                        idTraitement
                    ), lecteur =>
                    {
                        Etape etape = new Etape { Description = lecteur.GetString("nom") };

                        if (expand)
                            idEtapes.Add(int.Parse(lecteur.GetString("_id")));

                        etapes.Add(etape);
                    }
                );
                if (expand)
                    for (int i = 0; i < etapes.Count; i++)
                        etapes[i].Instructions = new ObservableCollection<string>(DataModelInstructionEtape.GetInstructions(idEtapes[i]));
            }

            return etapes;
        }

        public static void PostEtapes(List<Etape> etapes, int idTraitement)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                foreach (Etape etape in etapes)
                {
                    ConnexionBD.Instance().ExecuterRequete(
                        String.Format(
                            "INSERT INTO Etapes (description, idTraitement) " +
                            "VALUES ('{0}', {1})",
                            etape.Description, idTraitement
                        )
                    );

                    int idEtape = -1;
                    ConnexionBD.Instance().ExecuterRequete(
                        String.Format(
                            "SELECT idEtape " +
                            "FROM Etapes " +
                            "WHERE description = '{0}' AND idTraitement = {1}",
                            etape.Description, idTraitement
                        ), lecteur => idEtape = int.Parse(lecteur.GetString("idEtape"))
                    );
                    if (idEtape >= 0)
                        DataModelInstructionEtape.PostInstructions(new List<string>(etape.Instructions), idEtape);
                }
            }
        }

        public static void DeleteEtape(int idEtape)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                DataModelInstructionEtape.DeleteInstructions(idEtape);
                ConnexionBD.Instance().ExecuterRequete(
                     String.Format(
                         "DELETE FROM Etapes " +
                         "WHERE idEtape = {0}",
                         idEtape
                     )
                 );
            }
        }

        public static void DeleteEtapes(int idTraitement)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                List<int> idEtapes = new List<int>();
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT idEtape " +
                        "FROM Etapes " +
                        "WHERE idTraitement = {0} ",
                        idTraitement
                    ), lecteur => idEtapes.Add(int.Parse(lecteur.GetString("idEtape")))
                );
                for (int i = 0; i < idEtapes.Count; i++)
                    if (idEtapes[i] >= 0)
                        DeleteEtape(idEtapes[i]);
            }
        }
    }
}
