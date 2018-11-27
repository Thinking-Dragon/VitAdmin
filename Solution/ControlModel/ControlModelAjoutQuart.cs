using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel
{
    class ControlModelAjoutQuart
    {
        public List<Departement> LstNomsDepartements { get; set; }

        public ControlModelAjoutQuart()
        {
            LstNomsDepartements = Data.DataModelDepartement.GetNomsDepartements();
        }
    }
}
