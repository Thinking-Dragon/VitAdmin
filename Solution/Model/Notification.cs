using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class Notification
    {
        public string Message { get; set; }
        public DateTime TempsReception { get; set; }
        public bool EstLu { get; set; }
        public LienNotificationEcran LienNotificationEcran { get; set; }

        public void Voir()
        {
            DialogHost.Show(Activator.CreateInstance(LienNotificationEcran.TypeEcran), "dialogGeneral");
        }
    }
}
