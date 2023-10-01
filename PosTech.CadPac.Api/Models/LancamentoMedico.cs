using PosTech.CadPac.Api.ValidationAttribute;
using System.ComponentModel.DataAnnotations;
using static PosTech.CadPac.Api.Models.Enum;

namespace PosTech.CadPac.Api.Models
{
    public class LancamentoMedico
    {
        public string id { get;set; }
        [Required]
        public DateTime Data { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        public string Texto { get; set; }

        [Required]
        [TipoLancamentoMedicoValidation]
        public TipoLancamentoMedico Tipo { get; set; }
    }
}