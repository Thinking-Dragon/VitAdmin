using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class LienNotificationEcran
    {
        public Type TypeEcran { get; set; }
        public Dictionary<string, object> Parametres { get; set; }
    }
}
