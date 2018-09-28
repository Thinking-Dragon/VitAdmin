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

            if(ConnexionBD.Instance().EstConnecte())
            {
                if(expand)
                {
                    /*ConnexionBD.Instance().ExecuterRequete(
                        String.Format(
                            "SELECT ", 
                        ), lecteur =>
                        {

                        }
                    );*/
                }
                else
                {
                    ConnexionBD.Instance().ExecuterRequete(
                        "SELECT t.nom traitement, d.nom departement, d.abreviation dept " +
                        "FROM Traitements t " +
                        "JOIN Departements d ON d.idDepartement = t.idDepartement", lecteur =>
                        {


                            traitements.Add(new Traitement
                            {
                                Nom = lecteur.GetString("traitement"),
                                DepartementAssocie = new Departement
                                {
                                    Nom = lecteur.GetString("departement"),
                                    Abreviation = lecteur.GetString("dept"),
                                    
                                }
                            });
                        }
                    );
                }
            }

            return traitements;
        }
    }
}
