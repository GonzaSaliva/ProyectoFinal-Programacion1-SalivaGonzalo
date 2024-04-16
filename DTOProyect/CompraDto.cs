using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DTOProyect
{
    public class CompraDto
    {
        public int? CodProducto { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "El documento debe contener solo números.")]
        [Required(ErrorMessage = "Debe ingresar el documento")]
        public long? DniCliente { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "El documento debe contener solo números.")]
        [Required(ErrorMessage = "Debe ingresar la cantidad")]
        public int? Cantidad { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Ingrese la fecha de nacimiento de forma correcta.")]
        [Required(ErrorMessage = "Debe ingresar la fecha de entrega")]
        public DateTime FechaEntrega { get; set; }
    }
}

