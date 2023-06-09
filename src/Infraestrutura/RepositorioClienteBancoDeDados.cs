﻿using Dominio;
using System.Configuration;
using System.Data.SqlClient;

namespace Infraestrutura
{
    public class RepositorioClienteBancoDeDados : IRepositorioCliente
    {
        public static IRepositorioCliente _repositorioClienteBD;
        public static string connectionString = ConfigurationManager.ConnectionStrings["Cliente"].ConnectionString;

        public List<Cliente> ObterTodos()
        {
            SqlConnection ConexaoSQL = new(connectionString);

            try
            {
                ConexaoSQL.Open();

                List<Cliente> clientes = new();
                var sql = "SELECT * FROM clientes";

                SqlCommand cmd = new(sql, ConexaoSQL);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new()
                    {
                        Id = reader.GetInt32(0),
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
            SqlConnection ConexaoSQL = new(connectionString);

            try
            {
                ConexaoSQL.Open();

                var sql = "INSERT INTO Cliente (Nome, CPF, Email, DataDeNascimento) " +
                             "VALUES (@nome, @cpf, @email, @data_de_nascimento)";

                SqlCommand cmd = new(sql, ConexaoSQL);

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
            SqlConnection ConexaoSQL = new(connectionString);

            try
            {
                ConexaoSQL.Open();

                var sql = $"SELECT * FROM Cliente WHERE Id ={id}";

                SqlCommand cmd = new(sql, ConexaoSQL);


                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Cliente cliente = new()
                    {
                        Id = reader.GetInt32(0),
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
            SqlConnection ConexaoSQL = new(connectionString);

            try
            {
                ConexaoSQL.Open();

                var cliente = ObterPorId(id);
                var sql = $"DELETE FROM Cliente WHERE Id ={cliente.Id}";

                SqlCommand cmd = new(sql, ConexaoSQL);

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
            SqlConnection ConexaoSQL = new(connectionString);

            try
            {
                ConexaoSQL.Open();

                var sql = "UPDATE Cliente SET Nome=@nome, CPF=@cpf, Email=@email, DataDeNascimento=@data_de_nascimento " +
                       $"WHERE ID={clienteAtualizado.Id}";

                SqlCommand cmd = new(sql, ConexaoSQL);

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

        public bool VerificarCpfNoBancoDeDados(string cpf)
        {
            var cpfExisteNoBancoDeDados = false;

            SqlConnection ConexaoSQL = new(connectionString);

            try
            {
                string sql = "SELECT COUNT(CPF) FROM clientes WHERE CPF=@CPF";
                SqlCommand cmd = new(sql, ConexaoSQL);
                cmd.Parameters.AddWithValue("@CPF", cpf);

                ConexaoSQL.Open();

                int contadorCpf = (int)cmd.ExecuteScalar();

                cpfExisteNoBancoDeDados = contadorCpf > Decimal.Zero;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado. Contate o administrador do sistema.", ex);
            }
            finally
            {
                ConexaoSQL.Close();
            }
            return cpfExisteNoBancoDeDados;
        }

        public bool VerificarEmailNoBancoDeDados(string email)
        {
            var emailExisteNoBancoDeDados = false;

            SqlConnection ConexaoSQL = new(connectionString);

            try
            {
                string sql = "SELECT COUNT(Email) FROM clientes WHERE Email=@EMAIL";
                SqlCommand cmd = new(sql, ConexaoSQL);
                cmd.Parameters.AddWithValue("@EMAIL", email);

                ConexaoSQL.Open();

                int contadorEmail = (int)cmd.ExecuteScalar();

                emailExisteNoBancoDeDados = contadorEmail > Decimal.Zero;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado. Contate o administrador do sistema.", ex);
            }
            finally
            {
                ConexaoSQL.Close();
            }
            return emailExisteNoBancoDeDados;
        }

        public Cliente ObterPorCpf(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}



