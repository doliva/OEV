using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario
    {
        public Int32 idUsuario { set; get; }
        public String dni { set; get; }
        public String nombre { set; get; }
        public String apellido { set; get; }
        public String email { set; get; }
        public String domicilio { set; get; }
        public DateTime fecNac { set; get; }
        public String telefono { set; get; }
        public String ciudad { set; get; }
        public String clave { set; get; }
        public String rol { set; get; }
        public String digitoVerificador { set; get; }
        public Boolean estado { set; get; }
    }
}
