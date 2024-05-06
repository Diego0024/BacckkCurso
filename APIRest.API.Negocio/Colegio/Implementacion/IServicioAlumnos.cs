using APIRest.API.Model.Colegio;
using APIRest.API.Model.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRest.API.Negocio.Colegio.Implementacion
{
    public interface IServicioAlumnos
    {
        Task<Response> AgregarAlumnos(List<Alumnos> lsitaAlumnos);

        Task<Response> ObtenerAlumnosFiltro(int? idAlumno, int? idCarrera = null);

        Task<Response> ActualizarAlumnos(Alumnos alumnos);

        Task<Response> ObtenerTodosAlumnos();

        Task<Response> EliminarAlumno(int idAlumno);

        Task<Response> ActualizarAlumno(int idAlumno, Alumnos alumno);

    }
}
