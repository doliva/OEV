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
    public class ReservaDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        /// <summary>
        /// Registra una reserva en la base de datos
        /// </summary>
        /// <param name="reserva">Reserva</param>
        public static void InsertReserva(Reserva reserva)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilderRes = new StringBuilder();
            queryBuilderRes.Append("INSERT INTO RESERVA (CODIGO, CANTIDAD, FECHA, ESTADO, ID_CALENDARIO, IMPORTE, TIPO_PAGO) ");
            queryBuilderRes.Append("VALUES (@codigo, @cantidad, @fecha, @estado, @idCalendario, @importe, @tipoPago )");
            queryBuilderRes.Append("; SELECT SCOPE_IDENTITY()");

            //StringBuilder queryBuilderVenta = new StringBuilder();
            //queryBuilderVenta.Append(" INSERT INTO VENTA (ID_CLIENTE, CODIGO, ID_FACTURA) ");
            //queryBuilderVenta.Append(" VALUES (@idCliente, @codigo, @idFactura) ");

            SqlCommand cmdRes = new SqlCommand(queryBuilderRes.ToString(), conn);
            cmdRes.Parameters.Add("@codigo", SqlDbType.VarChar, 6).Value = reserva.codigo;
            cmdRes.Parameters.Add("@cantidad", SqlDbType.Int).Value = reserva.cantidad;
            cmdRes.Parameters.Add("@estado", SqlDbType.VarChar, 15).Value = reserva.estado;
            cmdRes.Parameters.Add("@idCalendario", SqlDbType.Int).Value = reserva.idCalendario;
            cmdRes.Parameters.Add("@importe", SqlDbType.Money).Value = reserva.importe;
            cmdRes.Parameters.Add("@tipoPago", SqlDbType.VarChar, 15).Value = reserva.tipoPago;
            cmdRes.Parameters.Add("@fecha", SqlDbType.DateTime).Value = reserva.fecha;
            cmdRes.CommandType = CommandType.Text;

            //SqlCommand cmdVenta = new SqlCommand(queryBuilderVenta.ToString(), conn);
            //cmdVenta.Parameters.Add("@idCliente", SqlDbType.Int).Value = reserva.idCliente;
            //cmdVenta.Parameters.Add("@codigo", SqlDbType.VarChar, 6).Value = reserva.codigo;
            //cmdVenta.Parameters.Add("@idFactura", SqlDbType.Int).Value = 0;
            conn.Open();
            try
            {
                cmdRes.ExecuteNonQuery();
                //cmdVenta.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " reserva ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtiene una reserva a partir de su codigo
        /// </summary>
        /// <param name="codigo">String</param>
        /// <returns>Reserva</returns>
        public static Reserva GetReservaByCodigo(String codigo)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[Reserva] ");
            queryBuilder.Append(" where codigo=@codigo ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@codigo", SqlDbType.VarChar, 6).Value = codigo;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Reserva reserva = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reserva = new Reserva();
                    reserva.cantidad = Int32.Parse(reader["cantidad"].ToString());
                    reserva.codigo = reader["codigo"].ToString();
                    reserva.estado = reader["estado"].ToString();
                    reserva.fecha = DateTime.Parse(reader["fecha"].ToString());
                    //reserva.idCliente = Int32.Parse(reader["id_cliente"].ToString());
                    reserva.idCalendario = Int32.Parse(reader["id_calendario"].ToString());
                    reserva.importe = Double.Parse(reader["importe"].ToString());
                    reserva.tipoPago = reader["tipo_pago"].ToString();
                }
                return reserva;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " reserva por codigo ", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
