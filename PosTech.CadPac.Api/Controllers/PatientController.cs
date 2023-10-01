using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PosTech.CadPac.Api.Models;

namespace PosTech.CadPac.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private List<Paciente> pacientes = new List<Paciente>();

        public PatientController(List<Paciente> pacientes)
        {
            this.pacientes = pacientes;
        }

        [HttpGet]
        public IActionResult GetPacientes()
        {
            return Ok(pacientes);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Paciente))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPaciente(string id)
        {
            var paciente = pacientes.Find(p => p.Id == id);
            if (paciente != null)
                return Ok(paciente);
            else
                return NotFound();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pessoa))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PutPaciente(Pessoa paciente)
        {
            if (ModelState.IsValid)
            {
                var pacienteAlterar = pacientes.Find(p => p.Id == paciente.Id);
                if (paciente != null)
                {
                    pacienteAlterar.Nome = paciente.Nome;
                    pacienteAlterar.Responsavel = paciente.Responsavel;
                    pacienteAlterar.DataNascimento = paciente.DataNascimento;
                    pacienteAlterar.Email = paciente.Email;
                    return Ok(pacienteAlterar);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pessoa))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostPaciente(Pessoa paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.Id = Guid.NewGuid().ToString();
                pacientes.Add((Paciente)paciente);
                return Ok(paciente);
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

    }
}
