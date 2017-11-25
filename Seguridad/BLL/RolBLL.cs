using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Base.BLL
{
    public class RolBLL : IRolBLL
    {
        public Int32 agregarRol(Rol rol)
        {
            try
            {
                return RolDAL.Insert(rol);
            }
            catch (Exception bllE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_INS + "Rol", bllE);
            }
        }

        public void actualizarRol(Rol rol)
        {
            try
            {
                RolDAL.Update(rol);
            }
            catch (Excepcion bllE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Rol", bllE);
            }
        }
        
        public List<Rol> obtenerRoles()
        {
            try
            {
                return RolDAL.GetRoles();
            }
            catch (Exception bllE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Roles", bllE);
            }
        }

        public Rol obtenerRolPorDesc(string descripcion)
        {
            try
            {
                return RolDAL.GetRolByDesc(descripcion); ;
            }
            catch (Excepcion bllE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Rol Por Descripcion", bllE);
            }
        }

        public Rol obtenerRolPorId(Int32 idRol)
        {
            try
            {
                return RolDAL.GetRolById(idRol);
            }
            catch (Excepcion bllE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Rol Por Id", bllE);
            }
        }
    }
}
