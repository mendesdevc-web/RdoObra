using Rdo.Dominio.Entidades;
using Rdo.Dominio.Repository;
using Rdo.Service.DTOs;
using Rdo.Service.DTOs.Result;


namespace Rdo.Service.Service.ObraService
{
    public class ObraService : IObraService
    {
        private readonly IObraRepository _obraRepository;
        public ObraService(IObraRepository obraRepository)
        {
            _obraRepository = obraRepository;
        }
        public async Task<Result<object>> BuscarTodasObras()
        {
            var obras = await _obraRepository.BuscarObras();
            return Result<object>.Success(obras); 
        }
        public async Task<Result<object>> BuscarObraId(Guid id)
        {
            var obras = await _obraRepository.BuscarObraId(id);

            if (obras == null)
                return Result<object>.Failure("Id não encontrado");

            return Result<object>.Success(obras);

        }
        public Task<Result<object>> BuscarObraEmpresa(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<object>> CriarObra(ObrasDto obrasDto)
        {
            var obraExiste = await _obraRepository.BuscarNomeObra(
           obrasDto.Nome);

            if (obraExiste != null)
            {
                return Result<object>.Failure("Ja possui obra com esse nome");
            }

            var obra = new ObraEntidade
            {
                Nome = obrasDto.Nome,
                Endereco = obrasDto.Endereco,
                ResponsavelTecnico = obrasDto.ResponsavelTecnico,
                Status = obrasDto.Status,
                DataInicio = obrasDto.DataInicio,
                DataFim = obrasDto.DataInicio
            };

            var obraCriada = await _obraRepository.CriarObra(obra);

            return Result<Object>.Success(new
            {
                obraCriada.Nome,
                obraCriada.Endereco,
                obraCriada.ResponsavelTecnico,
                obraCriada.Status,
                obraCriada.DataInicio,
                obra.DataFim
            });
        }

        public async Task<Result<object>> EditarObra(Guid id,ObrasDto obrasDto)
        {
            var obra = await _obraRepository.BuscarObraId(id);

            if (obra == null)
                return Result<object>.Failure("Obra não encontrada.");
            
            obra.Nome = obrasDto.Nome;
            obra.Endereco = obrasDto.Endereco;
            obra.ResponsavelTecnico = obrasDto.ResponsavelTecnico;
            obra.Status = obrasDto.Status;
            obra.DataInicio = obrasDto.DataInicio;
            obra.DataFim = obrasDto.DataFim;


            var obraEditada = await _obraRepository.EditarObra(id, obra);

            return Result<object>.Success(new
            {
                obraEditada.Id,
                obraEditada.Nome,
                obraEditada.Endereco,
                obraEditada.ResponsavelTecnico,
                obraEditada.Status,
                obraEditada.DataInicio,
                obraEditada.DataFim
            });
        }
    }
}
