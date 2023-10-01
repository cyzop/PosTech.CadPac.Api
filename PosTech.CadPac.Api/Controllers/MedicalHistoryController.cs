using Microsoft.AspNetCore.Mvc;
using PosTech.CadPac.Api.Models;

namespace PosTech.CadPac.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicalHistoryController : ControllerBase
    {
        private List<Paciente> pacientes = new List<Paciente>();

        public MedicalHistoryController(List<Paciente> pacientes)
        {
            this.pacientes = pacientes;
        }

        [HttpGet]
        [Route("patient/{idUsuario}")]
        public IActionResult GetHistoriUsuario(string idUsuario)
        {
            var paciente = pacientes.Find(p => p.Id == idUsuario);
            if (paciente != null)
                return Ok(paciente.Historico);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{idUsuario}/{id}")]
        public IActionResult GetLancamento(string idUsuario, string id)
        {
            var paciente = pacientes.Find(p => p.Id == idUsuario);
            if (paciente != null)
            {
                var lancamento = paciente.Historico.Find(h => h.id == id);
                return Ok(lancamento);
            }
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{idUsuario}/{id}")]
        public IActionResult DeleteLancamento(string idUsuario, string id)
        {
            var paciente = pacientes.Find(p => p.Id == idUsuario);
            if (paciente != null)
            {
                var lancamento = paciente.Historico.Find(h => h.id == id);
                if (lancamento != null)
                {
                    paciente.Historico.Remove(lancamento);
                    return Ok(paciente.Historico);
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Route("idUsuario")]
        public IActionResult PostLancamento(string idUsuario, LancamentoMedico lancamento)
        {
            if (ModelState.IsValid)
            {
                var paciente = pacientes.Find(p => p.Id == idUsuario);
                if (paciente != null)
                {
                    if (paciente.Historico == null)
                        paciente.Historico = new List<LancamentoMedico>();

                    paciente.Historico.Add(lancamento);

                    return Ok(paciente.Historico);
                }

                return NotFound();
            }
            else
            {
                var erros = ModelState.Values.Select(x => x.Errors[0].ErrorMessage).ToList();
                return BadRequest(new { 
                    PayloadErros = erros
                });
            }
        }
    }
}
