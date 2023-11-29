using System;
using System.Data.SqlClient;
using System.Data;
using Logic.Models.Services;

namespace Logic.Models
{
    public class User
    {
        #region Atributos 
        //Atributos 
        public int UserID { get; set; }
        public int PhoneNumber { get; set; }
        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public string UserPassword { get; set; }

        public string UserEmail { get; set; }

        public bool Active { get; set; }

        public string UserName { get; set; }

        public string answer { get; set; }
        public DateTime RegisterDay { get; set; }

        public DateTime DateFinal { get; set; }


        public DateTime DateStart { get; set; }



        //atributos compuestos 
        public UserRole MyUserRole { get; set; }
        public Membership membership { get; set; }



        public Question question { get; set; }

        #endregion

        #region ctor
        public User()
        {
            membership = new Membership();
            MyUserRole = new UserRole();

            question = new Question();
        }

        #endregion

        #region Lista de activos y inactivos 
        // Lista los usuarios activos y inactivos  
        public DataTable ActiveList(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();

            Conection MiCnn = new Conection();

            MiCnn.parameterlist.Add(new SqlParameter("@VerActivo", true));
            MiCnn.parameterlist.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));



            R = MiCnn.EjecutarSELECT("UsersListActives");

            return R;
        }

        // Lista los usuarios inactivos 
        public DataTable InactiveList(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();
            Conection MiCnn = new Conection();

            MiCnn.parameterlist.Add(new SqlParameter("@VerActivo", false));
            MiCnn.parameterlist.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));


            R = MiCnn.EjecutarSELECT("UsersListActives");

            return R;
        }

        #endregion

        #region Agregar 
        public bool Add()
        {
            bool R = false;
            Conection connection = new Conection();


            Crypto crypto = new Crypto();
            string Passwordencrypted = crypto.EncriptarEnUnSentido(this.UserPassword);
            connection.parameterlist.Add(new SqlParameter("@UserPassword", Passwordencrypted));

            connection.parameterlist.Add(new SqlParameter("@UserName", this.UserName));
            connection.parameterlist.Add(new SqlParameter("@weight", this.Weight));
            connection.parameterlist.Add(new SqlParameter("@Height", this.Height));
            connection.parameterlist.Add(new SqlParameter("@Email", this.UserEmail));
            connection.parameterlist.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
            connection.parameterlist.Add(new SqlParameter("@UserRoleID", this.MyUserRole.UserRoleID));
            connection.parameterlist.Add(new SqlParameter("@DateStart", this.DateStart));
            connection.parameterlist.Add(new SqlParameter("@DateFinal", this.DateFinal));
            connection.parameterlist.Add(new SqlParameter("@DateRegister", this.RegisterDay));

            connection.parameterlist.Add(new SqlParameter("@Type", this.membership.MerbershipID));

            connection.parameterlist.Add(new SqlParameter("@QuestionID", this.question.QuestionID));

            connection.parameterlist.Add(new SqlParameter("@UserAnswer ", this.answer));


            int result = connection.EjecutarInsertUpdateDelete("UserAdd");

            if (result > 0)
            {
                R = true;
            }
            return R;

        }
        #endregion

        #region Eliminar 
        //Eliminar Usuario
        public bool Delete()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.UserID));
            int r = connection.EjecutarInsertUpdateDelete("UserDelete");

            if (r > 0)
            {
                R = true;
            }

            return R;
        }
        #endregion


        #region Editar
        //Este Editar lo utilizamos en el FrmMe
        public bool UserUpdates()
        {
            bool R = false;
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.UserID));
            connection.parameterlist.Add(new SqlParameter("@UserName", this.UserName));
            connection.parameterlist.Add(new SqlParameter("@weight", this.Weight));
            connection.parameterlist.Add(new SqlParameter("@Height", this.Height));
            connection.parameterlist.Add(new SqlParameter("@Email", this.UserEmail));
            connection.parameterlist.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));

            int result = connection.EjecutarInsertUpdateDelete("UserUpdates");

            if (result > 0)
            {
                R = true;
            }
            return R;
        }

        #endregion


        #region Editar 
        //Este Editar lo utilizamos en el FrmUser 

        public bool Update()
        {
            bool R = false;
            Conection connection = new Conection();
            Crypto crypto = new Crypto();
            string Passwordencrypted = crypto.EncriptarEnUnSentido(this.UserPassword);
            connection.parameterlist.Add(new SqlParameter("@UserPassword", Passwordencrypted));
            connection.parameterlist.Add(new SqlParameter("@ID", this.UserID));
            connection.parameterlist.Add(new SqlParameter("@UserName", this.UserName));
            connection.parameterlist.Add(new SqlParameter("@weight", this.Weight));
            connection.parameterlist.Add(new SqlParameter("@Height", this.Height));
            connection.parameterlist.Add(new SqlParameter("@Email", this.UserEmail));
            connection.parameterlist.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
            connection.parameterlist.Add(new SqlParameter("@UserRoleID", this.MyUserRole.UserRoleID));
            connection.parameterlist.Add(new SqlParameter("@Membership", this.membership.MerbershipID));

            int result = connection.EjecutarInsertUpdateDelete("UserUpdate");

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
            connection.parameterlist.Add(new SqlParameter("@ID", this.UserID));
            int r = connection.EjecutarInsertUpdateDelete("UserActivate");

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
            connection.parameterlist.Add(new SqlParameter("@ID", this.UserID));
            int r = connection.EjecutarInsertUpdateDelete("UserInactivate");

            if (r > 0)
            {
                R = true;
            }

            return R;

        }
        #endregion

        #region Buscar ID del Usuario
        //traemos los Datos del usuario mediante su ID
        public User UserSearchID()
        {
            User R = new User();
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.UserID));
            DataTable dt = new DataTable();
            dt = connection.EjecutarSELECT("UserSearchID");
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                R.UserID = Convert.ToInt32(dr["UserID"]);
                R.PhoneNumber = Convert.ToInt32(dr["PhoneNumber"]);
                R.UserName = Convert.ToString(dr["UserName"]);
                R.UserEmail = Convert.ToString(dr["UserEmail"]);
                R.Weight = Convert.ToInt32(dr["Weight"]);
                R.Height = Convert.ToInt32(dr["Height"]);
                R.UserPassword = string.Empty;

                R.MyUserRole.Description = Convert.ToString(dr["Description"]);
                R.MyUserRole.UserRoleID = Convert.ToInt32(dr["UserRoleID"]);
                R.membership.MerbershipID = Convert.ToInt32(dr["MembershipID"]);
                R.membership.MembershipName = Convert.ToString(dr["MembershipName"]);
                R.question.QuestionID = Convert.ToInt32(dr["QuestionID"]);
                R.question.QuestionName = Convert.ToString(dr["QuestionName"]);
                R.answer = Convert.ToString(dr["Answer"]);
                R.DateStart = Convert.ToDateTime(dr["DateStart"]);
                R.DateFinal = Convert.ToDateTime(dr["DateFinal"]);
                R.RegisterDay = Convert.ToDateTime(dr["RegisterDay"]);
                R.membership.price = Convert.ToDecimal(dr["Price"]);


            }
            return R;

        }
        #endregion


        #region Buscar ID del Usuario
        //chequeamos los Datos de ID pregunta y respuesta    mediante el  ID del usuario 
        public User CheckUser()
        {
            User R = new User();
            Conection connection = new Conection();
            connection.parameterlist.Add(new SqlParameter("@ID", this.UserID));
            DataTable dt = new DataTable();
            dt = connection.EjecutarSELECT("CheckUser");
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                R.question.QuestionID = Convert.ToInt32(dr["QuestionID"]);
                R.answer = Convert.ToString(dr["Answer"]);

            }
            return R;

        }
        #endregion


        #region Consultar Correo electronico del usuario
        public bool ConsultUserEmail()
        {
            bool R = false;
            Conection MiCnn = new Conection();

            MiCnn.parameterlist.Add(new SqlParameter("@Email", this.UserEmail));

            DataTable consulta = new DataTable();

            consulta = MiCnn.EjecutarSELECT("UserConsultEmail");

            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;


        }
        #endregion


        #region
        public bool UpdateUserPassword()
        {
            bool R = false;
            Conection connection = new Conection();
            Crypto crypto = new Crypto();
            string Passwordencrypted = crypto.EncriptarEnUnSentido(this.UserPassword);
            connection.parameterlist.Add(new SqlParameter("@UserPassword", Passwordencrypted));
            connection.parameterlist.Add(new SqlParameter("@ID", this.UserID));
            int result = connection.EjecutarInsertUpdateDelete("UpdateUserPassword");

            if (result > 0)
            {
                R = true;
            }
            return R;

        }


        #endregion


        #region Consultar numero de telefono del usuario
        public bool ConsultPhonenumber()
        {
            bool R = false;
            Conection MiCnn = new Conection();

            //agregamos el parametro de correo 
            MiCnn.parameterlist.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));

            DataTable consulta = new DataTable();
            //paso 1.4.3 y 1.4.4
            consulta = MiCnn.EjecutarSELECT("UserConsultPhoneNumber");

            //paso 1.4.5
            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;


        }
        #endregion


        #region Validar acceso
        public User UserValidate(string pEmail, string pContrasennia)
        {
            User R = new User();

            Conection MiCnn = new Conection();


            Crypto crypto = new Crypto();
            string ContrasenniaEncriptada = crypto.EncriptarEnUnSentido(pContrasennia);

            MiCnn.parameterlist.Add(new SqlParameter("@usuario", pEmail));
            MiCnn.parameterlist.Add(new SqlParameter("@password", ContrasenniaEncriptada));


            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("UserValidate");

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                R.UserID = Convert.ToInt32(dr["UserID"]);
                R.UserName = Convert.ToString(dr["UserName"]);
                R.UserEmail = Convert.ToString(dr["UserEmail"]);
                R.Height = Convert.ToDecimal(dr["Height"]);
                R.Weight = Convert.ToDecimal(dr["Weight"]);
                R.UserPassword = string.Empty;
                R.MyUserRole.UserRoleID = Convert.ToInt32(dr["UserRoleID"]);
                R.MyUserRole.Description = Convert.ToString(dr["Description"]);

                //Revisamos el estado del Usuario
                // Si el resultado es negativo , el usuario es inactivo y no podra entrar al sistema
                R.Active = Convert.ToBoolean(dr["Active"]);
                if (!R.Active)
                {
                    R.UserID = -1;
                }


            }

            return R;

        }
        #endregion





    }
}
