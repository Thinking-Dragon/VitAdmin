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
    class ControlModelAjoutPrescription : ObjetObservable
    {
        public Prescription Prescript { get; set; }

        private string MessagePrivate = "";
        public String MessageErreur
        {
            get
            {
                return MessagePrivate;
            }
            set
            {
                MessagePrivate = value;
                RaisePropertyChangedEvent("MessageErreur");
            }
        }

        public Hospitalisation Hospit { get; set; }

        public ControlModelAjoutPrescription(Hospitalisation hospit)
        {
            Hospit = hospit;
        }

        public ICommand CmdBtnClicConfirmerPrescription
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        Prescript = (Prescription)param;
                        Data.DataModelPrescriptions.AddPrescription(Hospit, Prescript, Parameter.UsagerConnecte.Usager.idEmploye);
                    }
                );
            }
        }
    }
}
