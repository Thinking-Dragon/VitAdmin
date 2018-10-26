using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VitAdmin.ControlModel;
using VitAdmin.View;

namespace VitAdmin.Control
{
   /// <summary>
   /// Logique d'interaction pour ControlMenuUtilisateur.xaml
   /// </summary>
   public partial class ControlMenuUtilisateur : UserControl
   {
      private GestionnaireEcrans GestionnaireEcrans { get; set; }
      public ControlMenuUtilisateur(GestionnaireEcrans gestionnaireEcrans)
      {
         InitializeComponent();
         GestionnaireEcrans = gestionnaireEcrans;
         DataContext = new ControlModelMenuUtilisateur(GestionnaireEcrans);
      }

    }
}
