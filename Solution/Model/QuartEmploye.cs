using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class QuartEmploye
    {
        //public Employe EmployeAssocie { get; set; } N'est plus nécessaire. Sinon, cela fait du relationnel.
        public Quart QuartAssocie { get; set; }
        public List<Citoyen> LstCitoyen;
    }
}
