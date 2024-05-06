using APIRest.API.Model.Conexion;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRest.API.Model.Colegio.Implementacion
{
    public class AlumnosDataMapper :IAlumnosDataMapper
    {
        private readonly ConexionSql _conexionSql;

        private IDbConnection connection;

        public AlumnosDataMapper(ConexionSql conexionSql) 
        { 

           _conexionSql= conexionSql;
           connection = _conexionSql.CreateConnection();

        }

        public async Task<int> AgregarAlumnos(Alumnos alumno)

        {

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@Nombre", alumno.Nombre);
            dynamicParameters.Add("@Apellido", alumno.Apellido);
            dynamicParameters.Add("@F_Nacimiento", alumno.F_Nacimiento);
            dynamicParameters.Add("@idCarrera", alumno.idCarrera);
            dynamicParameters.Add("@Telefono", alumno.Telefono);

            int result = Convert.ToInt32(await connection.ExecuteScalarAsync("[dbo].[Alumnos_INSERT]", dynamicParameters, commandType: CommandType.StoredProcedure));

            return result;

        }

        public async Task<bool> AgregarAlumnos(string jsonAlumnos)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@JSONDATA", jsonAlumnos);        
           
           await connection.ExecuteScalarAsync("[dbo].[Alumnos_INSERT_JSON]", dynamicParameters, commandType: CommandType.StoredProcedure);

            return true;
        } 

        public async Task<List<AlumnosExtend>> ObtenerAlumnosFiltro(int? idAlumno = null, int? idCarrera = null)
        {
            List<AlumnosExtend> listaAlumnosExtend = new List<AlumnosExtend>();

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@idAlumno", idAlumno);
            dynamicParameters.Add("@idCarrera", idCarrera);

            var result = await connection.QueryAsync<AlumnosExtend>("[dbo].[Alumnos_SELECT]", dynamicParameters, commandType: CommandType.StoredProcedure);

            listaAlumnosExtend = result.ToList(); 
           
            return listaAlumnosExtend;

        }

        public async Task<bool> ActualizarAlumnos(Alumnos alumno)
        {
            bool respuesta = false;


            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@idAlumno", alumno.idAlumno);
            dynamicParameters.Add("@Nombre", alumno.Nombre);
            dynamicParameters.Add("@Apellido", alumno.Apellido);
            dynamicParameters.Add("@F_Nacimiento", alumno.F_Nacimiento);
            dynamicParameters.Add("@idCarrera", alumno.idCarrera);
            dynamicParameters.Add("@Telefono", alumno.Telefono);

            await connection.ExecuteScalarAsync("[dbo].[Alumnos_UPDATE]", dynamicParameters, commandType: CommandType.StoredProcedure);

            return true;

        }

        public async Task<bool> EliminarAlumno(int idAlumno)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@idAlumno", idAlumno);

                int rowsAffected = await connection.ExecuteAsync("[dbo].[EliminarAlumnoPorId]", dynamicParameters, commandType: CommandType.StoredProcedure);

                return rowsAffected > 0; 
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al eliminar el alumno: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> ActualizarAlumno(int idAlumno, Alumnos alumno)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@idAlumno", idAlumno);
                dynamicParameters.Add("@Nombre", alumno.Nombre);
                dynamicParameters.Add("@Apellido", alumno.Apellido);
                dynamicParameters.Add("@F_Nacimiento", alumno.F_Nacimiento);
                dynamicParameters.Add("@idCarrera", alumno.idCarrera);
                dynamicParameters.Add("@Telefono", alumno.Telefono);

                int rowsAffected = await connection.ExecuteAsync("[dbo].[ActualizarAlumnoPorId]", dynamicParameters, commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el alumno: {ex.Message}");
                return false;
            }
        }

    }
}
