using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DiariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/obras/{obraId}/diarios")]
        public async Task<IActionResult> ListarDiarios(Guid obraId,string? status)
        {
            var diarios = _context.Diarios
                .Where(d => d.ObraId == obraId);

            if (!string.IsNullOrEmpty(status))
            {
                diarios = diarios
                    .Where(d => d.Status == status);
            }

            var resultado = await diarios
                .ToListAsync();

            return Ok(resultado);
        }

        [HttpGet("/api/diarios/{id}")]
        public async Task<IActionResult> BuscarDiario(Guid id)
        {
            var diario = await _context.Diarios
                .Include(d => d.Apontamentos)
                .Include(d => d.HistoricosStatus)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diario == null)
                return NotFound("Diário não encontrado.");

            return Ok(diario);
        }

        [HttpPost("/api/obras/{obraId}/diarios")]
        public async Task<IActionResult> CriarDiario(Guid obraId,DiarioDto diarioDto)
        {
            var obra = await _context.Obras
                .FirstOrDefaultAsync(o => o.Id == obraId);

            if (obra == null)
                return NotFound("Obra não encontrada.");


            var diario = new DiarioEntidade
            {
              ObraId = obraId,
              UsuarioId = diarioDto.UsuarioId,
              NumeroSequencial = diarioDto.NumeroSequencial,
              Data = diarioDto.Data,
              Clima = diarioDto.Clima,
              EfetivoMaoObra = diarioDto.EfetivoMaoObra,
              Equipamentos  = diarioDto.Equipamentos,
              Ocorrencias = diarioDto.Ocorrencias,
              Comentario = diarioDto.Comentario,
              Status  = "rascunho",
              CriadoEm = DateTime.Now 
            };

            _context.Diarios.Add(diario);

            await _context.SaveChangesAsync();

            return Ok(diario);
        }

        [HttpPut("/api/diarios/{id}")]
        public async Task<IActionResult> EditarDiario(Guid id,DiarioEntidade diarioAtualizado)
        {
            var diario = await _context.Diarios
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diario == null)
                return NotFound("Diário não encontrado.");

            if (diario.Status != "rascunho" &&
                diario.Status != "devolvido")
            {
                return BadRequest(
                    "O diário só pode ser editado quando estiver como rascunho ou devolvido.");
            }

            diario.Data = diarioAtualizado.Data;
            diario.Clima = diarioAtualizado.Clima;
            diario.EfetivoMaoObra = diarioAtualizado.EfetivoMaoObra;
            diario.Equipamentos = diarioAtualizado.Equipamentos;
            diario.Ocorrencias = diarioAtualizado.Ocorrencias;
            diario.Comentario = diarioAtualizado.Comentario;

            await _context.SaveChangesAsync();

            return Ok(diario);
        }

        [HttpPost("/api/diarios/{id}/enviar")]
        public async Task<IActionResult> EnviarDiario(Guid id)
        {
            var diario = await _context.Diarios
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diario == null)
                return NotFound("Diário não encontrado.");

            if (diario.Status != "rascunho" &&
                diario.Status != "devolvido")
            {
                return BadRequest(
                    "O diário só pode ser enviado quando estiver como rascunho ou devolvido.");
            }

            diario.Status = "enviado";

            await _context.SaveChangesAsync();

            return Ok(diario);
        }

        [HttpPost("/api/diarios/{id}/aprovar")]
        public async Task<IActionResult> AprovarDiario(Guid id)
        {
            var diario = await _context.Diarios
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diario == null)
                return NotFound("Diário não encontrado.");

            if (diario.Status != "enviado")
            {
                return BadRequest(
                    "O diário só pode ser aprovado quando estiver como enviado.");
            }

            diario.Status = "aprovado";

            await _context.SaveChangesAsync();

            return Ok(diario);
        }

        [HttpPost("/api/diarios/{id}/devolver")]
        public async Task<IActionResult> DevolverDiario(Guid id,string comentario)
        {
            var diario = await _context.Diarios
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diario == null)
                return NotFound("Diário não encontrado.");

            if (diario.Status != "enviado")
            {
                return BadRequest(
                    "O diário só pode ser devolvido quando estiver como enviado.");
            }

            if (string.IsNullOrWhiteSpace(comentario))
            {
                return BadRequest("O comentário é obrigatório para devolver o diário.");
            }

            diario.Status = "devolvido";
            diario.Comentario = comentario;

            await _context.SaveChangesAsync();

            return Ok(diario);
        }

        [HttpGet("/api/diarios/{id}/historico")]
        public async Task<IActionResult> BuscarHistorico(Guid id)
        {
            var diario = await _context.Diarios
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diario == null)
                return NotFound("Diário não encontrado.");

            var historico = await _context.HistoricosStatus
                .Where(h => h.DiarioId == id)
                .OrderByDescending(h => h.DataHora)
                .ToListAsync();

            return Ok(historico);
        }
    }
}
