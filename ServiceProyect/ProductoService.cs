using DataProyect;
using DTOProyect;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProyect
{
    public class ProductoService
    {
        public ResultadoResponse AgregarProducto(ProductoDto prod)
        {
            List<Producto> products = ArchivoProducto.LeerDesdeArchivoJson();

            if (!products.Any(x => x.Nombre == prod.Nombre && x.FechaEliminacion == null))
            {
                Producto productoDb = new Producto();
                productoDb.Nombre = prod.Nombre;
                productoDb.AnchoCaja = prod.AnchoCaja;
                productoDb.AltoCaja = prod.AltoCaja;
                productoDb.ProfundidadCaja = prod.ProfundidadCaja;
                productoDb.CantidadStock = prod.CantidadStock;
                productoDb.Marca = prod.Marca;
                productoDb.CantidadStock = prod.CantidadStock;
                productoDb.PrecioUnitario = prod.PrecioUnitario;
                productoDb.StockMinimo = prod.StockMinimo;

                ArchivoProducto.GuardarEnArchivoJson(productoDb);

                return new ResultadoResponse() { Result = new ResultadoValidacion() { Success = true, Message = "El producto se agrego con exito", Estado = HttpStatusCode.OK }, Objeto = prod };
            }
            return new ResultadoResponse() { Result = new ResultadoValidacion() { Success = false, Message = "El producto con el nombre ingresado ya se encuentra en el sistema", Estado = HttpStatusCode.BadRequest } };
        }
        public ResultadoResponse ActualizarStockProducto(int id, int stock)
        {
            List<Producto> products = ArchivoProducto.LeerDesdeArchivoJson();
            Producto producto = products.Find(x => x.CodigoAutoincremetnal == id && x.FechaEliminacion == null);

            if (producto != null)
            {
                producto.CantidadStock = stock;
                producto.FechaUpgrade = DateTime.Now;
                ArchivoProducto.GuardarEnArchivoJson(producto);
                ProductoDto prod = new ProductoDto()
                {
                    Nombre = producto.Nombre,
                    Marca = producto.Marca,
                    AltoCaja = producto.AltoCaja,
                    AnchoCaja = producto.AnchoCaja,
                    ProfundidadCaja = producto.ProfundidadCaja,
                    PrecioUnitario = producto.PrecioUnitario,
                    StockMinimo = producto.StockMinimo,
                    CantidadStock = producto.CantidadStock,
                };
                return new ResultadoResponse() { Result = new ResultadoValidacion() { Success = true, Message = "El producto se actualizo con exito", Estado = HttpStatusCode.OK }, Objeto = prod };
            }
            return new ResultadoResponse() { Result = new ResultadoValidacion() { Success = false, Message = "No se encontro el producto que desea actualizar", Estado = HttpStatusCode.NotFound } };
        }

        public List<ProductoDto> ObtenerProductos()
        {
            return ArchivoProducto.LeerDesdeArchivoJson()
                .Where(x => x.FechaEliminacion == null)
                .Select(x => new ProductoDto()
                {
                    Nombre = x.Nombre,
                    Marca = x.Marca,
                    AltoCaja = x.AltoCaja,
                    AnchoCaja = x.AnchoCaja,
                    ProfundidadCaja = x.ProfundidadCaja,
                    PrecioUnitario = x.PrecioUnitario,
                    CantidadStock = x.CantidadStock,
                    StockMinimo = x.StockMinimo
                }).ToList();
        }
    }
}

