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
    public class CalendarioDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        public static Int32 InsertCalendario(Calendario calendario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
                StringBuilder queryBuilderCal = new StringBuilder();
                queryBuilderCal.Append("INSERT INTO CALENDARIO (ID_PRODUCTO, ID_ALOJAMIENTO, LEGAJO, ID_TRASLADO, ANIO, CUPO, FECHA_SALIDA) ");
                queryBuilderCal.Append("VALUES (@idProducto, @idAlojamiento, @legajo, @idTraslado, @anio, @cupo, @fechaSalida )");
                queryBuilderCal.Append("; SELECT SCOPE_IDENTITY()");

                SqlCommand cmdCal = new SqlCommand(queryBuilderCal.ToString(), conn);
                cmdCal.Parameters.Add("@idProducto", SqlDbType.Int).Value = calendario.producto.idProducto;
                cmdCal.Parameters.Add("@idAlojamiento", SqlDbType.Int).Value = calendario.alojamiento.id;
                cmdCal.Parameters.Add("@legajo", SqlDbType.Int).Value = calendario.instructor.legajo;
                cmdCal.Parameters.Add("@idTraslado", SqlDbType.Int).Value = calendario.traslado.id;
                cmdCal.Parameters.Add("@anio", SqlDbType.Int).Value = calendario.anio;
                cmdCal.Parameters.Add("@cupo", SqlDbType.Int).Value = calendario.cupo;
                cmdCal.Parameters.Add("@fechaSalida", SqlDbType.DateTime).Value = (calendario.producto.tipoProducto == EnumProducto.CURSO.ToString()) ? new DateTime(2017, 12, 31, 23, 59, 59) : calendario.producto.fechaSalida;
                cmdCal.CommandType = CommandType.Text;
                conn.Open();
                try
                {

                    Int32 idCalendario = Convert.ToInt32(cmdCal.ExecuteScalar());
                    if (calendario.producto.tipoProducto == EnumProducto.CURSO.ToString())
                    {
                        UpdateHorario(calendario.producto);
                    }
                    return idCalendario;
                }
                catch (Exception ex)
                {
                    throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " calendario ", ex);
                }
                finally
                {
                    conn.Close();
                }

        }

        public static void Update(Calendario calendario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE CALENDARIO SET ID_ALOJAMIENTO=@idAlojamiento, LEGAJO=@legajo, ID_TRASLADO=@idTraslado, CUPO=@cupo, FECHA_SALIDA=@fechaSalida ");
            queryBuilder.Append("WHERE ID_CALENDARIO = @idCalendario");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idCalendario", SqlDbType.Int).Value = calendario.idCalendario;
            cmd.Parameters.Add("@idAlojamiento", SqlDbType.Int).Value = calendario.alojamiento.id;
            cmd.Parameters.Add("@legajo", SqlDbType.Int).Value = calendario.instructor.legajo;
            cmd.Parameters.Add("@idTraslado", SqlDbType.Int).Value = calendario.traslado.id;
            cmd.Parameters.Add("@cupo", SqlDbType.Int).Value = calendario.cupo;
            cmd.Parameters.Add("@fechaSalida", SqlDbType.DateTime).Value = (calendario.producto.tipoProducto == EnumProducto.CURSO.ToString()) ? new DateTime(2017, 12, 31, 23, 59, 59) : calendario.producto.fechaSalida;
            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                if (calendario.producto.tipoProducto == EnumProducto.CURSO.ToString())
                {
                    UpdateHorario(calendario.producto);
                }
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " calendario " + calendario.idCalendario, ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateHorario(Producto producto)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            StringBuilder queryBuilderHor = new StringBuilder();
            queryBuilderHor.Append(" UPDATE HORARIO SET MES=@mes ");
            queryBuilderHor.Append(" WHERE NOMBRE=@nombre ");
            SqlCommand cmdHor = new SqlCommand(queryBuilderHor.ToString(), conn);
            cmdHor.Parameters.Add("@mes", SqlDbType.VarChar, 15).Value = producto.horario.mes;
            cmdHor.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = producto.nombre;
            cmdHor.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmdHor.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " calendario curso-horario" + producto.nombre, ex);
            }
            finally
            {
                conn.Close();
            }

        }
    
        public static Calendario GetCalendarioByAnio(Int32 anio)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[Calendario] ");
            queryBuilder.Append(" where anio=@anio ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@anio", SqlDbType.Int).Value = anio;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Calendario calendario = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    calendario = new Calendario();
                    calendario.idCalendario = Int32.Parse(reader["id_calendario"].ToString());
                    calendario.anio = Int32.Parse(reader["anio"].ToString());
                }
                return calendario;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " calendario por año ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Calendario GetCalendario(Calendario calendario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[Calendario] ");
            queryBuilder.Append(" where anio=@anio and id_producto=@idProducto and id_alojamiento=@idALojamiento and id_traslado=@idTraslado and legajo=@legajo and cupo=@cupo ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@anio", SqlDbType.Int).Value = calendario.anio;
            cmd.Parameters.Add("@idProducto", SqlDbType.Int).Value = calendario.producto.idProducto;
            cmd.Parameters.Add("@idAlojamiento", SqlDbType.Int).Value = calendario.alojamiento.id;
            cmd.Parameters.Add("@legajo", SqlDbType.Int).Value = calendario.instructor.legajo;
            cmd.Parameters.Add("@idTraslado", SqlDbType.Int).Value = calendario.traslado.id;
            cmd.Parameters.Add("@cupo", SqlDbType.Int).Value = calendario.cupo;
            //cmd.Parameters.Add("@fechaSalida", SqlDbType.DateTime).Value = cal.producto.fechaSalida;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Calendario cal = null;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cal = new Calendario();
                    cal.idCalendario = Int32.Parse(reader["id_calendario"].ToString());
                    Alojamiento aloj = new Alojamiento();
                    aloj.id = Int32.Parse(reader["id_alojamiento"].ToString());
                    cal.alojamiento = aloj;
                    cal.anio = Int32.Parse(reader["anio"].ToString());
                    cal.cupo = Int32.Parse(reader["cupo"].ToString());
                    cal.idCalendario = Int32.Parse(reader["id_calendario"].ToString());
                    Instructor inst = new Instructor();
                    inst.legajo = Int32.Parse(reader["legajo"].ToString());
                    cal.instructor = inst;
                    Producto prod = new Producto();
                    prod.idProducto = Int32.Parse(reader["id_producto"].ToString());
                    prod.fechaSalida = DateTime.Parse(reader["fecha_salida"].ToString());
                    cal.producto = prod;
                    Traslado tras = new Traslado();
                    tras.id = Int32.Parse(reader["id_traslado"].ToString());
                    cal.traslado = tras;

                }
                return cal;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " calendario  ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Calendario> GetAll(Int32 anio)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * ");
            queryBuilder.Append(" from CALENDARIO ");
            queryBuilder.Append(" where ANIO=@anio  ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@anio", SqlDbType.Int).Value = anio;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Calendario> listaCalendarios = new List<Calendario>();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Calendario cal = new Calendario();
                    Alojamiento aloj = new Alojamiento();
                    aloj.id = Int32.Parse(reader["id_alojamiento"].ToString());
                    cal.alojamiento = aloj;
                    cal.anio = Int32.Parse(reader["anio"].ToString());
                    cal.cupo = Int32.Parse(reader["cupo"].ToString());
                    cal.idCalendario = Int32.Parse(reader["id_calendario"].ToString());
                    Instructor inst = new Instructor();
                    inst.legajo =  Int32.Parse(reader["legajo"].ToString());
                    cal.instructor = inst;
                    Producto prod = new Producto();
                    prod.idProducto = Int32.Parse(reader["id_producto"].ToString());
                    prod.fechaSalida = DateTime.Parse(reader["fecha_salida"].ToString());
                    cal.producto = prod;
                    Traslado tras = new Traslado();
                    tras.id = Int32.Parse(reader["id_traslado"].ToString());
                    cal.traslado = tras;
                    
                    listaCalendarios.Add(cal);
                }

                return listaCalendarios;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " calendario ", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
