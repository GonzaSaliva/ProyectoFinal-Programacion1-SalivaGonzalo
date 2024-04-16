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
    public class ProductoTest
    {
        [SetUp]
        public void Setup()
        {
            ArchivoProducto.LimpiarProductosJson();
            ProductoDto productoDto = new ProductoDto() { Nombre = "Coca Cola", AltoCaja = 3, AnchoCaja = 1, CantidadStock = 500, Marca = "Coca Cola SA", PrecioUnitario = 900, ProfundidadCaja = 5, StockMinimo = 100 };
            ProductoService serviceProducto = new ProductoService();
            serviceProducto.AgregarProducto(productoDto);
        }

        [Test]
        public void TestAgregarProductoDeberiaDevolverHttpStatusCode200()
        {
            // arrange
            ProductoService serviceProducto = new ProductoService();
            
            //act
            ProductoDto productoDto = new ProductoDto() { Nombre = "Tornillosss", AltoCaja = 25, AnchoCaja = 25, CantidadStock = 30, Marca = "Tornillossa", PrecioUnitario = 200, ProfundidadCaja = 30, StockMinimo = 25 };
            ResultadoResponse res = serviceProducto.AgregarProducto(productoDto);

            //assert
            Assert.That(res.Result.Message, Is.EqualTo("El producto se agrego con exito"));

        }

        [Test]
        public void TestActualizarStockProductoDeberiaDevolverHttpStatusCode200()
        {
            // arrange
            ProductoService serviceProducto = new ProductoService();

            //act
            ResultadoResponse res = serviceProducto.ActualizarStockProducto(1, 50);

            //assert
            Assert.That(res.Result.Message, Is.EqualTo("El producto se actualizo con exito"));

        }
    }
}
