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

        [HttpGet]
        public IActionResult GetPacientes()
        {
            return Ok(_pacientes
                    .GetAll()
                    .Select(e => _pacienteConverter
                            .Convert(e)));
        }

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
        [HttpDelete("{id}")]
        public IActionResult DeletePaciente(string id)
        {
            _pacientes.RemovePaciente(id);
            return Ok();
        }

    }
}
