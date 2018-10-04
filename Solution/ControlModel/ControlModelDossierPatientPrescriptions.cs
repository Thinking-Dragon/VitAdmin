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
    class ControlModelDossierPatientPrescriptions : ObjetObservable
    {
        public ObservableCollection<Prescription> LstPrescriptions { get; set; }



        public ControlModelDossierPatientPrescriptions(List<Prescription> resultRequete)
        {
            LstPrescriptions = new ObservableCollection<Prescription>(resultRequete);
        }
    }
}
