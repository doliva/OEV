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
    public class FamiliaDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Familia familia)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO FAMILIA (DESCRIPCION, ESTADO) ");
            queryBuilder.Append("VALUES (@descripcion, @estado)");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 100).Value = familia.descripcion;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = familia.estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " familia", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Familia familia)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE FAMILIA SET ESTADO=@estado, DESCRIPCION=@descripcion ");
            queryBuilder.Append("WHERE ID_FAMILIA=@idFamilia");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 100).Value = familia.descripcion;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = familia.estado;
            cmd.Parameters.Add("@idFamilia", SqlDbType.Int).Value = familia.id;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " familia", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Familia GetFamiliaByDescripcion(string descripcion)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from familia ");
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
                    Familia fam = new Familia(Int32.Parse(reader["id_familia"].ToString()));
                    fam.descripcion = reader["descripcion"].ToString();
                    fam.estado = Boolean.Parse(reader["estado"].ToString());

                    return fam;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " familia descripcion", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Familia> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from familia ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Familia> listaFamilia = new List<Familia>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Familia fam = new Familia(Int32.Parse(reader["id_familia"].ToString()));
                    fam.descripcion = reader["descripcion"].ToString();
                    fam.estado = Boolean.Parse(reader["estado"].ToString());
                    listaFamilia.Add(fam);

                }
                return listaFamilia;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " familias", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
