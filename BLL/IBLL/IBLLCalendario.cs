using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IBLLCalendario
    {
        Int32 agregarCalendario(Calendario calendario);
        void actualizarCalendario(Calendario calendarios);
        Boolean existeCalendario(Int32 anio);
        List<Calendario> obtenerCalendarios(Int32 anio);
        Calendario obtenerCalendario(Calendario cal);
    }
}
