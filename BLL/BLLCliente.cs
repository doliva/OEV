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
    public class BLLCliente : IBLLCliente
    {
        /// <summary>
        /// Registra un cliente en la base de datos
        /// </summary>
        /// <param name="cliente">Cliente</param>
        /// <returns>Identificador</returns>
        public int agregarCliente(Cliente cliente)
        {
            try
            {
                return ClienteDAL.InsertCliente(cliente);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_INS + " Cliente", dalE);
            }
        }

        /// <summary>
        /// Obtiene un cliente a partir del DNI o pasaporte
        /// </summary>
        /// <param name="dni">String</param>
        /// <param name="pasaporte">String</param>
        /// <returns>Cliente</returns>
        public Cliente obtenerClientePorDniPasaporte(String dni, String pasaporte)
        {
            try
            {
                return ClienteDAL.GetClienteByDniPasaporte(dni, pasaporte);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Cliente por dni o pasaporte", dalE);
            }
        }

        /// <summary>
        /// Obtiene los clientes cuyo apellido coincide con un patron dado
        /// </summary>
        /// <param name="apellido">String</param>
        /// <returns>Lista</returns>
        public List<Cliente> obtenerClientePorApellido(String apellido)
        {
            try
            {
                return ClienteDAL.GetClientesByApellido(apellido);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Clientes por apellido", dalE);
            }
        }

        /// <summary>
        /// Actualiza un cliente
        /// </summary>
        /// <param name="cliente">Cliente</param>
        public void actualizarCliente(Cliente cliente)
        {
            try
            {
                ClienteDAL.Update(cliente);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Cliente ", dalE);
            }
        }

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns>Lista</returns>
        public List<Cliente> obtenerClientes()
        {
            try
            {
                return ClienteDAL.GetAll();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Clientes ", dalE);
            }
        }
    }
}
