using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Repositories;
using PosTech.CadPac.Domain.Services;

namespace PosTech.CadPac.Services
{
    public class CadastroPacienteService : ICadastroPacienteService
    {
        private readonly IPacienteRepository _repository;

        public CadastroPacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Paciente> GetAll()
        {
            return _repository.GetAll();
        }
        public IEnumerable<RegistroMedico> GetHistoricoMedico(string id)
        {
            var paciente = _repository.GetById(id);
            return paciente?.HistoricoMedico;
        }

        public RegistroMedico GetLancamentoMedico(string pacienteId, string id)
        {
            var paciente = _repository.GetById(pacienteId);
            if (paciente != null)
            {
                return paciente.HistoricoMedico.ToList().Find( h=> h.Id == id);
            }
            else
                return null;
        }

        public Paciente GetPaciente(string id)
        {
            return _repository.GetById(id);
        }

        public void RemoveLancamentoMedico(string pacienteId, string lancamentoId)
        {
            var paciente = _repository.GetById(pacienteId);
            var lancamento = paciente.HistoricoMedico.Where(h => h.Id == lancamentoId).First();
            if (lancamento != null)
            {
                paciente.HistoricoMedico.ToList().Remove(lancamento);
                _repository.UpSert(paciente);
            }
        }

        public void RemovePaciente(string id)
        {
            _repository.Delete(id);
        }

        public RegistroMedico SaveLancamentoMedico(string pacienteId, RegistroMedico registroMedico)
        {
            if (registroMedico.Id == null || !Guid.TryParse(registroMedico.Id, out _))
                registroMedico.SetId(Guid.NewGuid().ToString());

            var paciente = _repository.GetById(pacienteId);
            paciente.AddRegistroMedico(registroMedico);
            _repository.UpSert(paciente);

            return registroMedico;
        }

        public Paciente SavePaciente(Paciente paciente)
        {
            return _repository.UpSert(paciente);
        }

        public Paciente UpdatePacienteData(Paciente paciente)
        {
            //Find paciente data
            var pacienteAlterar = _repository.GetById(paciente.Id);

            if (pacienteAlterar != null)
            {
                pacienteAlterar.SetNome(paciente.Nome);
                pacienteAlterar.SetResponsavel(paciente.Responsavel);
                pacienteAlterar.SetDataNascimento(paciente.DataNascimento);
                pacienteAlterar.SetEmail(paciente.Email);

                return _repository.UpSert(pacienteAlterar);
            }
            else
                return pacienteAlterar;
        }
    }
}
