using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Voucher
    {
        public Int16 cantindad { set; get; }
        public Cliente cliente { set; get; }
        public String detalle { set; get; }
        public String estado { set; get; }
        public DateTime fecha { set; get; }
        public String numero { set; get; }
        public Producto producto { set; get; }
        public String numero { set; get; }
        // TODO: Agregar cambio a la documentación
        public DateTime fechaInicioActividad { set; get; }
        // TODO: Agregar cambio a la documentación
        public DateTime fechaFinActividad { set; get; }

    }
}
