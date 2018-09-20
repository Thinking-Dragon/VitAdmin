using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VitAdmin.MVVM
{
    public class CommandeDeleguee : ICommand
    {
        private readonly Action action;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action();

        public CommandeDeleguee(Action action)
            => this.action = action;
    }
}
