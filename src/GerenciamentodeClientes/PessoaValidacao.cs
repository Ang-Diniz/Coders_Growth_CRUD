using FluentValidation;

namespace GerenciamentodeClientes
{
    public class PessoaValidacao : AbstractValidator<Cliente>
    {
        public PessoaValidacao()
        {
            RuleFor(c => c.Nome)
            .NotEmpty()
            .MaximumLength(100);

            RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("E-mail inválido.")
            .MaximumLength(40);

            RuleFor(c => c.CPF)
            .Must(validarCPF)
            .WithMessage("CPF inválido. Por favor insira um CPF válido.")
            .MinimumLength(11)
            .MaximumLength(11)
            .WithMessage("  ");

            RuleFor(c => c.DataDeNascimento)
            .NotEmpty()
            .LessThan(DateTime.Now.AddYears(-18))
            .WithMessage("Cliente menor de 18 anos.");
        }

        private bool validarCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
