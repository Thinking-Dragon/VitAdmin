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
    class ControlModelAjoutResultatLabo : ObjetObservable
    {
        public ResultatLabo ResultLabo { get; set; }

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

        public ControlModelAjoutResultatLabo(Hospitalisation hospit)
        {
            Hospit = hospit;
        }

        public ICommand CmdBtnClicConfirmerResultatLabo
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        ResultLabo = (ResultatLabo)param;
                        Data.DataModelResultatsLabo.AddResultatLabo(Hospit, ResultLabo, Parameter.UsagerConnecte.Usager.idEmploye);
                    }
                );
            }
        }
    }
}
