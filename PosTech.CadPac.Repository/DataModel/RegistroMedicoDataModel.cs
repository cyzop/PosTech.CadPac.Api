using PosTech.CadPac.Domain.Shared.Entities;
using static PosTech.CadPac.Domain.Shared.Enum.Enum;

namespace PosTech.CadPac.Repository.DataModel
{
    public class RegistroMedicoDataModel : Entity
    {
        public RegistroMedicoDataModel(string id, DateTime data, string texto, TipoRegistroMedico tipo)
        {
            Id = id;
            Data = data;
            Texto = texto;
            Tipo = tipo;
        }

        public string Id
        {
            get;
            private set;
        }
        public DateTime Data
        {
            get;
            private set;
        }

        public string Texto
        {
            get;
            private set;
        }

        public TipoRegistroMedico Tipo
        {
            get;
            private set;
        }
    }
}
