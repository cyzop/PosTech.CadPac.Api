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
        private readonly IConverter<Paciente, PessoaDto> _pacienteConverter;
        private readonly IConverter<PessoaDto, Paciente> _pessoaDtoConverter;

        public PatientController(ICadastroPacienteService pacientes, ILogger<PatientController> logger, IConverter<Paciente, PessoaDto> pacienteConverter, IConverter<PessoaDto, Paciente> pessoaDtoConverter)
        {
            _pacientes = pacientes;
            _logger = logger;
            _pacienteConverter = pacienteConverter;
            _pessoaDtoConverter = pessoaDtoConverter;
        }

        [HttpGet]
        public IActionResult GetPacientes()
        {
            return Ok(_pacientes.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Paciente))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPaciente(string id)
        {
            _logger.LogInformation("GetPaciente", id);
            var paciente = _pacientes.GetPaciente(id);
            if (paciente != null)
                return 
                    Ok(_pacienteConverter
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

                    return Ok(_pacienteConverter
                        .Convert(pacienteAlterado)
                        );
                }
                else
                    return NotFound($"Paciente {paciente.Id} {paciente.Nome} não encontrado!");
            }
            else
            {
                var erros = ModelState.Values.Select(x => x.Errors[0].ErrorMessage).ToList();
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
                var novoPaciente = _pessoaDtoConverter.Convert(paciente);

                return Ok(_pacientes.SavePaciente(novoPaciente));
            }
            else
            {
                var erros = ModelState.Values.Select(x => x.Errors[0].ErrorMessage).ToList();
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
