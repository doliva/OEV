using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Producto
    {

        public Int32 idProducto { set; get; }
        public String destino { set; get; }
        public Boolean estado { set; get; }
        public String descripcion { set; get; }
        public String nombre {set; get;}
        public Double precio {set; get;}
        public String tipoProducto {set; get;}
        public String dificultad { set; get; }
        public Int32 duracion { set; get; }
        public Horario horario { set; get; }
        public DateTime fechaSalida { set; get; }
        public List<String> actividades { set; get; }
    }
}
