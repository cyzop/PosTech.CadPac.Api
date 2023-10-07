using PosTech.CadPac.Domain.Entities;
using System.IO.Pipes;

namespace PosTech.CadPac.Domain.Services
{
    public interface ICadastroPacienteService
    {
        IEnumerable<Paciente> GetAll();
        Paciente GetPaciente(string id);
        Paciente SavePaciente(Paciente paciente);

        Paciente UpdatePacienteData(Paciente paciente);

        void RemovePaciente(string id);

        IEnumerable<RegistroMedico> GetHistoricoMedico(string id);
        RegistroMedico GetLancamentoMedico(string pacienteId, string id);
        RegistroMedico SaveLancamentoMedico(string pacienteId, RegistroMedico registroMedico);

        void RemoveLancamentoMedico(string pacienteId, string lancamentoId);
        
    }
}
