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
    public class CompraTest
    {
        [SetUp]
        public void Setup()
        {
            ArchivoCliente.LimpiarClientesJson();
            ArchivoProducto.LimpiarProductosJson();
            ArchivoCompra.LimpiarCompraJson();

            DateTime fecha = new DateTime(2001, 10, 9);
            ClienteDto clienteDto = new ClienteDto() { Dni = 43494390, Nombre = "Gonzalo", Apellido = "Saliva", Email = "gonzisaliva@gmail.com", FechaNacimiento = Convert.ToDateTime("09/10/2001 00:00:00"), Telefono = 15567150, Latitud = -31.25033, Longitud = -61.4867, }; ;
            ClienteService clienteService = new ClienteService();
            clienteService.CrearCliente(clienteDto);

            ProductoDto productoDto = new ProductoDto() { Nombre = "Tornillosss", AltoCaja = 25, AnchoCaja = 25, CantidadStock = 200, Marca = "TornillosSA", PrecioUnitario = 200, ProfundidadCaja = 30, StockMinimo = 25 };
            ProductoService productoService = new ProductoService();
            productoService.AgregarProducto(productoDto);

        }

        [Test]
        public void TestGenerarCompraDeberiaDevolverHttpStatusCode200()
        {
            // arrange
            CompraService serviceCompra = new CompraService();
            DateTime fechaEntrega = new DateTime(2023, 11, 9);
            CompraDto compraDto = new CompraDto() { Cantidad = 15, CodProducto = 1, DniCliente = 43494390, FechaEntrega = fechaEntrega };

            // act
            ResultadoResponse res = serviceCompra.GenerarCompra(compraDto);

            //assert
            Assert.That(res.Result.Message, Is.EqualTo("La compra se registro con exito"));
        }

        [Test]
        public void TestObtenerListaComprasDeberiaDevolverHttpStatusCode200()
        {
            //arrange
            CompraService serviceCompra = new CompraService();
            DateTime fechaEntrega = new DateTime(2023, 11, 9);
            CompraDto compraDto = new CompraDto() { Cantidad = 15, CodProducto = 1, DniCliente = 43494390, FechaEntrega = fechaEntrega };
            serviceCompra.GenerarCompra(compraDto); 

            // act
            List<CompraDto> listadoCompras = serviceCompra.ObtenerListaCompras();

            // Assert
            Assert.That(listadoCompras.Count, Is.EqualTo(1));

        }

        [Test]
        public void TestObtenerListaComprasDeberiaDevolverHttpStatusCode400()
        {
            //arrange
            CompraService serviceCompra = new CompraService();

            // act
            List<CompraDto> listadoCompras = serviceCompra.ObtenerListaCompras();

            // Assert
            Assert.That(listadoCompras.Count, Is.EqualTo(0));

        }
    }
}
