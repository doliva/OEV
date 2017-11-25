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
    public class FamiliaPatenteDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Int32 idFamilia, Int32 idPatente, Boolean estado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO FAMILIA_PATENTE (ID_FAMILIA, ID_PATENTE, ESTADO) ");
            queryBuilder.Append("VALUES (@idFamilia, @idPatente, @estado)");
            //queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
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
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " familia_patente", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Dictionary<String, String> GetFamiliaPatenteByIds(Int32 idFamilia, Int32 idPatente)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from FAMILIA_PATENTE ");
            queryBuilder.Append("where ID_FAMILIA=@idFamilia and ID_PATENTE=@idPatente");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
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
                    usrFam.Add("idFamilia", reader["id_familia"].ToString());
                    usrFam.Add("idPatente", reader["id_patente"].ToString());
                    usrFam.Add("estado", reader["estado"].ToString());
                }
                return usrFam;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " familia_patente por ids", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Int32> GetPatenteById(Int32 idFamilia)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select ID_PATENTE from FAMILIA_PATENTE ");
            queryBuilder.Append("where ID_FAMILIA=@idFamilia and estado=1");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                List<Int32> idsPatentes = new List<int>();
                while (reader.Read())
                {
                    idsPatentes.Add(Int32.Parse(reader["id_patente"].ToString()));
                }
                return idsPatentes;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " familia_patente por idFamilia", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Int32 idFamilia, Int32 idPatente, Boolean estado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE FAMILIA_PATENTE SET ESTADO=@estado ");
            queryBuilder.Append("WHERE ID_FAMILIA=@idFamilia AND ID_PATENTE=@idPatente,");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;
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
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " familia_patente", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Familia GetFamiliaByIdPat(Int32 idPatente)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select fam.* from [BD_OEV_S].[dbo].[FAMILIA_PATENTE] fp, [BD_OEV_S].[dbo].[FAMILIA] fam ");
            queryBuilder.Append("where fp.ID_PATENTE=@idPatente and fp.ID_FAMILIA=fam.ID_FAMILIA");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idPatente", SqlDbType.Int).Value = idPatente;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                Familia familia = new Familia();
                if (reader.Read())
                {
                    Patente patente = new Patente();
                    familia.id = Int32.Parse(reader["id_familia"].ToString());
                    familia.descripcion = reader["descripcion"].ToString();
                    familia.estado = Boolean.Parse(reader["estado"].ToString());
                  
                }
                return familia;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " familia_patente por idPatente", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        internal static List<Patente> GetPatentesByIdFam(Int32 idFamilia)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select pat.* from [BD_OEV_S].[dbo].[FAMILIA_PATENTE] fp, [BD_OEV_S].[dbo].[PATENTE] pat ");
            queryBuilder.Append("where fp.ID_FAMILIA=@idFamilia and pat.ID_PATENTE=fp.ID_PATENTE ");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = idFamilia;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                List<Patente> listaPatente = new List<Patente>();
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
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " familia_patente por idFamilia", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
