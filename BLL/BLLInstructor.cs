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
