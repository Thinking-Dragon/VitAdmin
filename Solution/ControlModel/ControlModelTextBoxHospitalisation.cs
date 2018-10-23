using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel
{

    class ControlModelTextBoxHospitalisation
    {
        //public Hospitalisation Hospitalisation { get; set; }
        public string NomLabel { get; set; }
        public string DataText { get; set; }

        public ControlModelTextBoxHospitalisation(string dataText, string nomLabel)
        {
            DataText = dataText;
            NomLabel = nomLabel;
        }
    }
}
