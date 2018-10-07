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
        private Action<Page> ActionLorsqueFenetreChange { get; set; } = null;

        public GestionnaireEcrans(Panel parent)
        {
            Frame = new FrameSansNavigation();
            parent.Children.Add(Frame);
        }

        public GestionnaireEcrans(Panel parent, Action<Page> actionLorsqueFenetreChange) : this(parent)
            => ActionLorsqueFenetreChange = actionLorsqueFenetreChange;

        public GestionnaireEcrans(Panel parent, Page premierEcran) : this(parent)
            => Changer(premierEcran);

        public GestionnaireEcrans(Panel parent, Action<Page> actionLorsqueFenetreChange, Page premierEcran) : this(parent, actionLorsqueFenetreChange)
            => Changer(premierEcran);

        public void Changer(Page ecran)
        {
            Frame.Content = ecran;
            ActionLorsqueFenetreChange?.Invoke(ecran);
        }

        public Page GetEcranPresent() => (Frame.Content != null ? Frame.Content as Page : null);
    }
}
