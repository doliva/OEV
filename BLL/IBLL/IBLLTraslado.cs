using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IBLLTraslado
    {
        Int32 agregarTraslado(Traslado traslado);
        void actualizarTraslado(Traslado traslado);
        Traslado obtenerTrasladoPorCuit(String cuit);
        Traslado obtenerTrasladoPorRazon(String razonSocial);
        List<Traslado> obtenerTraslados();
    }
}
