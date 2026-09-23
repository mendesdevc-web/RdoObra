using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;
using Rdo.Dominio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UsuarioEntidade usuario)
        {
            await _context.AddAsync(usuario);
        }
        public async Task UpdateAsync(UsuarioEntidade usuario)
        {
            _context.Update(usuario);
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<UsuarioEntidade>> BuscarUsuarios()
        {
            var usuarios = await _context.Usuarios.AsNoTracking().ToListAsync();

            return (usuarios);
        }

        public async Task<UsuarioEntidade?> BuscarUsuarioPorEmail(string email)
        {
            var usuario= await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return (usuario);
        }

        public async Task<UsuarioEntidade?> BuscarUsuarioPorId(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UsuarioEntidade> CriarUsuario(UsuarioEntidade usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioEntidade> EditarUsuario(int id, UsuarioEntidade usuario)
        {
            var usuarioBanco = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuarioBanco == null)
                return null;

            usuarioBanco.Email = usuario.Email;
            usuarioBanco.UsuarioNome = usuario.UsuarioNome;
            usuarioBanco.Cargo = usuario.Cargo;
            usuarioBanco.SenhaHash = usuario.SenhaHash;
            usuarioBanco.SenhaSalt = usuario.SenhaSalt;

            await _context.SaveChangesAsync();

            return usuarioBanco;
        }
    }
}
