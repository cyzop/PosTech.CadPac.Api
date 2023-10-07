using Microsoft.AspNetCore.Mvc;
using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Services;
using PosTech.CadPac.Domain.Shared.Converter;

namespace PosTech.CadPac.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly ICadastroPacienteService _pacientes;
        private readonly ILogger<MedicalHistoryController> _logger;

        private readonly IConverter<RegistroMedico, LancamentoMedicoDto> _lancamentoConverter;
        private readonly IConverter<LancamentoMedicoDto, RegistroMedico> _lancamentoDtoConverter;

        public MedicalHistoryController(ICadastroPacienteService pacientes, ILogger<MedicalHistoryController> logger, IConverter<RegistroMedico, LancamentoMedicoDto> lancamentoConverter, IConverter<LancamentoMedicoDto, RegistroMedico> lancamentoDtoConverter)
        {
            _pacientes = pacientes;
            _logger = logger;
            _lancamentoConverter = lancamentoConverter;
            _lancamentoDtoConverter = lancamentoDtoConverter;
        }

        [HttpGet]
        [Route("patient/{idUsuario}")]
        public IActionResult GetHistoricoUsuario(string idUsuario)
        {
            _logger.LogInformation("GetHistoricoUsuario", idUsuario);

            var historico = _pacientes.GetHistoricoMedico(idUsuario);
            if (historico != null)
            {
                return Ok(historico.Select(e => _lancamentoConverter.Convert(e)));
            }
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{idUsuario}/{id}")]
        public IActionResult GetLancamento(string idUsuario, string id)
        {

            var lancamento = _pacientes.GetLancamentoMedico(idUsuario, id);
            if (lancamento != null)
                return Ok(_lancamentoConverter
                    .Convert(lancamento));
            else
                return NoContent();
        }

        [HttpDelete]
        [Route("{idUsuario}/{id}")]
        public IActionResult DeleteLancamento(string idUsuario, string id)
        {
            try
            {
                _logger.LogInformation("DeleteLancamento", idUsuario, id);

                _pacientes.RemoveLancamentoMedico(idUsuario, id);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "DeleteLancamento {Message}", ex.Message);
                return NotFound();
            }
        }

        [HttpPost]
        [Route("idUsuario")]
        public IActionResult PostLancamento(string idUsuario, [FromBody] LancamentoMedicoDto lancamento)
        {
            if (ModelState.IsValid)
            {
                var novoLancamento = _lancamentoDtoConverter.Convert(lancamento);
                
                var registroMedico = _pacientes.SaveLancamentoMedico(idUsuario, novoLancamento);
                if (registroMedico != null)
                    return Ok(_lancamentoConverter
                        .Convert(registroMedico));
                else
                    return NotFound();
            }
            else
            {
                var erros = ModelState.Values
                    .Where(x=> x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    .Select(x =>  x.Errors?.FirstOrDefault()?.ErrorMessage).ToList();
                return BadRequest(new { 
                    PayloadErros = erros
                });
            }
        }
    }
}
