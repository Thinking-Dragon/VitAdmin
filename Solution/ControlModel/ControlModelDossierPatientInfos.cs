﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelDossierPatientInfos : ObjetObservable
    {
        public Citoyen Citoyen { get; set; }

        public ControlModelDossierPatientInfos(Citoyen citoyen)
        {
            Citoyen = citoyen;
        }
    }
}