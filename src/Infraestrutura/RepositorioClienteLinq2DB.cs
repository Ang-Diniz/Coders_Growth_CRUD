using Dominio;
using LinqToDB;
using LinqToDB.Data;
using System.Configuration;
using LinqToDB.DataProvider.SqlServer;
using System.Data.SqlClient;

namespace Infraestrutura
{
    public class RepositorioClienteLinq2DB : ICliente
    {
        private DataConnection conexao;
        public ITable<Cliente> _clientes;

        string connectionString = ConfigurationManager.ConnectionStrings["Cliente"].ConnectionString;

        public void Atualizar(Cliente clienteAtualizado)
        {
            using var conexaoLinq2Db = CriarConexao();

            try
            {
                conexaoLinq2Db.Update(clienteAtualizado);
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO AO ALTERAR DADOS DO CLIENTE", ex);
            }
        }

        public void Criar(Cliente clienteNovo)
        {
            using var conexaoLinq2Db = CriarConexao();

            try
            {
                conexaoLinq2Db.Insert(clienteNovo);
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO AO INSERIR CLIENTE", ex);
            }
        }

        public Cliente ObterPorId(int id)
        {
            using var conexaoLinq2Db = CriarConexao();

            try
            {
                var cliente = conexaoLinq2Db.GetTable<Cliente>().
                FirstOrDefault(c => c.Id == id);

                return cliente;

            }
            catch (Exception ex)
            {
                throw new Exception("ERRO AO OBTER ID DO CLIENTE", ex);
            }
        }

        public List<Cliente> ObterTodos()
        {

            using var conexaoLinq2Db = CriarConexao();

            try
            {
                return conexaoLinq2Db.GetTable<Cliente>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO AO OBTER TODOS OS CLIENTE", ex);
            }
        }

        public void Remover(int id)
        {
            using var conexaoLinq2Db = CriarConexao();

            try
            {
                var cliente = ObterPorId(id);
                conexaoLinq2Db.Delete(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO AO DELETAR CLIENTE", ex);
            }
        }

        public bool VerificarCpfNoBancoDeDados(string cpf)
        {
            var cpfExisteNoBancoDeDados = false;

            SqlConnection ConexaoSQL = new(connectionString);

            try
            {
                string sql = "SELECT COUNT(CPF) FROM Cliente WHERE CPF=@CPF";
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
                string sql = "SELECT COUNT(Email) FROM Cliente WHERE Email=@EMAIL";
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

        private DataConnection CriarConexao()
        {
            conexao = SqlServerTools.CreateDataConnection(connectionString);
            return conexao;
        }
    }
}
