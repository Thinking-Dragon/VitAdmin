using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel
{

    public class ControlModelTextBoxHospitalisation
    {
        //public Hospitalisation Hospitalisation { get; set; }
        public string NomLabel { get; set; }

        public ControlModelTextBoxHospitalisation(string nomLabel)
        {
            NomLabel = nomLabel;
        }
    }
}
