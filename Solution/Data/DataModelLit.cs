using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public class DataModelLit
    {
        /*public static void PutLit(Citoyen citoyen)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                
                ConnexionBD.Instance().ExecuterRequete(

                        "INSERT INTO lits (idCitoyen, idTraitement) " +
                        "VALUES ((SELECT idHospitalisation FROM hospitalisations h JOIN citoyens c WHERE c.numAssuranceMaladie = '@AssMaladie'), " +
                        "(SELECT idTraitement FROM traitements t WHERE t.nom = '@TraitementNom'), ",
                        new Tuple<string, string>("@AssMaladie", citoyen.AssMaladie),
                        new Tuple<string, string>("@TraitementNom", traitement.Nom)
                );
            }
        }*/
    }

}
