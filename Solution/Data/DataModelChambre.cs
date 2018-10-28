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
        public static List<Chambre> GetChambres(string idDepartement, string extends = "")
        {
            List<Chambre> chambres = new List<Chambre>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                List<int> idChambres = new List<int>();

                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT idChambre, nom " +
                        "FROM Chambres " +
                        "WHERE idDepartement = {0}",
                        idDepartement
                    ), lecteur =>
                    {
                        chambres.Add( new Chambre { Numero = lecteur.GetString("nom") } );
                        if (extends != "")
                            idChambres.Add(int.Parse(lecteur.GetString("idChambre")));
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
    }
}
