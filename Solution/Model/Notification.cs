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
        public string LienVersFenetre { get; set; }
    }
}
