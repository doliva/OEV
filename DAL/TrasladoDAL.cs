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
    public class TrasladoDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        public static Int32 Insert(Traslado traslado)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO TRASLADO (RAZON_SOCIAL, CUIT, DIRECCION, CIUDAD, TIPO, TARIFA, EMAIL, CAPACIDAD, TELEFONO) ");
            queryBuilder.Append("VALUES (@razonSocial, @cuit, @direccion, @ciudad, @tipo, @tarifa, @email, @capacidad, @telefono)");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@razonSocial", SqlDbType.VarChar, 50).Value = traslado.razonSocial;
            cmd.Parameters.Add("@cuit", SqlDbType.VarChar, 15).Value = traslado.cuit;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 100).Value = traslado.direccion;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = traslado.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = traslado.telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = traslado.email;
            cmd.Parameters.Add("@capacidad", SqlDbType.Int).Value = traslado.capacidad;
            cmd.Parameters.Add("@tipo", SqlDbType.VarChar, 20).Value = traslado.vehiculo;
            cmd.Parameters.Add("@tarifa", SqlDbType.Money).Value = traslado.tarifa;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " traslado ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Traslado traslado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE TRASLADO SET RAZON_SOCIAL=@razonSocial, CUIT=@cuit, DIRECCION=@direccion, CIUDAD=@ciudad, TIPO=@tipo, TARIFA=@tarifa, EMAIL=@email, CAPACIDAD=@capacidad, TELEFONO=@telefono ");
            queryBuilder.Append("WHERE ID_TRASLADO = @idTraslado");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@razonSocial", SqlDbType.VarChar, 50).Value = traslado.razonSocial;
            cmd.Parameters.Add("@cuit", SqlDbType.VarChar, 15).Value = traslado.cuit;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 100).Value = traslado.direccion;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = traslado.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = traslado.telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = traslado.email;
            cmd.Parameters.Add("@capacidad", SqlDbType.Int).Value = traslado.capacidad;
            cmd.Parameters.Add("@tipo", SqlDbType.VarChar, 20).Value = traslado.vehiculo;
            cmd.Parameters.Add("@tarifa", SqlDbType.Money).Value = traslado.tarifa;
            cmd.Parameters.Add("@idTraslado", SqlDbType.Int).Value = traslado.id;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " traslado ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Traslado GetTrasladoByCuit(string cuit)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from traslado ");
            queryBuilder.Append("where cuit=@cuit");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@cuit", SqlDbType.VarChar, 15).Value = cuit;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Traslado tras = new Traslado();
                    tras.id = Int32.Parse(reader["id_traslado"].ToString());
                    tras.razonSocial = reader["razon_social"].ToString();
                    tras.cuit = reader["cuit"].ToString();
                    tras.ciudad = reader["ciudad"].ToString();
                    tras.vehiculo = reader["tipo"].ToString();
                    tras.tarifa = Double.Parse(reader["tarifa"].ToString());
                    tras.direccion = reader["direccion"].ToString();
                    tras.capacidad = Int32.Parse(reader["capacidad"].ToString());
                    tras.email = reader["email"].ToString();
                    tras.telefono = reader["telefono"].ToString();

                    return tras;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " traslado cuit ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Traslado GetTrasladoByRazon(String razonSocial)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from traslado ");
            queryBuilder.Append("where razon_social like '%' + @razonSocial + '%' ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@razonSocial", SqlDbType.VarChar, 50).Value = razonSocial;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Traslado tras = new Traslado();
                    tras.id = Int32.Parse(reader["id_traslado"].ToString());
                    tras.razonSocial = reader["razon_social"].ToString();
                    tras.cuit = reader["cuit"].ToString();
                    tras.ciudad = reader["ciudad"].ToString();
                    tras.vehiculo = reader["tipo"].ToString();
                    tras.tarifa =  Double.Parse(reader["tarifa"].ToString());
                    tras.direccion = reader["direccion"].ToString();
                    tras.capacidad = Int32.Parse(reader["capacidad"].ToString());
                    tras.email = reader["email"].ToString();
                    tras.telefono = reader["telefono"].ToString();

                    return tras;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " traslado razon social ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Traslado> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from traslado ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Traslado> listaTraslado = new List<Traslado>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Traslado tras = new Traslado();
                    tras.id = Int32.Parse(reader["id_traslado"].ToString());
                    tras.razonSocial = reader["razon_social"].ToString();
                    tras.cuit = reader["cuit"].ToString();
                    tras.ciudad = reader["ciudad"].ToString();
                    tras.vehiculo = reader["tipo"].ToString();
                    tras.tarifa = Double.Parse(reader["tarifa"].ToString());
                    tras.direccion = reader["direccion"].ToString();
                    tras.capacidad = Int32.Parse(reader["capacidad"].ToString());
                    tras.email = reader["email"].ToString();
                    tras.telefono = reader["telefono"].ToString();
                    listaTraslado.Add(tras);
                }
                return listaTraslado;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " traslados ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
