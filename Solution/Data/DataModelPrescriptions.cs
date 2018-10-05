using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelPrescriptions
    {
        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List<Prescription> GetPrescriptionsCitoyens(String NumAssMaladie)
        {
            // On crée une liste de citoyen venant de la BD
            List<Prescription> lstPrescriptions = new List<Prescription>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT produit prod, posologie poso, prescriptions.dateDebut Ddebut, nbJour nbj " +
                    "FROM prescriptions " +
                    "INNER JOIN evenements e on e.idEvenement = prescriptions.idEvenement " +
                    "INNER JOIN hospitalisations h on h.idHospitalisation = e.idHospitalisation " +
                    "INNER JOIN citoyens c on c.idCitoyen = h.idCitoyen " +
                    "WHERE c.numAssuranceMaladie = '" + NumAssMaladie +
                    "' AND (prescriptions.dateDebut + INTERVAL nbJour DAY >= CURDATE() OR nbJour = 0) "

                    , SqlDR => {
                        lstPrescriptions.Add(new Prescription
                        {
                            Produit = SqlDR.GetString("prod"),
                            Posologie = SqlDR.GetString("poso"),
                            DateDebut = SqlDR.GetDateTime("Ddebut"),
                            NbJour = SqlDR.GetUInt16("nbj")

                        });
                    }
                    );
            }

            return lstPrescriptions;
        }
    }
}
