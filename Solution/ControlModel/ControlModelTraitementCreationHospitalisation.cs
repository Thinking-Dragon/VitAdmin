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
    public class ControlModelTraitementCreationHospitalisation : ObjetObservable
    {
        public ObservableCollection<Traitement> Traitements { get; set; }
        private Action CallRequeteLits { get; set; }
        public List<Traitement> LstTraitements { get; set; }

        public ICommand CmdBtnSuivant => new CommandeDeleguee(param =>
        {
            MaterialDesignThemes.Wpf.Transitions.Transitioner.MoveNextCommand.Execute(null, null);

            foreach (Traitement traitement in Traitements)
            {
                LstTraitements.Add(traitement);
            }

            CallRequeteLits();
        });

        public ControlModelTraitementCreationHospitalisation(ObservableCollection<Traitement> traitements, Action callRequeteLits, List<Traitement> lstTraitement)
        {
            Traitements = traitements;
            CallRequeteLits = callRequeteLits;
            LstTraitements = lstTraitement;
        }
    }
}
