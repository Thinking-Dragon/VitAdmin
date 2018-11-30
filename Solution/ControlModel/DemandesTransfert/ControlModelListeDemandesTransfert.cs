using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Control.DemandesTransfert;

namespace VitAdmin.ControlModel.DemandesTransfert
{
    class ControlModelListeDemandesTransfert : ObjetObservable
    {
        private ObservableCollection<Citoyen> citoyens;
        public ObservableCollection<Citoyen> Citoyens
        {
            get
            {
                return citoyens;
            }

            set
            {
                citoyens = value;
                RaisePropertyChangedEvent("Citoyens");
            }
        }

        ControlListeLits ControlListeLits { get; set; }

        public ControlModelListeDemandesTransfert(List<Citoyen> lstCitoyen, ControlListeLits controlListeLits)
        {
            Citoyens = new ObservableCollection<Citoyen>(lstCitoyen);
            ControlListeLits = controlListeLits;
        }

        public ICommand CmddtgDemandeTransfertLeftClick
        {
            get
            {
                return new CommandeDeleguee(demandeSelectionnee =>
                {
                    if (demandeSelectionnee != null)
                    {
                        string dataFormat = DataFormats.Serializable;
                        DataObject dataObject = new DataObject(dataFormat, (Citoyen)demandeSelectionnee);
                        DragDrop.DoDragDrop(ControlListeLits, dataObject, DragDropEffects.All);
                    }

                });
            }
        }
    }
}
