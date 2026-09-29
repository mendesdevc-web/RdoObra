using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;
using Rdo.Service.Service.ServicoService;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly IServicoService _servicoService;

        public ServicosController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        [HttpGet("{obraId}/servicos")]
        public async Task<IActionResult> GetServicos(Guid obraId)
        {
            var resultado = await _servicoService.BuscarServicosPorObra(obraId);

            return Ok(resultado);
        }

        [HttpPost("{obraId}/servicos")]
        public async Task<IActionResult> CriarServico(Guid obraId,ServicosDto servicoDto)
        {
            var resultado = await _servicoService.CriarServico(obraId, servicoDto);

            return Ok(resultado);
        }

        [HttpPut("{obraId}/servicos/{id}")]
        public async Task<IActionResult> EditarServico(Guid obraId, Guid idServico, ServicosDto servicoDto)
        {
            var resultado = await _servicoService.EditarServico(obraId, idServico, servicoDto);
            return Ok(resultado);

        }

         [HttpDelete("{obraId}/servicos/{id}")]
        public async Task<IActionResult> ExcluirServico(Guid obraId, Guid idServico)
        {
            var resultado = await _servicoService.ExcluirServico(obraId, idServico);
            return Ok(resultado);
        }
    }
}
