﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelNotification
    {
        public static void PostNotification(string message, string lien, Employe employe)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "INSERT INTO Notifications (idEmploye, message, lien, estLu) " +
                        "VALUES ( " +
                        "   ( " +
                        "       SELECT idEmploye " +
                        "       FROM Employes " +
                        "       WHERE numEmploye = {0}" +
                        "   ), " +
                        "   '{1}', '{2}', false " +
                        ")",
                        employe.NumEmploye, message, lien
                    )
                );
            }
        }

        public static List<Notification> GetNotifications(Employe employe)
        {
            List<Notification> notifications = new List<Notification>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT n.message, n.lien, n.estLu " +
                        "FROM Notifications n " +
                        "JOIN Employes e ON n.idEmploye = e.idEmploye " +
                        "WHERE e.numEmploye = {0}",
                        employe.NumEmploye
                    ), lecteur =>
                    {
                        notifications.Add(
                            new Notification
                            {
                                Message = lecteur.GetString("message"),
                                LienVersFenetre = lecteur.GetString("lien"),
                                EstLu = Boolean.Parse(lecteur.GetString("estLu"))
                            }
                        );
                    }
                );
            }

            return notifications;
        }
    }
}