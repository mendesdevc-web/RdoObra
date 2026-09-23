using Rdo.Dominio.Entidades;
using Rdo.Service.DTOs.Result;
using Rdo.Service.DTOs.Usuarios;


namespace Rdo.Service.Service.UsuariosService
{
    public interface IUsuarioService
    {
        Task<Result<object>> BuscarUsuarios();
        Task<Result<object>> CriarUsuario(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<Result<object>> EditarUsuario(int id,UsuarioEditarDto usuarioEditarDto);

    }
}
