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
    }
}
