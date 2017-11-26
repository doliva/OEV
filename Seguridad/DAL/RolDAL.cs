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
    public class RolDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Rol rol)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO ROL (DESCRIPCION, ESTADO) ");
            queryBuilder.Append("VALUES (@descripcion,  @estado)");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 30).Value = rol.descripcion;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = rol.estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " rol", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Rol rol)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE ROL SET ESTADO=@estado, DESCRIPCION = @descripcion");
            queryBuilder.Append("WHERE ID_ROL=@idRol");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 30).Value = rol.descripcion;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = rol.estado;
            cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = rol.id;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " rol", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Rol GetRolByDesc(string descripcion)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from rol ");
            queryBuilder.Append("where descripcion=@descripcion and estado=1");
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
                    Rol rol = new Rol(Int32.Parse(reader["id_rol"].ToString()));
                    rol.descripcion = reader["descripcion"].ToString();
                    rol.estado = Boolean.Parse(reader["estado"].ToString());
                    return rol;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " rol por descripcion", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Rol GetRolById(Int32 idRol)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from rol ");
            queryBuilder.Append("where id_rol=@idRol");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    Rol rol = new Rol(Int32.Parse(reader["id_rol"].ToString()));
                    rol.descripcion = reader["descripcion"].ToString();
                    rol.estado = Boolean.Parse(reader["estado"].ToString());
                    return rol;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " rol por id", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Rol> GetRoles()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from rol ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Rol> listaRoles = new List<Rol>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Rol rol = new Rol(Int32.Parse(reader["id_rol"].ToString()));
                    rol.descripcion = reader["descripcion"].ToString();
                    rol.estado = Boolean.Parse(reader["estado"].ToString());
                    listaRoles.Add(rol);
                }
                return listaRoles;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " roles ", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
