using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Control;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    class ControlModelDossierPatientNotesInf : ObjetObservable
    {
        public static ObservableCollection<NoteInfirmiere> LstNotesInf { get; set; }

        public Hospitalisation Hospit { get; set; }

        public ControlModelDossierPatientNotesInf(Citoyen patient, Hospitalisation hospit, List<NoteInfirmiere> lstNotesInf)
        {
            LstNotesInf = new ObservableCollection<NoteInfirmiere>(lstNotesInf);
            Hospit = hospit;
        }

        public ICommand CmdBtnClicNouvelleNoteInf
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        DialogHost.Show(new ControlAjoutNote(Hospit), "dialogGeneral:modal=false");
                    }
                );
            }
        }


       
    }
}
