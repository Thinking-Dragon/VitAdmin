using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    abstract public class Evenement
    {
        public DateTime DateEvenement { get; set; }
        public String Description { get; set; }
        //Avoir pour la liste de notifs
        public List<Notification> Notifs { get; set; }
    }
}
