using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRest.API.Model.Generales
{
    public class Response
    {
       public object model {  get; set; }
       public int code {  get; set; }
       public string message { get; set; }
       public bool status { get; set; }

        public static implicit operator string(Response v)
        {
            throw new NotImplementedException();
        }
    }
}
