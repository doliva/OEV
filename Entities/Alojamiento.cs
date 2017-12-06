using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Alojamiento : Proveedor
    {
        public String categoria { set; get; }
        public String servicios { set; get; }

        public Alojamiento() : base() { }
    }
}
