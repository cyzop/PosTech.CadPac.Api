using System.ComponentModel.DataAnnotations;
using static PosTech.CadPac.Api.Models.Enum;

namespace PosTech.CadPac.Api.ValidationAttribute
{
    public class TipoLancamentoMedicoValidation : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var enumParsed = Enum.TryParse<TipoLancamentoMedicoDto>(value?.ToString(), out var tipoLancamento);

            if (enumParsed)
                return ValidationResult.Success;
            else
            {
                var enumValues = Enum.GetValues(typeof(TipoLancamentoMedicoDto)).Cast<TipoLancamentoMedicoDto>().ToList();
                var tipo = validationContext.ObjectInstance.GetType().Name;
                var msg = $"Valor {value} inválido para o {tipo}! Tipos esperados: {string.Join(", ", enumValues)}.";
                return new ValidationResult(msg);
            }
        }
    }
}
