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
    public class ControlModelSymptome
    {
        public Hospitalisation Hospitalisation { get; set; }

        public ControlModelSymptome(Hospitalisation hospitalisation)
        {
            Hospitalisation = hospitalisation;
        }

        public ICommand CmdAjoutSymptome
        {
            get
            {   
                return new CommandeDeleguee(action =>
                {
                    Symptome symptomeAjout = new Symptome { Description = "Ajouter la description"};

                    if (Hospitalisation.LstSymptomes == null)
                        Hospitalisation.LstSymptomes = new List<Symptome>();

                    Hospitalisation.LstSymptomes.Add(symptomeAjout);

                });
            }
        }
    }
}
