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
    public class RolUsuarioDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Int32 idRol, Int32 idUsuario, Boolean estado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO ROL_USUARIO (ID_ROL, ID_USUARIO, ESTADO) ");
            queryBuilder.Append("VALUES (@idRol, @idUsuario, @estado)");
            //queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " rol_usuario", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Int32 idRol, Int32 idUsuario, Boolean estado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE ROL_USUARIO SET ESTADO=@estado ");
            queryBuilder.Append("WHERE ID_ROL=@idRol AND ID_USUARIO=@idUsuario,");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " rol_usuario", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Dictionary<String, String> GetRolUsuarioByIds(Int32 idRol, Int32 idUsuario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from ROL_USUARIO ");
            queryBuilder.Append("where ID_ROL=@idRol and ID_USUARIO=@idUsuario");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                Dictionary<String, String> rolUsr = new Dictionary<string, string>();
                if (reader.Read())
                {
                    rolUsr.Add("idRol", reader["id_rol"].ToString());
                    rolUsr.Add("idUsuario", reader["id_usuario"].ToString());
                    rolUsr.Add("estado", reader["estado"].ToString());
                }
                return rolUsr;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " rol_usuario por ids", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Dictionary<String, String> GetRolByIdUsuario(Int32 idUsuario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from ROL_USUARIO ");
            queryBuilder.Append("where ID_USUARIO=@idUsuario");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                Dictionary<String, String> usrFam = new Dictionary<string, string>();
                if (reader.Read())
                {
                    usrFam.Add("idRol", reader["id_rol"].ToString());
                    usrFam.Add("idUsuario", reader["id_usuario"].ToString());
                    usrFam.Add("estado", reader["estado"].ToString());
                }
                return usrFam;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " rol_usuario por idUsuario", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
