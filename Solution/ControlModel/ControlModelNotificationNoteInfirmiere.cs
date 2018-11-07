using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelNotificationNoteInfirmiere : ObjetObservable
    {
        private NoteInfirmiere _noteInfirmiere;

        public NoteInfirmiere NoteInfirmiere
        {
            get => _noteInfirmiere;
            set { _noteInfirmiere = value; RaisePropertyChangedEvent("NoteInfirmiere"); }
        }

        public ControlModelNotificationNoteInfirmiere(NoteInfirmiere noteInfirmiere)
            => NoteInfirmiere = noteInfirmiere;
    }
}
