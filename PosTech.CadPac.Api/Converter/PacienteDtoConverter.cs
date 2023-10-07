using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;

namespace PosTech.CadPac.Api.Converter
{
    public class PacienteDtoConverter : IConverter<PacienteDto, Paciente>
    {
        private readonly IConverter<LancamentoMedicoDto, RegistroMedico> _lancamentoConverter;

        public PacienteDtoConverter(IConverter<LancamentoMedicoDto, RegistroMedico> lancamentoConverter)
        {
            _lancamentoConverter = lancamentoConverter;
        }

        public Paciente Convert(PacienteDto origing)
        {
            var paciente = new Paciente(origing.Id, origing.Nome, origing.DataNascimento, origing.Email, origing.Responsavel);
            origing.Historico?.ForEach(p =>
            {
                paciente.AddRegistroMedico(_lancamentoConverter.Convert(p));
            });
           return paciente;
        }
    }
}
