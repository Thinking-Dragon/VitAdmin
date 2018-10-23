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

            foreach (Prescription prescrip in lstPrescriptions)
            {
                prescrip.addISOEvenement();
                prescrip.addISODateDebut();
            }



            return lstPrescriptions;
        }

        public static void AddPrescription(Hospitalisation hospit, Prescription prescript, int numEmp)
        {
            prescript.DateEvenement = DateTime.Now;

            if (ConnexionBD.Instance().EstConnecte())
            {

                string requete = string.Format("INSERT INTO evenements " +
                                           "(idHospitalisation, idEmploye, dateHeure, estNotifier) " +
                                           "VALUES (" +
                                           "(SELECT idHospitalisation FROM hospitalisations WHERE dateDebut = '{0}')," +
                                           "{1}," +
                                           "'{2}'," +
                                           "{3})", hospit.DateDebut.ToString(), numEmp, prescript.DateEvenement.ToString(), prescript.EstNotifier);


                ConnexionBD.Instance().ExecuterRequete(requete);

                requete = string.Format("INSERT INTO Prescriptions " +
                                           "(idEvenement, produit, posologie, dateDebut, nbJour) " +
                                           "VALUES (" +
                                           "(SELECT idEvenement FROM evenements WHERE dateHeure = '{0}')," +
                                           "'{1}'," +
                                           "'{2}'," +
                                           "'{3}'," +
                                           "{4})"
                                           , prescript.DateEvenement.ToString(), prescript.Produit, prescript.Posologie, prescript.DateDebut.ToString(), prescript.NbJour);

                ConnexionBD.Instance().ExecuterRequete(requete);

            }
        }
    }
}
