using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class QuartEmploye
    {
        public Employe EmployeAssocie { get; set; }
        public Quart QuartAssocie { get; set; }
    }
}
