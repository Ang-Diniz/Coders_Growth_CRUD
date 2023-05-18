using FluentValidation;
using System.Text.RegularExpressions;

namespace Dominio
{
    public class ClienteFluentValidation : AbstractValidator<Cliente>
    {
        private static ICliente _repositorioClienteLinq2Db;

        public ClienteFluentValidation(ICliente repositorioClienteLinq2Db)
        {
            _repositorioClienteLinq2Db = repositorioClienteLinq2Db;

            RuleFor(c => c.Nome)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(4);

            RuleFor(c => c.DataDeNascimento)
            .NotEmpty()
            .LessThan(DateTime.Now.AddYears( - Cliente.valorMinimoIdade))
            .WithMessage("\nCliente menor de 18 anos.\n");

            RuleFor(c => c.CPF)
            .Must(validarCPF)
            .WithMessage("\nCPF inválido. Por favor insira um CPF válido.\n")
            .Must((cliente, CPF) => VerificarCpfExiste(cliente, CPF))
            .WithMessage("\nCPF já cadastrado na base da dados.\n");

            RuleFor(c => c.Email)
            .NotEmpty()
            .Must(ValidarEmail)
            .WithMessage("\nE-mail inválido.\n")
            .Must((cliente, EMAIL) => VerificarEmailExiste(cliente, EMAIL))
            .WithMessage("\nE-mail já cadastrado na base da dados.\n")
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
            var obtendoClientePorId = _repositorioClienteLinq2Db.ObterPorId(cliente.Id);
            var cpfExistente = _repositorioClienteLinq2Db.VerificarCpfNoBancoDeDados(cpf);

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

        public bool VerificarEmailExiste(Cliente cliente, string email)
        {
            var obtendoClientePorId = _repositorioClienteLinq2Db.ObterPorId(cliente.Id);
            var emailExistente = _repositorioClienteLinq2Db.VerificarEmailNoBancoDeDados(email);

            if (obtendoClientePorId != null)
            {
                if (obtendoClientePorId.Email == email)
                {
                    return true;
                }
                if (emailExistente != null)
                {
                    return !emailExistente;
                }
            }
            if (obtendoClientePorId == null)
            {
                if (emailExistente != null)
                {
                    return !emailExistente;
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
