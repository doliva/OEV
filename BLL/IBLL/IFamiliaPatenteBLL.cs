using Base;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IFamiliaPatenteBLL
    {
        Patente obtenerPatente(String familia);
        List<Patente> obtenerPatentes(String familia);
        Familia obtenerFamilia(String patente);
    }
}
