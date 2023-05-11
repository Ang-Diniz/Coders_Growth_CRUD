﻿using Dominio;
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

        private DataConnection CriarConexao()
        {
            conexao = SqlServerTools.CreateDataConnection(connectionString);
            return conexao;
        }

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

            using var conexaoLinq2Db = CriarConexao();

            try
            {
                var contadorCpf = conexaoLinq2Db.GetTable<Cliente>().Count(c => c.CPF == cpf);

                cpfExisteNoBancoDeDados = contadorCpf > Decimal.Zero;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado. Contate o administrador do sistema.", ex);
            }

            return cpfExisteNoBancoDeDados;
        }

        public bool VerificarEmailNoBancoDeDados(string email)
        {
            var emailExisteNoBancoDeDados = false;

            using var conexaoLinq2Db = CriarConexao();

            try
            {
                var contadorEmail = conexaoLinq2Db.GetTable<Cliente>().Count(c => c.Email == email);

                emailExisteNoBancoDeDados = contadorEmail > Decimal.Zero;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado. Contate o administrador do sistema.", ex);
            }
            
            return emailExisteNoBancoDeDados;
        }
    }
}