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
    public class ProductoDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        public static Int32 InsertProducto(Producto producto)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO PRODUCTO (DESTINO, ACTIVIDAD, ESTADO, NOMBRE, PRECIO, DURACION, TIPO_PRODUCTO, DESCRIPCION, DIFICULTAD) ");
            queryBuilder.Append("VALUES (@destino, @actividad, @estado, @nombre, @precio, @duracion, @tipoProducto, @descripcion, @dificultad )");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@destino", SqlDbType.VarChar, 100).Value = producto.destino;
            cmd.Parameters.Add("@actividad", SqlDbType.VarChar, 100).Value = String.Join(",", producto.actividades);
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = producto.estado;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = producto.nombre;
            cmd.Parameters.Add("@precio", SqlDbType.Money).Value = producto.precio;
            cmd.Parameters.Add("@duracion", SqlDbType.Int).Value = producto.duracion;
            cmd.Parameters.Add("@tipoProducto", SqlDbType.VarChar, 20).Value = producto.tipoProducto;
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 10000).Value = producto.descripcion;
            cmd.Parameters.Add("@dificultad", SqlDbType.VarChar, 20).Value = producto.dificultad;
            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                Int32 idProducto = Convert.ToInt32(cmd.ExecuteScalar());
                if (producto.tipoProducto == EnumProducto.CURSO.ToString())
                {
                    InsertHorario(producto);
                }
                
                return idProducto;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " producto ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertHorario(Producto producto)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO HORARIO (NOMBRE, DIAS, HORA_INICIO, HORA_FIN) ");
            queryBuilder.Append("VALUES (@nombre, @dias, @horaInicio, @horaFin )");
           // queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 35).Value = producto.nombre;
            cmd.Parameters.Add("@dias", SqlDbType.VarChar, 10).Value = producto.horario.dia;
            cmd.Parameters.Add("@horaInicio", SqlDbType.Time, 7).Value = producto.horario.horaInicio.TimeOfDay;
            cmd.Parameters.Add("@horaFin", SqlDbType.Time, 7).Value = producto.horario.horaFin.TimeOfDay;
            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " Horario ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Producto producto)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE PRODUCTO SET NOMBRE=@nombre, DESTINO=@destino, ACTIVIDAD=@actividad, ESTADO=@estado, PRECIO=@precio, DURACION=@duracion, TIPO_PRODUCTO=@tipoProducto, DESCRIPCION=@descripcion, DIFICULTAD=@dificultad ");
            queryBuilder.Append("WHERE ID_PRODUCTO = @idProducto");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idProducto", SqlDbType.Int).Value = producto.idProducto;
            cmd.Parameters.Add("@destino", SqlDbType.VarChar, 100).Value = producto.destino;
            cmd.Parameters.Add("@actividad", SqlDbType.VarChar, 100).Value = String.Join(",", producto.actividades);
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = producto.estado;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = producto.nombre;
            cmd.Parameters.Add("@precio", SqlDbType.Money).Value = producto.precio;
            cmd.Parameters.Add("@duracion", SqlDbType.Int).Value = producto.duracion;
            cmd.Parameters.Add("@tipoProducto", SqlDbType.VarChar, 20).Value = producto.tipoProducto;
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 10000).Value = producto.descripcion;
            cmd.Parameters.Add("@dificultad", SqlDbType.VarChar, 20).Value = producto.dificultad;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " producto " + producto.tipoProducto, ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Producto GetProductoByNombre(String nombre)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select * from PRODUCTO where NOMBRE=@nombre ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = nombre;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Producto prod = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    prod = new Producto();
                    prod.idProducto = Int32.Parse(reader["id_producto"].ToString());
                    prod.nombre = reader["nombre"].ToString(); 
                    prod.estado = Boolean.Parse(reader["estado"].ToString());
                    prod.actividades = reader["actividad"].ToString().Split(',').ToList();
                    prod.precio = Double.Parse(reader["precio"].ToString());
                    prod.tipoProducto = reader["tipo_producto"].ToString(); 
                    prod.dificultad = reader["dificultad"].ToString();
                    prod.descripcion = reader["descripcion"].ToString();

                    if (prod.tipoProducto == EnumProducto.CURSO.ToString())
                    {
                        prod.horario = GetHorario(prod.nombre);
                    }
                    else if (prod.tipoProducto == EnumProducto.EVENTO.ToString() || prod.tipoProducto == EnumProducto.PAQUETE.ToString())
                    {
                        prod.destino = reader["destino"].ToString();
                        prod.duracion = Int32.Parse(reader["duracion"].ToString());
                    }
                }
                return prod;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " producto por nombre ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Horario GetHorario(String nombre)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select * from HORARIO where NOMBRE=@nombre ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = nombre;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Horario hr = new Horario();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hr.mes = reader["mes"].ToString();
                    hr.dia = reader["dias"].ToString(); 
                    hr.horaInicio = DateTime.Parse(reader["hora_inicio"].ToString());
                    hr.horaFin = DateTime.Parse(reader["hora_fin"].ToString());
                }
                return hr;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " horario por nombre ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Producto GetProductoById(Int32 idProducto)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select * from PRODUCTO where ID_PRODUCTO=@idProducto ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idProducto", SqlDbType.Int).Value = idProducto;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Producto prod = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    prod = new Producto();
                    prod.idProducto = Int32.Parse(reader["id_producto"].ToString());
                    prod.nombre = reader["nombre"].ToString();
                    prod.estado = Boolean.Parse(reader["estado"].ToString());
                    prod.actividades = reader["actividad"].ToString().Split(',').ToList();
                    prod.precio = Double.Parse(reader["precio"].ToString());
                    prod.tipoProducto = reader["tipo_producto"].ToString();
                    prod.dificultad = reader["dificultad"].ToString();
                    prod.descripcion = reader["descripcion"].ToString();

                    if (prod.tipoProducto == EnumProducto.CURSO.ToString())
                    {
                        prod.horario = GetHorario(prod.nombre);
                    }
                    else if (prod.tipoProducto == EnumProducto.EVENTO.ToString() || prod.tipoProducto == EnumProducto.PAQUETE.ToString())
                    {
                        prod.destino = reader["destino"].ToString();
                        prod.duracion = Int32.Parse(reader["duracion"].ToString());
                    }
                }
                return prod;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " producto por id ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Producto> GetCursos()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select prod.*, hr.*  ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[PRODUCTO] prod, [BD_OEV_C].[dbo].[HORARIO] hr  ");
            queryBuilder.Append(" where prod.TIPO_PRODUCTO='CURSO' and prod.NOMBRE=hr.NOMBRE ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Producto> listaCursos = new List<Producto>();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto curso = new Producto();
                    curso.idProducto = Int32.Parse(reader["id_producto"].ToString());
                    curso.nombre = reader["nombre"].ToString();
                    curso.actividades = reader["actividad"].ToString().Split(',').ToList();
                    curso.estado = Boolean.Parse(reader["estado"].ToString());
                    curso.precio = Double.Parse(reader["precio"].ToString());
                    curso.tipoProducto = reader["tipo_producto"].ToString();
                    curso.descripcion = reader["descripcion"].ToString();
                    curso.dificultad = reader["dificultad"].ToString();
                    Horario hr = new Horario();
                    hr.mes = reader["mes"].ToString();
                    hr.dia = reader["dias"].ToString();
                    hr.horaInicio = DateTime.Parse(reader["hora_inicio"].ToString());
                    hr.horaFin = DateTime.Parse(reader["hora_fin"].ToString());
                    curso.horario = hr;

                    listaCursos.Add(curso);
                }

                return listaCursos;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " cursos ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Producto> GetProductos()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * ");
            queryBuilder.Append(" from PRODUCTO ");
            queryBuilder.Append(" where (TIPO_PRODUCTO='PAQUETE' or tipo_producto='EVENTO')  ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Producto> listaProductos = new List<Producto>();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto prod = new Producto();
                    prod.idProducto = Int32.Parse(reader["id_producto"].ToString());
                    prod.nombre = reader["nombre"].ToString();
                    prod.actividades = reader["actividad"].ToString().Split(',').ToList();
                    prod.destino = reader["destino"].ToString();
                    prod.estado = Boolean.Parse(reader["estado"].ToString());
                    prod.precio = Double.Parse(reader["precio"].ToString());
                    prod.duracion = Int32.Parse(reader["duracion"].ToString());
                    prod.tipoProducto = reader["tipo_producto"].ToString();
                    prod.descripcion = reader["descripcion"].ToString();
                    prod.dificultad = reader["dificultad"].ToString();

                    listaProductos.Add(prod);
                }

                return listaProductos;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " productos ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

  
    }
}
