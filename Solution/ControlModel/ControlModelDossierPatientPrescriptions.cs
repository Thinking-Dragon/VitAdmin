using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    class ControlModelDossierPatientPrescriptions : ObjetObservable
    {
        public List<Prescription> LstPrescriptions { get; set; }

        public ControlModelDossierPatientPrescriptions(Citoyen patient, Hospitalisation hospit /*résultat de la requête*/)
        {

        }
    }
}
