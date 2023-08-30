using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackUp
{
    internal class Respaldos
    {
        conexion conexionBD = new conexion();
        public bool CrearRespaldoFull(string p_database)
        {
            bool inesem_ok = true;

            string backupPath = @"C:\CURSO_C#\PROYECTO\BackUp\" + p_database + "Full" + ".bak";

            string sBackup = "BACKUP DATABASE @databaseName TO DISK = @backupPath ";
            //"WITH FORMAT, MEDIA NAME = 'RespaldoPais', [NAME] = 'respaldo';";


            using (conexionBD.ObtenerConexion())
            {
                conexionBD.AbrirConexion();
                using (SqlCommand command = new SqlCommand(sBackup, conexionBD.ObtenerConexion()))
                {
                    command.Parameters.AddWithValue("@databaseName", p_database);
                    command.Parameters.AddWithValue("@backupPath", backupPath);

                    try
                    {
                        conexionBD.AbrirConexion();
                        command.ExecuteNonQuery();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Respaldo Full OK");
                        Console.ResetColor();
                        conexionBD.CerrarConexion();
                    }
                    catch (Exception ex)
                    {
                        inesem_ok = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error al realizar el respaldo FULL " + ex);
                        Console.ResetColor();
                        conexionBD.CerrarConexion();
                    }
                }
            }

            return inesem_ok;
        }
        public bool CrearRespaldoIncremental(string p_database)
        {
            bool inesem_ok = true;

            string backupPath = @"C:\CURSO_C#\PROYECTO\BackUp\" + p_database + "Incremental" + ".bak";

            string sBackup = "BACKUP DATABASE @databaseName TO DISK = @backupPath ";
            //"WITH FORMAT, MEDIA NAME = 'RespaldoPais', [NAME] = 'respaldo';";


            using (conexionBD.ObtenerConexion())
            {
                conexionBD.AbrirConexion();
                using (SqlCommand command = new SqlCommand(sBackup, conexionBD.ObtenerConexion()))
                {
                    command.Parameters.AddWithValue("@databaseName", p_database);
                    command.Parameters.AddWithValue("@backupPath", backupPath);

                    try
                    {
                        conexionBD.AbrirConexion();
                        command.ExecuteNonQuery();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Respaldo Incremental OK");
                        Console.ResetColor();
                        conexionBD.CerrarConexion();
                    }
                    catch (Exception ex)
                    {
                        inesem_ok = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error al realizar el respaldo INCREMENTAL " + ex);
                        Console.ResetColor();
                        conexionBD.CerrarConexion();
                    }
                }
            }

            return inesem_ok;
        }
        public bool CrearRespaldoDiferencial(string p_database)
        {
            bool inesem_ok = true;

            string backupPath = @"C:\CURSO_C#\PROYECTO\BackUp\" + p_database + "Diferencial" + ".bak";

            string sBackup = "BACKUP DATABASE @databaseName TO DISK = @backupPath " +
                "WITH DIFFERENTIAL;";
            //"WITH FORMAT, MEDIA NAME = 'RespaldoPais', [NAME] = 'respaldo';";


            using (conexionBD.ObtenerConexion())
            {
                conexionBD.AbrirConexion();
                using (SqlCommand command = new SqlCommand(sBackup, conexionBD.ObtenerConexion()))
                {
                    command.Parameters.AddWithValue("@databaseName", p_database);
                    command.Parameters.AddWithValue("@backupPath", backupPath);

                    try
                    {
                        conexionBD.AbrirConexion();
                        command.ExecuteNonQuery();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Respaldo Diferencial OK");
                        Console.ResetColor();
                        conexionBD.CerrarConexion();
                    }
                    catch (Exception ex)
                    {
                        inesem_ok = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error al realizar el respaldo DIFERENCIAL " + ex);
                        Console.ResetColor();
                        conexionBD.CerrarConexion();
                    }
                }
            }

            return inesem_ok;
        }
    }
}
