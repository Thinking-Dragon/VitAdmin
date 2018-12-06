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
    class ControlModelAjoutNote : ObjetObservable
    {
        public NoteMedecin NoteMed { get; set; }
        public NoteInfirmiere NoteInf { get; set; }

        //contenuBtn
        private string contenuBtnPrivate = "Confirmer";
        public String contenuBtn
        {
            get
            {
                return contenuBtnPrivate;
            }
            set
            {
                contenuBtnPrivate = value;
                RaisePropertyChangedEvent("contenuBtn");
            }
        }


        private string MessagePrivate = "";
        public String MessageErreur
        {
            get
            {
                return MessagePrivate;
            }
            set
            {
                MessagePrivate = value;
                RaisePropertyChangedEvent("MessageErreur");
            }
        }

        public Hospitalisation Hospit { get; set; }

        public ControlModelAjoutNote(Hospitalisation hospit)
        {
            Hospit = hospit;
            NoteInf = new NoteInfirmiere();

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
