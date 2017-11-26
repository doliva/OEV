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
    public class UsuarioFamiliaDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Int32 idUsuario, Int32 idFamilia, Boolean estado)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO USUARIO_FAMILIA (ID_USUARIO, ID_FAMILIA, ESTADO) ");
            queryBuilder.Append("VALUES (@idUsuario, @idFamilia, @estado)");
            //queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " usuario_familia", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Dictionary<String, String> GetUsuarioFamiliaById(Int32 idUsuario, Int32 idFamilia)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from USUARIO_FAMILIA ");
            queryBuilder.Append("where ID_USUARIO=@idUsuario and ID_FAMILIA=@idFamilia");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;

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
                    usrFam.Add("idFamilia", reader["id_familia"].ToString());
                    usrFam.Add("estado", reader["estado"].ToString());
                }
                return usrFam;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " usuario_familia por ids", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Int32 idUsuario, Int32 idFamilia, Boolean estado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE USUARIO_FAMILIA SET ESTADO=@estado ");
            queryBuilder.Append("WHERE ID_USUARIO=@idUsuario AND ID_FAMILIA=@idFamilia,");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " usuario_familia", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
