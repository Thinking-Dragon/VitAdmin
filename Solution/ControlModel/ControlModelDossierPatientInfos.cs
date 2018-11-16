using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Tools;

namespace VitAdmin.ControlModel
{
    public class ControlModelDossierPatientInfos : ObjetObservable
    {
        public Citoyen Citoyen { get; set; }
        public MessageErreurInfosPatient MessageErreurInfosPatient { get; set; }

        public ControlModelDossierPatientInfos(Citoyen citoyen)
        {
            Citoyen = citoyen;
            MessageErreurInfosPatient = new MessageErreurInfosPatient();
        }
  
    }
}
