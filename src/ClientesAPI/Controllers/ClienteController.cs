using Dominio;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ClientesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClienteController : ControllerBase
    {
        private IRepositorioCliente _clienteRepositorio;
        private IValidator<Cliente> _validator;
        public ClienteController(IRepositorioCliente repositorio, IValidator<Cliente> validator)
        {
            _clienteRepositorio = repositorio;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult ObterTodos([FromQuery] string? nome)
        {
            try
            {
                List<Cliente> cliente = _clienteRepositorio.ObterTodos(nome);

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Cliente clienteNovo)
        {
            try
            {
                _validator.ValidateAndThrow(clienteNovo);

                _clienteRepositorio.Criar(clienteNovo);

                var cliente = _clienteRepositorio.ObterPorCpf(clienteNovo.CPF);

                return Ok(cliente.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            try
            {
                Cliente cliente = _clienteRepositorio.ObterPorId(id);

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message + ", \n" + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover([FromRoute] int id)
        {
            try
            {
                var cliente = _clienteRepositorio.ObterPorId(id);

                _clienteRepositorio.Remover(cliente.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ", \n" + ex.InnerException);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromRoute] int id, [FromBody] Cliente clienteAtualizado)
        {
            try
            {
                _validator.ValidateAndThrow(clienteAtualizado);

                _clienteRepositorio.Atualizar(clienteAtualizado);

                return Ok(clienteAtualizado.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
