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
        public NoteMedecin Note { get; set; }

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
                        Note = (NoteMedecin)param;
                        Data.DataModelNotesMed.AddNoteMed(Hospit, Note, Parameter.UsagerConnecte.Usager.idEmploye);
                    }
                );
            }
        }
    }
}
