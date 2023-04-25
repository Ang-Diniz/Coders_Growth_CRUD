using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentodeClientes
{
    public interface IRepository 
    {
        public List<Pessoa> ObterTodos();

        public void Criar(Pessoa pessoaNova);

        public void Remover(int id);

        public void Atualizar(int id, Pessoa pessoaAtualizada);
    }
}
