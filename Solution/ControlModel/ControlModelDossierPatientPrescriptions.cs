using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using MaterialDesignThemes.Wpf;
using VitAdmin.Control;

namespace VitAdmin.ControlModel
{
    class ControlModelDossierPatientPrescriptions : ObjetObservable
    {
        public static ObservableCollection<Prescription> LstPrescriptions { get; set; }

        public Hospitalisation Hospit { get; set; }

        public ControlModelDossierPatientPrescriptions(Citoyen patient, Hospitalisation hospit, List<Prescription> resultRequete)
        {
            LstPrescriptions = new ObservableCollection<Prescription>(resultRequete);
            Hospit = hospit;
        }

        public ICommand CmdBtnClicNouvellePrescription
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        DialogHost.Show(new ControlAjoutPrescription(Hospit), "dialogGeneral");
                    }
                );
            }
        }
    }
}
