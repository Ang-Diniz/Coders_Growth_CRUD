using FluentValidation;

namespace GerenciamentodeClientes
{
    internal class PessoaValidacao : AbstractValidator<Cliente>
    {
        public PessoaValidacao()
        {
            RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Campo obrigatório");
        }
    }
}
