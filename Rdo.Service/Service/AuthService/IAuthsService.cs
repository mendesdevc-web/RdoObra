using Rdo.Service.DTOs;
using Rdo.Service.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.Service.AuthService
{
    public interface IAuthsService
    {
        Task<ResponseDto<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegistro);
        Task<ResponseDto<string>> Login(UsuarioLoginDto usuarioLogin);
    }
}
