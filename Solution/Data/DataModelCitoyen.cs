using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelCitoyen
    {
        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List <Citoyen> GetCitoyens()
        {
            // On crée une liste de citoyen venant de la BD
            List<Citoyen> lstCitoyen = new List<Citoyen>();

            // On vérifie si la BD est connecté
            if(ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT * " +
                    "FROM citoyens" +
                    ""
                    ,  SqlDR => { lstCitoyen.Add(new Citoyen
                    {
                        Nom = SqlDR.GetString("nom"),
                        Prenom = SqlDR.GetString("prenom"),
                        AssMaladie = SqlDR.GetString("numAssuranceMaladie"),
                        NumTelephone = SqlDR.GetString("telephone"),
                        Adresse = SqlDR.GetString("adresse")

                    });}
                    );
            }

            return lstCitoyen;
        }

        public static List<Citoyen> GetCitoyensLstPatient(Employe employe)
        {
            // On crée une liste de citoyen venant de la BD
            List<Citoyen> lstCitoyen = new List<Citoyen>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT c.nom nomCit, c.prenom prenomCit, d.nom nomDep, ch.nom nomCh, l.numero numeroLit, c.numAssuranceMaladie AssMal " +
                    "FROM citoyens c " +
                    "INNER JOIN lits l ON l.idCitoyen = c.idCitoyen " +
                    "INNER JOIN etatslits elit ON l.idEtatLit = elit.idEtatLit " +
                    "INNER JOIN chambres ch ON ch.idChambre = l.idChambre " +
                    "INNER JOIN departements d ON d.idDepartement = ch.idDepartement " +
                    "INNER JOIN quarts q ON q.idDepartement = d.idDepartement " +
                    "INNER JOIN quartsEmployes qe ON qe.idQuart = q.idQuart " +
                    "INNER JOIN employes emp ON emp.idEmploye = qe.idEmploye " +
                    "WHERE emp.numEmploye = '" + employe.NumEmploye + "' "
                    , SqlDR => {
                        lstCitoyen.Add(new Citoyen
                        {
                            Nom = SqlDR.GetString("nomCit"),
                            Prenom = SqlDR.GetString("prenomCit"),
                            AssMaladie = SqlDR.GetString("AssMal"),
                            //NumTelephone = SqlDR.GetString("telephone"),
                            //Adresse = SqlDR.GetString("adresse"),
                            Lit = new Lit
                            {
                                Numero = SqlDR.GetString("numeroLit"),
                                //UnEtatLit = (EtatLit)Enum.Parse(typeof(EtatLit), SqlDR.GetString("e.nom")),
                                Chambre = new Chambre
                                {
                                    Nom = SqlDR.GetString("nomCh"),
                                    UnDepartement = new Departement
                                    {
                                        Nom = SqlDR.GetString("nomDep"),
                                       // Abreviation = SqlDR.GetString("d.abreviation")
                                    }
                                }
                            }
                            
                        });
                    }
                    );
            }

            return lstCitoyen;
        }

        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List<Prescription> GetPrescriptionsCitoyens(Citoyen patient, Hospitalisation hospit)
        {
            // On crée une liste de citoyen venant de la BD
            List<Prescription> lstPrescriptions = new List<Prescription>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT produit, posologie, p.dateDebut, p.dateFin from prescriptions p" +
                    "INNER JOIN evenements e on e.idEvenement = p.idEvenement" +
                    "INNER JOIN hospitalisations h on h.idHospitalisation = e.idHospitalisation" +
                    "INNER JOIN citoyens c on c.idCitoyen = h.idCitoyen" +
                    "where c.numAssuranceMaladie = '"+ patient.AssMaladie.ToString() + "'" 


                    , SqlDR => {
                        lstPrescriptions.Add(new Prescription
                        {
                            Produit = SqlDR.GetString("produit"),
                            Posologie = SqlDR.GetString("posologie"),
                            DateDebut = SqlDR.GetDateTime("DateDebut"),
                            DateFin = SqlDR.GetDateTime("DateFin")

                        });
                    }
                    );
            }

            return lstPrescriptions;
        }


        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static Citoyen GetCitoyen(int id)
        {
            // On crée une liste de citoyen venant de la BD
            Citoyen patient = new Citoyen();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT * " +
                    "WHERE idCitoyen = '" + id.ToString() + "'"

                    , SqlDR => {
                        patient = new Citoyen
                        {
                            Nom = SqlDR.GetString("nom"),
                            Prenom = SqlDR.GetString("prenom"),
                            DateNaissance = SqlDR.GetDateTime("dateNaissance"),
                            AssMaladie = SqlDR.GetString("NumAssuranceMaladie")



                        };
                    });
            }

            return patient;
        }


    }
}
