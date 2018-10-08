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

            if(ConnexionBD.Instance().EstConnecte())
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
    }
}
