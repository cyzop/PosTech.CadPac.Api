using PosTech.CadPac.Domain.Shared.Entities;

namespace PosTech.CadPac.Domain.Entities
{
    public class Paciente : Entity
    {
        public Paciente(string nome, DateTime dataNascimento, string email, string responsavel)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Email = email;
            Responsavel = responsavel;
            HistoricoMedico = new List<RegistroMedico>();
        }

        public string Id { 
            get; 
            private set; }
        public string Nome { 
            get; 
            private set; }

        public DateTime DataNascimento { 
            get; 
            private set; }

        public string Email { 
            get; 
            private set; }

        public string Responsavel { 
            get; 
            private set; }

        public IEnumerable<RegistroMedico> HistoricoMedico { 
            get; 
            private set; }

        public void AddRegistroMedico(RegistroMedico registroMedico) 
            => this.HistoricoMedico.ToList().Add(registroMedico);
    }
}
