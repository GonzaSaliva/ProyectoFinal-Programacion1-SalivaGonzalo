using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProyect;
using DTOProyect;
using Newtonsoft.Json;
using System.Drawing;
using DataProyect;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace ServiceProyect
{
    public class ViajeService
    {
        public List<Viaje> ObtenerViajes()
        {
            return ArchivoViaje.LeerViajeDesdeArchivoJson().Where(x => x.FechaEliminacion == null).ToList();
        }

        public ResultadoValidacion CrearViaje(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Viaje> listadoViajes = ObtenerViajes();
            List<Camioneta> listadoCamionetas = ArchivoCamioneta.LeerCamionetaDesdeArchivoJson();
            List<Compra> listadoCompras = ArchivoCompra.LeerCompraDesdeArchivoJson().Where(x => x.EstadoCompra == EnumEstadoCompra.OPEN).ToList();
            if (listadoCompras.Count == 0)
            {
                return new ResultadoValidacion() { Success = false, Message = "No se encontraron compras para realizar los viajes", Estado = HttpStatusCode.NotFound };
            }

            List<Cliente> listadoClientes = ArchivoCliente.LeerClienteDesdeArchivoJson().Where(x => x.FechaEliminacion == null).ToList();
            if (listadoClientes.Count == 0)
            {
                return new ResultadoValidacion() { Success = false, Message = "No se encontraron clientes para asignar los viajes", Estado = HttpStatusCode.NotFound };
            }

            List<Producto> listadoProductos = ArchivoProducto.LeerDesdeArchivoJson().Where(x => x.FechaEliminacion == null).ToList();
            if (listadoProductos.Count == 0)
            {
                return new ResultadoValidacion() { Success = false, Message = "No se encontraron productos para asignar los viajes", Estado = HttpStatusCode.NotFound };
            }

            foreach (Camioneta camioneta in listadoCamionetas)
            {
                Viaje viaje = new Viaje();
                viaje.IdCamion = camioneta.Patente;
                viaje.FechaRegistro = DateTime.Now;
                viaje.FechaEliminacion = null;
                viaje.FechaEntregaHasta = fechaHasta;
                viaje.FechaEntregaDesde = fechaDesde;
                viaje.ListadoCompras = new List<Compra>();

                double? volumenDisponible = camioneta.TamanioCarga;

                foreach (Compra compra in listadoCompras)
                {
                    double distanciaCompra = compra.ObtenerDistanciaCompraEnKilometros();
                    
                    if (distanciaCompra < camioneta.MaximoRecorridoKms)
                    {
                        double? volumenCompra = ObtenerTamanioCompra(compra.CodProducto, compra.Cantidad);
                        if ((volumenDisponible - volumenCompra) >= 0)
                        {
                            volumenDisponible -= volumenCompra;
                            viaje.IdCamion = camioneta.Patente;
                            viaje.ListadoCompras.Add(compra);
                            compra.EstadoCompra = EnumEstadoCompra.READY_TO_DISPATCH;
                            ArchivoCompra.GuardarCompraEnArchivoJson(compra);
                        } else
                        {
                            break;
                        }
                    }
                }

                double? tamañoFinalDelViaje = camioneta.TamanioCarga - volumenDisponible;
                viaje.OcupacionCarga = (tamañoFinalDelViaje * 100) / camioneta.TamanioCarga;

                viaje.FechaEntregaDesde = fechaDesde;
                viaje.FechaEntregaHasta = fechaHasta;
                viaje.FechaRegistro = DateTime.Now;

                listadoViajes.Add(viaje);
            }

            double? ObtenerTamanioCompra(int? codProd, int? cantidadProducto)
            {
                Producto producto = listadoProductos.Find(x => x.CodigoAutoincremetnal == codProd);

                return producto.Volumen * cantidadProducto;
            }

            foreach (Compra compra in listadoCompras)
            {
                if (compra.EstadoCompra == EnumEstadoCompra.OPEN)
                {
                    compra.FechaEntrega = compra.FechaEntrega.AddDays(14);
                    ArchivoCompra.GuardarCompraEnArchivoJson(compra);
                }
            }

            return new ResultadoValidacion() { Success = true, Message = "Se creo el viaje con exito", Estado = HttpStatusCode.OK };
        }

        public List<ViajeDto> getViajesServices()
        {
            return ArchivoViaje.LeerViajeDesdeArchivoJson().Where(x => x.FechaEliminacion == null).Select(x => new ViajeDto() { FechaDesde = x.FechaEntregaDesde, FechaHasta = x.FechaEntregaHasta }).ToList();
        }
    }
}
