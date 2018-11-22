using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel.DemandesTransfert
{
    class ControlModelListeDemandesTransfert
    {
        List<Citoyen> LstCitoyen { get; set; }

        public ControlModelListeDemandesTransfert(List<Citoyen> lstCitoyen)
        {
            LstCitoyen = lstCitoyen;
        }
    }
}
