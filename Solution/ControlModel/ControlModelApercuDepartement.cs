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
    public class ControlModelApercuDepartement : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        
        private Departement departement;
        public Departement Departement
        {
            get { return departement; }
            set { departement = value; RaisePropertyChangedEvent("Departement"); }
        }

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
