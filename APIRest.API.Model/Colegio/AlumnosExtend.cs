using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRest.API.Model.Colegio
{
    public class AlumnosExtend : Alumnos
    {
        public Carrera? Carrera { get; set; }

        public string carreraJSON { get; set; } 
                

    }
}
