﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    class ControlModelDossierPatientNotesMed : ObjetObservable
    {
        public List<NoteMedecin> LstNotesMed { get; set; }

        public ControlModelDossierPatientNotesMed(Citoyen patient, Hospitalisation hospit /*résultat de la requête*/)
        {

        }
    }
}