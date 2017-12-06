using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Base.BLL
{
    public class UsuarioBLL : IUsuarioBLL
    {
                

        public List<Usuario> obtenerUsuarios()
        {
            try
            {
                return UsuarioDAL.GetAll();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Usuarios", dalE);
            }
        }

        public String obtnerRolPorIdUsuario(Int32 usuarioId)
        {
            try
            {
                Dictionary<String, String> rolUsr = RolUsuarioDAL.GetRolByIdUsuario(usuarioId);
                return (rolUsr.Count==0) ? null: rolUsr["idRol"];
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Rol Por IdUsuario", dalE);
            }
        }

        public List<Patente> obtenerPatentesPorId(Int32 idUsuario)
        {
            try
            {
                 return UsuarioPatenteDAL.GetPatentesByIdUsuario(idUsuario);
                 
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Patentes Por IdUsuario", dalE);
            }
        }

        public List<Usuario> obtenerUsuariosPorRol(string rol)
        {
            try
            {
                return UsuarioDAL.GetUsuariosByRol(rol);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Usuario Por Rol", dalE);
            }
        }

        public void actualizarUsuario(Usuario usuario)
        {
            try
            {
                UsuarioDAL.Update(usuario);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Usuario", dalE);
            }
        }

        public Usuario obtenerUsuarioPorDni(String dni)
        {
            try
            {
                return UsuarioDAL.GetUsuarioByDni(dni);;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Usuario Por DNI", dalE);
            }
        }

        public Usuario obtenerUsuarioPorId(Int32 usuarioId)
        {
            try
            {
                return UsuarioDAL.GetUsuarioById(usuarioId);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Usuario Por Id", dalE);
            }
        }

        public Int32 agregarUsuario(Usuario usuario)
        {
            try
            {
                return UsuarioDAL.Insert(usuario);
            }
            catch (Exception dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + " Usuario", dalE);
            }
        }
    }
}
