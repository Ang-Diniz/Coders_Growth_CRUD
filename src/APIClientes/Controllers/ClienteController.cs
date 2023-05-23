using Dominio;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace APIClientes.controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private ICliente _cliente;
        private IValidator<Cliente> _validator;
        public ClienteController(ICliente cliente, IValidator<Cliente> validator)
        {
            _cliente = cliente;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            List<Cliente> listaClientes;

            listaClientes = _cliente.ObterTodos();

            return Ok(listaClientes);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Cliente clienteNovo)
        {
            var resultado = _validator.Validate(clienteNovo);

            if (!resultado.IsValid)
            {
                return BadRequest();
            }

            if (clienteNovo == null)
            {
                return BadRequest();
            }

            var id = _cliente.Criar(clienteNovo);

            clienteNovo.Id = id;

            return Created($"api/cliente/{id}", clienteNovo);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var cliente = _cliente.ObterPorId(id);

            if (cliente == null) { return NotFound(); }

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            _cliente.Remover(id);

            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Cliente clienteAtualizado)
        {
            var resultado = _validator.Validate(clienteAtualizado);

            if (!resultado.IsValid)
            {
                return BadRequest();
            }

            var id = _cliente.Atualizar(clienteAtualizado);

            clienteAtualizado.Id = id;

            return Ok();
        }
    }
}
