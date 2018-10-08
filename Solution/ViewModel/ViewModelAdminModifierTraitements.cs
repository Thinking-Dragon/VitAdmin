using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    public class ViewModelAdminModifierTraitements : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        public ObservableCollection<Traitement> Traitements { get; set; }

        public ICommand CmdEnregistrer
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        //
                        MaterialDesignThemes.Wpf.DialogHost.Show(new System.Windows.Controls.DataGrid
                        {
                            ItemsSource = Traitements
                        }, "dialogGeneral");
                        //

                        // POST
                        DataModelTraitement.PutTraitements(new List<Traitement>(Traitements));
                    }
                );
            }
        }

        public ViewModelAdminModifierTraitements(GestionnaireEcrans gestionnaireEcrans, List<Traitement> traitements)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Traitements = new ObservableCollection<Traitement>(traitements);
        }
    }
}
