using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;

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
        public async Task<IActionResult> BuscarObras()
        {
            var obras = await _context.Obras
                .AsNoTracking()
                .ToListAsync();

            return Ok(obras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscaObra(Guid id)
        {
            var obra = await _context.Obras
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null)
                return NotFound();

            return Ok(obra);
        }

        [HttpPost]
        public async Task<IActionResult> CriarObra(ObrasDto obraDto)
        {
            var obra = new ObraEntidade
            {
                Nome = obraDto.Nome,
                Endereco = obraDto.Endereco,
                ResponsavelTecnico = obraDto.ResponsavelTecnico,
                Status = obraDto.Status,
                DataInicio = obraDto.DataInicio,
                DataFim = obraDto.DataFim
            };

            _context.Obras.Add(obra);

            await _context.SaveChangesAsync();

            return Ok(obra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarObra(Guid id, ObrasDto obraDto)
        {
            var obra = await _context.Obras.FirstOrDefaultAsync(u => u.Id == id);

            if (obra == null)
                return NotFound("Obra não encontrata");

            obra.Nome = obraDto.Nome;
            obra.Endereco = obraDto.Endereco;
            obra.ResponsavelTecnico = obraDto.ResponsavelTecnico;
            obra.Status = obraDto.Status;
            obra.DataInicio = obraDto.DataInicio;
            obra.DataFim = obraDto.DataFim;

            await _context.SaveChangesAsync();

            return Ok(obra);
        }
    }
}
