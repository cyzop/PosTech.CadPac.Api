using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;
using PosTech.CadPac.Repository.DataModel;

namespace PosTech.CadPac.Repository.Converter
{
    public class RegistroMedicoConverter : IConverter<RegistroMedico, RegistroMedicoDataModel>
    {
        public RegistroMedicoDataModel Convert(RegistroMedico origing)
        {
            return new RegistroMedicoDataModel(origing.Id, origing.Data, origing.Texto, origing.Tipo);
        }
    }
}
