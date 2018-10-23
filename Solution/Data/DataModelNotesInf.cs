using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelNotesInf
    {
        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List<NoteInfirmiere> GetNotesInfirmiereCitoyens(String NumAssMaladie, DateTime dateDebut)
        {
            // On crée une liste de citoyen venant de la BD
            List<NoteInfirmiere> lstNoteInfirmiere = new List<NoteInfirmiere>();
            List<Employe> lstEmp = new List<Employe>();


            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                string test = dateDebut.ToString();
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT notesInfirmiere.note nte, e.dateHeure dthr, cit.nom nm, cit.prenom pm, em.numEmploye numEm " +
                          "FROM notesInfirmiere " +
                              "INNER JOIN evenements e on e.idEvenement = notesInfirmiere.idEvenement " +
                              "INNER JOIN hospitalisations h on h.idHospitalisation = e.idHospitalisation " +
                              "INNER JOIN citoyens c on c.idCitoyen = h.idCitoyen " +
                              "INNER JOIN employes em  on em.idEmploye = e.idEmploye " +
                              "INNER JOIN citoyens cit on cit.idCitoyen = em.idCitoyen " +
                                  "WHERE c.numAssuranceMaladie = '" + NumAssMaladie + "' AND h.dateDebut = '" + dateDebut.ToString() + "'"

                    , SqlDR =>
                    {
                        lstNoteInfirmiere.Add(new NoteInfirmiere
                        {
                            NotesInf = SqlDR.GetString("nte"),
                            DateEvenement = SqlDR.GetDateTime("dthr"),
                            EmployeImplique = new Employe
                            {
                                Nom = SqlDR.GetString("nm"),
                                Prenom = SqlDR.GetString("pm"),
                                NumEmploye = SqlDR.GetString("numEm")
                            }
                        });
                    });
            }

            foreach (NoteInfirmiere note in lstNoteInfirmiere)
            {
                note.addISOEvenement();
            }

            return lstNoteInfirmiere;
        }

        public static void AddNoteInf(Hospitalisation hospit, NoteInfirmiere noteInf, int numEmp)
        {
            noteInf.DateEvenement = DateTime.Now;

            if (ConnexionBD.Instance().EstConnecte())
            {

                string requete = string.Format("INSERT INTO evenements " +
                                           "(idHospitalisation, idEmploye, dateHeure, estNotifier) " +
                                           "VALUES (" +
                                           "(SELECT idHospitalisation FROM hospitalisations WHERE dateDebut = '{0}')," +
                                           "{1}," +
                                           "'{2}'," +
                                           "{3})", hospit.DateDebut.ToString(), numEmp, noteInf.DateEvenement.ToString(), noteInf.EstNotifier);


                ConnexionBD.Instance().ExecuterRequete(requete);

                requete = string.Format("INSERT INTO NotesInfirmiere " +
                                           "(idEvenement, note) " +
                                           "VALUES (" +
                                           "(SELECT idEvenement FROM evenements WHERE dateHeure = '{0}')," +
                                           "'{1}')"
                                           , noteInf.DateEvenement.ToString(), noteInf.NotesInf);

                ConnexionBD.Instance().ExecuterRequete(requete);

            }
        }


    }

}
