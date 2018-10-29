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
        #region CREATE

        /// <summary>
        /// Creates a new "Equipement" in the database.
        /// </summary>
        /// <param name="equipement">The "Equipement" to create</param>
        public static void PostEquipement(Equipement equipement)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "INSERT INTO Equipements (nom, description) " +
                        "VALUES ('{0}', '{1}')",
                        equipement.Nom, equipement.Description
                    )
                );
            }
        }

        /// <summary>
        /// Links an "Equipement" to a "Chambre"
        /// </summary>
        /// <param name="idChambre">Id of the "Chambre" to link</param>
        /// <param name="equipement">"Equipement" to link</param>
        public static void PostEquipementChambres(int idChambre, Equipement equipement)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "INSERT INTO ChambresEquipements (idChambre, idEquipement) " +
                        "VALUES ({0}, {1})",
                        idChambre, equipement._identifiant
                    )
                );
            }
        }

        /// <summary>
        /// Links every "Equipement" in {equipements} to the "Chambre" with id {idChambre}
        /// </summary>
        /// <param name="idChambre">Id of the "Chambre" to be linked</param>
        /// <param name="equipements">"Equipements" to link with the "Chambre"</param>
        public static void PostEquipementsChambres(int idChambre, List<Equipement> equipements)
        {
            foreach (var equipement in equipements)
                PostEquipementChambres(idChambre, equipement);
        }

        #endregion

        #region READ

        /// <summary>
        /// Get every "Equipements" in the collection.
        /// </summary>
        /// <returns>A list of every "Equipement" in the collection</returns>
        public static List<Equipement> GetEquipements()
        {
            List<Equipement> equipements = new List<Equipement>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT idEquipement _id, nom, description " +
                    "FROM Equipements",
                    lecteur =>
                        equipements.Add(
                            new Equipement
                            {
                                _identifiant = int.Parse(lecteur.GetString("_id")),
                                Nom = lecteur.GetString("nom"),
                                Description = lecteur.GetString("description")
                            }
                        )
                );
            }

            return equipements;
        }

        /// <summary>
        /// Retrieve every "Equipement" linked to the "Chambre" with id {idChambre}.
        /// </summary>
        /// <param name="idChambre"></param>
        /// <returns>Every "Equipement" linked to the "Chambre" with id {idChambre}</returns>
        public static List<Equipement> GetEquipements(int idChambre)
        {
            List<Equipement> equipements = new List<Equipement>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT e.idEquipement _id, nom, description " +
                        "FROM Equipements e " +
                        "JOIN ChambresEquipements ce ON e.idEquipement = ce.idEquipement " +
                        "WHERE ce.idChambre = {0}",
                        idChambre
                    ), lecteur => equipements.Add(
                        new Equipement
                        {
                            _identifiant = int.Parse(lecteur.GetString("_id")),
                            Nom = lecteur.GetString("nom"),
                            Description = lecteur.GetString("description")
                        }
                    )
                );
            }

            return equipements;
        }

        #endregion

        #region UPDATE

        /// <summary>
        /// Updates every fields of one "Equipement" in the database.
        /// </summary>
        /// <param name="equipement">New "Equipement"</param>
        public static void PutEquipement(Equipement equipement)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE Equipements " +
                        "SET nom = '{0}', description = '{1}' " +
                        "WHERE idEquipement = {2}",
                        equipement.Nom, equipement.Description, equipement._identifiant
                    )
                );
            }
        }

        /// <summary>
        /// Updates the "Equipements" collection: removes deleted elements, updates modified ones and adds new ones.
        /// </summary>
        /// <param name="equipements">New collection of "Equipements"</param>
        public static void PutEquipements(List<Equipement> equipements)
        {
            List<Equipement> equipementsExistants = GetEquipements();
            List<Equipement> equipementsASupprimer = new List<Equipement>();
            
            foreach (var equipementExistant in equipementsExistants)
                if (!equipements.Exists(equipement => equipement._identifiant == equipementExistant._identifiant))
                    equipementsASupprimer.Add(equipementExistant);

            foreach (var equipementASupprimer in equipementsASupprimer)
            {
                DeleteEquipement(equipementASupprimer);
                equipementsExistants.Remove(equipementASupprimer);
            }

            foreach (var equipement in equipements)
            {
                if (!equipementsExistants.Exists(equipementExistant => equipementExistant._identifiant == equipement._identifiant))
                    PostEquipement(equipement);
                else
                    PutEquipement(equipement);
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Deletes the "Equipement" {equipement}.
        /// </summary>
        /// <param name="equipement">The "Equipement" to delete</param>
        public static void DeleteEquipement(Equipement equipement)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "DELETE FROM Equipements " +
                        "WHERE idEquipement = {0}",
                        equipement._identifiant
                    )
                );
            }
        }

        /// <summary>
        /// Deletes every link connected to the "Chambre" with id {idChambre}
        /// </summary>
        /// <param name="idChambre">The id of the "Chambre" to unlink with "Equipements"</param>
        public static void DeleteEquipementsChambre(int idChambre)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "DELETE FROM ChambresEquipements " +
                        "WHERE idChambre = {0}",
                        idChambre
                    )
                );
            }
        }

        #endregion
    }
}
