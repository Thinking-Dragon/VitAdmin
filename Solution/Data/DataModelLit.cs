using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelLit
    {
        public static List<Lit> GetLits(int idChambre)
        {
            List<Lit> lits = new List<Lit>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT l.numero, el.nom etat " +
                        "FROM Lits l " +
                        "JOIN EtatsLits el ON l.idEtatLit = el.idEtatLit " +
                        "WHERE idChambre = {0}",
                        idChambre
                    ), lecteur => lits.Add(
                        new Lit
                        {
                            Numero = lecteur.GetString("numero"),
                            EtatLit = (EtatLit) Enum.Parse(typeof(EtatLit), lecteur.GetString("etat"))
                        }
                    )
                );
            }

            return lits;
        }
    }
}
