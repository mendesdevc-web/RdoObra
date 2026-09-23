using Rdo.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Dominio.Repository
{
    public interface IUsuarioRepository
    {
        Task AddAsync(UsuarioEntidade usuario);
        Task UpdateAsync(UsuarioEntidade usuario);
        Task CommitAsync();
        Task<List<UsuarioEntidade>> BuscarUsuarios();
        Task<UsuarioEntidade?> BuscarUsuarioPorEmail(string email);
        Task<UsuarioEntidade?> BuscarUsuarioPorId(int id);
        Task<UsuarioEntidade> CriarUsuario(UsuarioEntidade usuario);
        Task<UsuarioEntidade> EditarUsuario(int id, UsuarioEntidade usuario);
    }
}
