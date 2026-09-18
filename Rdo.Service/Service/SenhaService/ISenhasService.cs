using Rdo.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.Service.SenhaService.SenhaService
{
    public interface ISenhasService
    {
        void CriarSenhaHas(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
        string CriarToken(UsuarioEntidade usuario);
    }
}
