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
    public class ControlModelEditionEquipement : ObjetObservable
    {
        private ICommand CmdCallBack { get; set; }

        private string _nom = string.Empty;
        public string Nom
        {
            get => _nom;
            set { _nom = value; RaisePropertyChangedEvent("Nom"); }
        }
        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set { _description = value; RaisePropertyChangedEvent("Description"); }
        }

        public ICommand CmdConfirmer => new CommandeDeleguee(param =>
        {
            if(Nom == string.Empty)
                MessageErreur = "Le nom est obligatoire";
            else
                CmdCallBack.Execute(new Equipement { Nom = Nom, Description = Description });
        });

        private string _messageErreur = string.Empty;
        public string MessageErreur
        {
            get { return _messageErreur; }
            set
            {
                _messageErreur = value;
                RaisePropertyChangedEvent("MessageErreur");
            }
        }

        private string _titre = "Nouvel équipement";
        public string Titre
        {
            get { return _titre; }
            set
            {
                _titre = value;
                RaisePropertyChangedEvent("Titre");
            }
        }

        public ControlModelEditionEquipement(ICommand cmdConfirmer, Equipement equipement)
        {
            CmdCallBack = cmdConfirmer;
            if (equipement != null)
            {
                Nom = equipement.Nom;
                Description = equipement.Description;
                Titre = "Modifier équipement";
            }
        }
    }
}
