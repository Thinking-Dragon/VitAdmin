using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.MVVM
{
    public abstract class ObjetObservable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string nomPropriete)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
    }
}
