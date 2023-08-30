using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackUp
{
    internal class conexion
    {
        private SqlConnection conexionBd;
        private string cadenaConexion;
        private SqlCommand comando;

        public conexion()
        {

            //string connectionString = "Data Source=DESKTOP-I3;Initial Catalog=prueba_Back;Integrated Security=true";
            string connectionString = "Data Source=localhost;Initial Catalog=prueba_Back;Integrated Security=true";
            cadenaConexion = connectionString;
        }

        public SqlConnection AbrirConexion()
        {
            try
            {
                conexionBd = new SqlConnection(cadenaConexion);
                conexionBd.Open();
                //Console.WriteLine("Conexion OK ");
                return conexionBd;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
                return conexionBd;
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (conexionBd != null && conexionBd.State != ConnectionState.Closed)
                {
                    conexionBd.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }

        public SqlConnection ObtenerConexion()
        {
            return conexionBd;
        }
    }

}
