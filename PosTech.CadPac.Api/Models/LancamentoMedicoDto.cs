using PosTech.CadPac.Api.ValidationAttribute;
using PosTech.CadPac.Domain.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace PosTech.CadPac.Api.Models
{
    public class LancamentoMedicoDto : Entity
    {
        public string? Id { get;set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        public string Texto { get; set; }

        [Required]
        [TipoLancamentoMedicoValidation]
        public string Tipo { get; set; }
    }
}