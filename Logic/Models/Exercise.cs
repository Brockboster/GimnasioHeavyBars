using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Services;

namespace Logic.Models
{
    public class Exercise
    {

        #region Atributos
        public int ExerciseID { get; set; }
        public string NameExercise { get; set; }

        public string Description { get; set; }
        #endregion|


        #region buscar ejercicio mediante el ID
        //se utiliza para traer los datos del cellclick
        public Exercise ExerciseSearchID()
        {
            Exercise R = new Exercise();
            Conection conection = new Conection();
            conection.parameterlist.Add(new SqlParameter("@ID", this.ExerciseID));
            DataTable dt = new DataTable();
            dt = conection.EjecutarSELECT("ExerciseSearchID");
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                R.ExerciseID = Convert.ToInt32(dr["ExerciseID"]);
                R.Description = Convert.ToString(dr["Description"]);
                R.NameExercise = Convert.ToString(dr["NameExercise"]);


            }
            return R;


        }

        #endregion

        #region agregar
        public bool Add()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@NameExercise", this.NameExercise));

            connection.parameterlist.Add(new SqlParameter("@Description", this.Description));

            int result = connection.EjecutarInsertUpdateDelete("ExerciseAdd");

            if (result > 0)
            {
                R = true;
            }
            return R;

        }
        #endregion

        #region Modificar 
        //Modificar el ejercicio
        public bool Update()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@NameExercise", this.NameExercise));
            connection.parameterlist.Add(new SqlParameter("@Description", this.Description));


            int result = connection.EjecutarInsertUpdateDelete("ExerciseUpdate");

            if (result > 0)
            {
                R = true;
            }
            return R;
        }
        #endregion

        #region Eliminar 
        //Eliminar ejercicio
        public bool Delete()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.ExerciseID));
            int r = connection.EjecutarInsertUpdateDelete("ExerciseDelete");

            if (r > 0)
            {
                R = true;
            }

            return R;
        }
        #endregion

        #region Lista de activos
        // Lista de ejercicios 
        public DataTable ListExercise()
        {
            DataTable R = new DataTable();

            Conection MiCnn = new Conection();
            R = MiCnn.EjecutarSELECT("ExerciseList");

            return R;
        }



        #endregion

    }
}