using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Utils;

namespace Base.DAL
{
    public class UsuarioDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdServConnectionString"].ConnectionString;

        public static Int32 Insert(Usuario usuario)
        {
            
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO USUARIO (NOMBRE, APELLIDO, DNI, FECHA_NACIMIENTO, DIRECCION, CIUDAD, TELEFONO, EMAIL, ESTADO, CLAVE, DVH) ");
            queryBuilder.Append("VALUES (@nombre, @apellido, @dni, @fecNac, @direccion, @ciudad, @telefono, @email, @estado, @clave, @dvh)");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.apellido;
            cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = usuario.dni;
            cmd.Parameters.Add("@fecNac", SqlDbType.DateTime).Value = usuario.fecNac;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 100).Value = usuario.domicilio;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = usuario.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = usuario.telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.email;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = usuario.estado;
            //cmd.Parameters.Add("@codRol", SqlDbType.VarChar, 15).Value = usuario.idRol;
            cmd.Parameters.Add("@clave", SqlDbType.VarChar, 30).Value = usuario.clave;
            cmd.Parameters.Add("@dvh", SqlDbType.VarChar, 50).Value = usuario.digitoVerificador;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " usuario", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Update(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE USUARIO SET NOMBRE=@nombre, APELLIDO=@apellido, DNI=@dni, FECHA_NACIMIENTO=@fecha_nacimiento, DIRECCION=@direccion, TELEFONO=@telefono, EMAIL=@email, ESTADO=@estado, CIUDAD=@ciudad, CLAVE=@clave, DVH=@dvh ");
            queryBuilder.Append("WHERE ID_USUARIO = @id_usuario");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = usuario.nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 30).Value = usuario.apellido;
            cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = usuario.dni;
            cmd.Parameters.Add("@fecha_nacimiento", SqlDbType.DateTime).Value = usuario.fecNac;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 100).Value = usuario.domicilio;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = usuario.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = usuario.telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = usuario.email;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = usuario.estado;
            //cmd.Parameters.Add("@rol", SqlDbType.VarChar, 15).Value = usuario.idRol;
            cmd.Parameters.Add("@clave", SqlDbType.VarChar, 30).Value = usuario.clave;
            cmd.Parameters.Add("@dvh", SqlDbType.VarChar, 50).Value = usuario.digitoVerificador;
            cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario.id;
            
            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " usuario", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Usuario GetUsuarioByDni(string dni)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from usuario ");
            queryBuilder.Append("where dni=@dni");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
            
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Usuario usr = new Usuario(Int32.Parse(reader["id_Usuario"].ToString()));
                    usr.apellido = reader["apellido"].ToString();
                    usr.nombre = reader["nombre"].ToString();
                    usr.ciudad = reader["ciudad"].ToString();
                    usr.clave = reader["clave"].ToString();
                    usr.fecNac = (DateTime)reader["fecha_nacimiento"];
                    usr.digitoVerificador = reader["dvh"].ToString();
                    usr.dni = reader["dni"].ToString();
                    usr.domicilio = reader["direccion"].ToString();
                    usr.estado = Boolean.Parse(reader["estado"].ToString());
                    usr.email = reader["email"].ToString();
                    //usr.idRol = Int32.Parse(reader["id_rol"].ToString());
                    usr.telefono = reader["telefono"].ToString();

                    return usr;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " usuario dni", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Usuario GetUsuarioById(Int32 idUsuario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from usuario ");
            queryBuilder.Append("where id_usuario=@idUsuario");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    Usuario usr = new Usuario(Int32.Parse(reader["id_Usuario"].ToString()));
                    usr.apellido = reader["apellido"].ToString();
                    usr.nombre = reader["nombre"].ToString();
                    usr.ciudad = reader["ciudad"].ToString();
                    usr.clave = reader["clave"].ToString();
                    usr.fecNac = (DateTime)reader["fecha_nacimiento"];
                    usr.digitoVerificador = reader["dvh"].ToString();
                    usr.dni = reader["dni"].ToString();
                    usr.domicilio = reader["direccion"].ToString();
                    usr.estado = Boolean.Parse(reader["estado"].ToString());
                    usr.email = reader["email"].ToString();
                    //usr.idRol = Int32.Parse(reader["id_rol"].ToString());
                    usr.telefono = reader["telefono"].ToString();

                    return usr;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " usuario dni", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Usuario> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from usuario ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Usuario> listaUsuario = new List<Usuario>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usr = new Usuario(Int32.Parse(reader["id_Usuario"].ToString()));
                    usr.apellido = reader["apellido"].ToString();
                    usr.nombre = reader["nombre"].ToString();
                    usr.ciudad = reader["ciudad"].ToString();
                    usr.clave = reader["clave"].ToString();
                    usr.fecNac = (DateTime)reader["fecha_nacimiento"];
                    usr.digitoVerificador = reader["dvh"].ToString();
                    usr.dni = reader["dni"].ToString();
                    usr.domicilio = reader["direccion"].ToString();
                    usr.email = reader["email"].ToString();
                    usr.estado = Boolean.Parse(reader["estado"].ToString());
                    //usr.idRol = Int32.Parse(reader["id_rol"].ToString());
                    usr.telefono = reader["telefono"].ToString();
                    listaUsuario.Add(usr);
                }
                return listaUsuario;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " usuarios", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Usuario> GetUsuariosByRol(string rol)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from [BD_OEV_S].[dbo].[Usuario] usr, [BD_OEV_S].[dbo].[Rol] rol ");
            queryBuilder.Append("where rol.descripcion=@rol and rol.id_rol=usr.id_rol");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@rol", SqlDbType.VarChar).Value = rol;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Usuario> listaUsuario = new List<Usuario>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usr = new Usuario(Int32.Parse(reader["id_Usuario"].ToString()));
                    usr.apellido = reader["apellido"].ToString();
                    usr.nombre = reader["nombre"].ToString();
                    usr.ciudad = reader["ciudad"].ToString();
                    usr.clave = reader["clave"].ToString();
                    usr.fecNac = (DateTime)reader["fecha_nacimiento"];
                    usr.digitoVerificador = reader["dvh"].ToString();
                    usr.dni = reader["dni"].ToString();
                    usr.domicilio = reader["direccion"].ToString();
                    usr.email = reader["email"].ToString();
                    usr.estado = Boolean.Parse(reader["estado"].ToString());
                    usr.email = reader["email"].ToString();
                    //usr.idRol = Int32.Parse(reader["id_rol"].ToString());
                    usr.telefono = reader["telefono"].ToString();
                    listaUsuario.Add(usr);
                }
                return listaUsuario;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " usuario rol ", ex);
            }
            finally
            {
                conn.Close();
            }
        }



        public static String GetRolByIdUsuario(Int32 idUsuario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select rol.descripcion from [BD_OEV_S].[dbo].[Usuario] usr, [BD_OEV_S].[dbo].[Rol] rol ");
            queryBuilder.Append("where usr.id_usuario=@idUsuario and usr.id_rol=rol.id_rol");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                String rol = null;
                if (reader.Read())
                {
                    rol = reader["descripcion"].ToString();
                }
                return rol;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " Rol por IdUsuario", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
