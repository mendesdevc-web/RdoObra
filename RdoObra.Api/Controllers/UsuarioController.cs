using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rdo.Service.DTOs.Response;

namespace RdoObra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
       [Authorize]
       [HttpGet]
       public ActionResult<ResponseDto<string>> BuscarUsuario()
        {
            ResponseDto<string> response = new ResponseDto<string>();
            response.Messagem = "Acessei";

            return Ok(response);
        }
    }
}
