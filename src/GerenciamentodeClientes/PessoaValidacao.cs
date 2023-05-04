using FluentValidation;

namespace GerenciamentodeClientes
{
    internal class PessoaValidacao : AbstractValidator<Cliente>
    {
        //teste
        public PessoaValidacao()
        {
            RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Campo obrigatório");
        }
    }
}
