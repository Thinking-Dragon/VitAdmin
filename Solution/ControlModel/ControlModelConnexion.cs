﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelConnexion : ObjetObservable
    {
        private string _usager = string.Empty;
        public string Usager
        {
            get { return _usager; }
            set
            {
                _usager = value;
                RaisePropertyChangedEvent("Usager");
            }
        }

        private string _messageErreur = string.Empty;
        public string MessageErreur
        {
            get { return _messageErreur; }
            set
            {
                _messageErreur = value;
                RaisePropertyChangedEvent("MessageErreur");
            }
        }

        public ICommand CmdConnexion
        {
            get
            {
                return new CommandeDeleguee(password =>
                {
                    EtatAvecMessage validation = DataModelConnexion.ValiderIdentite(Usager, (password as PasswordBox).Password);
                    if (validation.Etat)
                    {
                        Usager usager = DataModelConnexion.GetUsager(Usager);
                            // ... //
                    }
                    else MessageErreur = validation.Message;
                });
            }
        }
    }
}
