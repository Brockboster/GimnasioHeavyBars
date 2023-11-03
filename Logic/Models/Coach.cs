using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Services;

namespace Logic.Models
{
    public  class Coach
    {
        #region Atributos
        public int CoachID { get; set; }
        public string CoachName { get; set; }
        public int CoachPhoneNumber { get; set; }
        public string CoachEmail { get; set; }

        //atributos compuestos 
        public CoachType MyCoachtipe { get; set; }

        #endregion

        #region Ctor 
        public Coach()
        {
            MyCoachtipe = new CoachType();
        }
        #endregion

        #region agregar 
        public bool Add()
        {
            bool R = false;
            Conection connection = new Conection();

            connection.parameterlist.Add(new SqlParameter("@CoachName", this.CoachName));
            connection.parameterlist.Add(new SqlParameter("@Email", this.CoachEmail));
            connection.parameterlist.Add(new SqlParameter("@PhoneNumber", this.CoachPhoneNumber));
            connection.parameterlist.Add(new SqlParameter("@Type", this.MyCoachtipe.CoachTypeID));

            int result = connection.EjecutarInsertUpdateDelete("CoachAdd");

            if (result > 0)
            {
                R = true;
            }
            return R;

        }
        #endregion

        #region Eliminar 
        public bool Delete()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.CoachID));
            int r = connection.EjecutarInsertUpdateDelete("CoachDelete");

            if (r > 0)
            {
                R = true;
            }

            return R;
        }
        #endregion

        #region Editar 
        public bool Update()
        {
            bool R = false;
            Conection connection = new Conection();

            connection.parameterlist.Add(new SqlParameter("@CoachName", this.CoachName));
            connection.parameterlist.Add(new SqlParameter("@Email", this.CoachEmail));
            connection.parameterlist.Add(new SqlParameter("@PhoneNumber", this.CoachPhoneNumber));
            connection.parameterlist.Add(new SqlParameter("@CoachTypeID", this.MyCoachtipe.CoachTypeID));
            connection.parameterlist.Add(new SqlParameter("@CoachID", this.CoachID));

            int result = connection.EjecutarInsertUpdateDelete("CoachUpdate");

            if (result > 0)
            {
                R = true;
            }
            return R;
        }

        #endregion

        #region Activar 
        public bool Activate()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.CoachID));
            int r = connection.EjecutarInsertUpdateDelete("CoachActivate");

            if (r > 0)
            {
                R = true;
            }

            return R;
        }
        #endregion
        #region Inactivar
        public bool Inactivate()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.CoachID));
            int r = connection.EjecutarInsertUpdateDelete("CoachInactivate");

            if (r > 0)
            {
                R = true;
            }

            return R;

        }
        #endregion

        #region Buscar ID del Entrenador

        public Coach CoachSearchID()
        {
            Coach R = new Coach();
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.CoachID));
            DataTable dt = new DataTable();
            dt = connection.EjecutarSELECT("CoachSearchID");
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                R.CoachID = Convert.ToInt32(dr["CoachID"]);
                R.CoachPhoneNumber = Convert.ToInt32(dr["CoachPhoneNumber"]);
                R.CoachName = Convert.ToString(dr["CoachName"]);
                R.CoachEmail = Convert.ToString(dr["CoachEmail"]);
                R.MyCoachtipe.CoachTypeID = Convert.ToInt32(dr["CoachTypeID"]);
                R.MyCoachtipe.Description = Convert.ToString(dr["Description"]);


            }
            return R;

        }
        #endregion

        #region Lista de activos
        // Lista los usuarios activos 
        public DataTable ActiveList(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();

            Conection MiCnn = new Conection();

            MiCnn.parameterlist.Add(new SqlParameter("@VerActivo", true));
            MiCnn.parameterlist.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));



            R = MiCnn.EjecutarSELECT("CoachListActives");

            return R;
        }

        // Lista los usuarios inactivos 
        public DataTable InactiveList(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();
            Conection MiCnn = new Conection();

            MiCnn.parameterlist.Add(new SqlParameter("@VerActivo", false));
            MiCnn.parameterlist.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));


            R = MiCnn.EjecutarSELECT("CoachListActives");

            return R;
        }

        #endregion

        #region Consultar Correo electronico del entrenador
        public bool ConsultCoachEmail()
        {
            bool R = false;
            Conection MiCnn = new Conection();

            //agregamos el parametro de correo 
            MiCnn.parameterlist.Add(new SqlParameter("@Email", this.CoachEmail));

            DataTable consulta = new DataTable();
            //paso 1.4.3 y 1.4.4
            consulta = MiCnn.EjecutarSELECT("CoachConsultEmail");

            //paso 1.4.5
            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;


        }
        #endregion

        #region Consultar numero de telefono del entrenador
        public bool ConsultPhonenumber()
        {
            bool R = false;
            Conection MiCnn = new Conection();

            //agregamos el parametro de correo 
            MiCnn.parameterlist.Add(new SqlParameter("@PhoneNumber", this.CoachPhoneNumber));

            DataTable consulta = new DataTable();
            //paso 1.4.3 y 1.4.4
            consulta = MiCnn.EjecutarSELECT("CoachConsultPhoneNumber");

            //paso 1.4.5
            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;


        }
        #endregion

    }
}
