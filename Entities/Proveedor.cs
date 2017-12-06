using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Proveedor
    {
        public Int32 id { set; get; }
        public String razonSocial { set; get; }
        public String cuit { set; get; }
        public String direccion { set; get; }
        public String ciudad { set; get; }
        public String email { set; get; }
        public String telefono { set; get; }
        public Double tarifa { set; get; }
        public Int32 capacidad { set; get; }

        public Proveedor() { }
    }
}
