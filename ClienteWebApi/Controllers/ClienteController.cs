using DataProyect;
using DTOProyect;
using Microsoft.AspNetCore.Mvc;
using ServiceProyect;

namespace ClienteWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ClientesController : ControllerBase
    {
        ClienteService clienteService = new ClienteService();

        [HttpPost]
        public IActionResult CrearCliente([FromBody] ClienteDto clienteDto)
        {
            ResultadoResponse res = clienteService.CrearCliente(clienteDto);

            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            }
            return BadRequest(res.Result.Message);
        }

        [HttpGet]
        [Route("{dni}")]
        public IActionResult ObtenerClienteDni(long dni)
        {
            ResultadoResponse res = clienteService.ObtenerClientePorDni(dni);

            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            }
            return NotFound("El DNI ingresado no corresponde a ningun cliente del sistema.");
        }

        [HttpGet]
        public IActionResult ObtenerListadoClientes()
        {
            List<ClienteDto> listadoClientes = clienteService.ObtenerTodosClientes();
            if (listadoClientes.Count != 0)
            {
                return Ok(listadoClientes);
            }
            return NotFound("No se encontraron clientes registrados en el sistema.");
        }

        [HttpDelete]
        [Route("{dni}")]
        public IActionResult DeleteCliente(long dni)
        {
            ResultadoResponse res = clienteService.EliminarCliente(dni);
            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            }
            return NotFound(res.Result.Message);
        }

        [HttpPut]
        [Route("{dni}")]
        public IActionResult ActualizarCliente(long dni, [FromBody] ClienteDto cliente)
        {
            ResultadoResponse res = clienteService.ActualizarCliente(dni, cliente);

            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            }
            return NotFound(res.Result.Message);
        }
    }
}