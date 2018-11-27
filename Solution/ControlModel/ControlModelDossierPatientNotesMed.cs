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
    class ControlModelDossierPatientNotesMed : ObjetObservable
    {
        public static ObservableCollection<NoteMedecin> LstNotesMed { get; set; }

        public Hospitalisation Hospit { get; set; }

        public ControlModelDossierPatientNotesMed(Citoyen patient, Hospitalisation hospit, List<NoteMedecin> lstNotesMed)
        {
            LstNotesMed = new ObservableCollection<NoteMedecin>(lstNotesMed);
            Hospit = hospit;
        }

        public ICommand CmdBtnClicNouvelleNoteMed
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
