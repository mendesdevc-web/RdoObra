using Rdo.Dominio.Entidades;
using Rdo.Dominio.Repository;
using Rdo.Service.DTOs.Result;
using Rdo.Service.DTOs.Usuarios;
using Rdo.Service.Service.SenhaService.SenhaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.Service.UsuariosService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhasService _senhasService;
        public UsuarioService(IUsuarioRepository usuarioRepository, ISenhasService senhasService)
        {
            _usuarioRepository = usuarioRepository;
            _senhasService = senhasService;
        }
        public async Task<Result<object>> BuscarUsuarios()
        {
            var usuarios = await _usuarioRepository.BuscarUsuarios();
            
            return Result<object>.Success(usuarios);
        }

        public async Task<Result<object>> CriarUsuario(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var usuarioExistente = await _usuarioRepository.BuscarUsuarioPorEmail(
            usuarioCriacaoDto.Email);

            if (usuarioExistente != null)
            {
                return Result<object>.Failure("E-mail já cadastrado.");
            }

            var usuario = new UsuarioEntidade
            {
                Email = usuarioCriacaoDto.Email,
                UsuarioNome = usuarioCriacaoDto.Usuario,
                Cargo = usuarioCriacaoDto.Cargo
            };

            var usuarioCriado = await _usuarioRepository.CriarUsuario(usuario);

            return Result<object>.Success(new
            {
                usuarioCriado.Id,
                usuarioCriado.Email,
                usuarioCriado.UsuarioNome,
                usuarioCriado.Cargo
            });
        }

        public async Task<Result<object>> EditarUsuario(int id, UsuarioEditarDto usuarioEditarDto)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioPorId(id);

            if (usuario == null)
            {
                return Result<object>.Failure(
                    "Usuário não encontrado.");
            }

            usuario.Email = usuarioEditarDto.Email;
            usuario.UsuarioNome = usuarioEditarDto.UsuarioNome;
            usuario.Cargo = usuarioEditarDto.Cargo;

            if (!string.IsNullOrWhiteSpace(usuarioEditarDto.Senha))
            {
                _senhasService.CriarSenhaHash(
                    usuarioEditarDto.Senha,
                    out byte[] senhaHash,
                    out byte[] senhaSalt);

                usuario.SenhaHash = senhaHash;
                usuario.SenhaSalt = senhaSalt;
            }

            var usuarioEditado =
                await _usuarioRepository.EditarUsuario(id, usuario);

            return Result<object>.Success(new
            {
                usuarioEditado!.Id,
                usuarioEditado.Email,
                usuarioEditado.UsuarioNome,
                usuarioEditado.Cargo
            });
        }
    }
}
