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
    }
}
