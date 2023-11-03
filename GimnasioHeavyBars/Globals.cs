using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GimnasioHeavyBars
{
    public static class Globals
    {
        public static Form PrincipalPage = new Forms.PrincipalPage();
        public static Logic.Models.User MyGlobalUser = new Logic.Models.User();
        public static Forms.FrmUsers FrmUsers = new Forms.FrmUsers();

    }
}
