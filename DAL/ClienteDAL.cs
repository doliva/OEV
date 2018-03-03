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
    public class ClienteDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        /// <summary>
        /// Registra un cliente en la base de datos
        /// </summary>
        /// <param name="cliente">Cliente</param>
        /// <returns>Identificador</returns>
        public static Int32 InsertCliente(Cliente cliente)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO CLIENTE (DNI, NOMBRE, APELLIDO, PASAPORTE, DOMICILIO, CIUDAD, TELEFONO, CELULAR, EMAIL, ESTADO) ");
            queryBuilder.Append("VALUES (@dni, @nombre, @apellido, @pasaporte, @domicilio, @ciudad, @telefono, @celular, @email, @estado )");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = cliente.dni;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = cliente.nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 30).Value = cliente.apellido;
            cmd.Parameters.Add("@pasaporte", SqlDbType.VarChar, 10).Value = cliente.pasaporte;
            cmd.Parameters.Add("@domicilio", SqlDbType.VarChar, 100).Value = cliente.domicilio;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = cliente.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = cliente.telefono;
            cmd.Parameters.Add("@celular", SqlDbType.VarChar, 20).Value = cliente.celular;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = cliente.email;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = cliente.estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " cliente ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Actualiza un cliente
        /// </summary>
        /// <param name="cliente">Cliente</param>
        public static void Update(Cliente cliente)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE CLIENTE SET NOMBRE=@nombre, APELLIDO=@apellido, DNI=@dni, PASAPORTE=@pasaporte, DOMICILIO=@domicilio, TELEFONO=@telefono, CELULAR=@celular, EMAIL=@email, ESTADO=@estado, CIUDAD=@ciudad ");
            queryBuilder.Append("WHERE ID_CLIENTE = @idCliente");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = cliente.nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 30).Value = cliente.apellido;
            cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = cliente.dni;
            cmd.Parameters.Add("@pasaporte", SqlDbType.VarChar, 10).Value = cliente.pasaporte;
            cmd.Parameters.Add("@domicilio", SqlDbType.VarChar, 100).Value = cliente.domicilio;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = cliente.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = cliente.telefono;
            cmd.Parameters.Add("@celular", SqlDbType.VarChar, 20).Value = cliente.celular;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = cliente.email;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = cliente.estado;
            cmd.Parameters.Add("@idCliente", SqlDbType.Int).Value = cliente.idCliente;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " cliente ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene un cliente a partir de su DNI o pasaporte
        /// </summary>
        /// <param name="dni">String</param>
        /// <param name="pasaporte">String</param>
        /// <returns>Cliente</returns>
        public static Cliente GetClienteByDniPasaporte(String dni, String pasaporte)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * ");
            queryBuilder.Append(" from CLIENTE ");
            if(!String.IsNullOrEmpty(dni) && !String.IsNullOrEmpty(pasaporte))
                queryBuilder.Append(" where DNI=@dni and PASAPORTE=@pasaporte ");
            else if(!String.IsNullOrEmpty(dni) && String.IsNullOrEmpty(pasaporte))
                queryBuilder.Append(" where DNI=@dni  ");
            else if (String.IsNullOrEmpty(dni) && !String.IsNullOrEmpty(pasaporte))
                queryBuilder.Append(" where PASAPORTE=@pasaporte ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            if (!String.IsNullOrEmpty(dni) && !String.IsNullOrEmpty(pasaporte))
            {
                cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = dni;
                cmd.Parameters.Add("@pasaporte", SqlDbType.VarChar, 10).Value = pasaporte;
            }
            else if (!String.IsNullOrEmpty(dni) && String.IsNullOrEmpty(pasaporte))
                cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = dni;
            else if (String.IsNullOrEmpty(dni) && !String.IsNullOrEmpty(pasaporte))
                cmd.Parameters.Add("@pasaporte", SqlDbType.VarChar, 10).Value = pasaporte;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Cliente clie = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    clie = new Cliente();
                    clie.idCliente = Int32.Parse(reader["id_cliente"].ToString());
                    clie.nombre = reader["nombre"].ToString();
                    clie.apellido = reader["apellido"].ToString();
                    clie.dni = reader["dni"].ToString();
                    clie.pasaporte = reader["pasaporte"].ToString();
                    clie.domicilio = reader["domicilio"].ToString();
                    clie.ciudad = reader["ciudad"].ToString();
                    clie.telefono = reader["telefono"].ToString();
                    clie.celular = reader["celular"].ToString();
                    clie.email = reader["email"].ToString();
                    clie.estado = Boolean.Parse(reader["estado"].ToString());
                }
                return clie;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " cliente por dni o pasaporte ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene los clientes cuyo apellido coincide con un patron dado
        /// </summary>
        /// <param name="apellido">String</param>
        /// <returns>Lista</returns>
        public static List<Cliente> GetClientesByApellido(String apellido)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[Cliente] ");
            queryBuilder.Append(" where apellido like '%' + @apellido + '%' ");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 30).Value = apellido;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Cliente> listaClientes = new List<Cliente>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cli = new Cliente();
                    cli.idCliente= Int32.Parse(reader["id_cliente"].ToString());
                    cli.nombre = reader["nombre"].ToString();
                    cli.apellido = reader["apellido"].ToString();
                    cli.dni = reader["dni"].ToString();
                    cli.pasaporte = reader["pasaporte"].ToString();
                    cli.domicilio = reader["domicilio"].ToString();
                    cli.ciudad = reader["ciudad"].ToString();
                    cli.telefono = reader["telefono"].ToString();
                    cli.celular = reader["celular"].ToString();
                    cli.email = reader["email"].ToString();
                    cli.estado = Boolean.Parse(reader["estado"].ToString());
                    listaClientes.Add(cli);

                }
                return listaClientes;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " clientes por apellido ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns>Lista</returns>
        public static List<Cliente> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from Cliente ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Cliente> listaClientes = new List<Cliente>();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cli = new Cliente();
                    cli.idCliente = Int32.Parse(reader["id_cliente"].ToString());
                    cli.nombre = reader["nombre"].ToString();
                    cli.apellido = reader["apellido"].ToString();
                    cli.dni = reader["dni"].ToString();
                    cli.pasaporte = reader["pasaporte"].ToString();
                    cli.ciudad = reader["ciudad"].ToString();
                    cli.domicilio = reader["domicilio"].ToString();
                    cli.email = reader["email"].ToString();
                    cli.telefono = reader["telefono"].ToString();
                    cli.celular = reader["celular"].ToString();
                    cli.estado = Boolean.Parse(reader["estado"].ToString());

                    listaClientes.Add(cli);
                }

                return listaClientes;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " clientes ", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
