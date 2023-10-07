using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;

namespace PosTech.CadPac.Api.Converter
{
    public class PacienteConverter : IConverter<Paciente, PacienteDto>
    {
        private readonly IConverter<RegistroMedico, LancamentoMedicoDto> _lancamentoConverter;

        public PacienteConverter(IConverter<RegistroMedico, LancamentoMedicoDto> lancamentoConverter)
        {
            _lancamentoConverter = lancamentoConverter;
        }

        public PacienteDto Convert(Paciente origing)
        {
            var paciente = new PacienteDto()
            {
                Id = origing.Id,
                DataNascimento = origing.DataNascimento,
                Email = origing.Email,
                Nome = origing.Nome,
                Responsavel = origing.Responsavel,
                Historico = new List<LancamentoMedicoDto>()
            };

            foreach (var reg in origing.HistoricoMedico)
                paciente.Historico.Add(_lancamentoConverter.Convert(reg));

            return paciente;

        }
    }
}
