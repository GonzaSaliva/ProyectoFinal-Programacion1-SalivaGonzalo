using DataProyect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DTOProyect;

namespace ServiceProyect
{
    public class CompraService
    {
        public ResultadoResponse GenerarCompra(CompraDto compra)
        {

            Cliente cliente = ArchivoCliente.LeerClienteDesdeArchivoJson().Find(x => x.Dni == compra.DniCliente && x.FechaEliminacion == null);

            Producto producto = ArchivoProducto.LeerDesdeArchivoJson().Find(x => x.CodigoAutoincremetnal == compra.CodProducto && x.FechaEliminacion == null);

            if (producto == null)
            {
                return new ResultadoResponse { Result = new ResultadoValidacion() { Success = false, Message = "El producto ingresado en la compra no corresponde a ningun producto del sistema", Estado = HttpStatusCode.BadRequest } };
            }

            if (producto.CantidadStock < compra.Cantidad)
            {
                return new ResultadoResponse { Result = new ResultadoValidacion() { Success = false, Message = "No se encuentra stock suficiente del producto para generar la compra.", Estado = HttpStatusCode.BadRequest } };
            }

            if (cliente == null)
            {
                return new ResultadoResponse { Result = new ResultadoValidacion() { Success = false, Message = "El dni ingresado para la compra no corresponde a ningun cliente.", Estado = HttpStatusCode.BadRequest } };
            }

            producto.CantidadStock -= compra.Cantidad;

            ArchivoProducto.GuardarEnArchivoJson(producto);

            Compra compraDb = new Compra();
            compraDb.CodProducto = compra.CodProducto;
            compraDb.Cantidad = compra.Cantidad;
            compraDb.DniCliente = compra.DniCliente;
            compraDb.EstadoCompra = (EnumEstadoCompra)1;
            compraDb.FechaCompra = DateTime.Now;
            compraDb.FechaEntrega = compra.FechaEntrega;
            compraDb.MontoTotal = compraDb.CalcularMontoFinal((int)compra.Cantidad,(double)(producto.PrecioUnitario));
            compraDb.PuntoDestino.Add(cliente.Latitud);
            compraDb.PuntoDestino.Add(cliente.Longitud);

            ArchivoCompra.GuardarCompraEnArchivoJson(compraDb);
            return new ResultadoResponse { Result = new ResultadoValidacion() { Success = true, Message = "La compra se registro con exito", Estado = HttpStatusCode.OK }, Objeto = compra };
        }

        public List<CompraDto> ObtenerListaCompras()
        {
            return ArchivoCompra.LeerCompraDesdeArchivoJson()
                .Where(x => x.FechaEliminacion == null)
                .Select(x => new CompraDto()
                {
                    CodProducto = x.CodProducto,
                    DniCliente = x.DniCliente,
                    Cantidad = x.Cantidad,
                    FechaEntrega = x.FechaEntrega,
                }).ToList();
        }

        public ResultadoResponse ObtenerCompraPorId(int id)
        { 
            Compra compraDb = ArchivoCompra.LeerCompraDesdeArchivoJson().FirstOrDefault(x => x.Id == id && x.FechaEliminacion == null);

            if (compraDb != null)
            {
                CompraDto compraDto = new CompraDto();
                compraDto.CodProducto = compraDb.CodProducto;
                compraDto.DniCliente = compraDb.DniCliente;
                compraDto.Cantidad = compraDb.Cantidad;
                compraDto.FechaEntrega = compraDb.FechaEntrega;

                return new ResultadoResponse() { Result = new ResultadoValidacion() { Success = true, Message = $"Compra con id: {compraDb.Id} ", Estado = HttpStatusCode.OK }, Objeto = compraDto };
            }
            return new ResultadoResponse() { Result = new ResultadoValidacion() { Success = false, Message = "El id ingresado no corresponde a ninguna compra registrada en el sistema", Estado = HttpStatusCode.NotFound } };
        }

        public ResultadoResponse EliminarCompra(int id)
        {
            Compra compra = ArchivoCompra.LeerCompraDesdeArchivoJson().Find(x => x.Id == id && x.FechaEliminacion == null);

            if (compra != null)
            {
                compra.FechaEliminacion = DateTime.Now;
                ArchivoCompra.GuardarCompraEnArchivoJson(compra);

                CompraDto compraDto = new CompraDto();
                compraDto.CodProducto = compra.CodProducto;
                compraDto.DniCliente = compra.DniCliente;
                compraDto.Cantidad = compra.Cantidad;
                compraDto.FechaEntrega = compra.FechaEntrega;
                return new ResultadoResponse() { Result = new ResultadoValidacion() { Success = true, Message = "La compra se elimino con exito", Estado = HttpStatusCode.OK }, Objeto = compraDto };
            }
            return new ResultadoResponse() { Result = new ResultadoValidacion() { Success = false, Message = "El id ingresado no corresponde a ninguna compra registrada en el sistema", Estado = HttpStatusCode.NotFound }};
        }
    }
}
