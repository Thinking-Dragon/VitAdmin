using MaterialDesignThemes.Wpf;
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
    class ControlModelAjoutNote
    {
        public NoteMedecin NoteMed { get; set; }
        public NoteInfirmiere NoteInf { get; set; }

        public Hospitalisation Hospit { get; set; }

        public ControlModelAjoutNote(Hospitalisation hospit)
        {
            Hospit = hospit;

        }

        public ICommand CmdBtnClicConfirmerNoteMed
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        NoteMed = (NoteMedecin)param;
                        Data.DataModelNotesMed.AddNoteMed(Hospit, NoteMed, Parameter.UsagerConnecte.Usager.idEmploye);
                    }
                );
            }
        }


        public ICommand CmdBtnClicConfirmerNoteInf
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        NoteInf = (NoteInfirmiere)param;
                        Data.DataModelNotesInf.AddNoteInf(Hospit, NoteInf, Parameter.UsagerConnecte.Usager.idEmploye);
                    }
                );
            }
        }
    }
}
