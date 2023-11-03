using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
   public  class CoachType
    {

        #region Atributos
        public int CoachTypeID { get; set; }
        public string Description { get; set; }
        #endregion

        #region Lista de tipos de entrenadores
        //Lista de roles de usuario
        public DataTable List()
        {
            DataTable R = new DataTable();
            Services.Conection MiCnn = new Services.Conection();
            R = MiCnn.EjecutarSELECT("CoachTypeList");
            return R;
        }
        #endregion
    }
}
