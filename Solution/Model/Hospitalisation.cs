using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.MVVM;

namespace VitAdmin.Model
{
    public class Hospitalisation: ObjetObservable
    {
        public DateTime DateDebut { get; set; }
        private DateTime? _dateFin;
        public DateTime? DateFin
        {
            get { return _dateFin; }
            set { _dateFin = value == new DateTime(0) ? null : value; }
        }
        private string _contexte;
        public string Contexte
        {
            get => _contexte;
            set
            {
                _contexte = value;
                RaisePropertyChangedEvent("Contexte");
            }
        }
       
        public List<Traitement> LstTraitements { get; set; }
        public List<Symptome> LstSymptomes { get; set; }

        public Hospitalisation()
        {
        }

        public Hospitalisation(DateTime dt)
        {
            DateDebut = dt;
        }
    }
}
