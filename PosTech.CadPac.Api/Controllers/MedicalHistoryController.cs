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


        /// <summary>
        /// Retorna o histórico médico do paciente
        /// </summary>
        /// <param name="idPaciente">Identificador do Paciente</param>
        /// <response code="200">Retorno realizado com sucesso</response>
        /// <response code="204">Paciente sem hitórico médico</response>
        [HttpGet]
        [Route("patient/{idPaciente}")]
        public IActionResult GetHistoricoUsuario(string idPaciente)
        {
            _logger.LogInformation("GetHistoricoUsuario", idPaciente);

            var historico = _pacientes.GetHistoricoMedico(idPaciente);
            if (historico != null)
            {
                return Ok(historico.Select(e => _lancamentoConverter.Convert(e)));
            }
            else
                return NoContent();
        }

        /// <summary>
        /// Lançamento médico do histórico médico do paciente, pode ser um registro de Sintoma, Diagnóstico ou Tratamento
        /// </summary>
        /// <param name="idPaciente">Identificador do Paciente</param>
        /// <param name="id">Identificador do Lançamento Médico</param>
        /// <response code="200">Retorno realizado com sucesso</response>
        /// <response code="404">Lançamento médico não encontrado para o Paciente</response>
        [HttpGet]
        [Route("{idPaciente}/{id}")]
        public IActionResult GetLancamento(string idPaciente, string id)
        {

            var lancamento = _pacientes.GetLancamentoMedico(idPaciente, id);
            if (lancamento != null)
                return Ok(_lancamentoConverter
                    .Convert(lancamento));
            else
                return NotFound();
        }

        /// <summary>
        /// Excluir o lançamento médico do histórico médico do paciente
        /// </summary>
        /// <param name="idPaciente">Identificador do Paciente</param>
        /// <param name="id">Identificador do Lançamento Médico</param>
        /// <response code="200">Exclusão realizada com sucesso</response>
        /// <response code="404">Lançamento médico não encontrado para o Paciente</response>
        [HttpDelete]
        [Route("{idPaciente}/{id}")]
        public IActionResult DeleteLancamento(string idPaciente, string id)
        {
            try
            {
                _logger.LogInformation("DeleteLancamento", idPaciente, id);

                _pacientes.RemoveLancamentoMedico(idPaciente, id);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "DeleteLancamento {Message}", ex.Message);
                return NotFound();
            }
        }
        
        /// <summary>
        /// Incluir lançamento médico no histórico médico do paciente
        /// </summary>
        /// <param name="idPaciente">Identificador do Paciente</param>
        /// <param name="lancamento">Lançamento métido para o histórico do paciente</param>
        /// <response code="200">Exclusão realizada com sucesso</response>
        /// <response code="404">Lançamento médico não encontrado para o Paciente</response>
        [HttpPost]
        [Route("idPaciente")]
        public IActionResult PostLancamento(string idPaciente, [FromBody] LancamentoMedicoDto lancamento)
        {
            if (ModelState.IsValid)
            {
                var novoLancamento = _lancamentoDtoConverter.Convert(lancamento);
                
                var registroMedico = _pacientes.SaveLancamentoMedico(idPaciente, novoLancamento);
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
