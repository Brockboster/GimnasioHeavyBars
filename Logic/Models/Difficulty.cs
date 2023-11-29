using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Difficulty
    {
        #region Atributos
        public int DifficultyID { get; set; }
        public string Description { get; set; }

        #endregion


        #region Lista de niveles de dificultad
        //Lista de roles de usuario
        public DataTable List()
        {
            DataTable R = new DataTable();
            Services.Conection MiCnn = new Services.Conection();
            R = MiCnn.EjecutarSELECT("DifficultyList");
            return R;
        }
        #endregion





    }
}