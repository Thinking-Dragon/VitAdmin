using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Data;
using MaterialDesignThemes.Wpf;
using VitAdmin.Control;

namespace VitAdmin.ControlModel
{
    class ControlModelEnregistrerHoraire : ObjetObservable
    {
        public Grid Horaire { get; set; }
        public Employe Employe { get; set; }
        public Action ChangerSemaine { get; set; }

        public ControlModelEnregistrerHoraire(Grid horaire, Employe employe, Action changeSemaine)
        {
            Horaire = horaire;
            Employe = employe;
            ChangerSemaine = changeSemaine;
        }

        public ICommand CmdOui
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        if (ControlGestionHoraire.Semaine[6] > DateTime.Now)
                        {
                            List<QuartEmploye> LstQuarts = new List<QuartEmploye>();

                            foreach (UIElement item in Horaire.Children)
                            {
                                if (item is Border && (item as Border).Child != null)
                                {
                                    if (((item as Border).Child as Label).Visibility == Visibility.Visible)
                                    {
                                        QuartEmploye quart = new QuartEmploye();
                                        quart.Employe = Employe;
                                        quart.DepartementAssocie = new Departement(((item as Border).Child as Label).Content as string);
                                        quart.Date = Control.ControlGestionHoraire.Semaine[Grid.GetColumn(item) - 2];

                                        switch (Grid.GetRow(item))
                                        {
                                            case 2:
                                                quart.TypeDeQuart = TypeQuart.nuit;
                                                break;
                                            case 3:
                                                quart.TypeDeQuart = TypeQuart.jour;
                                                break;
                                            case 4:
                                                quart.TypeDeQuart = TypeQuart.soir;
                                                break;
                                        }

                                        LstQuarts.Add(quart);

                                    }
                                    else
                                    {
                                        QuartEmploye quart = new QuartEmploye();
                                        quart.Employe = Employe;
                                        quart.DepartementAssocie = new Departement(((item as Border).Child as Label).Content as string);
                                        quart.Date = Control.ControlGestionHoraire.Semaine[Grid.GetColumn(item) - 2];

                                        switch (Grid.GetRow(item))
                                        {
                                            case 2:
                                                quart.TypeDeQuart = TypeQuart.nuit;
                                                break;
                                            case 3:
                                                quart.TypeDeQuart = TypeQuart.jour;
                                                break;
                                            case 4:
                                                quart.TypeDeQuart = TypeQuart.soir;
                                                break;
                                        }

                                        DataModelQuartEmploye.DELETE_quartEmploye(quart);
                                    }
                                }
                            }
                            DataModelQuartEmploye.POSTHoraire(LstQuarts);
                        }

                        DialogHost.CloseDialogCommand.Execute(null, null);
                        ChangerSemaine();
                        ControlModelAjoutQuart.estModifie = false;
                    }  
                );
            }
        }

        public ICommand CmdNon
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        ChangerSemaine();
                        ControlModelAjoutQuart.estModifie = false;
                    }
                );
            }
        }
    }
}
