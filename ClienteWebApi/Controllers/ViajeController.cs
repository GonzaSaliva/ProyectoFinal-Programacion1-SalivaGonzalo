using DataProyect;
using DTOProyect;
using ServiceProyect;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ClienteWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViajeController : ControllerBase
    {
        ViajeService viajeService = new();

        [HttpPost]
        public IActionResult GenerarViaje(ViajeDto viaje)
        {
            ResultadoValidacion res = viajeService.CrearViaje(viaje.FechaDesde, viaje.FechaHasta);

            if (res.Success)
            {
                return Ok(res.Message);
            }
            else if (res.Estado == HttpStatusCode.NotFound)
            {
                return NotFound(res.Message);
            }
            return BadRequest(res.Message);
        }

        [HttpGet]
        public IActionResult GetViajes()
        {
            List<ViajeDto> listadoViajes = viajeService.getViajesServices();

            if (listadoViajes.Count == 0)
            {
                return NotFound("No se encuentran viajes registrados");
            }
            return Ok(listadoViajes);
        }
    }
}
