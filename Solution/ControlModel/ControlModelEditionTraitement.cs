using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelEditionTraitement : ObjetObservable
    {
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
    }
}
