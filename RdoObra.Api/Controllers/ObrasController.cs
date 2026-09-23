using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;
using Rdo.Service.Service.ObraService;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController : ControllerBase
    {
        private readonly IObraService _obraService;

        public ObrasController(IObraService obraService)
        {
            _obraService = obraService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarObras()
        {
            var resultado = await _obraService.BuscarTodasObras();

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscaObra(Guid id)
        {
            var resultado = await _obraService.BuscarObraId(id);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CriarObra(ObrasDto obraDto)
        {
            var resultado = await _obraService.CriarObra(obraDto);

            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarObra(Guid id, ObrasDto obraDto)
        {
            var resultado = await _obraService.EditarObra(id, obraDto);
            return Ok(resultado);
        }
    }
}
