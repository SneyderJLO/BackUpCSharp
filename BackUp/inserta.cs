using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace BackUp
{
    internal class inserta
    {
        conexion conexionBD = new conexion();

        public void Insertar(object sender, ElapsedEventArgs e)
        {

            using (conexionBD.ObtenerConexion())
            {
                try
                {
                    conexionBD.AbrirConexion();
                    string query = "INSERT INTO Pais (descripcion) VALUES (@Value2)";

                    using (SqlCommand command = new SqlCommand(query, conexionBD.ObtenerConexion()))
                    {

                        string[] descript = { "Canadá", "EE.UU", "Costa Rica", "Panamá", "Argentina", "México", "Cuba", "Colombia", "Ecuador", "Brasil" };

                        Random random = new Random();
                        int randomIndex = random.Next(0, descript.Length);

                        string randomString = descript[randomIndex];
                        command.Parameters.AddWithValue("@Value2", randomString);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Inserción en la base de datos realizada.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    conexionBD.CerrarConexion();
                }
            }
        }
    }
}
