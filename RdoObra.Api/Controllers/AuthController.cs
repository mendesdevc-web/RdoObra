using Microsoft.AspNetCore.Mvc;
using Rdo.Service.DTOs.Usuarios;
using Rdo.Service.Service.AuthService;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthsService _authsService; 
        public AuthController(IAuthsService authsService)
        {
            _authsService = authsService;
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioLoginDto usuarioLogin)
        {

            var resposta = await _authsService.Login(usuarioLogin);
            return Ok(resposta);
        }


        [HttpPost("register")]
        public async Task<ActionResult>Registrar(UsuarioCriacaoDto usuarioRegistro)
        {
            var resposta = await _authsService.Registrar(usuarioRegistro);

            return Ok(resposta);
        }
        
    }
}
