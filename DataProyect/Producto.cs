using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProyect
{
    public class Producto
    {
        public int CodigoAutoincremetnal { get; set; }
        public string? Nombre { get; set; }
        public string? Marca { get; set; }
        public double? AltoCaja { get; set; }
        public double? AnchoCaja { get; set; }
        public double? ProfundidadCaja { get; set; }
        public double? PrecioUnitario { get; set; }
        public int? StockMinimo { get; set; }
        public int? CantidadStock { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public DateTime? FechaUpgrade { get; set; }
        public double? Volumen => AltoCaja * AnchoCaja * ProfundidadCaja;
    }
}
