using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IUsuarioBLL
    {
        List<Usuario> obtenerUsuarios();
        List<Entities.Usuario> obtenerUsuariosPorRol(string rol);
        void actualizarUsuario(Usuario usuario);
        Usuario obtenerUsuarioPorDni(string dni);
        Usuario obtenerUsuarioPorId(Int32 usuarioId);
        Int32 agregarUsuario(Usuario usuario);
    }
}
