using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Control;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelListeTraitementsAvecEtapes : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        private ObservableCollection<Traitement> traitements;
        public ObservableCollection<Traitement> Traitements
        {
            get { return traitements; }
            set { traitements = value; RaisePropertyChangedEvent("Traitements"); }
        }

        private Traitement traitementSelectionne;
        public Traitement TraitementSelectionne
        {
            get { return traitementSelectionne; }
            set { traitementSelectionne = value; RaisePropertyChangedEvent("TraitementSelectionne"); }
        }

        private Etape etapeSelectionnee;
        public Etape EtapeSelectionnee
        {
            get { return etapeSelectionnee; }
            set { etapeSelectionnee = value; RaisePropertyChangedEvent("EtapeSelectionnee"); }
        }

        private string instructionSelectionnee;
        public string InstructionSelectionnee
        {
            get { return instructionSelectionnee; }
            set { instructionSelectionnee = value; RaisePropertyChangedEvent("InstructionSelectionnee"); }
        }

        public ICommand CmdAjoutTraitement { get { return new CommandeDeleguee(
            param => {
                DialogHost.Show(new ControlDialogAjout(new CommandeDeleguee(nomTraitement =>
                {
                    Traitements.Add(new Traitement
                    {
                        Nom = nomTraitement as string
                    });
                }), "Nouveau traitement"));
            });
        }}

        public ICommand CmdSuppressionTraitement { get { return new CommandeDeleguee(
            param => {
                if(TraitementSelectionne != null)
                    Traitements.Remove(TraitementSelectionne);
            });
        }}

        public ICommand CmdAjoutEtapes { get { return new CommandeDeleguee(
            param => {
                System.Windows.MessageBox.Show(EtapeSelectionnee.Description);
            });
        }}

        public ICommand CmdSuppressionEtapes { get { return new CommandeDeleguee(
            param => {
                if (EtapeSelectionnee != null)
                    TraitementSelectionne.EtapesTraitement.Remove(EtapeSelectionnee);
            });
        }}

        public ICommand CmdAjoutInstructions { get { return new CommandeDeleguee(
            param => {
                System.Windows.MessageBox.Show(InstructionSelectionnee);
            });
        }}

        public ICommand CmdSuppressionInstructions { get { return new CommandeDeleguee(
            param => {
                if (InstructionSelectionnee != null)
                    EtapeSelectionnee.Instructions.Remove(InstructionSelectionnee);
            });
        }}

        public ControlModelListeTraitementsAvecEtapes(GestionnaireEcrans gestionnaireEcrans, List<Traitement> traitements)
        {
            Traitements = new ObservableCollection<Traitement>(traitements);
            GestionnaireEcrans = gestionnaireEcrans;
        }
    }
}
