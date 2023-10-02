using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PosTech.CadPac.Domain.Shared.Entities;

namespace PosTech.CadPac.Repository.DataModel
{
    public class PacienteDataModel : Entity
    {
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

        public IEnumerable<RegistroMedicoDataModel> HistoricoMedico
        {
            get;
            private set;
        }
    }
}
