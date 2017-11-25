using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Bitacora
{
    public class Bitacora
    {
        public Int16 idBitacora { set; get; }
        public String rol { set; get; }
        public DateTime fecha { set; get; }
        public String evento { set; get; }
        public String detalle { set; get; }

        public Bitacora() { }

        public Bitacora(Int16 id, String rol, DateTime fecha, String evento, String detalle)
        {
            this.idBitacora = id;
            this.rol = rol;
            this.fecha = fecha;
            this.evento = evento;
            this.detalle = detalle;
        }
    }
}
