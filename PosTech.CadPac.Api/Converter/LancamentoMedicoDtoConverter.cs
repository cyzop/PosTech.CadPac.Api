using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;
using static PosTech.CadPac.Domain.Shared.Enum.Enum;

namespace PosTech.CadPac.Api.Converter
{
    public class LancamentoMedicoDtoConverter : IConverter<LancamentoMedicoDto, RegistroMedico>
    {
        public RegistroMedico Convert(LancamentoMedicoDto origing) => new RegistroMedico(
                origing.Id, 
                origing.Data, 
                origing.Texto, 
                (TipoRegistroMedico) System.Enum.Parse<TipoRegistroMedico>(origing.Tipo));
        
    }
}
