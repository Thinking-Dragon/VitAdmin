using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel.DemandesTransfert
{
    public class ControlModelInfos : ObjetObservable
    {
        public Citoyen Citoyen { get; set; }

        public ControlModelInfos(Citoyen citoyen)
        {
            Citoyen = citoyen;
        }
    }
}
