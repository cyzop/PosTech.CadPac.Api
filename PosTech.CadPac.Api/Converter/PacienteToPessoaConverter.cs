using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;

namespace PosTech.CadPac.Api.Converter
{
    public class PacienteToPessoaDtoConverter : IConverter<Paciente, PessoaDto>
    {
        public PessoaDto Convert(Paciente origing) => new PessoaDto()
        {
            Id = origing.Id,
            Nome = origing.Nome,
            DataNascimento = origing.DataNascimento,
            Email = origing.Email,
            Responsavel = origing.Responsavel
        };
    }
}
