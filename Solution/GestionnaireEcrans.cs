using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VitAdmin.MVVM;

namespace VitAdmin
{
    public class GestionnaireEcrans
    {
        private FrameSansNavigation Frame { get; set; }

        public GestionnaireEcrans(Panel parent)
        {
            Frame = new FrameSansNavigation();
            parent.Children.Add(Frame);
        }

        public GestionnaireEcrans(Panel parent, Page premierEcran) : this(parent)
            => Changer(premierEcran);

        public void Changer(Page ecran)
            => Frame.Content = ecran;

        public Page GetEcranPresent() => (Frame.Content != null ? Frame.Content as Page : null);
    }
}
