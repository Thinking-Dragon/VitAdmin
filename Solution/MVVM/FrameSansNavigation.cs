using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace VitAdmin.MVVM
{
    /// <summary>
    /// Description: Frame sans l'interface de navigation par défaut de wpf.
    /// Auteur: Clément Gaßmann-Prince
    /// </summary>
    public class FrameSansNavigation : Frame
    {
        void FrameSansNavigation_Naviguee(object sender, NavigationEventArgs e)
            => NavigationService.RemoveBackEntry();

        public FrameSansNavigation()
        {
            Navigated += new NavigatedEventHandler(FrameSansNavigation_Naviguee);
            NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }
    }
}
