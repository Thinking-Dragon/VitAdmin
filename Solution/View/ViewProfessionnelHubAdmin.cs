using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View.Tool;

namespace VitAdmin.View
{
    public partial class ViewProfessionnelHubAdmin : ViewProfessionnelHub, IEcranRetour
    {
        public ViewProfessionnelHubAdmin(GestionnaireEcrans gestionnaireEcrans, Employe employe) : base(gestionnaireEcrans, employe) { }

        public Action CmdRetourEcranPrecedent => () => GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans));

        public string TexteBoutonRetourEcran => "< Accueil admin";
    }
}
