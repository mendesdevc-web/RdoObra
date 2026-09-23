using Microsoft.AspNetCore.Mvc;
using Rdo.Infra;
using Rdo.Service.DTOs.Usuarios;
using Rdo.Service.Service.SenhaService.SenhaService;
using Rdo.Service.Service.UsuariosService;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISenhasService _senhaService;
        private readonly ApplicationDbContext _context;

        public UsuarioController(IUsuarioService usuarioService, ISenhasService senhasService, ApplicationDbContext context)
        {
            _usuarioService = usuarioService;
            _senhaService = senhasService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var resultado = await _usuarioService.BuscarUsuarios();

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var resultado = await _usuarioService.CriarUsuario(usuarioCriacaoDto);

            return Ok(resultado);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(int id, UsuarioEditarDto usuarioDto)
        {
            var resultado = await _usuarioService.EditarUsuario(id, usuarioDto);

            return Ok(resultado);
        }
    }
}
