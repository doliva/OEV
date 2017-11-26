using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Base
{
    public class Familia : FuncionalidadComposite
    {
        //public Int32 idFamilia { set; get; }
        //public String descripcion { set; get; }
        //public Boolean estado { set; get; }

        private List<FuncionalidadComposite> subFuncionalidades = new List<FuncionalidadComposite>();

        //public Familia(String descripcion, Boolean estado)
        //{
        //    this.descripcion = descripcion;
        //    this.estado = estado;
        //}

        public Familia(Int32 id) :base(id){ }

        public Familia()
        {
            // TODO: Complete member initialization
        }

        //public TGrupoFuncionalidades(string nombreOpcion) : base(nombreOpcion)
        //{
        //    Console.WriteLine("Familia: " + nombreOpcion);
        //}

        public void Add(FuncionalidadComposite func)
        {
            this.subFuncionalidades.Add(func);
            Dictionary<String, String> famPat = FamiliaPatenteDAL.GetFamiliaPatenteByIds(this.id, func.id);
            if (famPat.Count == 0)
                FamiliaPatenteDAL.Insert(this.id, func.id, true);
            else
                FamiliaPatenteDAL.Update(Int32.Parse(famPat["idFamilia"]), Int32.Parse(famPat["idPatente"]), true);
            //String type = func.GetType() == typeof(Familia) ? Constantes.FAMILIA : Constantes.PATENTE;
            //Console.WriteLine("Agregando " + type + func.nombreFunc + " a familia " + this.descripcion);
        }

        public void Remove(FuncionalidadComposite func)
        {
            this.subFuncionalidades.Remove(func);
            Dictionary<String, String> famPat = FamiliaPatenteDAL.GetFamiliaPatenteByIds(this.id, func.id);
            if (famPat.Count == 0)
                FamiliaPatenteDAL.Insert(this.id, func.id, false);
            else
                FamiliaPatenteDAL.Update(Int32.Parse(famPat["idFamilia"]), Int32.Parse(famPat["idPatente"]), false);
        }

        public override Boolean Contains(FuncionalidadComposite func)
        {
            foreach (FuncionalidadComposite item in subFuncionalidades)
	        {
	            if(item.Contains(func)){
	                return true;
                }
	        }
            return false;
        }

    }
}
