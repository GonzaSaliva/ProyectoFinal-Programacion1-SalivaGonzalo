using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOProyect
{
    public class ProductoDto
    {
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El nombre debe contener solo letras.")]
        [Required(ErrorMessage = "Debe ingresar el nombre del producto")]
        public string? Nombre { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El nombre debe contener solo letras.")]
        [Required(ErrorMessage = "Debe ingresar la marca del producto")]
        public string? Marca { get; set; }

        [Range(-100.0, 100.0, ErrorMessage = "El valor debe estar entre -100 y 100.")]
        [Required(ErrorMessage = "Debe ingresar el alto de la caja")]
        public double? AltoCaja { get; set; }

        [Range(-100.0, 100.0, ErrorMessage = "El valor debe estar entre -100 y 100.")]
        [Required(ErrorMessage = "Debe ingresar el ancho de la caja")]
        public double? AnchoCaja { get; set; }

        [Range(-100.0, 100.0, ErrorMessage = "El valor debe estar entre -100 y 100.")]
        [Required(ErrorMessage = "Debe ingresar la profundidad de la caja")]
        public double? ProfundidadCaja { get; set; }

        [Range(0.01, 10000, ErrorMessage = "El valor del producto debe ser mayor a 0 y menor a 10.000.")]
        [Required(ErrorMessage = "Debe ingresar el precio")]
        public double? PrecioUnitario { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "El documento debe contener solo números.")]
        [Required(ErrorMessage = "Debe ingresar la cantidad de stock")]
        public int? CantidadStock { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "El documento debe contener solo números.")]
        [Required(ErrorMessage = "Debe ingresar el stock minimo")]
        public int? StockMinimo { get; set; }
    }
}
