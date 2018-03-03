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
    public class BLLReserva : IBLLReserva
    {
        /// <summary>
        /// Registra una reserva en la base de datos
        /// </summary>
        /// <param name="reserva">Reserva</param>
        public void agregarReserva(Reserva reserva)
        {
            try
            {
                ReservaDAL.InsertReserva(reserva);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_INS + " reserva ", dalE);
            }
        }

        /// <summary>
        /// Obtiene una reserva a partir del codigo
        /// </summary>
        /// <param name="codigo">String</param>
        /// <returns>Reserva</returns>
        public Reserva obtenerReservaPorCodigo(String codigo)
        {
            try
            {
                return ReservaDAL.GetReservaByCodigo(codigo);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Reserva ", dalE);
            }
        }
    }
}
