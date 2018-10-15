using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    class ControlModelAjoutNote
    {
        public NoteMedecin Note { get; set; }

        public ICommand CmdBtnClicConfirmerNoteMed
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        
                    }
                );
            }
        }

        public ICommand CmdBtnClicNotifierNoteMed
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        
                    }
                );
            }
        }

        public ICommand CmdBtnClicUnNotifyNoteMed
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {

                    }
                );
            }
        }
    }
}
