using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Utils.Excepciones;

namespace Utils.Bitacora
{
    public class BitacoraDAL
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;
        
        public static int Insert(int id, string rol, DateTime fecha, string evento, string detalle)
        {
            //string connectionString = "Data Source=NATI-PC\\SQLEXPRESS;Initial Catalog=BD_OEV;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO BITACORA (ID_USUARIO, ROL, FECHA, EVENTO, DETALLE) ");
            queryBuilder.Append("VALUES (@idUsuario, @rol, @fecha, @evento, @detalle)");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@rol", SqlDbType.VarChar, 15).Value = rol;
            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha;
            cmd.Parameters.Add("@evento", SqlDbType.VarChar, 30).Value = evento;
            cmd.Parameters.Add("@detalle", SqlDbType.VarChar, 50).Value = detalle;
            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " bitacora", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Suprime un registro de la tabla Bitacora por una clave primaria(primary key).
        /// </summary>
        public static void Delete(int codBitacora)
        {
        }

        /// <summary>
        /// Selecciona lista de registros desde la tabla Bitacora por rol.
        /// </summary>
        /// <returns>List<Bitacora></returns>
        public static List<Bitacora> SelectBitacorasByRol(string rol)
        {
            //string connectionString = "Data Source=NATI-PC\\SQLEXPRESS;Initial Catalog=BD_OEV;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("SELECT * FROM BITACORA ");
            queryBuilder.Append("WHERE rol=@rol");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@rol", SqlDbType.VarChar,15).Value = rol;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Bitacora> listaBitacoras = new List<Bitacora>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listaBitacoras.Add(obtenerBitacoraDesdeReader(reader));
                }
                return listaBitacoras;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " bitacora", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Selecciona lista de registros desde la tabla Bitacora por evento.
        /// </summary>
        /// <returns>List<Bitacora></returns>
        public static List<Bitacora> SelectBitacorasByEvento(string evento)
        {
            //string connectionString = "Data Source=NATI-PC\\SQLEXPRESS;Initial Catalog=BD_OEV;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("SELECT * FROM BITACORA ");
            queryBuilder.Append("WHERE evento=@evento");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@evento", SqlDbType.VarChar, 30).Value = evento;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Bitacora> listaBitacoras = new List<Bitacora>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listaBitacoras.Add(obtenerBitacoraDesdeReader(reader));
                }
                return listaBitacoras;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " bitacora", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Selecciona lista de registros desde la tabla Bitacora por rango de fechas.
        /// </summary>
        /// <returns>List<Bitacora></returns>
        public static List<Bitacora> SelectBitacorasByFechas(DateTime desde, DateTime hasta)
        {
            //string connectionString = "Data Source=NATI-PC\\SQLEXPRESS;Initial Catalog=BD_OEV;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("SELECT * FROM BITACORA ");
            queryBuilder.Append("WHERE fecha>=@desde and fecha<@hasta");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = hasta;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Bitacora> listaBitacoras = new List<Bitacora>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listaBitacoras.Add(obtenerBitacoraDesdeReader(reader));
                }
                return listaBitacoras;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " bitacora", ex);
            }
            finally
            {
                conn.Close();
            }
        }


        private static Bitacora obtenerBitacoraDesdeReader(SqlDataReader reader){
            Bitacora bitacora = new Bitacora();
            bitacora.idBitacora = Int16.Parse(reader["id_bitacora"].ToString());
            bitacora.rol = reader["rol"].ToString();
            bitacora.evento = reader["evento"].ToString();
            bitacora.detalle = reader["detalle"].ToString();
            bitacora.fecha = DateTime.Parse(reader["fecha"].ToString());

            return bitacora;
        }

        /// <summary>
        /// Selecciona todos los registros de la tabla Bitacora.
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet SelectAll()
        {
            return new DataSet();
        }
    }
}
