using Dominio;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            try
            {
                List<Cliente> cliente = _cliente.ObterTodos();

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

                _cliente.Criar(clienteNovo);

                return Ok();
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
                Cliente cliente = _cliente.ObterPorId(id);

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
                var cliente = _cliente.ObterPorId(id);

                _cliente.Remover(cliente.Id);

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

                _cliente.Atualizar(clienteAtualizado);

                return Ok(clienteAtualizado.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
