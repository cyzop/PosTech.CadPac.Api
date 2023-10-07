using PosTech.CadPac.Domain.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace PosTech.CadPac.Api.Models
{
    public class PessoaDto : Entity
    {
        public string? Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required] 
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Email { get; set; }

        public string Responsavel { get; set; }



    }
}
