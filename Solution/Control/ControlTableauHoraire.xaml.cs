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
using VitAdmin.Data;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Interaction logic for ControlTableauHoraire.xaml
    /// </summary>
    public partial class ControlTableauHoraire : UserControl
    {
        public ControlTableauHoraire()
        {
            InitializeComponent();
            DataContext = new ControlModelTableauHoraire(DataModelQuartEmploye.GetHoraire());

            List<DateTime> semaine = new List<DateTime>();
            DateTime aujourdhui = DateTime.Now;
            DateTime dimanche = new DateTime();

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

                Grid.SetColumn(temp, i+2);
                Grid.SetRow(temp, 1);
                semaine.Add(dimanche.AddDays(i));
                GrdHoraire.Children.Add(temp);
            }



            List<QuartEmploye> resultRequete = DataModelQuartEmploye.GetHoraire();

        }
    }
}
