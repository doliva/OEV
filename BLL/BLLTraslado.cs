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
