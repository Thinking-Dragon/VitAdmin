using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
                // Pour afficher un contrôle dans une fenêtre contextuelle, vous pouvez appeler la fonction DialogHost.Show :
                // Le premier paramètre est le contenu (je vous conseille de mettre une nouvelle instance d'un UserControl)
                // et le deuxième paramètre est l'identifiant du dialog. Si vous voulez un dialog qui s'affiche par dessus tout l'interface,
                // veuillez utiliser un dialog que j'ai créé dans le « super-écran », dont l'identifiant est "dialogGeneral".

                // Exemple d'utilisation :
                // DialogHost.Show(new Control(...), "dialogGeneral");

                DialogHost.Show(new ControlEditionTraitement(new CommandeDeleguee(traitement =>
                {
                    Traitements.Add(traitement as Traitement);
                    DialogHost.CloseDialogCommand.Execute(null, null);
                })), "dialogGeneral");
            });
        }}

        public ICommand CmdEditerTraitement { get { return new CommandeDeleguee(
            param => {
                if(TraitementSelectionne != null)
                {
                    DialogHost.Show(new ControlEditionTraitement(new CommandeDeleguee(traitement =>
                    {
                        TraitementSelectionne.Nom = (traitement as Traitement).Nom;
                        TraitementSelectionne.DepartementAssocie = (traitement as Traitement).DepartementAssocie;
                        Traitements = new ObservableCollection<Traitement>(Traitements);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }), TraitementSelectionne), "dialogGeneral");
                }
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
                if (TraitementSelectionne == null)
                {
                    Grid grid = new Grid();
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.Children.Add(new Label { Content = "Sélectionnez d'abord un traitement !", Margin = new Thickness(32) });
                    Button buttonClose = new Button { Content = "Okay" };
                    buttonClose.Command = DialogHost.CloseDialogCommand;
                    Grid.SetRow(buttonClose, 1);
                    grid.Children.Add(buttonClose);
                    
                    DialogHost.Show(grid, "dialogGeneral");
                }
                else
                {
                    DialogHost.Show(new ControlDialogAjout(new CommandeDeleguee(descriptionEtape =>
                    {
                        if((descriptionEtape as string) != string.Empty)
                        {
                            TraitementSelectionne.EtapesTraitement.Add(
                                new Etape
                                {
                                    Description = descriptionEtape as string,
                                    Instructions = new ObservableCollection<string>()
                                }
                            );
                            DialogHost.CloseDialogCommand.Execute(null, null);
                        }
                        else
                        {
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            Grid grid = new Grid();
                            grid.RowDefinitions.Add(new RowDefinition());
                            grid.RowDefinitions.Add(new RowDefinition());
                            grid.Children.Add(new Label { Content = "Un nom d'étape ne peut pas être vide", Margin = new Thickness(32) });
                            Button buttonClose = new Button { Content = "Okay" };
                            buttonClose.Command = DialogHost.CloseDialogCommand;
                            Grid.SetRow(buttonClose, 1);
                            grid.Children.Add(buttonClose);

                            DialogHost.Show(grid, "dialogGeneral");
                        }
                    }), "Nouvelle étape"), "dialogGeneral");
                }
            });
        }}

        public ICommand CmdEditerEtape { get { return new CommandeDeleguee(
            param => {
                if(EtapeSelectionnee != null)
                {
                    DialogHost.Show(new ControlDialogAjout(new CommandeDeleguee(descriptionEtape =>
                    {
                        EtapeSelectionnee.Description = descriptionEtape as string;
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }), "Modifier l'étape « " + EtapeSelectionnee.Description + " »", EtapeSelectionnee.Description), "dialogGeneral");
                }
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
                if (EtapeSelectionnee == null)
                {
                    Grid grid = new Grid();
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.Children.Add(new Label { Content = "Sélectionnez d'abord une étape !", Margin = new Thickness(32) });
                    Button buttonClose = new Button { Content = "Okay" };
                    buttonClose.Command = DialogHost.CloseDialogCommand;
                    Grid.SetRow(buttonClose, 1);
                    grid.Children.Add(buttonClose);

                    DialogHost.Show(grid, "dialogGeneral");
                }
                else
                {
                    DialogHost.Show(new ControlDialogAjout(new CommandeDeleguee(instruction =>
                    {
                        if((instruction as string) != string.Empty)
                        {
                            EtapeSelectionnee.Instructions.Add(instruction as string);
                            DialogHost.CloseDialogCommand.Execute(null, null);
                        }
                        else
                        {
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            Grid grid = new Grid();
                            grid.RowDefinitions.Add(new RowDefinition());
                            grid.RowDefinitions.Add(new RowDefinition());
                            grid.Children.Add(new Label { Content = "Un nom d'instruction ne peut pas être vide", Margin = new Thickness(32) });
                            Button buttonClose = new Button { Content = "Okay" };
                            buttonClose.Command = DialogHost.CloseDialogCommand;
                            Grid.SetRow(buttonClose, 1);
                            grid.Children.Add(buttonClose);

                            DialogHost.Show(grid, "dialogGeneral");
                        }
                    }), "Nouvelle instruction"), "dialogGeneral");
                }
            });
        }}

        public ICommand CmdEditerInstructions { get { return new CommandeDeleguee(
            param => {
                if(InstructionSelectionnee != null)
                {
                    DialogHost.Show(new ControlDialogAjout(new CommandeDeleguee(instruction =>
                    {
                        for (int i = 0; i < EtapeSelectionnee.Instructions.Count; ++i)
                            if (EtapeSelectionnee.Instructions[i] == InstructionSelectionnee)
                                EtapeSelectionnee.Instructions[i] = instruction as string;
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }), "Modifier l'instruction « " + InstructionSelectionnee + " »", InstructionSelectionnee), "dialogGeneral");
                }
            });
        }}

        public ICommand CmdSuppressionInstructions { get { return new CommandeDeleguee(
            param => {
                if (InstructionSelectionnee != null)
                    EtapeSelectionnee.Instructions.Remove(InstructionSelectionnee);
            });
        }}

        public ControlModelListeTraitementsAvecEtapes(GestionnaireEcrans gestionnaireEcrans, ObservableCollection<Traitement> traitements)
        {
            Traitements = traitements;
            GestionnaireEcrans = gestionnaireEcrans;
        }
    }
}
