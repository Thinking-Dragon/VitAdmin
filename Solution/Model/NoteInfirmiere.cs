using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class NoteInfirmiere : Evenement
    {
        public String NotesInf { get; set; }

        public NoteInfirmiere()
        {

        }

        public NoteInfirmiere(string note, bool notifier)
        {
            EstNotifier = notifier;
            NotesInf = note;
        }
    }
}
