using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VitAdmin.MVVM
{
    /// <summary>
    /// Description: permet de définir une ICommand à son affectation en définissant une fonction lambda.
    /// Auteur: Clément Gaßmann-Prince
    /// </summary>
    public class CommandeDeleguee : ICommand
    {
        private readonly Action<object> action;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action(parameter);

        public CommandeDeleguee(Action<object> action)
            => this.action = action;
    }
}
