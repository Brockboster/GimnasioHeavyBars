using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace Logic.Models.Services
{
    public  class Conection
    {
        String CnnString { get; set; }
        public List<SqlParameter> parameterlist = new List<SqlParameter>();
        public int EjecutarInsertUpdateDelete(String NombreSP)
        {
            int Retorno = 0;

            using (SqlConnection MyCnn = new SqlConnection(CnnString))

            {
                SqlCommand MyComando = new SqlCommand(NombreSP, MyCnn);
                MyComando.CommandType = CommandType.StoredProcedure;

                if (parameterlist != null && parameterlist.Count > 0)
                {
                    foreach (SqlParameter item in parameterlist)
                    {
                        MyComando.Parameters.Add(item);
                    }
                }

                MyCnn.Open();

                //Si el comando a ejecutar en un DML (update, Insert o delete) 
                //establecer SET NOCOUNT OFF; en el SP 

                Retorno = MyComando.ExecuteNonQuery();
            }

            return Retorno;
        }

        /*
         Esta función ejecuta un procedimiento almacenado. 
         Recibe por parámetro el nombre del SP y además 
         agrega cualquier parámetro tipo SqlParameter
         que esté en la lista de parámetros "ListadoParametros". 
         Sirve para hacer consultas tipo: SELECT FROM y crear un
         DataTable con dicha info.
        */
        public DataTable EjecutarSELECT(String NombreSP, bool CargarEsquema = false)
        {
            DataTable Retorno = new DataTable();

            using (SqlConnection MyCnn = new SqlConnection(CnnString))
            {
                SqlCommand MyComando = new SqlCommand(NombreSP, MyCnn);
                MyComando.CommandType = CommandType.StoredProcedure;
                if (parameterlist != null && parameterlist.Count > 0)
                {
                    foreach (SqlParameter item in parameterlist)
                    {
                        MyComando.Parameters.Add(item);
                    }
                }
                SqlDataAdapter MyAdaptador = new SqlDataAdapter(MyComando);

                MyAdaptador.Fill(Retorno);
                if (CargarEsquema)
                {

                    MyAdaptador.FillSchema(Retorno, SchemaType.Source);
                }
            }
            return Retorno;
        }

        public Object EjecutarSELECTEscalar(String NombreSP)
        {
            Object Retorno = null;
            using (SqlConnection MyCnn = new SqlConnection(CnnString))

            {
                SqlCommand MyComando = new SqlCommand(NombreSP, MyCnn);
                MyComando.CommandType = CommandType.StoredProcedure;

                if (parameterlist != null && parameterlist.Count > 0)
                {
                    foreach (SqlParameter item in parameterlist)
                    {
                        MyComando.Parameters.Add(item);
                    }
                }
                MyCnn.Open();
                Retorno = MyComando.ExecuteScalar();
            }

            return Retorno;
        }

        //Constructor. Carga la info del app.config
        //en la cadena de conexión al momento de crear 
        //una instancia nueva de la clase.
        public Conection()
        {
            this.CnnString = ConfigurationManager.ConnectionStrings["CNNSTR"].ToString();

        }
    }
}