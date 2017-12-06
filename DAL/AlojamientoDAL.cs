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
    public class AlojamientoDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        public static Int32 Insert(Alojamiento alojamiento)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO ALOJAMIENTO (RAZON_SOCIAL, CUIT, DIRECCION, CIUDAD, TELEFONO, TARIFA, CATEGORIA, EMAIL, CAPACIDAD, SERVICIOS) ");
            queryBuilder.Append("VALUES (@razonSocial, @cuit, @direccion, @ciudad, @telefono, @tarifa, @categoria, @email, @capacidad, @servicios)");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@razonSocial", SqlDbType.VarChar, 50).Value = alojamiento.razonSocial;
            cmd.Parameters.Add("@cuit", SqlDbType.VarChar, 15).Value = alojamiento.cuit;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 100).Value = alojamiento.direccion;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = alojamiento.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = alojamiento.telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = alojamiento.email;
            cmd.Parameters.Add("@capacidad", SqlDbType.Int).Value = alojamiento.capacidad;
            cmd.Parameters.Add("@categoria", SqlDbType.VarChar, 20).Value = alojamiento.categoria;
            cmd.Parameters.Add("@tarifa", SqlDbType.Money).Value = alojamiento.tarifa;
            cmd.Parameters.Add("@servicios", SqlDbType.VarChar, 2000).Value = alojamiento.servicios;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " alojamiento ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Alojamiento alojamiento)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE ALOJAMIENTO SET RAZON_SOCIAL=@razonSocial, CUIT=@cuit, DIRECCION=@direccion, CIUDAD=@ciudad, TELEFONO=@telefono, TARIFA=@tarifa, CATEGORIA=@categoria, EMAIL=@email, CAPACIDAD=@capacidad, SERVICIOS=@servicios ");
            queryBuilder.Append("WHERE ID_ALOJAMIENTO = @idAlojamiento");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@razonSocial", SqlDbType.VarChar, 50).Value = alojamiento.razonSocial;
            cmd.Parameters.Add("@cuit", SqlDbType.VarChar, 15).Value = alojamiento.cuit;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 100).Value = alojamiento.direccion;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = alojamiento.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = alojamiento.telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = alojamiento.email;
            cmd.Parameters.Add("@capacidad", SqlDbType.Int).Value = alojamiento.capacidad;
            cmd.Parameters.Add("@categoria", SqlDbType.VarChar, 20).Value = alojamiento.categoria;
            cmd.Parameters.Add("@tarifa", SqlDbType.Money).Value = alojamiento.tarifa;
            cmd.Parameters.Add("@servicios", SqlDbType.VarChar, 2000).Value = alojamiento.servicios;
            cmd.Parameters.Add("@idAlojamiento", SqlDbType.Int).Value = alojamiento.id;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " alojamiento ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Alojamiento GetAlojamientoByCuit(string cuit)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from alojamiento ");
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
                    Alojamiento aloj = new Alojamiento();
                    aloj.id = Int32.Parse(reader["id_alojamiento"].ToString());
                    aloj.razonSocial = reader["razon_social"].ToString();
                    aloj.cuit = reader["cuit"].ToString();
                    aloj.ciudad = reader["ciudad"].ToString();
                    aloj.categoria = reader["categoria"].ToString();
                    aloj.tarifa = Double.Parse(reader["tarifa"].ToString());
                    aloj.direccion = reader["direccion"].ToString();
                    aloj.capacidad = Int32.Parse(reader["capacidad"].ToString());
                    aloj.email = reader["email"].ToString();
                    aloj.telefono = reader["telefono"].ToString();
                    aloj.servicios = reader["servicios"].ToString();

                    return aloj;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " alojamiento cuit ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Alojamiento GetAlojamientoByRazon(String razonSocial)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from alojamiento ");
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
                    Alojamiento aloj = new Alojamiento();
                    aloj.id = Int32.Parse(reader["id_alojamiento"].ToString());
                    aloj.razonSocial = reader["razon_social"].ToString();
                    aloj.cuit = reader["cuit"].ToString();
                    aloj.ciudad = reader["ciudad"].ToString();
                    aloj.categoria = reader["categoria"].ToString();
                    aloj.tarifa = Double.Parse(reader["tarifa"].ToString());
                    aloj.direccion = reader["direccion"].ToString();
                    aloj.capacidad = Int32.Parse(reader["capacidad"].ToString());
                    aloj.email = reader["email"].ToString();
                    aloj.telefono = reader["telefono"].ToString();
                    aloj.servicios = reader["servicios"].ToString();

                    return aloj;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " alojamiento razon social ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Alojamiento> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from alojamiento ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Alojamiento> listaAlojamiento = new List<Alojamiento>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Alojamiento aloj = new Alojamiento();
                    aloj.id = Int32.Parse(reader["id_alojamiento"].ToString());
                    aloj.razonSocial = reader["razon_social"].ToString();
                    aloj.cuit = reader["cuit"].ToString();
                    aloj.ciudad = reader["ciudad"].ToString();
                    aloj.categoria = reader["categoria"].ToString();
                    aloj.tarifa = Double.Parse(reader["tarifa"].ToString());
                    aloj.direccion = reader["direccion"].ToString();
                    aloj.capacidad = Int32.Parse(reader["capacidad"].ToString());
                    aloj.email = reader["email"].ToString();
                    aloj.telefono = reader["telefono"].ToString();
                    aloj.servicios = reader["servicios"].ToString();
                    listaAlojamiento.Add(aloj);
                }
                return listaAlojamiento;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " alojamientos ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
