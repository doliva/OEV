using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class Patente: FuncionalidadComposite
    {
        //public Int32 idPatente { set; get; }
        //public String descripcion { set; get; }
        //public Boolean estado { set; get; }

        //public Patente(String descripcion, Boolean estado)
        //{
        //    this.descripcion = descripcion;
        //    this.estado = estado;
        //}

        public Patente(Int32 id) : base(id){ }

        public Patente()
        {
            // TODO: Complete member initialization
        }

        public override Boolean Contains(FuncionalidadComposite func)
        {
            //Console.WriteLine("No se implementa contains");
            return false;
        }

    }
}
