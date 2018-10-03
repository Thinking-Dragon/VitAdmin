using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelHospitalisation
    {
        public static List<Hospitalisation> getHospitalisation(Citoyen citoyen)
        {
            // On crée une liste de citoyen venant de la BD
            List<Hospitalisation> lstHospitalisation = new List<Hospitalisation>();
            List<Traitement> lstTraitement = new List<Traitement>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT h.dateDebut dDebut, h.dateFin dFin, d.nom depNom " +
                    "FROM hospitalisation h " +
                    "INNER JOIN traitements t ON t.idHospitalisation = t.idHospitalisation " +
                    "INNER JOIN departements d ON d.idDepartement = t.idDepartement "
                    , SqlDR => {
                        lstHospitalisation.Add(new Hospitalisation
                        {
                            DateDebut = SqlDR.GetDateTime("dDebut"),
                            DateFin = SqlDR.GetDateTime("dFin"),
                            //LstTraitements


                        });
                    }
                    );
            }

            return new List<Hospitalisation>();
        }
    }
}
