using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlEditionTraitement.xaml
    /// </summary>
    public partial class ControlEditionTraitement : UserControl
    {
        private ControlRechercheDepartement ControlRechercheDepartement { get; set; }

        public ControlEditionTraitement(ICommand cmdConfirmer)
        {
            InitializeComponent();
            cpDepartement.Content = ControlRechercheDepartement = new ControlRechercheDepartement();
            cpNomTraitement.Content = new ControlDialogAjout(
                new CommandeDeleguee(
                    nomTraitement =>
                    {
                        if (nomTraitement as string != string.Empty && ControlRechercheDepartement.ControlModel.DepartementSelectionne != null)
                        {
                            cmdConfirmer.Execute(
                                new Traitement
                                {
                                    Nom = nomTraitement as string,
                                    DepartementAssocie = ControlRechercheDepartement.ControlModel.DepartementSelectionne,
                                    EtapesTraitement = new ObservableCollection<Etape>()
                                }
                            );
                        }
                        else MessageBox.Show("!");
                    }
                )
            , "Entrez le nom du traitement");
        }
    }
}
