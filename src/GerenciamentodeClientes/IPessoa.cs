using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentodeClientes
{
    public interface IPessoa 
    {
        public List<Pessoa> ObterTodos();
        public void Criar(Pessoa pessoaNova);
        public Pessoa ObterPorId(int id);
        public void Remover(int id);
        public void Atualizar(Pessoa pessoaAtualizada);
    }
}
