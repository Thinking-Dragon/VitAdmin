using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                    param =>
                    {
                        MessageBox.Show("Test");
                    }
                );
            }
        }
    }
}
