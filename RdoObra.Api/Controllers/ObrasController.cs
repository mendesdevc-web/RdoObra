using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ObrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetObras()
        {
            var obras = await _context.Obras
                .AsNoTracking()
                .ToListAsync();

            return Ok(obras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetObra(Guid id)
        {
            var obra = await _context.Obras
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null)
                return NotFound();

            return Ok(obra);
        }

        [HttpPost]
        public async Task<IActionResult> CriarObra(ObraEntidade obra)
        {
            _context.Obras.Add(obra);

            await _context.SaveChangesAsync();

            return Ok(obra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarObra(Guid id, ObraEntidade obra)
        {
            var obraExistente = await _context.Obras
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obraExistente == null)
                return NotFound();

            obraExistente.Nome = obra.Nome;
            obraExistente.Endereco = obra.Endereco;
            obraExistente.ResponsavelTecnico = obra.ResponsavelTecnico;
            obraExistente.Status = obra.Status;
            obraExistente.DataInicio = obra.DataInicio;
            obraExistente.DataFim = obra.DataFim;

            await _context.SaveChangesAsync();

            return Ok(obraExistente);
        }
    }
}
