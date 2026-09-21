using Rdo.Dominio.Entidades;
using Rdo.Infra;
using Rdo.Service.DTOs;
using Rdo.Service.DTOs.Response;
using Rdo.Service.Service.SenhaService.SenhaService;
using Microsoft.EntityFrameworkCore;
using System;

namespace Rdo.Service.Service.AuthService
{
    public class AuthsService : IAuthsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISenhasService _senhasService;
        public AuthsService(ApplicationDbContext context, ISenhasService senhasService)
        {
            _context = context;
            _senhasService = senhasService;

        }

        public async Task<ResponseDto<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegistro)
        {
            ResponseDto<UsuarioCriacaoDto> responseService = new ResponseDto<UsuarioCriacaoDto>();

            try
            {
                if (!VerificaSeEmailUsuarioJaExiste(usuarioRegistro))
                {
                    responseService.Dados = null;
                    responseService.Status = false;
                    responseService.Messagem = "Email ja cadastrado";
                    return responseService;
                }
                _senhasService.CriarSenhaHas(usuarioRegistro.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioEntidade usuario = new UsuarioEntidade()
                {
                    UsuarioNome = usuarioRegistro.Usuario,
                    Email = usuarioRegistro.Email,
                    Cargo = usuarioRegistro.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                responseService.Messagem = "Usuário criado com sucesso!";

            }
            catch (Exception ex)
            {
                responseService.Dados = null;
                responseService.Messagem = ex.Message;
                responseService.Status = false;
            }
            return responseService;

        }

        public async Task<ResponseDto<string>> Login(UsuarioLoginDto usuarioLogin)
        {
            ResponseDto<string> responseService = new ResponseDto<string>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(userBanco =>
                                                        userBanco.Email == usuarioLogin.Email);

                if (usuario == null)
                {
                    responseService.Messagem = "Credenciais inválidas";
                    responseService.Status = false;
                    return responseService;
                }

                if (!_senhasService.VerificaSenhaHash(usuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    responseService.Messagem = "Credenciais inválidas";
                    responseService.Status = false;
                    return responseService;
                }

                var token = _senhasService.CriarToken(usuario);

                responseService.Dados = token;
                responseService.Status = true;
                responseService.Messagem = "Usuario Logado com sucesso";

            }
            catch (Exception ex)
            {
                responseService.Dados = null;
                responseService.Messagem = ex.Message;
                responseService.Status = false;

                return responseService;
            }

            return responseService;
        }

        public bool VerificaSeEmailUsuarioJaExiste(UsuarioCriacaoDto usuarioRegistro)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(userBanco =>
                    userBanco.Email == usuarioRegistro.Email ||
                    userBanco.UsuarioNome == usuarioRegistro.Usuario);

            if (usuario != null)
                return false;

            return true;
        }
    }
}