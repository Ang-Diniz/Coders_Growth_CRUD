using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GerenciamentodeClientes
{
    public class ClienteFluentValidation : AbstractValidator<Cliente>
    {
        public ClienteFluentValidation()
        {
            RuleFor(c => c.Nome)
            .NotEmpty()
            .MaximumLength(100);

            RuleFor(c => c.CPF)
            .Must(validarCPF)
            .WithMessage("CPF inválido. Por favor insira um CPF válido.")
            .Must((cpf) =>
            {
                if (verificarCpfExiste(cpf) == false)
                {
                    return true;
                }
                return false;
            })
            .WithMessage("CPF já cadastrado na base da dados.");

            RuleFor(c => c.DataDeNascimento)
            .NotEmpty()
            .LessThan(DateTime.Now.AddYears(-18))
            .WithMessage("Cliente menor de 18 anos.");

            RuleFor(c => c.Email)
            .NotEmpty()
            .Must(validarEmail)
            .WithMessage("E-mail inválido.")
            .MaximumLength(40);
        }

        private bool validarEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$"))
            {
                return false;
            }
            return true;
        }

        public bool verificarCpfExiste(string cpf)
        {
            SqlConnection ConexaoSQL = new SqlConnection(RepositorioClienteBancoDeDados.connectionString);

            try
            {
                ConexaoSQL.Open();

                string sql = "SELECT COUNT(1) FROM clientes WHERE cpf=@CPF";
                SqlCommand cmd = new SqlCommand(sql, ConexaoSQL);

                cmd.Parameters.AddWithValue("@CPF", cpf);
                var resultado = cmd.ExecuteScalar();

                if (resultado != null)
                {
                    return (int)resultado > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado. Contate o administrador do sistema.", ex);
            }
            finally
            {
                ConexaoSQL.Close();
            }
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
