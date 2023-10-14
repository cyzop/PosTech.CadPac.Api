using Microsoft.AspNetCore.Mvc;
using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Services;
using PosTech.CadPac.Domain.Shared.Converter;

namespace PosTech.CadPac.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ICadastroPacienteService _pacientes;
        private readonly ILogger<PatientController> _logger;
        private readonly IConverter<Paciente, PessoaDto> _pacienteToPessoaDtoConverter;
        private readonly IConverter<Paciente, PacienteDto> _pacienteConverter;
        private readonly IConverter<PessoaDto, Paciente> _pessoaDtoConverter;

        public PatientController(ICadastroPacienteService pacientes, ILogger<PatientController> logger, IConverter<Paciente, PessoaDto> pacienteToPessoaDtoConverter, IConverter<PessoaDto, Paciente> pessoaDtoConverter, IConverter<Paciente, PacienteDto> pacienteConverter)
        {
            _pacientes = pacientes;
            _logger = logger;
            _pacienteToPessoaDtoConverter = pacienteToPessoaDtoConverter;
            _pessoaDtoConverter = pessoaDtoConverter;
            _pacienteConverter = pacienteConverter;
        }

        /// <summary>
        /// Retorna a listagem de pacientes
        /// </summary>
        /// <response code="200">Retorno realizado com sucesso</response>
        /// <response code="400">Falha na consulta dos pacientes</response>
        [HttpGet]
        public IActionResult GetPacientes()
        {
            return Ok(_pacientes
                    .GetAll()
                    .Select(e => _pacienteConverter
                            .Convert(e)));
        }

        /// <summary>
        /// Retorna as informações de um paciente, identificado pelo id, e seu histórico médico 
        /// </summary>
        /// <param name="id"> Identificador do Paciente </param>
        /// <response code="200">Retorno realizado com sucesso</response>
        /// <response code="404">Paciente não encontrado</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PacienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPaciente(string id)
        {
            _logger.LogInformation("GetPaciente", id);
            var paciente = _pacientes.GetPaciente(id);
            if (paciente != null)
                return
                    Ok(_pacienteToPessoaDtoConverter
                        .Convert(paciente));
            else
                return NotFound();
        }

        /// <summary>
        /// Atualiza as informações do Paciente
        /// </summary>
        /// <param name="paciente">Json representando as informações do Paciente</param>
        /// <response code="200">Paciente atualizado com sucesso</response>
        /// <response code="404">Paciente não encontrado</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PessoaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PutPaciente(PessoaDto paciente)
        {

            if (ModelState.IsValid)
            {
                _logger.LogInformation("PutPaciente {Nome}", paciente.Nome);

                var pacienteData = _pessoaDtoConverter.Convert(paciente);

                if (pacienteData != null)
                {
                    var pacienteAlterado = _pacientes.UpdatePacienteData(pacienteData);

                    return Ok(_pacienteToPessoaDtoConverter
                        .Convert(pacienteAlterado)
                        );
                }
                else
                    return NotFound($"Paciente {paciente.Id} {paciente.Nome} não encontrado!");
            }
            else
            {
                var erros = ModelState.Values
                    .Where(x => x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    .Select(x => x.Errors?.FirstOrDefault()?.ErrorMessage).ToList();
                return BadRequest(new
                {
                    PayloadErros = erros
                });
            }
        }

        /// <summary>
        /// Inclusão de um novo Paciente
        /// </summary>
        /// <param name="paciente">Json representando as informações do Paciente</param>
        /// <response code="200">Paciente atualizado com sucesso</response>
        /// <response code="400">Falha na inclusão do Paciente</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PessoaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostPaciente(PessoaDto paciente)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Post {Id}", paciente.Id);
                //PessoaDto to Paciente
                var novoPaciente = _pessoaDtoConverter.Convert(paciente);
                //Paciente (saved with id) to PessoaDto
                var pacienteSaved = _pacientes.SavePaciente(novoPaciente);

                return Ok(_pacienteToPessoaDtoConverter
                    .Convert(pacienteSaved));
            }
            else
            {
                var erros = ModelState.Values
                    .Where(x => x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    .Select(x => x.Errors?.FirstOrDefault()?.ErrorMessage).ToList();
                return BadRequest(new
                {
                    PayloadErros = erros
                });
            }
        }
        /// <summary>
        /// Exclusão de um paciente
        /// </summary>
        /// <param name="id">Identificador do Paciente</param>
        /// <response code="200">Paciente excluído com sucesso</response>
        /// <response code="400">Falha na exclusão do Paciente</response>
        [HttpDelete("{id}")]
        public IActionResult DeletePaciente(string id)
        {
            _pacientes.RemovePaciente(id);
            return Ok();
        }

    }
}
