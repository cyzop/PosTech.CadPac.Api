

using MongoDB.Driver;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Repositories;
using PosTech.CadPac.Repository.DataModel;
using PosTech.CadPac.Repository.Extensions;

namespace PosTech.CadPac.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly IMongoCollection<PacienteDataModel> _database;

        public PacienteRepository(RepositorySettings config)
        {
            var mongoClient = new MongoClient(config.ConnectionString);
            var mongoDataBase = mongoClient.GetDatabase(config.DataBase);

            _database = mongoDataBase.GetCollection<PacienteDataModel>(config.RepositoryName);
        }

        public void Delete(string id)
        {
            //TODO:
            throw new NotImplementedException();
        }

        public List<Paciente> FindByName(string name)
        {
            //TODO:
            throw new NotImplementedException();
        }

        public List<Paciente> GetAll()
        {
            //TODO:
            throw new NotImplementedException();
        }

        public Paciente GetById(string id)
        {
            //TODO:
            throw new NotImplementedException();
        }

        public Paciente UpSert(Paciente paciente)
        {
            //TODO:
            throw new NotImplementedException();
        }
    }
}