using Microsoft.Data.SqlClient;
using System.Configuration;

namespace GerenciamentodeClientes
{
    public class RepositorioClienteBancoDeDados : ICliente
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Cliente"].ConnectionString;

        public List<Cliente> ObterTodos()
        {
            SqlConnection ConexaoSQL = new SqlConnection(connectionString);

            try
            {
                ConexaoSQL.Open();

                List<Cliente> clientes = new List<Cliente>();
                var sql = "SELECT * FROM clientes";

                SqlCommand cmd = new SqlCommand(sql, ConexaoSQL);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new()
                    {
                        Id = (int)reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        CPF = reader.GetString(2),
                        Email = reader.GetString(3),
                        DataDeNascimento = reader.GetDateTime(4)

                    };

                    clientes.Add(cliente);
                }
                return clientes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todos os clientes.", ex);
            }
            finally
            {
                ConexaoSQL.Close();
            }
        }

        public void Criar(Cliente clienteNovo)
        {
            SqlConnection ConexaoSQL = new SqlConnection(connectionString);

            try
            {
                ConexaoSQL.Open();

                var sql = "INSERT INTO clientes (nome, cpf, email, data_de_nascimento) " +
                             "VALUES (@nome, @cpf, @email, @data_de_nascimento)";

                SqlCommand cmd = new SqlCommand(sql, ConexaoSQL);
                cmd.Parameters.AddWithValue("@nome", clienteNovo.Nome);
                cmd.Parameters.AddWithValue("@cpf", clienteNovo.CPF);
                cmd.Parameters.AddWithValue("@email", clienteNovo.Email);
                cmd.Parameters.AddWithValue("@data_de_nascimento", clienteNovo.DataDeNascimento);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar o cliente.", ex);
            }
            finally
            {
                ConexaoSQL.Close();
            }
        }

        public Cliente ObterPorId(int id)
        {
            SqlConnection ConexaoSQL = new SqlConnection(connectionString);

            try
            {
                ConexaoSQL.Open();

                var sql = $"SELECT * FROM CLIENTES WHERE Id ={id}";

                SqlCommand cmd = new SqlCommand(sql, ConexaoSQL);


                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Cliente cliente = new()
                    {
                        Id = (int)reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        CPF = reader.GetString(2),
                        Email = reader.GetString(3),
                        DataDeNascimento = reader.GetDateTime(4)

                    };
                    return cliente;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter o id do cliente.", ex);
            }
            finally
            {
                ConexaoSQL.Close();
            }
        }

        public void Remover(int id)
        {
            SqlConnection ConexaoSQL = new SqlConnection(connectionString);

            try
            {
                ConexaoSQL.Open();

                var cliente = ObterPorId(id);
                var sql = $"DELETE FROM clientes WHERE Id ={cliente.Id}";

                SqlCommand cmd = new SqlCommand(sql, ConexaoSQL);

                SqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover o cliente.", ex);
            }
            finally
            {
                ConexaoSQL.Close();
            }
        }

        public void Atualizar(Cliente clienteAtualizado)
        {
            SqlConnection ConexaoSQL = new SqlConnection(connectionString);

            try
            {
                ConexaoSQL.Open();

                var sql = "UPDATE clientes SET nome=@nome, cpf=@cpf, email=@email, data_de_nascimento=@data_de_nascimento " +
                       $"WHERE ID={clienteAtualizado.Id}";

                SqlCommand cmd = new SqlCommand(sql, ConexaoSQL);

                cmd.Parameters.AddWithValue("@nome", clienteAtualizado.Nome);
                cmd.Parameters.AddWithValue("@cpf", clienteAtualizado.CPF);
                cmd.Parameters.AddWithValue("@email", clienteAtualizado.Email);
                cmd.Parameters.AddWithValue("@data_de_nascimento", clienteAtualizado.DataDeNascimento);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar o cliente.", ex);
            }
            finally
            {
                ConexaoSQL.Close();
            }
        }
    }
}



