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
    public class Routine
    {
        //Atributos 
        public int RoutineID { get; set; }
        public string RoutineName { get; set; }
        public List<RoutineDetail> MyRoutineDetail { get; set; } = new List<RoutineDetail>();

        public int RoutineDuration { get; set; }

        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }

        public Routine()
        {
            Difficulty = new Difficulty();
            MyRoutineDetail = new List<RoutineDetail>();
        }

        #region Lista de   rutinas
        //Lista de roles de usuario
        public DataTable List()
        {
            DataTable R = new DataTable();
            Services.Conection MiCnn = new Services.Conection();
            R = MiCnn.EjecutarSELECT("RoutineList");
            return R;
        }
        #endregion

        public bool Add()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@RoutineName", this.RoutineName));
            connection.parameterlist.Add(new SqlParameter("@RoutineDuration", this.RoutineDuration));
            connection.parameterlist.Add(new SqlParameter("@Description", this.Description));
            connection.parameterlist.Add(new SqlParameter("@Difficulty", this.Difficulty.DifficultyID));

            Object Retorno = connection.EjecutarSELECTEscalar("RoutineAdd");

            int IdRutinaRecienCreada = 0;

            if (Retorno != null)
            {
                IdRutinaRecienCreada = Convert.ToInt32(Retorno.ToString());

                // Una vez que se tiene el ID de la factura, se pueden agregar los detalles
                this.RoutineID = IdRutinaRecienCreada;

                foreach (RoutineDetail exercise in this.MyRoutineDetail)
                {
                    // Se hace un insert por cada iteración en detalles
                    Conection MyCnnDetalle = new Conection();

                    MyCnnDetalle.parameterlist.Add(new SqlParameter("@RoutineID", IdRutinaRecienCreada));
                    MyCnnDetalle.parameterlist.Add(new SqlParameter("ExerciseID", exercise.MyExercise.ExerciseID));
                    MyCnnDetalle.parameterlist.Add(new SqlParameter("@Sets", exercise.Sets));
                    MyCnnDetalle.parameterlist.Add(new SqlParameter("@Reps", exercise.Reps));


                    MyCnnDetalle.EjecutarInsertUpdateDelete("RoutineaddExcercise");

                }

                R = true;
            }

            return R;
        }




    }
}