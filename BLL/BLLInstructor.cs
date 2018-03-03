using Base;
using BLL.IBLL;
using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BLL
{
    public class BLLInstructor : IBLLInstructor
    {
        /// <summary>
        /// Registra un instructor en la base de datos
        /// </summary>
        /// <param name="inst">Instructor</param>
        /// <returns>Identificador</returns>
        public Int32 agregarInstructor(Instructor inst)
        {
            try
            {
                Int32 legajo = InstructorDAL.InsertInstructor(inst);
                inst.legajo = legajo;
                InstructorDAL.InsertEspecialidad(inst);
                return legajo;
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_INS + " Instructor", dalE);
            }
        }

        /// <summary>
        /// Actualiza un instructor
        /// </summary>
        /// <param name="inst">Instructor</param>
        public void actualizarInstructor(Instructor inst)
        {
            try
            {
                InstructorDAL.Update(inst);
                InstructorDAL.UpdateEspecialidad(inst);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_UPD + " Instructor", dalE);
            }
        }

        /// <summary>
        /// Obtiene las especialidades de un instructor a partir de su legajo
        /// </summary>
        /// <param name="legajo">Int32</param>
        /// <returns>Lista</returns>
        public List<Especialidad> obtenerEspecialidadesPorLegajo(Int32 legajo)
        {
            try
            {
                return InstructorDAL.GetEspecialidadesByLegajo(legajo);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Especialidades por legajo ", dalE);
            }
        }

        /// <summary>
        /// Obtiene un instructor a partir de su DNI
        /// </summary>
        /// <param name="dni">String</param>
        /// <returns>Instructor</returns>
        public Instructor obtenerInstructorPorDni(String dni)
        {
            try
            {
                return InstructorDAL.GetInstructorByDni(dni);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Instructor por dni ", dalE);
            }
        }

        /// <summary>
        /// Obtiene un instructor a partir de su apellido
        /// </summary>
        /// <param name="apellido">String</param>
        /// <returns>Instructor</returns>
        public Instructor obtenerInstructorPorApellido(String apellido)
        {
            try{
                return InstructorDAL.GetInstructorByApellido(apellido);
                }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Instructor por apellido ", dalE);
            }
        }

        /// <summary>
        /// Obtiene los instructores a partir de una especialidad
        /// </summary>
        /// <param name="especialidad">String</param>
        /// <returns>Lista</returns>
        public List<Instructor> obtenerInstructoresPorEspecialidad(String especialidad)
        {
            try
            {
                return InstructorDAL.GetInstructoresByEspecialidad(especialidad);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Instructores por especialidad ", dalE);
            }
        }

        /// <summary>
        /// Obtiene un instructor a partir de su legajo
        /// </summary>
        /// <param name="legajo">Int32</param>
        /// <returns>Instructor</returns>
        public Instructor obtenerInstructorPorLegajo(Int32 legajo)
        {
            try
            {
                return InstructorDAL.GetInstructorByLegajo(legajo);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Instructore por legajo ", dalE);
            }
        }

        /// <summary>
        /// Obtiene todos los instructores
        /// </summary>
        /// <returns>Lista</returns>
        public List<Instructor> obtenerInstructores()
        {
            try
            {
                return InstructorDAL.GetAll();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Instructores ", dalE);
            }
        }

        /// <summary>
        /// Obtiene todas las especialidades
        /// </summary>
        /// <returns>Lista</returns>
        public List<Especialidad> obtenerEspecialidades()
        {
            try
            {
                return InstructorDAL.GetAllEspecialidades();
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_SEL + " Especialidades ", dalE);
            }
        }
    }
}
