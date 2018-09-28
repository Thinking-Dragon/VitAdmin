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
    class ControlModelDossierPatientNotesInf : ObjetObservable
    {
        public List<NoteInfirmiere> LstNotesInf { get; set; }
        public ControlModelDossierPatientNotesInf(Citoyen patient, Hospitalisation hospit /*le résultat de la requête*/)
        {

        }
    }
}
