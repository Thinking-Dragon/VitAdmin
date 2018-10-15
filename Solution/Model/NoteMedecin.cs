using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class NoteMedecin : Evenement
    {
        public String NotesMed { get; set; }

        public NoteMedecin()
        {

        }

        public NoteMedecin(string note, bool notifier)
        {
            EstNotifier = notifier;
            NotesMed = note;
        }
    }
}
