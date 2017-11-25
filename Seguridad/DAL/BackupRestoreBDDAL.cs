using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Base.DAL
{
    public class BackupRestoreBDDAL
    {
         static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        /// <summary>
        /// Genera el backup de la base de datos en la ruta indicada por parámetro.
        /// </summary>
        /// <param name="rutaArchivo"></param>
        public static void backupBD(String rutaArchivo)
        {
            //string connectionString = "Data Source=NATI-PC\\SQLEXPRESS;Initial Catalog=BD_OEV;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            //String query = "BACKUP DATABASE [BD_OEV] TO  DISK = N'" + rutaArchivo + "' WITH NOFORMAT, NOINIT, NAME = N'BD_OEV', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            String query = "BACKUP DATABASE [" + Constantes.NOMBRE_BD + "] TO DISK='" + rutaArchivo + "'";
            //string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + textBox1.Text + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_BACKUP, ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene la restauración de la base de datos a partir de la ruta indicada por parámetro.
        /// </summary>
        /// <param name="rutaArchivo"></param>
        public static void restoreBD(String rutaArchivo)
        {
            try
            {
                //string connectionString = "Data Source=NATI-PC\\SQLEXPRESS;Initial Catalog=BD_OEV;Integrated Security=True";
                SqlConnection connMaster = new SqlConnection(connectionString);
                connMaster.Open();

                //String query = "ALTER DATABASE [BD_OEV] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                //query += "RESTORE DATABASE [BD_OEV] FROM  DISK = N'" + rutaArchivo + "' WITH REPLACE;";

                string sqlStmt2 = string.Format("ALTER DATABASE [" + Constantes.NOMBRE_BD + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, connMaster);
                bu2.ExecuteNonQuery();

                string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + Constantes.NOMBRE_BD + "] FROM DISK='" + rutaArchivo + "'WITH REPLACE;";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, connMaster);
                bu3.ExecuteNonQuery();

                string sqlStmt4 = string.Format("ALTER DATABASE [" + Constantes.NOMBRE_BD + "] SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, connMaster);
                bu4.ExecuteNonQuery();


                //SqlCommand com = new SqlCommand(query, connMaster);
                //com.CommandType = CommandType.Text;
                //com.ExecuteNonQuery();
                connMaster.Close();
                connMaster.Dispose();

            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_RESTORE, ex);
            }
        }
    }
}
