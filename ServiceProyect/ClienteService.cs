 using DataProyect;
using DTOProyect;
using System.Net;

namespace ServiceProyect
{
    public class ClienteService
    {
        public ResultadoResponse CrearCliente(ClienteDto clienteDto)
        {
            List<Cliente> listado = ArchivoCliente.LeerClienteDesdeArchivoJson();

            if (!listado.Any(x => x.Dni == clienteDto.Dni && x.FechaEliminacion == null))
            {
                Cliente cliente1 = new Cliente();
                cliente1.Dni = clienteDto.Dni;
                cliente1.Email = clienteDto.Email;
                cliente1.Nombre = clienteDto.Nombre;
                cliente1.Apellido = clienteDto.Apellido;
                cliente1.FechaNac = clienteDto.FechaNacimiento;
                cliente1.Telefono = clienteDto.Telefono;
                cliente1.Latitud = clienteDto.Latitud;
                cliente1.Longitud = clienteDto.Longitud;
                cliente1.FechaEliminacion = null;

                ArchivoCliente.GuardarClienteEnArchivoJson(cliente1);

                return new ResultadoResponse { Result = new ResultadoValidacion() { Success = true, Message = "El cliente se ha registrado con exito", Estado = HttpStatusCode.OK }, Objeto = clienteDto };
            }
            return new ResultadoResponse { Result = new ResultadoValidacion() { Success = false, Message = "El DNI ingresado ya se encuentra registrado en el sistema", Estado = HttpStatusCode.BadRequest } };
        }

        public ResultadoResponse EliminarCliente(long dni)
        {
            List<Cliente> listado = ArchivoCliente.LeerClienteDesdeArchivoJson();
            Cliente clienteDb = listado.Find(x => x.Dni == dni && x.FechaEliminacion == null);

            if (clienteDb != null)
            {
                clienteDb.FechaEliminacion = DateTime.Now;
                ArchivoCliente.GuardarClienteEnArchivoJson(clienteDb);
                ClienteDto clienteDto = new ClienteDto() 
                {
                    Dni = clienteDb.Dni,
                    Email = clienteDb.Email,
                    Nombre = clienteDb.Nombre,
                    Apellido = clienteDb.Apellido,
                    FechaNacimiento = clienteDb.FechaNac,
                    Telefono = clienteDb.Telefono,
                    Latitud = clienteDb.Latitud,
                    Longitud = clienteDb.Longitud
                };
                
                return new ResultadoResponse { Result = new ResultadoValidacion() { Success = true, Message = "El cliente se eliminó con éxito", Estado = HttpStatusCode.OK }, Objeto = clienteDto };

            }
            return new ResultadoResponse { Result = new ResultadoValidacion() { Success = false, Message = "El documento ingresado no corresponde a ningun cliente del sistema", Estado = HttpStatusCode.NotFound }};
        }

        public ResultadoResponse ActualizarCliente(long dni, ClienteDto clienteDto)
        {
            List<Cliente> listado = ArchivoCliente.LeerClienteDesdeArchivoJson();
            Cliente clienteDb = listado.Find(x => x.Dni == dni && x.FechaEliminacion == null);

            if (clienteDb != null)
            {
                clienteDb.Dni = clienteDto.Dni;
                clienteDb.Email = clienteDto.Email;
                clienteDb.Nombre = clienteDto.Nombre;
                clienteDb.Apellido = clienteDto.Apellido;
                clienteDb.FechaNac = clienteDto.FechaNacimiento;
                clienteDb.Telefono = clienteDto.Telefono;
                clienteDb.Latitud = clienteDto.Latitud;
                clienteDb.Longitud = clienteDto.Longitud;
                clienteDb.FechaUpgrade = DateTime.Now;

                ArchivoCliente.GuardarClienteEnArchivoJson(clienteDb);
                return new ResultadoResponse { Result = new ResultadoValidacion() { Success = true, Message = "El cliente se ha actualizado con exito", Estado = HttpStatusCode.OK }, Objeto = clienteDto };
            }
            return new ResultadoResponse { Result = new ResultadoValidacion() { Success = false, Message = "El documento ingresado no corresponde a ningun cliente del sistema", Estado = HttpStatusCode.NotFound } };
        }

        public List<ClienteDto> ObtenerTodosClientes()
        {
            return ArchivoCliente.LeerClienteDesdeArchivoJson()
                .Where(x => x.FechaEliminacion == null)
                .Select(x => new ClienteDto()
                {
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Email = x.Email,
                    Dni = x.Dni,
                    FechaNacimiento = x.FechaNac,
                    Telefono = x.Telefono,
                    Latitud = x.Latitud,
                    Longitud = x.Longitud,
                }).ToList();
        }
        public ResultadoResponse ObtenerClientePorDni(long dni)
        {
            List<Cliente> listado = ArchivoCliente.LeerClienteDesdeArchivoJson();
            Cliente clienteDb = listado.Find(x => x.Dni == dni && x.FechaEliminacion == null);

            if (clienteDb != null)
            {
                ClienteDto clienteDto = new ClienteDto();
                clienteDto.Dni = clienteDb.Dni;
                clienteDto.Email = clienteDb.Email;
                clienteDto.Nombre = clienteDb.Nombre;
                clienteDto.Apellido = clienteDb.Apellido;
                clienteDto.FechaNacimiento = clienteDb.FechaNac;
                clienteDto.Telefono = clienteDb.Telefono;
                clienteDto.Latitud = clienteDb.Latitud;
                clienteDto.Longitud = clienteDb.Longitud;
                return new ResultadoResponse { Result = new ResultadoValidacion() { Success = true, Message = $"Cliente con documento: {clienteDto.Dni}:", Estado = HttpStatusCode.OK }, Objeto = clienteDto};
            }
            return new ResultadoResponse { Result = new ResultadoValidacion() { Success = false, Message = "El documento ingresado no corresponde a ningun cliente del sistema", Estado = HttpStatusCode.NotFound } };
        }
    }
}
