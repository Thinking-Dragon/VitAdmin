using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelEquipement
    {
        public static List<Equipement> GetEquipements(int idChambre)
        {
            List<Equipement> equipements = new List<Equipement>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT nom, description " +
                        "FROM Equipements e " +
                        "JOIN ChambresEquipements ce ON e.idEquipement = ce.idEquipement " +
                        "WHERE ce.idChambre = {0}",
                        idChambre
                    ), lecteur => equipements.Add(
                        new Equipement
                        {
                            Nom = lecteur.GetString("nom"),
                            Description = lecteur.GetString("description")
                        }
                    )
                );
            }

            return equipements;
        }
    }
}
