using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    public class ViewModelMessageNotification : ObjetObservable
    {
        private string _titre;
        public string Titre
        {
            get => _titre;
            set { _titre = value; RaisePropertyChangedEvent(nameof(Titre)); }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; RaisePropertyChangedEvent(nameof(Message)); }
        }

        public ViewModelMessageNotification(string titre, string message)
        {
            Titre = titre;
            Message = message;
        }
    }
}
