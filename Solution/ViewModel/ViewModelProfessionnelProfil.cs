using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelProfil
    {
        Employe Employe { get; set; }
        public ICommand CmdBtnCreer
        {
            get
            {
                return new CommandeDeleguee(newEmploye =>
                {

                });
            }
        }
    }
}
