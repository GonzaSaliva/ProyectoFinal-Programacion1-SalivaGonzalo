using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProyect
{
    public class Cliente
    {
        public int Id { get; set; }
        public long? Dni { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        [EmailAddress]
        public long? Telefono { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime? FechaNac { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaUpgrade { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
