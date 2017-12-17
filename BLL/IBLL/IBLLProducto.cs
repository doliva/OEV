using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IBLLProducto
    {
        Producto obtenerProductoPorNombre(String nombre);
        Producto obtenerProductoPorId(Int32 idProducto);
        Int32 agregarProducto(Producto producto);
        void actualizarProducto(Producto producto);
        List<Producto> obtenerCursos();
        List<Producto> obtenerProductos();
    }
}
