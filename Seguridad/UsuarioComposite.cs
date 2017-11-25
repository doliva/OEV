using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public abstract class UsuarioComposite
    {
        public Int32 id { set; get; }
        /// Los roles a los que pertenece
        protected List<UsuarioComposite> perteneceA = new List<UsuarioComposite>();
        /// Las patentes/familias que accede
        protected List<FuncionalidadComposite> accedeA = new List<FuncionalidadComposite>();

        public UsuarioComposite() { }

        public UsuarioComposite(Int32 id) {
            this.id = id;
        }

        public abstract void Agregar(UsuarioComposite U);

        public abstract void Eliminar(UsuarioComposite U);

        public void PermitirAcceso(FuncionalidadComposite func)
        {
            this.accedeA.Add(func);
            if (func.GetType() == typeof(Familia) && this.GetType() == typeof(Usuario))
            {
                Dictionary<String, String> usrFam = UsuarioFamiliaDAL.GetUsuarioFamiliaById(this.id, func.id);
                List<Int32> idsPatentes = FamiliaPatenteDAL.GetPatenteById(func.id);
                if (usrFam.Count == 0)
                {
                    UsuarioFamiliaDAL.Insert(this.id, func.id, true);
                    foreach (Int32 item in idsPatentes)
                    {
                        UsuarioPatenteDAL.Insert(this.id, item, true);
                    }
                }
                else
                {
                    UsuarioFamiliaDAL.Update(Int32.Parse(usrFam["idUsuario"]), Int32.Parse(usrFam["idFamilia"]), true);
                    foreach (Int32 item in idsPatentes)
                    {
                        UsuarioPatenteDAL.Update(Int32.Parse(usrFam["idRol"]), item, true);
                    }
                }
            }
            else if (func.GetType() == typeof(Patente) && this.GetType() == typeof(Usuario))
            {
                Dictionary<String, String> usrPat = UsuarioPatenteDAL.GetUsuarioPatenteById(this.id, func.id);
                if (usrPat.Count == 0)
                    UsuarioPatenteDAL.Insert(this.id, func.id, true);
                else
                    UsuarioPatenteDAL.Update(Int32.Parse(usrPat["idUsuario"]), Int32.Parse(usrPat["idPatente"]), true);
                //Console.WriteLine("Permitir acceso a " + func.descripcion + " al usuario " + this.nombre + " " + this.apellido);
            }
            else if (func.GetType() == typeof(Familia) && this.GetType() == typeof(Rol))
            {
                Dictionary<String, String> rolFam = RolFamiliaDAL.GetRolFamiliaById(this.id, func.id);
                List<Int32> idsPatentes = FamiliaPatenteDAL.GetPatenteById(func.id);
                if (rolFam.Count == 0)
                {
                    RolFamiliaDAL.Insert(this.id, func.id, true);
                    foreach (Int32 item in idsPatentes)
                    {
                        RolPatenteDAL.Insert(this.id, item, true);
                    }
                }
                else
                {
                    RolFamiliaDAL.Update(this.id, Int32.Parse(rolFam["idFamilia"]), true);
                    foreach (Int32 item in idsPatentes)
                    {
                        RolPatenteDAL.Update(Int32.Parse(rolFam["idRol"]), item, true);
                    }
                }
            }
            else if (func.GetType() == typeof(Patente) && this.GetType() == typeof(Rol))
            {
                Dictionary<String, String> rolPat = RolPatenteDAL.GetRolPatenteById(this.id, func.id);
                if (rolPat.Count == 0)
                    RolPatenteDAL.Insert(this.id, func.id, true);
                else
                    RolPatenteDAL.Update(Int32.Parse(rolPat["idRol"]), Int32.Parse(rolPat["idPatente"]), true);
            }
        }

        public void DenegarAcceso(FuncionalidadComposite func)
        {
            this.accedeA.Remove(func);
            if (func.GetType() == typeof(Familia) && this.GetType() == typeof(Usuario))
            {
                Dictionary<String, String> usrFam = UsuarioFamiliaDAL.GetUsuarioFamiliaById(this.id, func.id);
                if (usrFam.Count == 0)
                    UsuarioFamiliaDAL.Insert(this.id, func.id, false);
                else
                    UsuarioFamiliaDAL.Update(Int32.Parse(usrFam["idUsuario"]), Int32.Parse(usrFam["idFamilia"]), false);
            }
            else if (func.GetType() == typeof(Patente) && this.GetType() == typeof(Usuario))
            {
                Dictionary<String, String> usrPat = UsuarioPatenteDAL.GetUsuarioPatenteById(this.id, func.id);
                if (usrPat.Count == 0)
                    UsuarioPatenteDAL.Insert(this.id, func.id, false);
                else
                    UsuarioPatenteDAL.Update(Int32.Parse(usrPat["idUsuario"]), Int32.Parse(usrPat["idPatente"]), false);
                //Console.WriteLine("Denegar acceso a " + func.descripcion + " al usuario " + this.nombre + " " + this.apellido);
            }
            else if (func.GetType() == typeof(Familia) && this.GetType() == typeof(Rol))
            {
                Dictionary<String, String> rolFam = RolFamiliaDAL.GetRolFamiliaById(this.id, func.id);
                if (rolFam.Count == 0)
                    RolFamiliaDAL.Insert(this.id, func.id, true);
                else
                    RolFamiliaDAL.Update(Int32.Parse(rolFam["idRol"]), Int32.Parse(rolFam["idFamilia"]), false);
            }
            else if (func.GetType() == typeof(Patente) && this.GetType() == typeof(Rol))
            {
                Dictionary<String, String> rolPat = RolPatenteDAL.GetRolPatenteById(this.id, func.id);
                if (rolPat.Count == 0)
                    RolPatenteDAL.Insert(this.id, func.id, true);
                else
                    RolPatenteDAL.Update(Int32.Parse(rolPat["idRol"]), Int32.Parse(rolPat["idPatente"]), false);
            }
        }

        public Boolean TieneAcceso(FuncionalidadComposite func)
        {
            if (this.GetType() == typeof(Rol) && func.GetType() == typeof(Familia))
            {
                Dictionary<String, String> rolFam = RolFamiliaDAL.GetRolFamiliaById(this.id, func.id);
                if (rolFam.Count > 0)
                    return Boolean.Parse(rolFam["estado"]);
            }
            if (this.GetType() == typeof(Rol) && func.GetType() == typeof(Patente))
            {
                Dictionary<String, String> rolPat = RolPatenteDAL.GetRolPatenteById(this.id, func.id);
                if (rolPat.Count > 0)
                    return Boolean.Parse(rolPat["estado"]);
            }

            Boolean result = false;
            foreach (FuncionalidadComposite item in accedeA)
            {
                if (item.descripcion == func.descripcion)
                    return true;
                result = item.Contains(func);
                if (result)
                {
                    foreach (UsuarioComposite usr in perteneceA)
                    {
                        result = usr.TieneAcceso(func);
                    }
                }
            }

            return result;
        }
    }
}
