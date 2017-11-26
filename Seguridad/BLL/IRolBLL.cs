using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.BLL
{
    public interface IRolBLL
    {
        List<Rol> obtenerRoles();
        Rol obtenerRolPorDesc(String descripcion);
        Rol obtenerRolPorId(Int32 idRol);
        void actualizarRol(Rol rol);
        Int32 agregarRol(Rol rol);
    }
}
