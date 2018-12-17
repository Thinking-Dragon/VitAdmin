using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Control;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{

    public class ControlModelTextBoxHospitalisation : ObjetObservable
    {
        public Hospitalisation Hospitalisation { get; set; }
        public string NomLabel { get; set; }

        public ControlModelTextBoxHospitalisation(string nomLabel, Hospitalisation hospitalisation)
        {
            NomLabel = nomLabel;
            Hospitalisation = hospitalisation;
            //Hospitalisation.Contexte = "Ajouter le contexte ici";
        }
    }
}
