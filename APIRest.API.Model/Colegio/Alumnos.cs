using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRest.API.Model.Colegio
{
    public class Alumnos
    {
        public int? idAlumno {  get; set; }
        public String? Nombre { get; set; }
        public String? Apellido { get; set; }
        public DateTime? F_Nacimiento { get; set; }
        public int? idCarrera { get; set; }
        public String? Telefono { get; set; }


    }
}
