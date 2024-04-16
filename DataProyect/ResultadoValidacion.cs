using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataProyect
{
    public class ResultadoValidacion
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Estado { get; set; }
    }
}
