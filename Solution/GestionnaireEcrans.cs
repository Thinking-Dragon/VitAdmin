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
        private static Frame Frame { get; set; }

        public static void Initialiser(Panel parent)
        {
            Frame = new Frame();
            parent.Children.Add(Frame);
        }

        public static void Initialiser(Panel parent, Page premierEcran)
        {
            Initialiser(parent);
            Changer(premierEcran);
        }

        public static void Changer(Page ecran)
            => Frame.Content = ecran;
    }
}
