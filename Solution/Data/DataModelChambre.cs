using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelChambre
    {
        public static List<Chambre> GetChambres(string idDepartement, string extends = "")
        {
            List<Chambre> chambres = new List<Chambre>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                Chambre chambre = null;
                int idChambre = 0;

                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT idChambre, nom " +
                        "FROM Chambres " +
                        "WHERE idDepartement = {0}",
                        idDepartement
                    ), lecteur =>
                    {
                        chambres.Add( new Chambre { Nom = lecteur.GetString("nom") } );
                        if (extends != "")
                            idChambre = int.Parse(lecteur.GetString("idChambre"));
                    }
                );

                if (chambre != null && idChambre != 0)
                {
                    if (extends.ToLower().Contains("lits"))
                        ;// chambre.Lits = DataModelLit.GetLits(idChambre);
                    if (extends.ToLower().Contains("equipements"))
                        ;// chambre.Equipements = DataModelEquipement.GetEquipements(idChambre);
                }
            }

            return chambres;
        }
    }
}
