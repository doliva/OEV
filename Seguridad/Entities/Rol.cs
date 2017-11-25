using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class Rol : UsuarioComposite
    {
        //public Int32 idRol { set; get; }
        public String descripcion { set; get; }
        public Boolean estado { set; get; }

        private List<UsuarioComposite> subGrupos = new List<UsuarioComposite>();

        //public Rol(String descripcion, Boolean estado)
        //{
        //    this.descripcion = descripcion;
        //    this.estado = estado;
        //}

        public Rol(Int32 id) : base(id) { }

        public Rol()
        {
            // TODO: Complete member initialization
        }  

        //public Rol(string nombre) : base(nombre)
        //{
        //    Console.WriteLine("Grupo usuarios: " + nombre);
        //}

        public override void Agregar(UsuarioComposite usrComp)
        {
            this.subGrupos.Add(usrComp);
            this.perteneceA.Add(usrComp);
            Dictionary<String, String> rolUsr = RolUsuarioDAL.GetRolUsuarioByIds(this.id, usrComp.id);
            if (rolUsr.Count == 0)
                RolUsuarioDAL.Insert(this.id, usrComp.id, true);
            else
                RolUsuarioDAL.Update(Int32.Parse(rolUsr["idrol"]), Int32.Parse(rolUsr["idUsuario"]), true);

        }

        public override void Eliminar(UsuarioComposite usrComp)
        {
            this.subGrupos.Remove(usrComp);
            this.perteneceA.Remove(usrComp);
            Dictionary<String, String> rolUsr = RolUsuarioDAL.GetRolUsuarioByIds(this.id, usrComp.id);
            if (rolUsr.Count == 0)
                RolUsuarioDAL.Insert(this.id, usrComp.id, false);
            else
                RolUsuarioDAL.Update(Int32.Parse(rolUsr["idrol"]), Int32.Parse(rolUsr["idUsuario"]), false);
        }
    }
}
