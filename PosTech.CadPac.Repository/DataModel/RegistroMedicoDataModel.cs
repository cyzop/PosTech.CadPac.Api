using PosTech.CadPac.Domain.Shared.Entities;
using static PosTech.CadPac.Domain.Shared.Enum.Enum;

namespace PosTech.CadPac.Repository.DataModel
{
    public class RegistroMedicoDataModel : Entity
    {
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
