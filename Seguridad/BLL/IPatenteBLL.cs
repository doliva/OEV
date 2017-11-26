using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.BLL
{
    public interface IPatenteBLL
    {
        Int32 agregarPatente(Patente patente);
        Patente obtenerPatentePorDesc(string descripcion);
        void actualizarPatente(Patente patente);
        List<Patente> obtenerPatentes();
        List<Patente> obtenerPatentesPorIdFam(Int32 idFamilia);
        List<Usuario> obtenerUsuariosPorIdPat(Int32 idPatente);
    }
}
