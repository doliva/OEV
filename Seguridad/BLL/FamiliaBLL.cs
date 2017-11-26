using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Base.BLL
{
    public class FamiliaBLL : IFamiliaBLL
    {
        public int agregarFamilia(Familia familia)
        {
            try
            {
                return FamiliaDAL.Insert(familia);
            }
            catch (Exception e)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + "Familia", e);
            }
        }

        public Familia obtenerFamiliaPorDesc(string descripcion)
        {
            try
            {
                return FamiliaDAL.GetFamiliaByDescripcion(descripcion); ;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Familia por descripcion", dalE);
            }
        }

        public Familia obtenerFamiliaPorIdPat(Int32 idPatente)
        {
            try
            {
                return FamiliaPatenteDAL.GetFamiliaByIdPat(idPatente); ;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Familia por idPatente", dalE);
            }
        }

        public void actualizarFamilia(Familia familia)
        {
            try
            {
                FamiliaDAL.Update(familia);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Familia", dalE);
            }
        }

        public List<Familia> obtenerFamilias()
        {
            try
            {
                return FamiliaDAL.GetAll();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Familias", dalE);
            }
        }
    }
}
