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
    public class UsuarioPatenteDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Int32 idUsuario, Int32 idPatente, Boolean estado)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO USUARIO_PATENTE (ID_USUARIO, ID_PATENTE, ESTADO) ");
            queryBuilder.Append("VALUES (@idUsuario, @idPatente, @estado)");
            //queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@idPatente", SqlDbType.Int).Value = idPatente;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " usuario_patente", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Dictionary<String, String> GetUsuarioPatenteById(Int32 idUsuario, Int32 idPatente)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from USUARIO_PATENTE ");
            queryBuilder.Append("where ID_USUARIO=@idUsuario and ID_PATENTE=@idPatente");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@idPatente", SqlDbType.Int).Value = idPatente;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                Dictionary<String, String> usrFam = new Dictionary<string, string>();
                if (reader.Read())
                {
                    usrFam.Add("idUsuario", reader["id_usuario"].ToString());
                    usrFam.Add("idPatente", reader["id_patente"].ToString());
                    usrFam.Add("estado", reader["estado"].ToString());
                }
                return usrFam;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " usuario_patente por ids", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Int32 idUsuario, Int32 idPatente, Boolean estado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE USUARIO_PATENTE SET ESTADO=@estado ");
            queryBuilder.Append("WHERE ID_USUARIO=@idUsuario AND ID_FAMILIA=@idPatente,");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@idPatente", SqlDbType.Int).Value = idPatente;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " usuario_patente", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Patente> GetPatentesByIdUsuario(Int32 idUsuario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select pat.* from [BD_OEV_S].[dbo].[USUARIO_PATENTE] up, [BD_OEV_S].[dbo].[PATENTE] pat ");
            queryBuilder.Append("where up.ID_USUARIO=@idUsuario and up.ID_PATENTE=pat.ID_PATENTE");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                List<Patente> listaPatentes = new List<Patente>();
                while (reader.Read())
                {
                    Patente patente = new Patente();
                    patente.id = Int32.Parse(reader["id_patente"].ToString());
                    patente.descripcion = reader["descripcion"].ToString();
                    patente.estado = Boolean.Parse(reader["estado"].ToString());
                    listaPatentes.Add(patente);
                }
                return listaPatentes;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " usuario_patente por idUsuario", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Usuario> GetUsuariosByIdPat(Int32 idPatente)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select usr.* from [BD_OEV_S].[dbo].[USUARIO_PATENTE] up, [BD_OEV_S].[dbo].[USUARIO] usr ");
            queryBuilder.Append("where up.ID_PATENTE=@idPatente and usr.ID_USUARIO=up.ID_USUARIO ");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idPatente", SqlDbType.Int).Value = idPatente;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                List<Usuario> listaUsuarios = new List<Usuario>();
                while (reader.Read())
                {
                    Usuario usr = new Usuario(Int32.Parse(reader["id_Usuario"].ToString()));
                    usr.apellido = reader["apellido"].ToString();
                    usr.nombre = reader["nombre"].ToString();
                    usr.ciudad = reader["ciudad"].ToString();
                    usr.clave = reader["clave"].ToString();
                    usr.fecNac = (DateTime)reader["fecha_nacimiento"];
                    usr.digitoVerificador = reader["dvh"].ToString();
                    usr.dni = reader["dni"].ToString();
                    usr.domicilio = reader["direccion"].ToString();
                    usr.email = reader["email"].ToString();
                    usr.estado = Boolean.Parse(reader["estado"].ToString());
                    usr.telefono = reader["telefono"].ToString();
                    listaUsuarios.Add(usr);
                }
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " usuario_patente por idPatente", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
