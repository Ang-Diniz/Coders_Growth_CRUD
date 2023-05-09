using FluentValidation;
using System.Text.RegularExpressions;

namespace GerenciamentodeClientes
{
    public class ClienteFluentValidation : AbstractValidator<Cliente>
    {
        public ClienteFluentValidation()
        {
            RuleFor(c => c.Nome)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(4);

            RuleFor(c => c.CPF)
            .Must(validarCPF)
            .WithMessage("\nCPF inválido. Por favor insira um CPF válido.\n")
            .Must((cliente, CPF) => VerificarCpfExiste(cliente, CPF))
            .WithMessage("\nCPF já cadastrado na base da dados.\n");

            RuleFor(c => c.DataDeNascimento)
            .NotEmpty()
            .LessThan(DateTime.Now.AddYears(-18))
            .WithMessage("\nCliente menor de 18 anos.\n");

            RuleFor(c => c.Email)
            .NotEmpty()
            .Must(ValidarEmail)
            .WithMessage("\nE-mail inválido.\n")
            .MaximumLength(40);
        }

        private bool ValidarEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$"))
            {
                return false;
            }

            return true;
        }

        public bool VerificarCpfExiste(Cliente cliente, string cpf)
        {
            var obtendoClientePorId = TelaInicial._repositorioCliente.ObterPorId(cliente.Id);
            var cpfExistente = RepositorioClienteBancoDeDados.VerificarCpfNoBancoDeDados(cpf);

            if (obtendoClientePorId != null)
            {
                if (obtendoClientePorId.CPF == cpf)
                {
                    return true;
                }
                if (cpfExistente != null)
                {
                    return !cpfExistente;
                }
            }
            if (obtendoClientePorId == null)
            {
                if (cpfExistente != null)
                {
                    return !cpfExistente;
                }
            }

            return false;
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
