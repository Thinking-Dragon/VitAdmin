using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Data
{
    public class ConnexionBD
    {
        private ConnexionBD() { }

        public string NomBD { get; set; } = string.Empty;
   
        public string Mdp { get; set; }

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
                if (String.IsNullOrEmpty(NomBD)) return false;
                string strConnexion = string.Format("Server=420.cstj.qc.ca; database={0}; UID=VitAdmin; password=infoV84658; SslMode=none", NomBD);
                connexion = new MySqlConnection(strConnexion);
                Connexion.Open();
            }
            return true;
        }

        public void ExecuterRequete(string requete, Action<MySqlDataReader> callback)
        {
            MySqlCommand commande = new MySqlCommand(requete, Connexion);
            MySqlDataReader lecteur = commande.ExecuteReader();

            while (lecteur.Read()) callback(lecteur);

            lecteur.Close();
        }

        public void Fermer()
        {
            if (Connexion != null) Connexion.Close();
        }
    }
}
