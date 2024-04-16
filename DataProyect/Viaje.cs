using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProyect
{
    public class Viaje
    {
        public int Codigo { get; set; }
        public string? IdCamion { get; set; }
        public DateTime FechaEntregaDesde { get; set; }
        public DateTime FechaEntregaHasta { get; set; }
        public double? OcupacionCarga { get; set; }
        public List<Compra>? ListadoCompras { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaUpgrade { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
