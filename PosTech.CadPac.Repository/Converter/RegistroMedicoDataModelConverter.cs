using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;
using PosTech.CadPac.Repository.DataModel;

namespace PosTech.CadPac.Repository.Converter
{
    public class RegistroMedicoDataModelConverter : IConverter<RegistroMedicoDataModel, RegistroMedico>
    {
        public RegistroMedico Convert(RegistroMedicoDataModel origing)
        {
            return new RegistroMedico(origing.Id, origing.Data, origing.Texto, origing.Tipo);
        }
    }
}
