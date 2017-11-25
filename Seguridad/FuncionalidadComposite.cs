using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public abstract class FuncionalidadComposite
    {
        public Int32 id{ set; get; }
        public String descripcion { set; get; }
        public Boolean estado { set; get; }

        public FuncionalidadComposite(Int32 id)
        {
            this.id = id;
        }
        
        //public String nombreFunc{set; get;}

        //public FuncionalidadComposite(string descripcion)
        //{
        //    this.descripcion = descripcion;
            
        //}

        public FuncionalidadComposite() { }

        public abstract Boolean Contains(FuncionalidadComposite func);

        public void Ejecutar(string funcionalidad)
        {
            Console.WriteLine("Ejecutando funcionalidad...." + funcionalidad);
        }
    }
}
