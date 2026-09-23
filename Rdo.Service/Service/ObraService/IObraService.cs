using Rdo.Service.DTOs;
using Rdo.Service.DTOs.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rdo.Service.Service.ObraService
{
    public interface IObraService
    {
        Task<Result<object>>BuscarTodasObras();
        Task<Result<object>> BuscarObraId(Guid id);
        Task<Result<object>> BuscarObraEmpresa(string id);
        Task<Result<object>> CriarObra(ObrasDto obrasDto);
        Task<Result<object>> EditarObra(Guid id,ObrasDto obrasDto);

    }
}
