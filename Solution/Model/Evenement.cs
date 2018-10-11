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
        public Employe EmployeImplique { get; set; }

    }
}
