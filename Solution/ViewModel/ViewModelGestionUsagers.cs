﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    public class ViewModelGestionUsagers : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ObservableCollection<Usager> Usagers { get; set; }

        private Usager _usagerSelectionne { get; set; }
        public Usager UsagerSelectionne
        {
            get => _usagerSelectionne;
            set { _usagerSelectionne = value; RaisePropertyChangedEvent("UsagerSelectionne"); }
        }

        public ICommand CmdAjouter => new CommandeDeleguee(param =>
            GestionnaireEcrans.Changer(new ViewGestionUsagersCreation(GestionnaireEcrans))
        );

        public ICommand CmdSupprimer => new CommandeDeleguee(param =>
        {

        });

        public ViewModelGestionUsagers(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Usagers = new ObservableCollection<Usager>(DataModelUsager.GetUsagers());
        }
    }
}
