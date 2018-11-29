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
        private static Employe Employe { get; set; }
        private static Grid Horaire { get; set; }

        private static DateTime aujourdhuiP = DateTime.Now;
        public static DateTime aujourdhui
        {
            get
            {
                return aujourdhuiP;
            }
            set
            {
                aujourdhuiP = value;
                Semaine = new List<DateTime>();
                InitialiseHoraire(Horaire, true);
                ViderHoraire(Employe, Horaire);
                RemplirHoraire(Employe, Horaire);
            }
        }


        public ControlGestionHoraire(Employe employe)
        {
            InitializeComponent();
            Employe = employe;
            Horaire = GrdHoraire;
            DataContext = Contexte = new ControlModelGestionHoraire(Employe, Horaire);
            InitialiseHoraire(Horaire, false);
            RemplirHoraire(Employe, Horaire);
        }

        public static void InitialiseHoraire(Grid horaire, bool dejaFait)
        {
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

            switch (aujourdhuiP.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dimanche = aujourdhuiP;
                    break;
                case DayOfWeek.Monday:
                    dimanche = aujourdhuiP.AddDays(-1);
                    break;
                case DayOfWeek.Tuesday:
                    dimanche = aujourdhuiP.AddDays(-2);
                    break;
                case DayOfWeek.Wednesday:
                    dimanche = aujourdhuiP.AddDays(-3);
                    break;
                case DayOfWeek.Thursday:
                    dimanche = aujourdhuiP.AddDays(-4);
                    break;
                case DayOfWeek.Friday:
                    dimanche = aujourdhuiP.AddDays(-5);
                    break;
                case DayOfWeek.Saturday:
                    dimanche = aujourdhuiP.AddDays(-6);
                    break;
            }

            if (dejaFait)
            {
                foreach (UIElement item in horaire.Children)
                {
                    if (item is Label)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if ((item as Label).Name == "L" + i.ToString())
                            {
                                (item as Label).Content = dimanche.AddDays(i).Date.ToString("dd MMMM");
                            }
                            Semaine.Add(dimanche.AddDays(i));
                        }

                    }
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    Label temp = new Label
                    {
                        Content = dimanche.AddDays(i).Date.ToString("dd MMMM"),
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Name = "L" + i.ToString()
                    };

                    Grid.SetColumn(temp, i + 2);
                    Grid.SetRow(temp, 1);
                    Semaine.Add(dimanche.AddDays(i));
                    horaire.Children.Add(temp);
                }
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
                    ((item as Border).Child as Label).Visibility = Visibility.Visible;
                    (item as Border).Background = Brushes.DodgerBlue;
                }
            }
        }

        public static void ViderHoraire(Employe employe, Grid gridHoraireParam)
        {
            foreach (UIElement item in gridHoraireParam.Children)
            {
                if (item is Border && (item as Border).Child is Label)
                {
                    ((item as Border).Child as Label).Content = "";
                    ((item as Border).Child as Label).Visibility = Visibility.Hidden;
                    (item as Border).Background = Brushes.Transparent;
                }
            }
        }
    }
}
