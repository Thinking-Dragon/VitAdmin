﻿using System;
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

        public ControlModelSymptome(Hospitalisation hospitalisation)
        {
            if (hospitalisation.LstSymptomes == null)
                hospitalisation.LstSymptomes = new List<Symptome>();

            Symptomes = new ObservableCollection<Symptome>();
            hospitalisation.LstSymptomes.ForEach(s => Symptomes.Add(s));
        }

        public ICommand CmdAjoutSymptome
        {
            get
            {   
                return new CommandeDeleguee(action =>
                {
                    Symptome symptomeAjout = new Symptome { Description = "Ajouter la description"};

                    Symptomes.Add(symptomeAjout);

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

                });
            }
        }
    }
}
