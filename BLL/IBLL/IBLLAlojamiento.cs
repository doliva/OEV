using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IBLLAlojamiento
    {
        Int32 agregarAlojamiento(Alojamiento alojamiento);
        void actualizarAlojamiento(Alojamiento alojamiento);
        Alojamiento obtenerAlojamientoPorCuit(String cuit);
        Alojamiento obtenerAlojamientoPorRazon(String razonSocial);
        List<Alojamiento> obtenerAlojamientos();
    }
}
