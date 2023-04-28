using Microsoft.Data.SqlClient;
using System.Configuration;

namespace GerenciamentodeClientes
{
    public class RepositorioClienteBancoDeDados : ICliente
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Cliente"].ConnectionString;
        SqlConnection ConexaoSQL = new SqlConnection(connectionString);
        public void Atualizar(Pessoa pessoaAtualizada)
        {
            throw new NotImplementedException();
        }

        public void Criar(Pessoa clienteNovo)
        {
            string inserirNovoCliente = "INSERT INTO clientes (nome, cpf, email, data_de_nascimento) VALUES (@nome, @cpf, @email, @data_de_nascimento)";

            SqlCommand cmd = new SqlCommand(inserirNovoCliente, ConexaoSQL);
            cmd.Parameters.AddWithValue("@nome", clienteNovo.Nome);
            cmd.Parameters.AddWithValue("@cpf", clienteNovo.CPF);
            cmd.Parameters.AddWithValue("@email", clienteNovo.Email);
            cmd.Parameters.AddWithValue("@data_de_nascimento", clienteNovo.DataDeNascimento);

            ConexaoSQL.Open();
            cmd.ExecuteNonQuery();
            ConexaoSQL.Close();
        }

        public Pessoa ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pessoa> ObterTodos()
        {
            ConexaoSQL.Open();

            List<Pessoa> clientes = new List<Pessoa>();

            string sql = "SELECT * FROM clientes";
            SqlCommand cmd = new(sql, ConexaoSQL);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Pessoa cliente = new()
                {
                    Id = (int)reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    CPF = reader.GetString(2),
                    Email = reader.GetString(3),
                    DataDeNascimento = reader.GetDateTime(4)

                };

                clientes.Add(cliente);
            }

            ConexaoSQL.Close();
            return clientes.ToList();
        }

        public void Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}

