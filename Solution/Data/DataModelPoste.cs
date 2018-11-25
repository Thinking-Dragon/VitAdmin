using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Data
{
    public static class DataModelPoste
    {
        public static List<string> GetPostes()
        {
            List<string> postes = new List<string>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT nom FROM Postes",
                    lecteur => postes.Add(lecteur.GetString("nom"))
                );
            }

            return postes;
        }
    }
}
