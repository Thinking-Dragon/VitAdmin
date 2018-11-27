using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VitAdmin.Control;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    class ControlModelGestionHoraire
    {
        public Employe Employe { get; set; }

        private Grid GridHoraire { get; set; }

        public ControlModelGestionHoraire(Employe employe, Grid grid)
        {
            Employe = employe;
            GridHoraire = grid;
        }

        public ICommand CmdDoubleClickQuart
        {
            get
            {
                return new CommandeDeleguee(
                    quart =>
                    {   
                        if ((((quart as Border).Background)as SolidColorBrush).Color == (Brushes.Transparent as SolidColorBrush).Color)
                        {
                            DialogHost.Show(new ControlAjoutQuart(), "dialogLaurence");
                        }
                        else
                        {
                            (quart as Border).Background = Brushes.Transparent;
                            ((quart as Border).Child as Label).Content = "";
                        }
                    }
                );
            }
        }
    }
}
