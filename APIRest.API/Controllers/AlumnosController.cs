using APIRest.API.Model.Colegio;
using APIRest.API.Model.Generales;
using APIRest.API.Negocio.Colegio.Implementacion;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace APIRest.API.Controllers
{
    [ApiController]
    [Route("Colegio/api/Alumnos")]
    public class AlumnosController : ControllerBase
    {
        private readonly IServicioAlumnos _servicioAlumnos;
        public AlumnosController(IServicioAlumnos servicioAlumnos) {


            _servicioAlumnos = servicioAlumnos;


        }
        [HttpPost, Route("AgregarAlumnos")]
        public async Task<Response> AgregarAlumnos(List<Alumnos> alumnos)
        {
            Response response = await _servicioAlumnos.AgregarAlumnos(alumnos);

            return response;

        }
        [HttpGet, Route("ObtenerAlumnoFiltro")]
        public async Task<Response> ObtenerAlumnosFiltro(int? idAlumno, int? idCarrera = null)
        {
            Response response = await _servicioAlumnos.ObtenerAlumnosFiltro(idAlumno, idCarrera);

            return response;

        }

        [HttpPut, Route("ActualizarAlumnos")]
        public async Task<Response> ActualizarAlumnos(Alumnos alumnos)
        {
            Response response = await _servicioAlumnos.ActualizarAlumnos(alumnos);

            return response;

        }

        [HttpGet, Route("ObtenerTodosAlumnos")]
        public async Task<Response> ObtenerTodosLosAlumnos()
        {
            Response response = await _servicioAlumnos.ObtenerTodosAlumnos();
            return response;
        }



        [HttpDelete, Route("EliminarAlumno/{idAlumno}")]
        public async Task<IActionResult> EliminarAlumno(int idAlumno)
        {
            try
            {
                Response response = await _servicioAlumnos.EliminarAlumno(idAlumno);

                if (response.status)
                {
                    return Ok(response); 
                }
                else
                {
                    if (response.code == (int)HttpStatusCode.NotFound)
                    {
                        return NotFound(response); 
                    }
                    else
                    {
                        return StatusCode(response.code, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response { message = ex.Message, status = false, code = (int)HttpStatusCode.InternalServerError });
            }
        }

        [HttpPut, Route("ActualizarAlumno/{idAlumno}")]
        public async Task<IActionResult> ActualizarAlumno(int idAlumno, Alumnos alumno)
        {
            try
            {
                Response obtenerAlumnoResponse = await _servicioAlumnos.ObtenerAlumnosFiltro(idAlumno);
                if (!obtenerAlumnoResponse.status)
                {
                    return NotFound(new Response { message = "El alumno no existe.", status = false, code = (int)HttpStatusCode.NotFound });
                }

                Response response = await _servicioAlumnos.ActualizarAlumno(idAlumno, alumno);

                if (response.status)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(response.code, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response { message = ex.Message, status = false, code = (int)HttpStatusCode.InternalServerError });
            }
        }




    }

}
