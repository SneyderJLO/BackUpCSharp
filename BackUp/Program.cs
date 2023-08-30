//using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Timers;
using System.IO;

namespace BackUp
{
    internal class Program
    {
        private static Respaldos respaldos;
        private static DateTime lastBackupTimeFull;
        private static DateTime lastBackupTimeIncremental;
        private static DateTime lastBackupTimeDiferencial;
        private static bool fullBackupDone;
        public static Dictionary<string, int> timers;

        static void Main(string[] args)
        {
            timerJson();
            conexion con = new conexion();
            con.AbrirConexion();

            respaldos = new Respaldos();
            lastBackupTimeFull = DateTime.Now;
            lastBackupTimeIncremental = DateTime.Now;
            lastBackupTimeDiferencial = DateTime.Now;
            fullBackupDone = false;

            Timer timer = new Timer();
            timer.Interval = timers["timer1"];

            timer.Elapsed += Insertar;
            timer.AutoReset = true;
            timer.Start();
            // Console.WriteLine($"time: {timers["timer1"]}");
            Timer timer2 = new Timer();
            timer2.Interval = timers["timer2"]; //cada 2 min
            timer2.Elapsed += RespaldoFull;
            timer2.AutoReset = true;
            timer2.Start();
            //  Console.WriteLine($"time: {timers["timer2"]}");
            Timer timer3 = new Timer();
            timer3.Interval = timers["timer3"]; // cada 20seg
            timer3.Elapsed += RespaldoIncremental;
            timer3.AutoReset = true;
            timer3.Start();
            //Console.WriteLine($"time: {timers["timer3"]}");
            Timer timer4 = new Timer();
            timer4.Interval = timers["timer4"]; //  cada min
            timer4.Elapsed += RespaldoDiferencial;
            timer4.AutoReset = true;
            timer4.Start();
            // Console.WriteLine($"time: {timers["timer4"]}");
            Console.WriteLine("Presiona Enter para detener el temporizador.");
            Console.ReadLine();
        }



        public static void timerJson()
        {
            string jsonFilePath = (@"C:\CURSO_C#\PROYECTO\json\timerParametros.json");
            string jsonString = File.ReadAllText(jsonFilePath);
            timers = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonString);
            //Console.WriteLine($"time: {timers["timer1"]}");
            //Console.WriteLine($"time: {timers["timer2"]}");
            //Console.WriteLine($"time: {timers["timer3"]}");
        }



        private static void Insertar(object sender, ElapsedEventArgs e)
        {
            // Insertar datos cada segundo
            inserta insertar = new inserta();
            insertar.Insertar(sender, e);
            //Console.WriteLine("Insertando datos...");
        }




        private static void RespaldoFull(object sender, ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            if (ShouldPerformBackupFull(currentTime))
            {
                respaldos.CrearRespaldoFull("prueba_Back");
                //baseJason();
                lastBackupTimeFull = currentTime;
                fullBackupDone = true; // Marcar que se ha realizado un respaldo completo
            }
        }

        private static bool ShouldPerformBackupFull(DateTime currentTime)
        {
            TimeSpan timeSinceLastBackup = currentTime - lastBackupTimeFull;
            return timeSinceLastBackup.TotalMinutes >= 2;
        }



        private static void RespaldoIncremental(object sender, ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            if (fullBackupDone && ShouldPerformBackupIncremental(currentTime))
            {
                respaldos.CrearRespaldoIncremental("prueba_Back");
                //baseJason();
                lastBackupTimeIncremental = currentTime;
            }
        }

        private static bool ShouldPerformBackupIncremental(DateTime currentTime)
        {
            TimeSpan timeSinceLastBackup = currentTime - lastBackupTimeIncremental;
            return timeSinceLastBackup.TotalSeconds >= 20;
        }




        private static void RespaldoDiferencial(object sender, ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            if (fullBackupDone && ShouldPerformBackupDiferencial(currentTime))
            {
                respaldos.CrearRespaldoDiferencial("prueba_Back");
                //baseJason();
                lastBackupTimeDiferencial = currentTime;
            }
        }

        private static bool ShouldPerformBackupDiferencial(DateTime currentTime)
        {
            TimeSpan timeSinceLastBackup = currentTime - lastBackupTimeDiferencial;
            return timeSinceLastBackup.TotalMinutes >= 1;
        }



        /* public static void baseJason()
         {
             string connectionString = "Data Source=DESKTOP-I3;Initial Catalog=prueba_Back;Integrated Security=true";
             string query = "SELECT * FROM pais;";
             try
             {
                 string outputPath = @"C:\CURSO_C#\PROYECTO\BackUp\respaldoBase.json";

                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {
                     connection.Open();

                     using (SqlCommand command = new SqlCommand(query, connection))
                     {
                         using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                         {
                             DataTable dataTable = new DataTable();
                             adapter.Fill(dataTable);
                             List<pais> transacciones = new List<pais>();
                             foreach (DataRow row in dataTable.Rows)
                             {
                                 pais transaccion = new pais
                                 {
                                     cod = Convert.ToInt32(row["cod"]),
                                     descripcion = (row["descripcion"].ToString())
                                 };
                                 transacciones.Add(transaccion);
                             }

                             string jsonResult = JsonConvert.SerializeObject(transacciones, Newtonsoft.Json.Formatting.Indented);

                             File.WriteAllText(outputPath, jsonResult);

                             Console.WriteLine("Archivo JSON guardado exitosamente.");
                         }
                     }
                 }
                 Console.ReadLine();
             }
             catch (Exception ex)
             {

                 Console.WriteLine(ex); Console.ReadLine();
             }

         }
        */
    }
}
