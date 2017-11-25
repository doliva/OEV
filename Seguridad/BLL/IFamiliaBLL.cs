using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.BLL
{
    public interface IFamiliaBLL
    {
        Int32 agregarFamilia(Familia patente);
        Familia obtenerFamiliaPorDesc(string descripcion);
        void actualizarFamilia(Familia patente);
        List<Familia> obtenerFamilias();

        Familia obtenerFamiliaPorIdPat(Int32 idPatente);
    }
}
