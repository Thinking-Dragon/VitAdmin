using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    class ControlModelAjoutQuart : ObjetObservable
    {
        public List<Departement> LstNomsDepartements { get; set; }
        public Departement DepartSelectionne { get; set; }
        public static bool EstPremierClick { get; set; } = true;
        public static bool estModifie { get; set; } = false;
        public Border Quart { get; set; }

        private String PrivateTextButton = "Confirmer";
        public String TextButton
        {
            get
            {
                return PrivateTextButton;
            }
            set
            {
                PrivateTextButton = value;
                RaisePropertyChangedEvent("TextButton");
            }
        }

        private string MessagePrivate = "";
        public String MessageErreur
        {
            get
            {
                return MessagePrivate;
            }
            set
            {
                MessagePrivate = value;
                RaisePropertyChangedEvent("MessageErreur");
            }
        }

        public ControlModelAjoutQuart(Border quart)
        {
            LstNomsDepartements = Data.DataModelDepartement.GetNomsDepartements();
            DepartSelectionne = LstNomsDepartements[0];
            Quart = quart;

        }

        public ICommand CmdConfirmClick
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        if (EstPremierClick)
                        {
                            MessageErreur = "Voulez-vous vraiment confirmer?";
                            TextButton = "Oui";

                            EstPremierClick = false;
                        }
                        else
                        {
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            EstPremierClick = true;
                            estModifie = true;
                            Quart.Background = Brushes.DodgerBlue;
                            (Quart.Child as Label).Content = DepartSelectionne.Nom;
                            (Quart.Child as Label).Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                );
            }
        }
    }
}
