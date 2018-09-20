using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace VitAdmin.Model
{
    public class ResultatLabo : Evenement
    {
        public List<BitmapImage> Resultats { get; set; }
    }
}
