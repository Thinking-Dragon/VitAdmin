using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VitAdmin
{
    public class GestionnaireEcrans
    {
        private Frame Frame { get; set; }

        public GestionnaireEcrans(Panel parent)
        {
            Frame = new Frame();
            parent.Children.Add(Frame);
        }

        public GestionnaireEcrans(Panel parent, Page premierEcran) : this(parent)
            => Changer(premierEcran);

        public void Changer(Page ecran)
            => Frame.Content = ecran;
    }
}
