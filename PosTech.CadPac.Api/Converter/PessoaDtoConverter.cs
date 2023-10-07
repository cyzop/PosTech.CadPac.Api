using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;

namespace PosTech.CadPac.Api.Converter
{
    public class PessoaDtoConverter : IConverter<PessoaDto, Paciente>
    {
        public Paciente Convert(PessoaDto origing) => 
            new Paciente(origing.Id, origing.Nome, origing.DataNascimento, origing.Email, origing.Responsavel);
        
    }
}
