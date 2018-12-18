using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VitAdmin.Data
{
    public class ConnexionBD
    {
        private ConnexionBD() { }

        private MySqlConnection connexion = null;
        public MySqlConnection Connexion { get { return connexion; } }

        private static ConnexionBD instance = null;
        public static ConnexionBD Instance()
        {
            if (instance == null)
                instance = new ConnexionBD();
            return instance;
        }

        public bool EstConnecte()
        {
            if(Connexion == null)
            {
                try
                {
                    //POUR LAURENCE : Utilisation de l'alias 420
                    string strConnexion = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
                    connexion = new MySqlConnection(strConnexion);
                    Connexion.Open();
                }
                catch (ConfigurationErrorsException e) { return false; }
            }
            return true;
        }

        public void ExecuterRequete(string requete, Action<MySqlDataReader> callback, params Tuple<string, string>[] parametres)
        {
            MySqlCommand commande = new MySqlCommand(requete, Connexion);

            for (int i = 0; i < parametres.Length; ++i)
                commande.Parameters.AddWithValue(parametres[i].Item1, parametres[i].Item2);

            MySqlDataReader lecteur = commande.ExecuteReader();

            while (lecteur.Read()) callback(lecteur);

            lecteur.Close();
        }

        // Si retourne 0, requête a échoué
        public int ExecuterRequete(string requete, params Tuple<string, string>[] parametres)
        {
            MySqlCommand commande = new MySqlCommand(requete, Connexion);

            for (int i = 0; i < parametres.Length; ++i)
                commande.Parameters.AddWithValue(parametres[i].Item1, parametres[i].Item2);

            return commande.ExecuteNonQuery();
        }

        public void Fermer()
        {
            if (Connexion != null) Connexion.Close();
        }
    }
}
