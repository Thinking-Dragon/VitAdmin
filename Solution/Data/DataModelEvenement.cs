using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelEvenement
    {
        public static void AddEvenement(Hospitalisation hospit, Evenement evenement)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = string.Format("INSERT INTO evenement " +
                                           "(idHospitalisation, idEmploye) " +
                                           "VALUES (" +
                                           "(SELECT idHospitalisation FROM hospitalisation WHERE dateDebut = '{0}')," +
                                           "(SELECT idEmploye FROM employe WHERE numEmploye = '{1}'))", hospit.DateDebut.ToString(), evenement.EmployeImplique.NumEmploye);
                                           

                ConnexionBD.Instance().ExecuterRequete(requete);

            }
        }
    }
}
