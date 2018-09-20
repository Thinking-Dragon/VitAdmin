using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelConnexion : ObjetObservable
    {
        public string Usager { get; set; }
        public string MotDePasse { get; set; }

        public ICommand CmdConnexion
        {
            get
            {
                return new CommandeDeleguee(() =>
                {
                    System.Windows.MessageBox.Show(Usager);
                });
            }
        }
    }
}
