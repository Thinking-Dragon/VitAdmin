﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    public class ModelViewAdminModificationStructure
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public Departement DepartementSelectionne { get; set; }

        public ICommand CmdEquipements
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    // TODO: GestionnaireEcrans.Changer(new (...)(GestionnaireEcrans));
                });
            }
        }

        public ModelViewAdminModificationStructure(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;
    }
}