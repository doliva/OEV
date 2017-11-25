using Base;
using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DAL
{
    public class FamiliaPatenteDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Patente GetPatenteByFamilia(string descripcion)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select p.* from [BD_OEV_C].[dbo].[Familia_Patente] fp, [BD_OEV_C].[dbo].[Familia] f, [BD_OEV_C].[dbo].[Patente] p ");
            queryBuilder.Append("where f.descripcion=@descripcion and f.id_familia = fp.id_familia and p.id_patente = fp.id_patente");
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
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " patente por descripcion de familia", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Patente> GetPatentesByFamilia(string descripcion)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select p.* from [BD_OEV_S].[dbo].[Familia_Patente] fp, [BD_OEV_S].[dbo].[Familia] f, [BD_OEV_S].[dbo].[Patente] p ");
            queryBuilder.Append("where f.descripcion=@descripcion and f.id_familia = fp.id_familia and p.id_patente = fp.id_patente");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = descripcion;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Patente> listaPatentes = new List<Patente>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Patente pat = new Patente(Int32.Parse(reader["id_patente"].ToString()));
                    pat.descripcion = reader["descripcion"].ToString();
                    pat.estado = Boolean.Parse(reader["estado"].ToString());
                    listaPatentes.Add(pat);

                }
                return listaPatentes;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " patentes por descripcion de familia", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Familia GetFamiliaByPatente(string descripcion)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select f.* from [BD_OEV_C].[dbo].[Familia_Patente] fp, [BD_OEV_C].[dbo].[Familia] f, [BD_OEV_C].[dbo].[Patente] p ");
            queryBuilder.Append("where p.descripcion=@descripcion and f.id_familia = fp.id_familia and p.id_patente = fp.id_patente");
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
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " familia por descripcion de patente", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
