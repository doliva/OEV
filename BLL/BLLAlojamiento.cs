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
    public class BLLAlojamiento : IBLLAlojamiento
    {
        public int agregarAlojamiento(Alojamiento alojamiento)
        {
            try
            {
                return AlojamientoDAL.Insert(alojamiento);
            }
            catch (Exception dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " Alojamiento ", dalE);
            }
        }

        public void actualizarAlojamiento(Alojamiento alojamiento)
        {
            try
            {
                AlojamientoDAL.Update(alojamiento);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Alojamiento ", dalE);
            }
        }

        public Alojamiento obtenerAlojamientoPorCuit(string cuit)
        {
            try
            {
                return AlojamientoDAL.GetAlojamientoByCuit(cuit);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Alojamiento Por CUIT ", dalE);
            }
        }

        public Alojamiento obtenerAlojamientoPorRazon(String razonSocial)
        {
            try
            {
                return AlojamientoDAL.GetAlojamientoByRazon(razonSocial);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Alojamiento Por Razon Social ", dalE);
            }
        }

        public List<Alojamiento> obtenerAlojamientos()
        {
            try
            {
                return AlojamientoDAL.GetAll();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Alojamiento ", dalE);
            }
        }

    }
}
