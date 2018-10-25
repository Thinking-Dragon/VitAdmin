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
        public BitmapImage Resultats { get; set; }

        public String LienImage { get; set; }

        public String NomAnalyse { get; set; }

        public ResultatLabo( string lienImage, string nomAnalyse, bool estNotifie)
        {
            LienImage = lienImage;
            NomAnalyse = nomAnalyse;
            EstNotifier = estNotifie;
            Resultats = new BitmapImage(new Uri(LienImage));
        }

        public ResultatLabo()
        {

        }
    }
}
