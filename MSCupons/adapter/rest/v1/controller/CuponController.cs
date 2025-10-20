using Microsoft.AspNetCore.Mvc;
using MSCupons.application.service;
using MSCupons.domain.Entity;
using System.Collections.Generic;

namespace MSCupons.adapter.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuponController : ControllerBase
    {
        private readonly CuponService _service;

        public CuponController(CuponService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cupon>> Get() => Ok(_service.ObtenerTodos());

        [HttpGet("{codigo}")]
        public ActionResult<Cupon> GetByCodigo(string codigo)
        {
            var cupon = _service.ObtenerPorCodigo(codigo);
            if (cupon == null) return NotFound("Cupón no encontrado");
            return Ok(cupon);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Cupon cupon)
        {
            _service.Agregar(cupon);
            return Ok("Cupón agregado con éxito");
        }

        [HttpPost("aplicar")]
        public ActionResult Aplicar([FromQuery] string codigo, [FromQuery] decimal monto)
        {
            var total = _service.AplicarDescuento(codigo, monto);
            return Ok(new { TotalConDescuento = total });
        }
    }
}
