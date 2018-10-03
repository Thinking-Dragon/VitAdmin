using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel
{
    class ControlModelProfessionnelDossierPatient
    {
        ObservableCollection<Hospitalisation> hospitalisations { get; set; }

        public ControlModelProfessionnelDossierPatient(ObservableCollection<Hospitalisation> hospit)
        {
            hospitalisations = hospit;
        }
    }
}
