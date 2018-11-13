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
using VitAdmin.ControlModel;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlBarreRechercheTraitement.xaml
    /// </summary>
    public partial class ControlBarreRechercheTraitement : UserControl
    {
        List<Traitement> LstTraitementsTemp { get; set; }
        ControlModelBarreRechercheTraitement controlModelBarreRechercheTraitement { get; set; }

        // Constructeur
        public ControlBarreRechercheTraitement(ObservableCollection<Traitement> traitementsTemps, ObservableCollection<Traitement> traitements) // Pour rendre la barre de recherche un jour accessible à plus de contexte
        {
            InitializeComponent();
            controlModelBarreRechercheTraitement = new ControlModelBarreRechercheTraitement(traitementsTemps, traitements);
            DataContext = controlModelBarreRechercheTraitement;
            LstTraitementsTemp = traitementsTemps.ToList<Traitement>();
        }

        private void cboRecherche_KeyUp(object sender, KeyEventArgs e)
        {
            ControlModelBarreRechercheTraitement controlModelBarreRechercheTraitement = (ControlModelBarreRechercheTraitement)DataContext;
            string texteRecherche = cboRecherche.Text;

            if (e.Key == Key.Enter)
            {
                // On ajoute dans la liste des traitements à donnés au patient le traitement sélectionné.
                controlModelBarreRechercheTraitement.Traitements.Add(LstTraitementsTemp.Find((traitement) => traitement.Nom.IndexOf(cboRecherche.Text) != -1));
            }

        }

    }
}
