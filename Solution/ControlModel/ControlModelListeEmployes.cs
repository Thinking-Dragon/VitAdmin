using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.Data;
using System.Collections.ObjectModel;

namespace VitAdmin.ControlModel
{
    class ControlModelListeEmployes
    {
        public ObservableCollection<Employe> LstEmployes { get; set; }

        public ControlModelListeEmployes()
        {
            LstEmployes = new ObservableCollection<Employe>(DataModelEmploye.GetEmployes());
        }
    }
}
