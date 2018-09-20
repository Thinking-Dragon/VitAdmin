using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelConnexion : ObjetObservable
    {
        public string Usager { get; set; }

        public ICommand CmdConnexion
        {
            get
            {
                return new CommandeDeleguee(password =>
                {
                    System.Windows.MessageBox.Show(Usager + ", " + (password as PasswordBox).Password);
                });
            }
        }
    }
}
