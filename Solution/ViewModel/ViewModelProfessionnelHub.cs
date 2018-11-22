using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelHub
    {
        public GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewModelProfessionnelHub(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
        }

        public ICommand CmdBtnCreerPatient
        {
            get
            {
                return new CommandeDeleguee(action =>
                {
                   
                    this.GestionnaireEcrans.Changer(new ViewProfessionnelCreerPatient(GestionnaireEcrans));

                });
            }
        }

        public ICommand CmdBtnGestionLits
        {
            get
            {
                return new CommandeDeleguee(action =>
                {

                    this.GestionnaireEcrans.Changer(new ViewDemandesTransfert(GestionnaireEcrans));

                });
            }
        }
    }
}
