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
    class ControlModelDossierPatientResultatsLabo : ObjetObservable
    {
        public static ObservableCollection<ResultatLabo> LstResultsLabo { get; set; }

        public Hospitalisation Hospit { get; set; }

        public ControlModelDossierPatientResultatsLabo(Citoyen patient, Hospitalisation hospit, List<ResultatLabo> lstResulLabo)
        {
            LstResultsLabo = new ObservableCollection<ResultatLabo>(lstResulLabo);
            Hospit = hospit;
        }

        public ICommand CmdBtnClicNouveauResultat
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        DialogHost.Show(new ControlAjoutResultatLabo(Hospit), "dialogGeneral");
                    }
                );
            }
        }
    }
}
