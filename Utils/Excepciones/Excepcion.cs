using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Excepciones
{
    public class Excepcion : Exception
    {
        public Excepcion() { }

        public Excepcion(String mensaje)
            : base(mensaje)
        {

        }

        public Excepcion(String mensaje, Exception innerException) : base(mensaje, innerException) { }
    }
}
