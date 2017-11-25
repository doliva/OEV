using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.BLL
{
    public interface IUsuarioBLL
    {
        List<Usuario> obtenerUsuarios();
        List<Usuario> obtenerUsuariosPorRol(string rol);
        void actualizarUsuario(Usuario usuario);
        Usuario obtenerUsuarioPorDni(string dni);
        Usuario obtenerUsuarioPorId(Int32 usuarioId);
        Int32 agregarUsuario(Usuario usuario);

        String obtnerRolPorIdUsuario(Int32 usuarioId);

        List<Patente> obtenerPatentesPorId(Int32 usuarioId);
    }
}
