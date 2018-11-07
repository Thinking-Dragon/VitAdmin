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
                    GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans));
                }
            };
            dt.Start();
        }
    }
}
