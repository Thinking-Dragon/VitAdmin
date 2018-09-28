using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelEtape
    {
        public static List<Etape> GetEtapes(int idTraitement)
        {
            List<Etape> etapes = new List<Etape>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT description FROM Etapes WHERE idTraitement = {0}", idTraitement
                    ), lecteur =>
                    {
                        etapes.Add(new Etape
                        {
                            Description = 
                        })
                    }
                );
            }

            return etapes;
        }
    }
}
