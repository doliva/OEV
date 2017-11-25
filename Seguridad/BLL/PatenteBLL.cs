using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Base.BLL
{
    public class PatenteBLL : IPatenteBLL
    {

        public int agregarPatente(Patente patente)
        {
            try
            {
                return PatenteDAL.Insert(patente);
            }
            catch (Exception e)
            {
                throw new Excepcion(Constantes.EXCEPCION_DAL_INS + "Patente", e);
            }
        }

        public Patente obtenerPatentePorDesc(string descripcion)
        {
            try
            {
                return PatenteDAL.GetPatenteByDescripcion(descripcion); ;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Patente por descripcion", dalE);
            }
        }

        public List<Usuario> obtenerUsuariosPorIdPat(Int32 idPatente)
        {
            try
            {
                return UsuarioPatenteDAL.GetUsuariosByIdPat(idPatente); ;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Usuarios por idPatente", dalE);
            }
        }

        public List<Patente> obtenerPatentesPorIdFam(Int32 idFamilia)
        {
            try
            {
                return FamiliaPatenteDAL.GetPatentesByIdFam(idFamilia); ;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Patentes por idFamilia", dalE);
            }
        }

        public void actualizarPatente(Patente patente)
        {
            try
            {
                PatenteDAL.Update(patente);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Patente", dalE);
            }
        }

        public List<Patente> obtenerPatentes()
        {
            try
            {
                return PatenteDAL.GetAll();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Patentes", dalE);
            }
        }

        
    }
}
