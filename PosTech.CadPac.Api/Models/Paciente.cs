using System.ComponentModel.DataAnnotations;

namespace PosTech.CadPac.Api.Models
{
    public class Paciente : Pessoa
    {
        public List<LancamentoMedico> Historico { get; set; }

        
    }
}