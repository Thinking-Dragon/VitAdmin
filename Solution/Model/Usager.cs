﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{    // TODO: Max
    public class Usager : Employe
    {
        public String NomUtilisateur { get; set; }
        public Role RoleUsager { get; set; }

       /* internal bool ValiderDuplicataAssMaladie(object dateModelCitoyen)
        {
            throw new NotImplementedException();
        } */
    }
}
