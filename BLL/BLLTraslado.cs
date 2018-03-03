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
    public class BLLTraslado : IBLLTraslado
    {
        /// <summary>
        /// Registra un traslado en la base de datos
        /// </summary>
        /// <param name="traslado">Traslado</param>
        /// <returns>Identificador</returns>
        public int agregarTraslado(Traslado traslado)
        {
            try
            {
                return TrasladoDAL.Insert(traslado);
            }
            catch (Exception dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " Traslado ", dalE);
            }
        }

        /// <summary>
        /// Actualiza un traslado
        /// </summary>
        /// <param name="traslado">Traslado</param>
        public void actualizarTraslado(Traslado traslado)
        {
            try
            {
                TrasladoDAL.Update(traslado);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Traslado ", dalE);
            }
        }

        /// <summary>
        /// Obtiene un traslado a partir del CUIT
        /// </summary>
        /// <param name="cuit">string</param>
        /// <returns>Traslado</returns>
        public Traslado obtenerTrasladoPorCuit(string cuit)
        {
            try
            {
                return TrasladoDAL.GetTrasladoByCuit(cuit);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Traslado Por CUIT ", dalE);
            }
        }

        /// <summary>
        /// Obtiene un traslado a partir de la Razon social
        /// </summary>
        /// <param name="razonSocial">string</param>
        /// <returns>Traslado</returns>
        public Traslado obtenerTrasladoPorRazon(string razonSocial)
        {
            try
            {
                return TrasladoDAL.GetTrasladoByRazon(razonSocial);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Traslado Por Razon Social ", dalE);
            }
        }

        /// <summary>
        /// Obtiene todos los traslados
        /// </summary>
        /// <returns>Lista</returns>
        public List<Traslado> obtenerTraslados()
        {
            try
            {
                return TrasladoDAL.GetAll();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Traslados ", dalE);
            }
        }

    }
}
