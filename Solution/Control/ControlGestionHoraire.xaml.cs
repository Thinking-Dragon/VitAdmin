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
        public static List<DateTime> Semaine { get; set; } = new List<DateTime>();
        private ControlModelGestionHoraire Contexte {get; set;}
        private Employe Employe { get; set; }
        public static DateTime aujourdhui { get; set; } = DateTime.Now;

        public ControlGestionHoraire(Employe employe)
        {
            InitializeComponent();
            Employe = employe;
            DataContext = Contexte = new ControlModelGestionHoraire(Employe, GrdHoraire);
            InitialiseHoraire(GrdHoraire);
            RemplirHoraire(Employe, GrdHoraire);
        }

        public static void InitialiseHoraire(Grid horaire)
        {
            //DateTime aujourdhui = new DateTime(2018, 10, 29);
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
                    horaire.Children.Add(temp);
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
                horaire.Children.Add(temp);
            }
        }

        public static void RemplirHoraire(Employe employe, Grid gridHoraireParam)
        {
            List<QuartEmploye> horaire = DataModelQuartEmploye.GetHoraire(employe);

            string nom;

            for (int c = 0; c < 7; c++)
            {
                foreach (QuartEmploye quart in horaire)
                {
                    if (Semaine[c].Year == quart.Date.Year && Semaine[c].Day == quart.Date.Day && Semaine[c].Month == quart.Date.Month)
                    {
                        switch (quart.TypeDeQuart)
                        {
                            case TypeQuart.jour:

                                nom = "B" + "J" + (c + 2).ToString();

                                AjouterQuart(nom, gridHoraireParam, quart);

                                break;

                            case TypeQuart.nuit:

                                nom = "B" + "N" + (c + 2).ToString();

                                AjouterQuart(nom, gridHoraireParam, quart);

                                break;

                            case TypeQuart.soir:

                                nom = "B" + "S" + (c + 2).ToString();

                                AjouterQuart(nom, gridHoraireParam, quart);

                                break;
                        }
                    }
                }
            }
        }

        public static void AjouterQuart(string nom, Grid gridHoraireParam, QuartEmploye quart)
        {
            foreach (UIElement item in gridHoraireParam.Children)
            {
                if (item is Border && (item as Border).Name == nom)
                {
                    ((item as Border).Child as Label).Content = quart.DepartementAssocie.Nom;
                    (item as Border).Background = Brushes.DodgerBlue;
                }
            }
        }
    }
}
