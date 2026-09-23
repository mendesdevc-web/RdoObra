using Rdo.Service.DTOs.Response;
using Rdo.Service.DTOs.Usuarios;


namespace Rdo.Service.Service.AuthService
{
    public interface IAuthsService
    {
        Task<ResponseDto<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegistro);
        Task<ResponseDto<string>> Login(UsuarioLoginDto usuarioLogin);
    }
}
