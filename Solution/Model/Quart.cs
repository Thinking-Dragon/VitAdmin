using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class Quart
    {
        public DateTime Date { get; set; }
        public TypeQuart TypeDeQuart { get; set; }
        public Departement DepartementAssocie { get; set; }

    }
}
