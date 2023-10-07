using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;
using static PosTech.CadPac.Api.Models.Enum;

namespace PosTech.CadPac.Api.Converter
{
    public class LancamentoMedicoConverter : IConverter<RegistroMedico, LancamentoMedicoDto>
    {
        public LancamentoMedicoDto Convert(RegistroMedico origing) => new LancamentoMedicoDto() { 
                Id = origing.Id,
                Data = origing.Data,
                Texto = origing.Texto,
                Tipo = System.Enum.Parse<TipoLancamentoMedicoDto>(origing.Tipo.ToString()).ToString(),
            };
    }
}
