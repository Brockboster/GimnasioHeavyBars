using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Question
    {
        public int QuestionID { get; set; }

        public string QuestionName { get; set; }





        #region Lista de rol de Preguntas
        //Lista de roles de usuario
        public DataTable List()
        {
            DataTable R = new DataTable();
            Services.Conection MiCnn = new Services.Conection();
            R = MiCnn.EjecutarSELECT("QuestionList");
            return R;
        }
        #endregion
    }
}
