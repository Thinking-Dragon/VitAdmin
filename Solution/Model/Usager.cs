using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{    // TODO: Max
    public class Usager : Employe
    {
        String NomUtilisateur { get; set; }
        Role RoleUsager { get; set; }
    }
}
