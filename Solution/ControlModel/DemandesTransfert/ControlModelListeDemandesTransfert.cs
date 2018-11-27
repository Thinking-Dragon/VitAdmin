using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel.DemandesTransfert
{
    class ControlModelListeDemandesTransfert
    {
        public ObservableCollection<Citoyen> Citoyens { get; set; }

        public ControlModelListeDemandesTransfert(List<Citoyen> lstCitoyen)
        {
            Citoyens = new ObservableCollection<Citoyen>(lstCitoyen);
        }
    }
}
