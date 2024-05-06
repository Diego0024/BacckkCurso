using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRest.API.Model.Colegio.Implementacion
{
    public interface IAlumnosDataMapper
    {
        Task<bool> AgregarAlumnos(string jsonAlumnos);
        Task<List<AlumnosExtend>> ObtenerAlumnosFiltro(int? idAlumno = null, int? idCarrera = null);
        Task<bool> ActualizarAlumnos(Alumnos alumno);
        Task<bool> EliminarAlumno(int idAlumno);
        Task<bool> ActualizarAlumno(int idAlumno, Alumnos alumno);


    }
}
