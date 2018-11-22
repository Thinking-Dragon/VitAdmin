using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.View.Tool;

namespace VitAdmin.View
{
    public partial class ViewListeEmployesAdmin : ViewListeEmployes, IEcranRetour
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewListeEmployesAdmin(GestionnaireEcrans gestionnaireEcrans) : base(gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;

        public Action CmdRetourEcranPrecedent => () => GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans));

        public string TexteBoutonRetourEcran => "< Accueil";
    }
}
