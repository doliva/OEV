using Base;
using BLL.IBLL;
using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BLL
{
    public class BLLCalendario : IBLLCalendario
    {
        /// <summary>
        /// Registra un calendario en la base de datos
        /// </summary>
        /// <param name="calendario">Calendario</param>
        /// <returns>Identificador</returns>
        public Int32 agregarCalendario(Calendario calendario)
        {
            try
            {
                return CalendarioDAL.InsertCalendario(calendario);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_INS + " Calendario " , dalE);
            }
        }

        /// <summary>
        /// Actualiza un calendario
        /// </summary>
        /// <param name="calendario">Calendario</param>
        public void actualizarCalendario(Calendario calendario)
        {
            try
            {
                CalendarioDAL.Update(calendario);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Calendario " , dalE);
            }
        }

        /// <summary>
        /// Actualiza el cupo de un calendario
        /// </summary>
        /// <param name="idCalendario">Int32</param>
        /// <param name="cupo">Int32</param>
        public void actualizarCalendarioCupo(Int32 idCalendario, Int32 cupo)
        {
            try
            {
                CalendarioDAL.UpdateCupo(idCalendario, cupo);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Calendario cupo ", dalE);
            }
        }
        
        /// <summary>
        /// Verifica la existencia de un calendario
        /// </summary>
        /// <param name="anio">Int32</param>
        /// <returns>Existencia</returns>
        public Boolean existeCalendario(Int32 anio)
        {
            try
            {
                return (CalendarioDAL.GetCalendarioByAnio(anio) != null) ? true: false;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Calendario por año ", dalE);
            }
        }

        /// <summary>
        /// Obtiene todos los calendarios
        /// </summary>
        /// <param name="anio">Int32</param>
        /// <returns>Lista</returns>
        public List<Calendario> obtenerCalendarios(Int32 anio)
        {
            try
            {
                return CalendarioDAL.GetAll(anio);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Calendarios ", dalE);
            }
        }

        /// <summary>
        /// Obtiene un calendario
        /// </summary>
        /// <param name="cal">Calendario</param>
        /// <returns>Calendario</returns>
        public Calendario obtenerCalendario(Calendario cal)
        {
            try
            {
                return CalendarioDAL.GetCalendario(cal);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Calendario ", dalE);
            }
        }

        /// <summary>
        /// Obtiene un calendario a partir de su identificador
        /// </summary>
        /// <param name="idCalendario">Int32</param>
        /// <returns>Calendario</returns>
        public Calendario obtenerCalendarioPorId(Int32 idCalendario)
        {
            try
            {
                return CalendarioDAL.GetCalendarioById(idCalendario);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Calendario por Id ", dalE);
            }
        }
    }
}
