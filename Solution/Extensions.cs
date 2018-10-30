using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin
{
    public static class Extensions
    {
        public static IEnumerable<EtatLit> EtatLitsEnumTypes => Enum.GetValues(typeof(EtatLit)).Cast<EtatLit>();
    }
}
