using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel.DemandesTransfert
{
    public class ControlModelListeLits : ObjetObservable
    {
        private ObservableCollection<Lit> lits;
        public ObservableCollection<Lit> Lits
        {
            get
            {
                return lits;
            }

            set
            {
                lits = value;
                RaisePropertyChangedEvent("Lits");
            }
        }

        public ControlModelListeLits(List<Lit> lstLits = null)
        {
            Lits = new ObservableCollection<Lit>(lstLits);
        }

        
    }
}
