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
    class ControlModelDossierPatientNotesInf : ObjetObservable
    {
        public ObservableCollection<NoteInfirmiere> LstNotesInf { get; set; }

        public ControlModelDossierPatientNotesInf(Citoyen patient, Hospitalisation hospit, List<NoteInfirmiere> lstNotesInf)
        {
            LstNotesInf = new ObservableCollection<NoteInfirmiere>(lstNotesInf);
        }

        public ICommand CmdBtnClicNouvelleNoteInf
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    System.Windows.MessageBox.Show("Hey!");
                });
            }
        }
    }
}
