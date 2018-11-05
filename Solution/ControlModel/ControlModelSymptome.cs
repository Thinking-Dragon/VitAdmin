using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelSymptome : ObjetObservable
    {
        public ObservableCollection<Symptome> Symptomes { get; set; }
        public Hospitalisation Hospitalisation { get; set; }

        public ControlModelSymptome(Hospitalisation hospitalisation)
        {
            if (hospitalisation.LstSymptomes == null)
                hospitalisation.LstSymptomes = new List<Symptome>();

            Hospitalisation = hospitalisation;

            Symptomes = new ObservableCollection<Symptome>(hospitalisation.LstSymptomes);

        }

        public ICommand CmdAjoutSymptome
        {
            get
            {   
                return new CommandeDeleguee(action =>
                {
                    Symptome symptomeAjout = new Symptome { Description = "Ajouter la description"};

                    Symptomes.Add(symptomeAjout);
                    Hospitalisation.LstSymptomes.Add(symptomeAjout);

                });
            }
        }

        public ICommand CmdBtnSupprimer
        {
            get
            {
                return new CommandeDeleguee(symptome =>
                {

                    Symptomes.Remove((Symptome)symptome);
                    Hospitalisation.LstSymptomes.Remove((Symptome)symptome);

                });
            }
        }
    }
}
