using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class UserRole
    {
        #region Atributos
        //Atributos del UserRole
        public int UserRoleID { get; set; }
        public string Description { get; set; }
        #endregion


        #region Lista de rol de usuarios
        //Lista de roles de usuario
        public DataTable List()
        {
            DataTable R = new DataTable();
            Services.Conection MiCnn = new Services.Conection();
            R = MiCnn.EjecutarSELECT("UsersRoleList");
            return R;
        }
        #endregion

    }
}
