using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentodeClientes
{
    public class RepositorioPessoaLista : IPessoa
    {
        public List<Pessoa> ObterTodos()
        {
            return PessoaListSingleton.Instancia.PessoaList;
        }
        public void Criar(Pessoa pessoaNova)
        {
            PessoaListSingleton.Instancia.PessoaList.Add(pessoaNova);
        }
        public Pessoa ObterPorId(int id)
        {
            Pessoa pessoa = PessoaListSingleton.Instancia.PessoaList.
            Find(pessoa => pessoa.Id == id);

            return pessoa;
        }
        public void Remover(int id)
        {
            var clienteARemover = ObterPorId(id);
            PessoaListSingleton.Instancia.PessoaList.Remove(clienteARemover);
        }
        public void Atualizar(Pessoa pessoaAtualizada)
        {
            Pessoa pessoaParaAtualizar = ObterPorId(pessoaAtualizada.Id);

            pessoaParaAtualizar.Nome = pessoaAtualizada.Nome;
            pessoaParaAtualizar.Email = pessoaAtualizada.Email;
            pessoaParaAtualizar.CPF = pessoaAtualizada.CPF;
            pessoaParaAtualizar.DataDeNascimento = pessoaAtualizada.DataDeNascimento;

            PessoaListSingleton.Instancia.PessoaList = ObterTodos();
        }
    }
}
