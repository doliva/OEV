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
    public class InstructorDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        public static Int32 InsertInstructor(Instructor instructor)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO INSTRUCTOR (DNI, NOMBRE, APELLIDO, DOMICILIO, CIUDAD, TELEFONO, EMAIL, ESTADO) ");
            queryBuilder.Append("VALUES (@dni, @nombre, @apellido, @domicilio, @ciudad, @telefono, @email, @estado )");
            queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = instructor.dni;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = instructor.nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 30).Value = instructor.apellido;
            cmd.Parameters.Add("@domicilio", SqlDbType.VarChar, 100).Value = instructor.domicilio;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = instructor.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = instructor.telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = instructor.email;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = instructor.estado;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " instructor ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertEspecialidad(Instructor instructor)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            Int32 codigo = 0;
            foreach (Especialidad item in instructor.especialidadLista)
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("INSERT INTO ESPECIALIDAD (DESCRIPCION) ");
                queryBuilder.Append("VALUES (@descripcion )");
                queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
                SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = item.descripcion;

                cmd.CommandType = CommandType.Text;
                conn.Open();
                try
                {
                     codigo = Convert.ToInt32(cmd.ExecuteScalar());
                     InsertEspInst(codigo, instructor.legajo, item);
                }
                catch (Exception ex)
                {
                    throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " especialidad ", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static void InsertEspInst(Int32 codigo, Int32 legajo, Especialidad esp)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO INSTRUCTOR_ESPECIALIDAD (LEGAJO, CODIGO, TARIFA, EXPERIENCIA) ");
            queryBuilder.Append("VALUES (@legajo, @codigo, @tarifa, @experiencia )");
            //queryBuilder.Append("; SELECT SCOPE_IDENTITY()");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@legajo", SqlDbType.Int).Value = legajo;
            cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = codigo;
            cmd.Parameters.Add("@tarifa", SqlDbType.Money).Value = esp.tarifa;
            cmd.Parameters.Add("@experiencia", SqlDbType.Int).Value = esp.experiencia;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " especialidad_instructor ", ex);
            }
            finally
            {
                conn.Close();
            }
        }


        public static void Update(Instructor inst)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE INSTRUCTOR SET NOMBRE=@nombre, APELLIDO=@apellido, DNI=@dni, DOMICILIO=@domicilio, TELEFONO=@telefono, EMAIL=@email, ESTADO=@estado, CIUDAD=@ciudad ");
            queryBuilder.Append("WHERE LEGAJO = @legajo");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = inst.nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 30).Value = inst.apellido;
            cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = inst.dni;
            cmd.Parameters.Add("@domicilio", SqlDbType.VarChar, 100).Value = inst.domicilio;
            cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = inst.ciudad;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 20).Value = inst.telefono;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = inst.email;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = inst.estado;
            cmd.Parameters.Add("@legajo", SqlDbType.Int).Value = inst.legajo;

            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " instructor", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateEspecialidad(Instructor inst)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            foreach (Especialidad item in inst.especialidadLista)
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("UPDATE ESPECIALIDAD SET DESCRIPCION=@descripcion  ");
                queryBuilder.Append("WHERE CODIGO=@codigo ");
                SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
                cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = item.codigo;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 30).Value = item.descripcion;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    UpdateEspInst(inst.legajo, item.codigo, item);
                }
                catch (Exception ex)
                {
                    throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " especialidad ", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static void UpdateEspInst(Int32 legajo, Int32 codigo, Especialidad esp)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("UPDATE INSTRUCTOR_ESPECIALIDAD SET TARIFA=@tarifa, EXPERIENCIA=@experiencia  ");
            queryBuilder.Append("WHERE CODIGO=@codigo AND LEGAJO=@legajo");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = codigo;
            cmd.Parameters.Add("@legajo", SqlDbType.Int).Value = legajo;
            cmd.Parameters.Add("@tarifa", SqlDbType.Money).Value = esp.tarifa;
            cmd.Parameters.Add("@experiencia", SqlDbType.Int).Value = esp.experiencia;
            cmd.CommandType = CommandType.Text;
            conn.Open();
             try
                {
                    cmd.ExecuteNonQuery();
                }
             catch (Exception ex)
             {
                 throw new Excepcion(Constantes.EXCEPCION_DAL_UPD + " especialidad_instructor ", ex);
             }
             finally
             {
                 conn.Close();
             }
        }

        public static List<Especialidad> GetEspecialidadesByLegajo(Int32 legajo){
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select iesp.*, esp.* from [BD_OEV_C].[dbo].[Instructor_Especialidad] iesp, [BD_OEV_C].[dbo].[Especialidad] esp  ");
            queryBuilder.Append(" where iesp.legajo=@legajo and iesp.codigo=esp.codigo");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@legajo", SqlDbType.Int).Value = legajo;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Especialidad> espLista = new List<Especialidad>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Especialidad esp = new Especialidad();
                    esp.legajo = Int32.Parse(reader["legajo"].ToString());
                    esp.codigo = Int32.Parse(reader["codigo"].ToString());
                    esp.tarifa = Double.Parse(reader["tarifa"].ToString());
                    esp.experiencia = Int32.Parse(reader["experiencia"].ToString());
                    esp.descripcion = reader["descripcion"].ToString();

                    espLista.Add(esp);
                }
                return espLista;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " especilidades por legajo ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Instructor> GetInstructoresByEspecialidad(String esp)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select inst.legajo, inst.dni, inst.nombre, inst.apellido, inst.domicilio, inst.ciudad, ");
            queryBuilder.Append(" inst.telefono, inst.email, inst.estado, iesp.tarifa, iesp.experiencia, esp.descripcion ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[Instructor] inst, [BD_OEV_C].[dbo].[Instructor_Especialidad] iesp, [BD_OEV_C].[dbo].[Especialidad] esp ");
            queryBuilder.Append(" where inst.legajo=iesp.legajo and iesp.codigo=esp.codigo and esp.descripcion=@esp ");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@esp", SqlDbType.VarChar, 50).Value = esp;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Instructor> instLista = new List<Instructor>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Instructor inst = new Instructor();
                    inst.legajo = Int32.Parse(reader["legajo"].ToString());
                    inst.nombre = reader["nombre"].ToString();
                    inst.apellido = reader["apellido"].ToString();
                    inst.dni = reader["dni"].ToString();
                    inst.domicilio = reader["domicilio"].ToString();
                    inst.ciudad = reader["ciudad"].ToString();
                    inst.telefono = reader["telefono"].ToString();
                    inst.email = reader["email"].ToString();
                    inst.estado = Boolean.Parse(reader["estado"].ToString());
                    Especialidad especialidad = new Especialidad();
                    especialidad.tarifa = Double.Parse(reader["tarifa"].ToString());
                    especialidad.experiencia = Int32.Parse(reader["experiencia"].ToString());
                    especialidad.descripcion = reader["descripcion"].ToString();
                    inst.especialidadLista = new List<Especialidad>();
                    inst.especialidadLista.Add(especialidad);
                    instLista.Add(inst);
                }
                return instLista;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " instructores por especialidad ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Instructor GetInstructorByDni(String dni)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select inst.legajo, inst.dni, inst.nombre, inst.apellido, inst.domicilio, inst.ciudad, ");
            queryBuilder.Append(" inst.telefono, inst.email, inst.estado, iesp.tarifa, iesp.experiencia, esp.descripcion ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[Instructor] inst, [BD_OEV_C].[dbo].[Instructor_Especialidad] iesp, [BD_OEV_C].[dbo].[Especialidad] esp ");
            queryBuilder.Append(" where inst.legajo=iesp.legajo and iesp.codigo=esp.codigo and inst.dni=@dni ");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = dni;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Instructor inst = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    inst = new Instructor();
                    inst.legajo = Int32.Parse(reader["legajo"].ToString());
                    inst.nombre = reader["nombre"].ToString();
                    inst.apellido = reader["apellido"].ToString();
                    inst.dni = reader["dni"].ToString();
                    inst.domicilio = reader["domicilio"].ToString();
                    inst.ciudad = reader["ciudad"].ToString();
                    inst.telefono = reader["telefono"].ToString();
                    inst.email = reader["email"].ToString();
                    inst.estado = Boolean.Parse(reader["estado"].ToString());
                    Especialidad especialidad = new Especialidad();
                    especialidad.tarifa = Double.Parse(reader["tarifa"].ToString());
                    especialidad.experiencia = Int32.Parse(reader["experiencia"].ToString());
                    especialidad.descripcion = reader["descripcion"].ToString();
                    inst.especialidadLista = new List<Especialidad>();
                    inst.especialidadLista.Add(especialidad);
                    
                }
                return inst;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " instructor por dni ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Instructor GetInstructorByLegajo(Int32 legajo){
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select inst.legajo, inst.dni, inst.nombre, inst.apellido, inst.domicilio, inst.ciudad, ");
            queryBuilder.Append(" inst.telefono, inst.email, inst.estado, iesp.tarifa, iesp.experiencia, esp.descripcion ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[Instructor] inst, [BD_OEV_C].[dbo].[Instructor_Especialidad] iesp, [BD_OEV_C].[dbo].[Especialidad] esp ");
            queryBuilder.Append(" where inst.legajo=iesp.legajo and iesp.codigo=esp.codigo and inst.legajo=@legajo ");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@legajo", SqlDbType.Int).Value = legajo;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Instructor inst = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    inst = new Instructor();
                    inst.legajo = Int32.Parse(reader["legajo"].ToString());
                    inst.nombre = reader["nombre"].ToString();
                    inst.apellido = reader["apellido"].ToString();
                    inst.dni = reader["dni"].ToString();
                    inst.domicilio = reader["domicilio"].ToString();
                    inst.ciudad = reader["ciudad"].ToString();
                    inst.telefono = reader["telefono"].ToString();
                    inst.email = reader["email"].ToString();
                    inst.estado = Boolean.Parse(reader["estado"].ToString());
                    Especialidad especialidad = new Especialidad();
                    especialidad.tarifa = Double.Parse(reader["tarifa"].ToString());
                    especialidad.experiencia = Int32.Parse(reader["experiencia"].ToString());
                    especialidad.descripcion = reader["descripcion"].ToString();
                    inst.especialidadLista = new List<Especialidad>();
                    inst.especialidadLista.Add(especialidad);

                }
                return inst;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " instructor por legajo ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static Instructor GetInstructorByApellido(String apellido)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select inst.legajo, inst.dni, inst.nombre, inst.apellido, inst.domicilio, inst.ciudad, ");
            queryBuilder.Append(" inst.telefono, inst.email, inst.estado, iesp.tarifa, iesp.experiencia, esp.descripcion ");
            queryBuilder.Append(" from [BD_OEV_C].[dbo].[Instructor] inst, [BD_OEV_C].[dbo].[Instructor_Especialidad] iesp, [BD_OEV_C].[dbo].[Especialidad] esp ");
            queryBuilder.Append(" where inst.legajo=iesp.legajo and iesp.codigo=esp.codigo and  inst.apellido like '%' + @apellido + '%' ");

            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 30).Value = apellido;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            Instructor inst = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    inst = new Instructor();
                    inst.legajo = Int32.Parse(reader["legajo"].ToString());
                    inst.nombre = reader["nombre"].ToString();
                    inst.apellido = reader["apellido"].ToString();
                    inst.dni = reader["dni"].ToString();
                    inst.domicilio = reader["domicilio"].ToString();
                    inst.ciudad = reader["ciudad"].ToString();
                    inst.telefono = reader["telefono"].ToString();
                    inst.email = reader["email"].ToString();
                    inst.estado = Boolean.Parse(reader["estado"].ToString());
                    Especialidad especialidad = new Especialidad();
                    especialidad.tarifa = Double.Parse(reader["tarifa"].ToString());
                    especialidad.experiencia = Int32.Parse(reader["experiencia"].ToString());
                    especialidad.descripcion = reader["descripcion"].ToString();
                    inst.especialidadLista = new List<Especialidad>();
                    inst.especialidadLista.Add(especialidad);

                }
                return inst;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " instructor por dni ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Especialidad> GetAllEspecialidades()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from Especialidad ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Especialidad> listaEspecialidad = new List<Especialidad>();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                            Especialidad esp = new Especialidad();
                            esp.codigo = Int32.Parse(reader["codigo"].ToString());
                            esp.descripcion = reader["descripcion"].ToString();
                    listaEspecialidad.Add(esp);
                }

                return listaEspecialidad;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " especialidades ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Instructor> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" select * from Instructor ");
            SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();
            List<Instructor> listaInstructor = new List<Instructor>();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Instructor inst = new Instructor();
                    inst.legajo = Int32.Parse(reader["legajo"].ToString());
                    inst.nombre = reader["nombre"].ToString();
                    inst.apellido = reader["apellido"].ToString();
                    inst.dni = reader["dni"].ToString();
                    inst.ciudad = reader["ciudad"].ToString();
                    inst.domicilio = reader["domicilio"].ToString();
                    inst.email = reader["email"].ToString();
                    inst.telefono = reader["telefono"].ToString();
                    inst.estado = Boolean.Parse(reader["estado"].ToString());

                    inst.especialidadLista = GetEspecialidadesByLegajo(inst.legajo);
                    listaInstructor.Add(inst);
               }
                
                return listaInstructor;
            }
            catch (Exception ex)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " instructores ", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        
    }
}
