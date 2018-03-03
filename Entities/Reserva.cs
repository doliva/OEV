using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reserva
    {
        public String codigo { set; get; }
        public Int32 cantidad { set; get; }
        public DateTime fecha { set; get; }
        public String estado { set; get; }
        public Int32 idCalendario { set; get; }
        public Double importe { set; get; }
        public String tipoPago { set; get; }
        public Int32 idCliente { set; get; }
    }
}
