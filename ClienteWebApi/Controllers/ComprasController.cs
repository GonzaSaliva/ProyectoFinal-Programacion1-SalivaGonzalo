using DataProyect;
using DTOProyect;
using Microsoft.AspNetCore.Mvc;
using ServiceProyect;
using System.Net;

namespace ClienteWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComprasController : ControllerBase
    {
        CompraService compraService = new CompraService();

        [HttpPost]
        public IActionResult GenerarCompra([FromBody] CompraDto compraDto)
        {
            ResultadoResponse res = compraService.GenerarCompra(compraDto);

            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            } else if (res.Result.Estado == HttpStatusCode.NotFound)
            {
                return NotFound(res.Result.Message);
            }
            return BadRequest(res.Result.Message);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ObtenerCompraId(int id)
        {
            ResultadoResponse res = compraService.ObtenerCompraPorId(id);

            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            }
            return NotFound(res.Result.Message);
        }

        [HttpGet]
        public IActionResult ObtenerListadoCompras()
        {
            List<CompraDto> listadoCompras = compraService.ObtenerListaCompras();
            if (listadoCompras.Count != 0)
            {
                return Ok(listadoCompras);
            }
            return NotFound("No se encontraron clientes registrados en el sistema.");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult EliminarCompra(int id)
        {
            ResultadoResponse res = compraService.EliminarCompra(id);
            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            }
            return NotFound(res.Result.Message);
        }
    }
}
