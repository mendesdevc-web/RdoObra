using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rdo.Dominio.Entidades;
using Rdo.Service.Service.SenhaService.SenhaService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Rdo.Service.Service.SenhaService
{
    public class SenhasService : ISenhasService
    {
        private readonly IConfiguration _config;
        public SenhasService(IConfiguration config)
        {
            _config = config;
        }

        public void CriarSenhaHas(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using var hmac = new HMACSHA512();

            senhaSalt = hmac.Key;
            senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public bool VerificaSenhaHash(string senha,byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash);
            }
        }

        public string CriarToken(UsuarioEntidade usuario)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Cargo", usuario.Cargo.ToString()),
                new Claim("Email", usuario.Email),
                new Claim("Username", usuario.Usuario)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _config.GetSection("AppSetting:Token").Value!
                )
            );

            var cred = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512Signature
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}