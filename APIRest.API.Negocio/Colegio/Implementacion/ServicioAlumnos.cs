using APIRest.API.Model.Colegio.Implementacion;
using APIRest.API.Model.Colegio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIRest.API.Model.Generales;
using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Data.Common;
using Dapper;

namespace APIRest.API.Negocio.Colegio.Implementacion
{


    public class ServicioAlumnos : IServicioAlumnos

    {
        private readonly IAlumnosDataMapper _alumnosDataMapper;

        public ServicioAlumnos(IAlumnosDataMapper alumnosDataMapper) {



            _alumnosDataMapper = alumnosDataMapper;

        }


        public async Task<Response> AgregarAlumnos(List<Alumnos> listaAlumnos)
        {
            Response response = new Response { status = false, code = (int)HttpStatusCode.InternalServerError };

            List<AlumnosExtend> listaAlumnosExtend = new List<AlumnosExtend>();


            try {

                string JsonAlumnos = JsonConvert.SerializeObject(listaAlumnos);

                await _alumnosDataMapper.AgregarAlumnos(JsonAlumnos);

                listaAlumnosExtend = await _alumnosDataMapper.ObtenerAlumnosFiltro();


                response.model = listaAlumnos;
                response.code = (int)HttpStatusCode.OK;
                response.status = true;

            } catch (Exception ex) {

                response.code = (int)HttpStatusCode.InternalServerError;
                response.message = ex.Message;
                response.status = true;
            }


            return response;

        }

        public async Task<Response> ObtenerAlumnosFiltro(int? idAlumno = null, int? idCarrera = null)
        {
            Response response = new Response { status = false, code = (int)HttpStatusCode.InternalServerError };

            List<AlumnosExtend> listaAlumnos = new List<AlumnosExtend>();


            try
            {

                listaAlumnos = await _alumnosDataMapper.ObtenerAlumnosFiltro(idAlumno, idCarrera);

                foreach (AlumnosExtend alumno in listaAlumnos)
                {

                    alumno.Carrera = JsonConvert.DeserializeObject<List<Carrera>>(alumno.carreraJSON).FirstOrDefault();


                }


                if (!listaAlumnos.Any())
                {
                    response.code = (int)HttpStatusCode.NoContent;
                    response.status = false;

                    return response;

                }

                response.model = listaAlumnos;
                response.code = (int)HttpStatusCode.OK;
                response.status = true;

            }
            catch (Exception ex)
            {

                response.code = (int)HttpStatusCode.InternalServerError;
                response.message = ex.Message;
                response.status = true;
            }

            return response;

        }


        public async Task<Response> ActualizarAlumnos(Alumnos alumnos)
        {

            Response response = new Response { status = false, code = (int)HttpStatusCode.InternalServerError };

            List<AlumnosExtend> listaAlumnos = new List<AlumnosExtend>();

            try
            {
                await _alumnosDataMapper.ActualizarAlumnos(alumnos);

                listaAlumnos = await _alumnosDataMapper.ObtenerAlumnosFiltro();

                if (!listaAlumnos.Any())
                {
                    response.code = (int)HttpStatusCode.NoContent;
                    response.status = false;

                    return response;

                }

                response.model = listaAlumnos;
                response.code = (int)HttpStatusCode.OK;
                response.status = true;

            }
            catch (Exception ex) {

                response.code = (int)HttpStatusCode.InternalServerError;
                response.message = ex.Message;
                response.status = false;

            }

            return response;
        }

        public async Task<Response> ObtenerTodosAlumnos()
        {
            Response response = new Response { status = false, code = (int)HttpStatusCode.InternalServerError };

            try
            {
                List<AlumnosExtend> listaAlumnos = await _alumnosDataMapper.ObtenerAlumnosFiltro();

                if (listaAlumnos == null || listaAlumnos.Count == 0)
                {
                    response.code = (int)HttpStatusCode.NoContent;
                    response.status = false;
                    return response;
                }


                var detallesAlumnos = listaAlumnos.Select(alumno => new
                {
                    idAlumno = alumno.idAlumno,
                    Nombre = alumno.Nombre,
                    Apellido = alumno.Apellido,
                    F_Nacimiento = alumno.F_Nacimiento,
                    idCarrera = alumno.idCarrera,
                    Telefono = alumno.Telefono
                }).ToList();

                response.model = detallesAlumnos;
                response.code = (int)HttpStatusCode.OK;
                response.status = true;
            }
            catch (Exception ex)
            {
                response.code = (int)HttpStatusCode.InternalServerError;
                response.message = ex.Message;
                response.status = true;
            }

            return response;
        }

        public async Task<Response> EliminarAlumno(int idAlumno)
        {
            Response response = new Response { status = false, code = (int)HttpStatusCode.InternalServerError };

            try
            {
                bool eliminado = await _alumnosDataMapper.EliminarAlumno(idAlumno);
                if (eliminado)
                {
                    response.code = (int)HttpStatusCode.OK;
                    response.status = true;
                }
                else
                {
                    response.code = (int)HttpStatusCode.NotFound;
                    response.message = "No se encontró el alumno .";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
        public async Task<Response> ActualizarAlumno(int idAlumno, Alumnos alumno)
        {
            Response response = new Response { status = false, code = (int)HttpStatusCode.InternalServerError };

            try
            {
                bool actualizado = await _alumnosDataMapper.ActualizarAlumno(idAlumno, alumno);
                if (actualizado)
                {
                    response.code = (int)HttpStatusCode.OK;
                    response.status = true;
                }
                else
                {
                    response.code = (int)HttpStatusCode.NotFound;
                    response.message = "No se encontró el alumno.";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }



    }
}
