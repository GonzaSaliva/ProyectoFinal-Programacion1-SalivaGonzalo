using DataProyect;
using DTOProyect;
using Microsoft.AspNetCore.Mvc;
using ServiceProyect;

namespace ClienteWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        ProductoService productoService = new ProductoService();

        [HttpPost]
        public IActionResult AgregarProducto([FromBody] ProductoDto prod)
        {
            ResultadoResponse res = productoService.AgregarProducto(prod);

            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            }
            return BadRequest(res.Result.Message);
        }

        [HttpPut]
        [Route("{id}/{stock}")]
        public IActionResult ActualizarProducto(int id, int stock)
        {
            ResultadoResponse res = productoService.ActualizarStockProducto(id, stock);
            if (res.Result.Success)
            {
                return Ok(res.Objeto);
            }
            return NotFound(res.Result.Message);
        }

        [HttpGet]
        public IActionResult ObtenerProductos()
        {
            List<ProductoDto> listado = productoService.ObtenerProductos();

            if (listado.Count > 0)
            {
                return Ok(listado);
            }
            return NotFound("No se encuentra ningun producto guardado en el sistema");
        }

    }
}

