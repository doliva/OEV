using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Excepciones;

namespace Utils.Bitacora
{
    public class BitacoraBLL
    {
        public static void registrarBitacora(Bitacora bitacora)
        {
            try
            {
               int result = BitacoraDAL.Insert(bitacora.idBitacora, bitacora.rol, bitacora.fecha, bitacora.evento, bitacora.detalle);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_INS + "Bitacora", dalE.InnerException);
            }
        }

        public static List<Bitacora> obtenerBitacorasPorRol(string rol)
        {
            try
            {
                return BitacoraDAL.SelectBitacorasByRol(rol);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Bitacora Por Role", dalE);
            }          
        }

        public static List<Bitacora> obtenerBitacorasPorEvento(string evento)
        {
            try
            {
                return BitacoraDAL.SelectBitacorasByEvento(evento);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Bitacora Por Evento", dalE);
            } 
        }

        public static List<Bitacora> obtenerBitacorasPorFechas(DateTime desde, DateTime hasta)
        {
            try
            {
                return BitacoraDAL.SelectBitacorasByFechas(desde, hasta);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Bitacora Por Fechas", dalE);
            } 
         
        }

        /// <summary>
        /// Facade para seleccionas todos los registros de la tabla Bitacora.
        /// </summary>
        /// <returns>List</returns>
        public static List<Bitacora> obtenerBitacoras()
        {
            List<Bitacora> bitacoras = new List<Bitacora>();
            DataSet ds = BitacoraDAL.SelectAll();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Bitacora bitacora = new Bitacora();
                bitacora.idBitacora = Convert.ToInt16(row["id"].ToString());
                bitacora.rol = row["rol"].ToString();
                bitacora.fecha = Convert.ToDateTime(row["fecha"].ToString());
                bitacora.evento = row["evento"].ToString();
                bitacora.detalle = row["detalle"].ToString();
                bitacoras.Add(bitacora);

            }
            return bitacoras;
        }
    }
}
