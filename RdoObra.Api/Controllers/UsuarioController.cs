using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;
using Rdo.Service.DTOs.Response;
using Rdo.Service.Service.SenhaService.SenhaService;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISenhasService _senhaService;

        public UsuarioController(ApplicationDbContext context, ISenhasService senhasService)
        {
            _context = context;
            _senhaService = senhasService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _context.Usuarios
                .AsNoTracking()
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriacaoDto usuarioDto)
        {
            if (usuarioDto.Senha != usuarioDto.ConfirmarSenha)
                return BadRequest("As senhas não coincidem");

            //Gerando senha hash e salt
            _senhaService.CriarSenhaHas(usuarioDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            var usuario = new UsuarioEntidade
            {
                UsuarioNome = usuarioDto.Usuario,
                Email = usuarioDto.Email,
                Cargo = usuarioDto.Cargo,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt,
                TokenDataCriacao = DateTime.Now
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(int id, UsuarioCriacaoDto usuarioDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            // Verifica se as senhas coincidem
            if (usuarioDto.Senha != usuarioDto.ConfirmarSenha)
                return BadRequest("As senhas não coincidem.");

            usuario.UsuarioNome = usuarioDto.Usuario;
            usuario.Email = usuarioDto.Email;
            usuario.Cargo = usuarioDto.Cargo;

            _senhaService.CriarSenhaHas(usuarioDto.Senha, out byte[] senhaHash,out byte[] senhaSalt);

            usuario.SenhaHash = senhaHash;
            usuario.SenhaSalt = senhaSalt;
            usuario.TokenDataCriacao = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(usuario);
        }
    }
}
