using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ControlModel
{
   class ControlModelMenuUtilisateur
   {
        GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ControlModelMenuUtilisateur(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
        }
        public ICommand CmdAfficheHoraire 
        {
              get
              {
                  return new CommandeDeleguee(
                     param =>
                     {
                        GestionnaireEcrans.Changer(new ViewProfessionnelHoraire());
                     }
            );
         }
      }
   }
}
