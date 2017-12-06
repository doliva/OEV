using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IBLL
{
    public interface IBLLInstructor
    {
        Int32 agregarInstructor(Instructor inst);
        void actualizarInstructor(Instructor inst);
        List<Especialidad> obtenerEspecialidadesPorLegajo(Int32 legajo);
        List<Instructor> obtenerInstructoresPorEspecialidad(String especialidad);
        Instructor obtenerInstructorPorDni(String dni);
        Instructor obtenerInstructorPorApellido(String apellido);
        Instructor obtenerInstructorPorLegajo(Int32 legajo);
        List<Instructor> obtenerInstructores();
        List<Especialidad> obtenerEspecialidades();
    }
}
