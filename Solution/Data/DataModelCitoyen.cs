﻿using MySql.Data.MySqlClient;
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
                    "FROM citoyens "
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

        // ****** FÉLIX, tu peux utiliser cette requête pour l'infirmière-chef!! *******
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
                    "SELECT c.nom nomCit, c.prenom prenomCit, d.nom nomDep, ch.nom nomCh, l.numero numeroLit, c.numAssuranceMaladie AssMal, el.nom EtLitNom " +
                    "FROM citoyens c " +
                    "INNER JOIN lits l ON l.idCitoyen = c.idCitoyen " +
                    "INNER JOIN etatslits el ON el.idEtatLit = l.idEtatLit " +
                    "INNER JOIN chambres ch ON ch.idChambre = l.idChambre " +
                    "INNER JOIN departements d ON d.idDepartement = ch.idDepartement " +
                    "INNER JOIN quartsemployescitoyens qec ON qec.idCitoyen = c.idCitoyen " +
                    "INNER JOIN quartsemployes qe ON qe.idQuartEmploye = qec.idQuartEmploye " +
                    "INNER JOIN employes e ON e.idEmploye = qe.idEmploye " +
                    "WHERE e.numEmploye = '" + employe.NumEmploye + "' "
                    , SqlDR => {
                        lstCitoyen.Add(new Citoyen
                        {
                            Nom = SqlDR.GetString("nomCit"),
                            Prenom = SqlDR.GetString("prenomCit"),
                            AssMaladie = SqlDR.GetString("AssMal"),
                            Lit = new Lit
                            {
                                Numero = SqlDR.GetString("numeroLit"),
                                UnEtatLit = (EtatLit)Enum.Parse(typeof(EtatLit), SqlDR.GetString("EtLitNom")),
                                Chambre = new Chambre
                                {
                                    Nom = SqlDR.GetString("nomCh"),
                                    UnDepartement = new Departement
                                    {
                                        Nom = SqlDR.GetString("nomDep"),
                                      
                                    }
                                }
                            }
                            
                        });
                    }
                    );
            }

            return lstCitoyen;
        }

        // ****** FÉLIX, tu peux utiliser cette requête pour l'infirmière-chef!! *******
        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List<Citoyen> GetTousCitoyensDepartement(Departement departementSelectionne)
        {
            // On crée une liste de citoyen venant de la BD
            List<Citoyen> lstCitoyen = new List<Citoyen>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT c.nom nomCit, c.prenom prenomCit, d.nom nomDep, ch.nom nomCh, l.numero numeroLit, c.numAssuranceMaladie AssMal, el.nom EtLitNom " +
                    "FROM citoyens c " +
                    "INNER JOIN lits l ON l.idCitoyen = c.idCitoyen " +
                    "INNER JOIN etatslits el ON el.idEtatLit = l.idEtatLit " +
                    "INNER JOIN chambres ch ON ch.idChambre = l.idChambre " +
                    "INNER JOIN departements d ON d.idDepartement = ch.idDepartement " +
                    "WHERE d.nom = '" + departementSelectionne.Nom + "' "
                    , SqlDR => {
                        lstCitoyen.Add(new Citoyen
                        {
                            Nom = SqlDR.GetString("nomCit"),
                            Prenom = SqlDR.GetString("prenomCit"),
                            AssMaladie = SqlDR.GetString("AssMal"),
                            Lit = new Lit
                            {
                                Numero = SqlDR.GetString("numeroLit"),
                                UnEtatLit = (EtatLit)Enum.Parse(typeof(EtatLit), SqlDR.GetString("EtLitNom")),
                                Chambre = new Chambre
                                {
                                    Nom = SqlDR.GetString("nomCh"),
                                    UnDepartement = new Departement
                                    {
                                        Nom = SqlDR.GetString("nomDep"),

                                    }
                                }
                            }

                        });
                    }
                    );
            }

            return lstCitoyen;
        }



    }
}
