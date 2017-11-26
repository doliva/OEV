using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public interface ISeguridad
    {
        String generarSHA(String cadena);
        Boolean verificarConsistencia();
        List<Usuario> verificarHorizontal();
        String Encriptar(String cadenaAencriptar);
        String DesEncriptar(String cadenaAdesencriptar);
    }
}
