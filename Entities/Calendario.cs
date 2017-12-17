using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Calendario
    {
        public Int32 idCalendario { set; get; }
        public Producto producto { set; get; }
        public Int32 anio { set; get; }
        public Int32 cupo { set; get; }
        public Instructor instructor { set; get; }
        public Alojamiento alojamiento { set; get; }
        public Traslado traslado { set; get; }

    }
}
