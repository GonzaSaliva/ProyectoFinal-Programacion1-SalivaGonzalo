using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProyect
{
    public class Compra
    {
        public int Id { get; set; }
        public int? CodProducto { get; set; }
        public long? DniCliente { get; set; }
        public DateTime FechaCompra { get; set; }
        public int? Cantidad { get; set; }
        public DateTime FechaEntrega { get; set; }
        public EnumEstadoCompra? EstadoCompra { get; set; }
        public double? MontoTotal { get; set; } 
        public List<double> PuntoDestino { get; set; }
        public DateTime? FechaUpgrade { get; set; }
        public DateTime? FechaEliminacion { get; set; }

        public double CalcularMontoFinal(int cantidad, double precio)
        {
            double monto = cantidad * precio;
            monto += monto * 21 / 100;

            if (cantidad > 4)
            {
                monto -= monto * 25 / 100;
            }
            return monto;
        }

        public double ObtenerDistanciaCompraEnKilometros()
        {
            const double radioTierraKilometros = 6371; // Radio promedio de la Tierra en kilómetros
            double latitud = PuntoDestino[0];
            double longitud = PuntoDestino[1];


            // Convertir las latitudes y longitudes de grados a radianes
            double latitudRadianes1 = ConvertirAGradosARadianes(-31.26706);
            double longitudRadianes1 = ConvertirAGradosARadianes(-61.496384);
            double latitudRadianes2 = ConvertirAGradosARadianes(latitud);
            double longitudRadianes2 = ConvertirAGradosARadianes(longitud);

            // Diferencias en latitud y longitud
            double diferenciaLatitud = latitudRadianes2 - latitudRadianes1;
            double diferenciaLongitud = longitudRadianes2 - longitudRadianes1;

            // Fórmula del semiverseno
            double a = Math.Sin(diferenciaLatitud / 2) * Math.Sin(diferenciaLatitud / 2) +
                       Math.Cos(latitudRadianes1) * Math.Cos(latitudRadianes2) *
                       Math.Sin(diferenciaLongitud / 2) * Math.Sin(diferenciaLongitud / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distancia = radioTierraKilometros * c;

            return distancia;
        }

        private double ConvertirAGradosARadianes(double grados)
        {
            return grados * Math.PI / 180.0;
        }
    }
}
