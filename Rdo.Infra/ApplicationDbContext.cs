using Microsoft.EntityFrameworkCore;
using Rdo.Dominio.Entidades;

namespace Rdo.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<UsuarioEntidade> Usuarios { get; set; }
    }
}