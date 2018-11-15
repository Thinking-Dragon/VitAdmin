using System;
using System.Collections.Generic;
using System.IO;
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

        private string _urlImage;
        public string UrlImage
        {
            get => _urlImage;
            set { _urlImage = value; RaisePropertyChangedEvent("UrlImage"); }
        }

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

            Random random = new Random();
            //string[] nomsImagesChargement = Directory.GetFiles(@"Resource\Graphic\Animated", "*.gif");
            //UrlImage = @"..\" + nomsImagesChargement[random.Next() % nomsImagesChargement.Length];

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
                            GestionnaireEcrans.Changer(new ViewListeEmployes(GestionnaireEcrans));
                            break;
                        case Role.InfChef:
                            GestionnaireEcrans.Changer(new ViewCreerNotifications(GestionnaireEcrans));
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
