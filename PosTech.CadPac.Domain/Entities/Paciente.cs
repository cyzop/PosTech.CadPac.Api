using PosTech.CadPac.Domain.Shared.Entities;

namespace PosTech.CadPac.Domain.Entities
{
    public class Paciente : Entity
    {
        public Paciente(string id, string nome, DateTime dataNascimento, string email, string responsavel)
        {
            Id = id;
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
        { 
            this.HistoricoMedico = this.HistoricoMedico.Concat(new[] { registroMedico });
        }

        public void DeleteRegistroMedico(RegistroMedico registroMedico)
        {
            var lista = this.HistoricoMedico.ToList();
            lista.Remove(registroMedico);
            this.HistoricoMedico = lista;
        }

        public void SetNome(string nome)
        {
            Nome = nome;
        }

        public void SetDataNascimento(DateTime dataNascimento)
        {
            DataNascimento = dataNascimento;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetResponsavel(string responsavel)
        {
            Responsavel = responsavel;
        }
    }
}
