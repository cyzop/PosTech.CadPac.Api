using PosTech.CadPac.Domain.Entities;

namespace PosTech.CadPac.Domain.Repositories
{
    public interface IPacienteRepository
    {
        public List<Paciente> GetAll();
        public Paciente GetById(string id);
        public List<Paciente> FindByName(string name);
        
        public Paciente UpSert(Paciente paciente);
        public void Delete(string id);
        
    }
}