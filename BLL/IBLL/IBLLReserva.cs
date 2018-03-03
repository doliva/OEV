using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IBLLReserva
    {
        /// <summary>
        /// Registra una reserva en la base de datos
        /// </summary>
        /// <param name="reserva">Reserva</param>
        void agregarReserva(Reserva reserva);

        /// <summary>
        /// Obtiene una reserva a partir de su codigo
        /// </summary>
        /// <param name="codigo">String</param>
        /// <returns>Reserva</returns>
        Reserva obtenerReservaPorCodigo(String codigo);
    }
}
