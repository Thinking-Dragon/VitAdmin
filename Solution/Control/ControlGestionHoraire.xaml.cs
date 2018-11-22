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
using VitAdmin.Model;
using VitAdmin.ControlModel;
using VitAdmin.Data;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlGestionHoraire.xaml
    /// </summary>
    public partial class ControlGestionHoraire : UserControl
    {
        private List<DateTime> Semaine { get; set; } = new List<DateTime>();
        private ControlModelGestionHoraire Contexte {get; set;}

        public ControlGestionHoraire(Employe employe)
        {
            InitializeComponent();
            DataContext = Contexte = new ControlModelGestionHoraire(employe, GrdHoraire);
            InitialiseHoraire();

            /*List<QuartEmploye> horaire = DataModelQuartEmploye.GetHoraire(employe);



            MouseBinding commandDoubleClick = new MouseBinding();
            commandDoubleClick.MouseAction = MouseAction.LeftDoubleClick;
            commandDoubleClick.Command = Contexte.CmdDoubleClickQuart;

            for (int c = 0; c < 7; c++)
            {

                foreach (QuartEmploye quart in horaire)
                {
                    if (Semaine[c].Year == quart.Date.Year && Semaine[c].Day == quart.Date.Day && Semaine[c].Month == quart.Date.Month)
                    {



                        switch (quart.TypeDeQuart)
                        {
                            case TypeQuart.jour:

                                Label temp = new Label
                                {
                                    Content = quart.DepartementAssocie.Nom,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Foreground = Brushes.White
                                };

                                Grid.SetColumn(temp, c + 2);
                                Grid.SetRow(temp, 3);
                                GrdHoraire.Children.Add(temp);

                                Border travaille = new Border
                                {
                                    Background = Brushes.DodgerBlue
                                    
                                };

                                travaille.InputBindings.Add(commandDoubleClick);

                                Panel.SetZIndex(travaille, -1);
                                Grid.SetColumn(travaille, c + 2);
                                Grid.SetRow(travaille, 3);
                                GrdHoraire.Children.Add(travaille);
                                break;

                                //////////////////////////////////////////////////////////////////

                            case TypeQuart.nuit:
                                Label temp2 = new Label
                                {
                                    Content = quart.DepartementAssocie.Nom,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Foreground = Brushes.White
                                };

                                Grid.SetColumn(temp2, c + 2);
                                Grid.SetRow(temp2, 2);
                                GrdHoraire.Children.Add(temp2);

                                Border travaille2 = new Border
                                {
                                    Background = Brushes.DodgerBlue

                                };

                                travaille2.InputBindings.Add(commandDoubleClick);

                                Panel.SetZIndex(travaille2, -1);
                                Grid.SetColumn(travaille2, c + 2);
                                Grid.SetRow(travaille2, 2);
                                GrdHoraire.Children.Add(travaille2);
                                break;

                                /////////////////////////////////////////////////////////////////

                            case TypeQuart.soir:
                                Label temp3 = new Label
                                {
                                    Content = quart.DepartementAssocie.Nom,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Foreground = Brushes.White
                                };

                                Grid.SetColumn(temp3, c + 2);
                                Grid.SetRow(temp3, 4);
                                GrdHoraire.Children.Add(temp3);

                                Border travaille3 = new Border
                                {
                                    Background = Brushes.DodgerBlue
                                };

                                travaille3.InputBindings.Add(commandDoubleClick);


                                Panel.SetZIndex(travaille3, -1);
                                Grid.SetColumn(travaille3, c + 2);
                                Grid.SetRow(travaille3, 4);
                                GrdHoraire.Children.Add(travaille3);
                                break;
                        }
                    }
                }
            }*/
        }

        private void InitialiseHoraire()
        {
            /*29/10/2018*/

            //DateTime aujourdhui = DateTime.Now;
            DateTime aujourdhui = new DateTime(2018, 10, 29);
            DateTime dimanche = new DateTime();

            for (int i = 1; i < 5; i++)
            {
                for (int c = 1; c < 9; c++)
                {
                    Border temp = new Border
                    {
                        BorderBrush = Brushes.DarkGray,
                        BorderThickness = new Thickness(1)
                    };

                    Grid.SetColumn(temp, c);
                    Grid.SetRow(temp, i);
                    GrdHoraire.Children.Add(temp);
                }
            }



            switch (aujourdhui.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dimanche = aujourdhui;
                    break;
                case DayOfWeek.Monday:
                    dimanche = aujourdhui.AddDays(-1);
                    break;
                case DayOfWeek.Tuesday:
                    dimanche = aujourdhui.AddDays(-2);
                    break;
                case DayOfWeek.Wednesday:
                    dimanche = aujourdhui.AddDays(-3);
                    break;
                case DayOfWeek.Thursday:
                    dimanche = aujourdhui.AddDays(-4);
                    break;
                case DayOfWeek.Friday:
                    dimanche = aujourdhui.AddDays(-5);
                    break;
                case DayOfWeek.Saturday:
                    dimanche = aujourdhui.AddDays(-6);
                    break;
            }


            for (int i = 0; i < 7; i++)
            {
                Label temp = new Label
                {
                    Content = dimanche.AddDays(i).Date.ToString("dd MMMM"),
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                Grid.SetColumn(temp, i + 2);
                Grid.SetRow(temp, 1);
                Semaine.Add(dimanche.AddDays(i));
                GrdHoraire.Children.Add(temp);
            }
        }
    }
}


/*
  référence :
  https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.mousebinding?redirectedfrom=MSDN&view=netframework-4.7.2

  code utilisé :
  MouseGesture OpenCmdMouseGesture = new MouseGesture();
  OpenCmdMouseGesture.MouseAction = MouseAction.WheelClick;
  OpenCmdMouseGesture.Modifiers = ModifierKeys.Control;

  MouseBinding OpenCmdMouseBinding = new MouseBinding();
  OpenCmdMouseBinding.Gesture = OpenCmdMouseGesture;
  OpenCmdMouseBinding.Command = ApplicationCommands.Open;

  this.InputBindings.Add(OpenCmdMouseBinding)
*/
