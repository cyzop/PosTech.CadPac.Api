using PosTech.CadPac.Domain.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace PosTech.CadPac.Api.Models
{
    public class PacienteDto : PessoaDto
    {
        public List<LancamentoMedicoDto> Historico { get; set; }
    }
}