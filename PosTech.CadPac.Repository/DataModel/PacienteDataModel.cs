using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PosTech.CadPac.Domain.Shared.Entities;

namespace PosTech.CadPac.Repository.DataModel
{
    public class PacienteDataModel : Entity
    {
        public PacienteDataModel(string id, string nome, DateTime dataNascimento, string email, string responsavel)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Email = email;
            Responsavel = responsavel;
            HistoricoMedico = new List<RegistroMedicoDataModel>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get;
            private set;
        }
        public string Nome
        {
            get;
            private set;
        }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DataNascimento
        {
            get;
            private set;
        }
        public string Email
        {
            get;
            private set;
        }

        public string Responsavel
        {
            get;
            private set;
        }

        public List<RegistroMedicoDataModel> HistoricoMedico
        {
            get;
            private set;
        }

        public void AddRegistroMedico(RegistroMedicoDataModel registroMedicoDataModel)
        {
            this.HistoricoMedico.Add(registroMedicoDataModel);
        }
    }
}
