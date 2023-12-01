using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class TrainingType
    {

        //Atributos 
        public int TrainingTypeID { get; set; }

        public string Description { get; set; }

        #region Lista de tipos de entrenamiento
         
        public DataTable List()
        {
            DataTable R = new DataTable();
            Services.Conection MiCnn = new Services.Conection();
            R = MiCnn.EjecutarSELECT("Trainingtypelist");
            return R;
        }
        #endregion


    }
}
