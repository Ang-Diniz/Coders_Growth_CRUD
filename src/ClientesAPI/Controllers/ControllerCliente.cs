using Dominio;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ClientesAPI.Controllers
{
    [Route("api/cliente")]
    [ApiController]

    public class ControllerCliente : ControllerBase
    {
        private ICliente _cliente;
        private IValidator<Cliente> _validator;
        public ControllerCliente(ICliente cliente, IValidator<Cliente> validator)
        {
            _cliente = cliente;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            List<Cliente> listaClientes;

            try
            {
                listaClientes = _cliente.ObterTodos();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(listaClientes);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Cliente clienteNovo)
        {
            int id;

            try
            {
                _validator.ValidateAndThrow(clienteNovo);

                 id = _cliente.Criar(clienteNovo);

                clienteNovo.Id = id;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created($"api/cliente/{id}", clienteNovo);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            Cliente cliente;

            try
            {
                cliente = _cliente.ObterPorId(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                _cliente.Remover(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Cliente clienteAtualizado)
        {
            try
            {
                _validator.ValidateAndThrow(clienteAtualizado);

                var id = _cliente.Atualizar(clienteAtualizado);

                clienteAtualizado.Id = id;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
