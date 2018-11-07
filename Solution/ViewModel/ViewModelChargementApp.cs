using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Parameter;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    public class ViewModelChargementApp : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        private int _indexTransitionneur = 0;
        /// <summary>
        /// L'index du transitionneur qui affiche le message de bienvenue.
        /// </summary>
        public int IndexTransitionneur
        {
            get => _indexTransitionneur;
            set { _indexTransitionneur = value; RaisePropertyChangedEvent("IndexTransitionneur"); }
        }

        public ViewModelChargementApp(GestionnaireEcrans gestionnaireEcrans)
        {
            if (!UsagerConnecte.EstConnecte)
                gestionnaireEcrans.RetourAncienEcran();

            GestionnaireEcrans = gestionnaireEcrans;

            DispatcherTimer dt = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            dt.Tick += (object sender, EventArgs e) =>
            {
                if(dt.Interval == TimeSpan.FromSeconds(1))
                {
                    IndexTransitionneur = 1;
                    dt.Interval = TimeSpan.FromMilliseconds(500);
                }
                else
                {
                    dt.Stop();
                    switch(UsagerConnecte.Usager.RoleUsager)
                    {
                        case Role.admin:
                            GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans));
                            break;
                        case Role.GestPersonnel:
                            GestionnaireEcrans.AfficherMessage("L'écran d'accueil d'un gestionnaire du personnel n'existe pas encore");
                            break;
                        case Role.InfChef:
                            GestionnaireEcrans.AfficherMessage("L'écran d'accueil d'une infirmière en chef n'existe pas encore");
                            break;
                        case Role.PersonnelSante:
                            GestionnaireEcrans.Changer(new ViewProfessionnelHub(GestionnaireEcrans, UsagerConnecte.Usager));
                            break;
                        default:
                            GestionnaireEcrans.RetourAncienEcran();
                            break;
                    }
                }
            };
            dt.Start();
        }
    }
}
