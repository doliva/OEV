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
    public class RolFamiliaDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Int32 idRol, Int32 idFamilia, Boolean estado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO ROL_FAMILIA (ID_ROL, ID_FAMILIA, ESTADO) ");
            queryBuilder.Append("VALUES (@idRol, @idFamilia, @estado)");
            //queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
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
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " rol_familia", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Int32 idRol, Int32 idFamilia, Boolean estado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE ROL_FAMILIA SET ESTADO=@estado ");
            queryBuilder.Append("WHERE ID_ROL=@idRol AND ID_FAMILIA=@idFamilia,");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
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
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " rol_familia", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Dictionary<String, String> GetRolFamiliaById(Int32 idRol, Int32 idFamilia)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from ROL_FAMILIA ");
            queryBuilder.Append("where ID_ROL=@idRol and ID_FAMILIA=@idFamilia");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;
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
                    usrFam.Add("idRol", reader["id_rol"].ToString());
                    usrFam.Add("idFamilia", reader["id_familia"].ToString());
                    usrFam.Add("estado", reader["estado"].ToString());
                }
                return usrFam;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " rol_familia por ids", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
