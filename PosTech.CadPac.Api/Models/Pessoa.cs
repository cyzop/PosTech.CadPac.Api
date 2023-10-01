using System.ComponentModel.DataAnnotations;

namespace PosTech.CadPac.Api.Models
{
    public class Pessoa
    {

        public string Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required] 
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Email { get; set; }

        public string Responsavel { get; set; }



    }
}
