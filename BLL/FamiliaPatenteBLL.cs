using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Base;
using BLL.IBLL;
using DAL;

namespace BLL
{
    public class FamiliaPatenteBLL : IFamiliaPatenteBLL
    {
        /// <summary>
        /// Obtiene una patente a partir de la familia
        /// </summary>
        /// <param name="familia">string</param>
        /// <returns>Patente</returns>
        public Patente obtenerPatente(string familia)
        {
            try
            {
                return FamiliaPatenteDAL.GetPatenteByFamilia(familia); ;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Patente por familia", dalE);
            }
        }

        /// <summary>
        /// Obtiene las patentes asociadas a una familia
        /// </summary>
        /// <param name="familia">string</param>
        /// <returns>Lista</returns>
        public List<Patente> obtenerPatentes(string familia)
        {
            try
            {
                return FamiliaPatenteDAL.GetPatentesByFamilia(familia); ;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Patentes por familia", dalE);
            }
        }

        /// <summary>
        /// Obtiene una familia asociada a una patente
        /// </summary>
        /// <param name="patente">string</param>
        /// <returns>Familia</returns>
        public Familia obtenerFamilia(string patente)
        {
            try
            {
                return FamiliaPatenteDAL.GetFamiliaByPatente(patente); ;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Familia por patente", dalE);
            }
        }
    }
}
