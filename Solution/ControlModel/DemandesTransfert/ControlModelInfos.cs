﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel.DemandesTransfert
{
    public class ControlModelListeLits : ObjetObservable
    {
        public List<Citoyen> LstCitoyen { get; set; }

        public ControlModelListeLits(List<Citoyen> lstCitoyen)
        {
            LstCitoyen = lstCitoyen;
        }
    }
}
