using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;
using PosTech.CadPac.Repository.DataModel;

namespace PosTech.CadPac.Repository.Converter
{
    public class PacienteDataModelConverter : IConverter<PacienteDataModel, Paciente>
    {
        private readonly IConverter<RegistroMedicoDataModel, RegistroMedico> _registroMedicoConverter;

        public PacienteDataModelConverter(IConverter<RegistroMedicoDataModel, RegistroMedico> registroMedicoConverter)
        {
            _registroMedicoConverter = registroMedicoConverter;
        }

        public Paciente Convert(PacienteDataModel origing)
        {
            var retorno = new Paciente(origing.Id, origing.Nome, origing.DataNascimento, origing.Email, origing.Responsavel);

            foreach (RegistroMedicoDataModel item in origing.HistoricoMedico)
                retorno.AddRegistroMedico(_registroMedicoConverter.Convert(item));

            return retorno;
        }
    }
}
