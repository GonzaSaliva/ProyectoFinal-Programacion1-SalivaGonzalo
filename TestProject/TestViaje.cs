using DataProyect;
using DTOProyect;
using ServiceProyect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestViaje
    {
        [SetUp]
        public void SetUp()
        {
            ArchivoCliente.LimpiarClientesJson();
            ArchivoProducto.LimpiarProductosJson();
            ArchivoCompra.LimpiarCompraJson();
            ArchivoViaje.LimpiarViajesJson();

            DateTime fecha = new DateTime(2001, 10, 3);
            ClienteDto clienteDto = new ClienteDto() { Dni = 43494390, Nombre = "Gonzalo", Apellido = "Saliva", Email = "gonzisaliva@gmail.com", FechaNacimiento = Convert.ToDateTime("09/10/2001 00:00:00"), Telefono = 15567150, Latitud = -31.25033, Longitud = -61.4867, }; ;
            ClienteService clienteService = new ClienteService();
            clienteService.CrearCliente(clienteDto);

            ProductoDto productoDto = new ProductoDto() { Nombre = "Tornillosss", AltoCaja = 25, AnchoCaja = 25, CantidadStock = 200, Marca = "TornillosSA", PrecioUnitario = 200 ,ProfundidadCaja = 30, StockMinimo = 25 };
            ProductoService productoService = new ProductoService();
            productoService.AgregarProducto(productoDto);

            CompraService serviceCompra = new CompraService();
            DateTime fechaEntrega = new DateTime(2023, 11, 3);
            CompraDto compraDto = new CompraDto() { Cantidad = 15, CodProducto = 1, DniCliente = 43494390, FechaEntrega = fechaEntrega };
            serviceCompra.GenerarCompra(compraDto);

            CompraDto compraDto1 = new CompraDto() { Cantidad = 17, CodProducto = 1, DniCliente = 54672389, FechaEntrega = fechaEntrega };
            serviceCompra.GenerarCompra(compraDto1);
        }

        [Test]
        public void TestGenerarViajeDeberiaDevolverOK()
        {   
            //arrange
            DateTime fechaDesde = new DateTime(2023, 11, 2);
            DateTime fechaHasta = new DateTime(2023, 11, 4);
            ViajeService viajeService = new ViajeService();

            //act
            ResultadoValidacion res = viajeService.CrearViaje(fechaDesde, fechaHasta);

            //assert
            Assert.That(res.Success, Is.True);
        }
    }
}
