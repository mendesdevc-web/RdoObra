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
    public class ObraRepository : IObraRepository
    {
        private readonly ApplicationDbContext _context;
        public ObraRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ObraEntidade>> BuscarObras()
        {
            var obras = await _context.Obras.AsNoTracking().ToListAsync();

            return (obras);
        }
        public async Task<ObraEntidade> BuscarObraId(Guid id)
        {
            var obra = await _context.Obras.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

            return (obra);
        }

        public Task<List<ObraEntidade>> BuscarObrasOrganizacao(string id)
        {
            throw new NotImplementedException();
        }
        public async Task<ObraEntidade> CriarObra(ObraEntidade obra)
        {
            await _context.Obras.AddAsync(obra);
            await _context.SaveChangesAsync();

            return obra;
        }

        public async Task<ObraEntidade> EditarObra(Guid id, ObraEntidade obra)
        {
            var obraBanco = await _context.Obras.FirstOrDefaultAsync(o => o.Id == id);

            if (obraBanco == null)
                return null;

            obraBanco.Nome = obra.Nome;
            obraBanco.Endereco = obra.Endereco;
            obraBanco.ResponsavelTecnico = obra.ResponsavelTecnico;
            obraBanco.Status = obra.Status;
            obraBanco.DataInicio = obra.DataInicio;
            obraBanco.DataFim = obra.DataFim;

            _context.SaveChangesAsync();

            return obraBanco;
        }
        public async Task<ObraEntidade?> BuscarNomeObra(string nomeObra)
        {
            var obra = await _context.Obras.AsNoTracking().FirstOrDefaultAsync(u => u.Nome == nomeObra);

            return (obra);
        }
        public Task AddAsync(UsuarioEntidade usuario)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UsuarioEntidade usuario)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

    }
}
