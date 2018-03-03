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
    public class BLLProducto : IBLLProducto
    {
        /// <summary>
        /// Obtiene un producto a partir de su nombre
        /// </summary>
        /// <param name="nombre">String</param>
        /// <returns>Producto</returns>
        public Producto obtenerProductoPorNombre(String nombre)
        {
            try
            {
                return ProductoDAL.GetProductoByNombre(nombre);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Producto por nombre " + nombre, dalE);
            }  
        }

        /// <summary>
        /// Obtiene un producto a partir de us identificador
        /// </summary>
        /// <param name="idProducto">Int32</param>
        /// <returns>Producto</returns>
        public Producto obtenerProductoPorId(Int32 idProducto)
        {
            try
            {
                return ProductoDAL.GetProductoById(idProducto);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Producto por id " + idProducto, dalE);
            } 
        }

        /// <summary>
        /// Registra un producto en la base de datos
        /// </summary>
        /// <param name="producto">Producto</param>
        /// <returns>Identificador</returns>
        public int agregarProducto(Producto producto)
        {
            try
            {
                Int32 codigo = ProductoDAL.InsertProducto(producto);

                return codigo;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_INS + " Producto " + producto.tipoProducto, dalE);
            }
        }

        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="producto">Producto</param>
        public void actualizarProducto(Producto producto)
        {
            try
            {
                ProductoDAL.Update(producto);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Producto " + producto.tipoProducto, dalE);
            }
        }

        /// <summary>
        /// Obtiene todos los productos de tipo curso
        /// </summary>
        /// <returns>Lista</returns>
        public List<Producto> obtenerCursos()
        {
            try
            {
                return ProductoDAL.GetCursos();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Cursos ", dalE);
            }  
        }

        /// <summary>
        /// Obtiene todos los productos de tipo evento y paquete
        /// </summary>
        /// <returns>Lista</returns>
        public List<Producto> obtenerProductos()
        {
            try
            {
                return ProductoDAL.GetProductos();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Productos ", dalE);
            } 
        }

    }
}
