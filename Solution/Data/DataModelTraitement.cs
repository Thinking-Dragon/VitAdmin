using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelTraitement
    {
        public static List<Traitement> GetTraitements(bool expand = false)
        {
            List<Traitement> traitements = new List<Traitement>();
            List<int> idTraitements = new List<int>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT t.idTraitement _id, t.nom traitement, d.nom departement, d.abreviation dept " +
                    "FROM Traitements t " +
                    "JOIN Departements d ON d.idDepartement = t.idDepartement", lecteur =>
                    {
                        Traitement traitement = new Traitement
                        {
                            Nom = lecteur.GetString("traitement"),
                            DepartementAssocie = new Departement
                            {
                                Nom = lecteur.GetString("departement"),
                                Abreviation = lecteur.GetString("dept")
                            }
                        };

                        if (expand)
                            idTraitements.Add(int.Parse(lecteur.GetString("_id")));

                        traitements.Add(traitement);
                    }
                );
                if (expand)
                    for (int i = 0; i < traitements.Count; i++)
                        traitements[i].EtapesTraitement = DataModelEtape.GetEtapes(idTraitements[i], true);
            }

            return traitements;
        }
    }
}
