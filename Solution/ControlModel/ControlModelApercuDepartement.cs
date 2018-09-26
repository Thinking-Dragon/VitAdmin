using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelApercuDepartement
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public Departement Departement { get; set; }

        public ICommand CmdModifierDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    System.Windows.MessageBox.Show("Hey!");
                });
            }
        }

        public ControlModelApercuDepartement(GestionnaireEcrans gestionnaireEcrans, Departement departement)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Departement = departement;
        }
    }
}
