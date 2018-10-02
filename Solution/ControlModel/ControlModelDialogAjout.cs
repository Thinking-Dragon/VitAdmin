using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelDialogAjout : ObjetObservable
    {
        public ICommand CmdAjout { get; set; }

        public ControlModelDialogAjout(ICommand actionAjout)
            => CmdAjout = actionAjout;
    }
}
