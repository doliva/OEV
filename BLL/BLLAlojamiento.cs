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
        /// <summary>
        /// Registra un alojamiento en la base de datos
        /// </summary>
        /// <param name="alojamiento">Alojamiento</param>
        /// <returns>Identificador</returns>
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

        /// <summary>
        /// Actualiza un alojamiento
        /// </summary>
        /// <param name="alojamiento">Alojamiento</param>
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

        /// <summary>
        /// Obtiene un alojamiento a partir del CUIT
        /// </summary>
        /// <param name="cuit">string</param>
        /// <returns>Alojamiento</returns>
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

        /// <summary>
        /// Obtiene un alojamiento a partir de la Razon social
        /// </summary>
        /// <param name="razonSocial">String</param>
        /// <returns>Alojamiento</returns>
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

        /// <summary>
        /// Obtiene todos los alojamientos
        /// </summary>
        /// <returns>Lista</returns>
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
