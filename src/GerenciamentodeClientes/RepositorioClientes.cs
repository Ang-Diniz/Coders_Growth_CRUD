using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentodeClientes
{
    public class RepositorioClientes : InterfaceCliente
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
            ToList().
            Find(pessoa => pessoa.Id == id);

            return pessoa;
        }
        public void Remover(int id)
        {
            var clienteARemover = ObterPorId(id);
            PessoaListSingleton.Instancia.PessoaList.Remove(clienteARemover);
        }
        public void Atualizar(int id, Pessoa pessoaAtualizada)
        {
            int index = PessoaListSingleton.Instancia.PessoaList.FindIndex(pessoa => pessoa.Id == id);

            if (index != -1)
            {
                PessoaListSingleton.Instancia.PessoaList[index] = pessoaAtualizada;
            }
        }
    }
}
