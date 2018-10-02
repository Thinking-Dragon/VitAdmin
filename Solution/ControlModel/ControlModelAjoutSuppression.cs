using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelAjoutSuppression : ObjetObservable
    {
        public ICommand CmdAjout { get; private set; }
        public ICommand CmdSuppression { get; private set; }

        public ControlModelAjoutSuppression(ICommand commandeAjout, ICommand commandeSuppression)
        {
            CmdAjout = commandeAjout;
            CmdSuppression = commandeSuppression;
        }
    }
}
