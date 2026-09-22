using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller")]
    [ApiController]
    public class ApontamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApontamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/diarios/{diarioId}/apontamentos")]
        public async Task<IActionResult> BuscarApontamentos(Guid diarioId)
        {
            var diario = await _context.Diarios
                .FirstOrDefaultAsync(d => d.Id == diarioId);

            if (diario == null)
                return NotFound("Diário não encontrado.");

            var apontamentos = await _context.Apontamentos
                .Where(a => a.DiarioId == diarioId)
                .ToListAsync();

            return Ok(apontamentos);
        }

        [HttpPost("/api/diarios/{diarioId}/apontamentos")]
        public async Task<IActionResult> CriarApontamento(Guid diarioId, ApontamentoDtos apontamentoDto)
        {
            var diario = await _context.Diarios
                .FirstOrDefaultAsync(d => d.Id == diarioId);

            if (diario == null)
                return NotFound("Diário não encontrado.");

            var apontamento = new ApontamentoEntidade
            {
                DiarioId = diarioId,
                ServicoId = apontamentoDto.ServicoId,
                QuantidadeExecutada = apontamentoDto.QuantidadeExecutada
            };

            _context.Apontamentos.Add(apontamento);

            await _context.SaveChangesAsync();

            return Ok(apontamento);
        }


        [HttpPut("/api/diarios/{diarioId}/apontamentos/{id}")]
        public async Task<IActionResult> EditarApontamento(Guid diarioId,Guid id, ApontamentoDtos apontamentoAtualizado)
        {
            var apontamento = await _context.Apontamentos
                .FirstOrDefaultAsync(a =>
                    a.Id == id &&
                    a.DiarioId == diarioId);

            if (apontamento == null)
                return NotFound("Apontamento não encontrado.");

            apontamento.ServicoId = apontamentoAtualizado.ServicoId;
            apontamento.QuantidadeExecutada = apontamentoAtualizado.QuantidadeExecutada;

            await _context.SaveChangesAsync();

            return Ok(apontamento);
        }

        [HttpDelete("/api/diarios/{diarioId}/apontamentos/{id}")]
        public async Task<IActionResult> ExcluirApontamento(Guid diarioId,Guid id)
        {
            var apontamento = await _context.Apontamentos
                .FirstOrDefaultAsync(a =>
                    a.Id == id &&
                    a.DiarioId == diarioId);

            if (apontamento == null)
                return NotFound("Apontamento não encontrado.");

            _context.Apontamentos.Remove(apontamento);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
