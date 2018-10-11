using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    class ControlModelDossierPatientNotesMed : ObjetObservable
    {
        public ObservableCollection<NoteMedecin> LstNotesMed { get; set; }

        public ControlModelDossierPatientNotesMed(Citoyen patient, Hospitalisation hospit, List<NoteMedecin> lstNotesMed)
        {
            LstNotesMed = new ObservableCollection<NoteMedecin>(lstNotesMed);
        }
    }
}
