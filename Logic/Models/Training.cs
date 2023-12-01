using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Services;

namespace Logic.Models
{
    public class Training
    {
        //Atributos 
        public int TraningID { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
        public string Notes { get; set; }


        public TrainingType MYtrainingType { get; set; }
        public User MYUser { get; set; }

        public Routine MyRoutine { get; set; }

        public Coach MyCoach { get; set; }

        public Training()
        {
            MYtrainingType= new TrainingType(); 
            MYUser= new User();
            MyRoutine= new Routine();
            MyCoach= new Coach();

        }

        #region Agregar 
        public bool add()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@Notes", this.Notes));
            connection.parameterlist.Add(new SqlParameter("@CoachID", this.MyCoach.CoachID));
            connection.parameterlist.Add(new SqlParameter("@TrainingTypeID", this.MYtrainingType.TrainingTypeID));
            connection.parameterlist.Add(new SqlParameter("@RoutineID", this.MyRoutine.RoutineID));
            connection.parameterlist.Add(new SqlParameter("@UserID", this.MYUser.UserID));
            connection.parameterlist.Add(new SqlParameter("@Date", this.Date));

            int result = connection.EjecutarInsertUpdateDelete("TrainingAdd");

            if (result > 0)
            {
                R = true;
            }
            return R;

        }
        #endregion


        public DataTable listar(int IDUser)
        {
            DataTable R = new DataTable();

            Conection MiCnn = new Conection();

            MiCnn.parameterlist.Add(new SqlParameter("@VerActivo", true));
            MiCnn.parameterlist.Add(new SqlParameter("@UserID", IDUser));


            R = MiCnn.EjecutarSELECT("Traininglistar");

            return R;
        }



    }
}
