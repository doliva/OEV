using BLL.IBLL;
using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Excepciones;

namespace BLL
{
    public class UsuarioBLL : IUsuarioBLL
    {
                

        public List<Entities.Usuario> obtenerUsuarios()
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

        public List<Entities.Usuario> obtenerUsuariosPorRol(string rol)
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

        public Entities.Usuario obtenerUsuarioPorDni(String dni)
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

        public Entities.Usuario obtenerUsuarioPorId(Int32 usuarioId)
        {
            try
            {
                return UsuarioDAL.GetUsuarioById(usuarioId);;
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
            catch (Exception e)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + "Usuario", e);
            }
        }
    }
}
