using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InstructorEspecialidadDAL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["OEVbdConnectionString"].ConnectionString;

        public static List<Especialidad> GetEspecialidadsByLegajo(Int32 legajo)
        {
            //SqlConnection conn = new SqlConnection(connectionString);
            //StringBuilder queryBuilder = new StringBuilder();
            //queryBuilder.Append(" select inst.*, iesp.*, esp.* from [BD_OEV_S].[dbo].[INSTRUCTOR] inst, [BD_OEV_S].[dbo].[INSTRUCTOR_ESPECIALIDAD] iesp, [BD_OEV_S].[dbo].[ESPECIALIDAD] esp ");
            //queryBuilder.Append("where inst.legajo=@legajo and inst.legajo=iesp.legajo ");

            //SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);
            //cmd.Parameters.Add("@legajo", SqlDbType.Int).Value = legajo;

            //cmd.CommandType = CommandType.Text;
            //SqlDataReader reader;
            //conn.Open();
            //try
            //{
            //    reader = cmd.ExecuteReader();
            List<Especialidad> listaPatente = new List<Especialidad>();
            //    while (reader.Read())
            //    {
            //        Especialidad esp = new Especialidad();
            //        esp.codigo = Int32.Parse(reader["codigo"].ToString());
            //        esp.descripcion = reader["descripcion"].ToString();
            //        esp.estado = Boolean.Parse(reader["estado"].ToString());

            //        listaPatente.Add(esp);
            //    }
            return listaPatente;
            //}
            //catch (Exception ex)
            //{
            //    throw new Excepcion(Constantes.EXCEPCION_DAL_SEL + " familia_patente por idFamilia", ex);
            //}
            //finally
            //{
            //    conn.Close();
            //}
        }
    }
}
