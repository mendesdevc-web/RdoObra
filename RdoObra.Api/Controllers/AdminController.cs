using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Infra;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/admin/obras/resumo")]
        public async Task<IActionResult> ResumoObras()
        {
            var obras = await _context.Obras
                .Include(o => o.Servicos)
                    .ThenInclude(s => s.Apontamentos)
                .ToListAsync();

            var resultado = obras.Select(o =>
            {
                var quantidadeOrcada = o.Servicos
                    .Sum(s => s.QuantidadeOrcada);

                var quantidadeExecutada = o.Servicos
                    .Sum(s => s.Apontamentos
                        .Sum(a => a.QuantidadeExecutada));

                var percentualAvanco = quantidadeOrcada > 0
                    ? (quantidadeExecutada / quantidadeOrcada) * 100
                    : 0;

                return new
                {
                    o.Id,
                    o.Nome,
                    o.Status,
                    PercentualAvanco = percentualAvanco
                };
            });

            return Ok(resultado);
        }

        [HttpGet("/api/admin/diarios/pendentes")]
        public async Task<IActionResult> DiariosPendentes()
        {
            var diarios = await _context.Diarios
                .Include(d => d.Obra)
                .Where(d => d.Status == "enviado")
                .OrderBy(d => d.ObraId)
                .ThenBy(d => d.Data)
                .ToListAsync();

            return Ok(diarios);
        }

        [HttpGet("/api/admin/obras/alertas")]
        public async Task<IActionResult> AlertasObras()
        {
            var limite = DateTime.Now.AddDays(-7);

            var obras = await _context.Obras
                .Include(o => o.Diarios)
                .ToListAsync();

            var alertas = obras
                .Where(o => !o.Diarios.Any(d => d.Data >= limite))
                .Select(o => new
                {
                    o.Id,
                    o.Nome,
                    o.Status,
                    UltimoDiario = o.Diarios
                        .OrderByDescending(d => d.Data)
                        .Select(d => d.Data)
                        .FirstOrDefault()
                })
                .ToList();

            return Ok(alertas);
        }
    }
}
