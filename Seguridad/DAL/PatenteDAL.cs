using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Base.DAL
{
    public class PatenteDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Patente patente)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO PATENTE (DESCRIPCION, ESTADO) ");
            queryBuilder.Append("VALUES (@descripcion, @estado)");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 100).Value = patente.descripcion;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = patente.estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " patente", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Patente patente)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE PATENTE SET ESTADO=@estado, DESCRIPCION=@descripcion ");
            queryBuilder.Append("WHERE ID_PATENTE=@idPatente");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 100).Value = patente.descripcion;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = patente.estado;
            cmd.Parameters.Add("@idPatente", SqlDbType.Int).Value = patente.id;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " patente", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Patente GetPatenteByDescripcion(string descripcion)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from patente ");
            queryBuilder.Append("where descripcion=@descripcion");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = descripcion;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Patente pat = new Patente(Int32.Parse(reader["id_patente"].ToString()));
                    pat.descripcion = reader["descripcion"].ToString();
                    pat.estado = Boolean.Parse(reader["estado"].ToString());

                    return pat;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " patente descripcion", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Patente> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from patente ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Patente> listaPatente = new List<Patente>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Patente pat = new Patente(Int32.Parse(reader["id_patente"].ToString()));
                    pat.descripcion = reader["descripcion"].ToString();
                    pat.estado = Boolean.Parse(reader["estado"].ToString());
                    listaPatente.Add(pat);

                }
                return listaPatente;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " patentes", ex);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
