using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.MVVM
{
    /// <summary>
    /// Description: Classe de base qui permet d'invoquer l'évènement de changement de propriété,
    ///              qui actualise les éléments de la vue liés à la propriété modifiée.
    /// Auteur: Clément Gaßmann-Prince
    /// </summary>
    public abstract class ObjetObservable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string nomPropriete)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
    }
}
