using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;
using PosTech.CadPac.Repository.DataModel;

namespace PosTech.CadPac.Repository.Converter
{
    public class PacienteConverter : IConverter<Paciente, PacienteDataModel>
    {
        private readonly IConverter<RegistroMedico, RegistroMedicoDataModel> _registroMedicoConverter;

        public PacienteConverter(IConverter<RegistroMedico, RegistroMedicoDataModel> registroMedicoConverter)
        {
            _registroMedicoConverter = registroMedicoConverter;
        }

        public PacienteDataModel Convert(Paciente origing)
        {
            var retorno = new PacienteDataModel(origing.Id, origing.Nome, origing.DataNascimento, origing.Email, origing.Responsavel);
            foreach(RegistroMedico item in origing.HistoricoMedico)
                retorno.AddRegistroMedico(_registroMedicoConverter.Convert(item));

            return retorno;
        }
    }
}
