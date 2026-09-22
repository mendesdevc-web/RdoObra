using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;
using Rdo.Service.DTOs.ObrasUsuarioDtos;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraUsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ObraUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{usuarioId}/vinculos")]
        public async Task<IActionResult> BuscarUsuarioObra(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var vinculos = await _context.ObrasUsuarios
                .Where(ou => ou.UsuarioId == usuarioId)
                .Include(ou => ou.Obra)
                .ToListAsync();

            return Ok(vinculos);
        }


        [HttpPost]
        public async Task<IActionResult> CriarVinculo(ObraUsuarioDto vinculoDto)
        {
            var obra = await _context.Obras
                .FirstOrDefaultAsync(o => o.Id == vinculoDto.ObraId);

            if (obra == null)
                return NotFound("Obra não encontrada.");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == vinculoDto.UsuarioId);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var vinculoExistente = await _context.ObrasUsuarios
                .FirstOrDefaultAsync(ou =>
                    ou.ObraId == vinculoDto.ObraId &&
                    ou.UsuarioId == vinculoDto.UsuarioId);

            if (vinculoExistente != null)
                return BadRequest("Usuário já está vinculado a esta obra.");

            var vinculo = new ObraUsuarioEntidade
            {
                ObraId = vinculoDto.ObraId,
                UsuarioId = vinculoDto.UsuarioId,
                Papel = vinculoDto.Papel
            };

            _context.ObrasUsuarios.Add(vinculo);

            await _context.SaveChangesAsync();

            return Ok(vinculo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarPapel(Guid id, AlterarUsuarioObraDto dto)
        {
            var vinculo = await _context.ObrasUsuarios
                .FirstOrDefaultAsync(ou => ou.Id == id);

            if (vinculo == null)
                return NotFound("Vínculo não encontrado.");

            vinculo.Papel = dto.Papel;

            await _context.SaveChangesAsync();

            return Ok(vinculo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverVinculo(Guid id)
        {
            var vinculo = await _context.ObrasUsuarios
                .FirstOrDefaultAsync(ou => ou.Id == id);

            if (vinculo == null)
                return NotFound("Vínculo não encontrado.");

            _context.ObrasUsuarios.Remove(vinculo);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
