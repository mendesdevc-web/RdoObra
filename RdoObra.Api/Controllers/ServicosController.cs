using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{obraId}/servicos")]
        public async Task<IActionResult> GetServicos(Guid obraId)
        {
            var servicos = await _context.Servicos
                .AsNoTracking()
                .Where(s => s.ObraId == obraId)
                .ToListAsync();

            return Ok(servicos);
        }

        [HttpPost("{obraId}/servicos")]
        public async Task<IActionResult> CriarServico(Guid obraId,ServicosDto servicoDto)
        {
            var obra = await _context.Obras
                .FirstOrDefaultAsync(o => o.Id == obraId);

            if (obra == null)
                return NotFound("Obra não encontrada.");

            var servico = new ServicoEntidade
            {
                ObraId = obraId,
                Descricao = servicoDto.Descricao,
                UnidadeMedida = servicoDto.UnidadeMedida,
                QuantidadeOrcada = servicoDto.QuantidadeOrcada,
                PesoOrcamento = servicoDto.PesoOrcamento
            };

            _context.Servicos.Add(servico);

            await _context.SaveChangesAsync();

            return Ok(servico);
        }

        [HttpPut("{obraId}/servicos/{id}")]
        public async Task<IActionResult> EditarServico(Guid obraId, Guid id,ServicosDto servicoDto)
        {
            // Procura o serviço dentro da obra informada
            var servico = await _context.Servicos
                .FirstOrDefaultAsync(s =>
                    s.Id == id &&
                    s.ObraId == obraId);

            if (servico == null)
                return NotFound("Serviço não encontrado.");

            servico.Descricao = servicoDto.Descricao;
            servico.UnidadeMedida = servicoDto.UnidadeMedida;
            servico.QuantidadeOrcada = servicoDto.QuantidadeOrcada;
            servico.PesoOrcamento = servicoDto.PesoOrcamento;

            await _context.SaveChangesAsync();

            return Ok(servico);
        }

        [HttpDelete("{obraId}/servicos/{id}")]
        public async Task<IActionResult> ExcluirServico(Guid obraId, Guid id)
        {
            // Procura o serviço dentro da obra
            var servico = await _context.Servicos
                .FirstOrDefaultAsync(s =>
                    s.Id == id &&
                    s.ObraId == obraId);

            if (servico == null)
                return NotFound("Serviço não encontrado.");

            // Verifica se o serviço possui apontamentos
            var possuiApontamentos = await _context.Apontamentos
                .AnyAsync(a => a.ServicoId == id);

            if (possuiApontamentos)
                return BadRequest("Não é possível excluir o serviço porque existem apontamentos vinculados.");

            // Remove o serviço
            _context.Servicos.Remove(servico);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
