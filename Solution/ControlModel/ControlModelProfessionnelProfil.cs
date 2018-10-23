using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel
{
    public class ControlModelProfessionnelProfil
    {
        Employe Employe { get; set; }

        public ControlModelProfessionnelProfil(Employe employe)
        {
            Employe = employe;
        }
    }
}
