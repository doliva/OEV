using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IBLLCliente
    {
        /// <summary>
        /// Registra un cliente en la base de datos
        /// </summary>
        /// <param name="cliente">Cliente</param>
        /// <returns></returns>
        Int32 agregarCliente(Cliente cliente);

        /// <summary>
        /// Actualiza un cliente
        /// </summary>
        /// <param name="cliente">Cliente</param>
        void actualizarCliente(Cliente cliente);

        /// <summary>
        /// Obtiene un cliente a partir del Dni o pasaporte
        /// </summary>
        /// <param name="dni">String</param>
        /// <param name="pasaporte">String</param>
        /// <returns>Cliente</returns>
        Cliente obtenerClientePorDniPasaporte(String dni, String pasaporte);

        /// <summary>
        /// Obtiene los clientes cuyo apellido coincide con un patron dado
        /// </summary>
        /// <param name="apellido">String</param>
        /// <returns>Lista</returns>
        List<Cliente> obtenerClientePorApellido(String apellido);

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns>Lista</returns>
        List<Cliente> obtenerClientes();
    }
}
