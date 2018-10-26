using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VitAdmin.ControlModel;
using VitAdmin.Data;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Interaction logic for ControlAjoutNote.xaml
    /// </summary>
    public partial class ControlAjoutNote : UserControl
    {
        private ControlModelAjoutNote ControlModelNote { get; set; }

        private bool EstDeuxiemeClick { get; set; }

        public ControlAjoutNote(Hospitalisation hospit)
        {
            InitializeComponent();
            DataContext = ControlModelNote = new ControlModelAjoutNote(hospit);
        }

        private void Confirmer_Click(object sender, RoutedEventArgs e)
        {
            if (Note.Text == "")
            {
                (DataContext as ControlModelAjoutNote).MessageErreur = "Vous devez remplir tous les champs";
            }
            else if (!EstValide())
            {
                (DataContext as ControlModelAjoutNote).MessageErreur = "La limite de caractère de la note est atteinte";
            }
            else
            {
                if (EstDeuxiemeClick)
                {

                    if (Parameter.UsagerConnecte.Usager.Poste == "admin")
                    {
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        DialogHost.Show(new QuelPosteOccupesTuAdmin(() =>
                        {
                            ExecuteCmdClicNoteETgereDialog();
                        }), "dialogGeneral");
                    }
                    else
                    {
                        ExecuteCmdClicNoteETgereDialog();
                    }
                }
                else
                {
                    (DataContext as ControlModelAjoutNote).MessageErreur = "Voulez-vous vraiment confirmer?";
                    EstDeuxiemeClick = true;
                }
            }
        }

        private bool EstValide()
        {
            return Note.Text.Length > 254;
        }

        private void ExecuteCmdClicNoteETgereDialog()
        {
            if (Parameter.UsagerConnecte.Usager.Poste == "médecin")
            {
                ControlModelNote.CmdBtnClicConfirmerNoteMed.Execute(new NoteMedecin(Note.Text, (bool)Notifier.IsChecked));
                DialogHost.CloseDialogCommand.Execute(null, null);
                Parameter.UsagerConnecte.Usager.Poste = "admin";
            }
            else if (Parameter.UsagerConnecte.Usager.Poste == "infirmière")
            {
                ControlModelNote.CmdBtnClicConfirmerNoteInf.Execute(new NoteInfirmiere(Note.Text, (bool)Notifier.IsChecked));
                DialogHost.CloseDialogCommand.Execute(null, null);
                Parameter.UsagerConnecte.Usager.Poste = "admin";
            }
        }
    }
}
