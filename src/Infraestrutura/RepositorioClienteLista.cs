using Dominio;

namespace Infraestrutura
{
    public class RepositorioClienteLista : IRepositorioCliente
    {
        public List<Cliente> ObterTodos()
        {
            return ClienteListSingleton.Instancia.ClienteList;
        }
        public void Criar(Cliente clienteNovo)
        {
            ClienteListSingleton.Instancia.ClienteList.Add(clienteNovo);
        }
        public Cliente ObterPorId(int id)
        {
            Cliente cliente = ClienteListSingleton.Instancia.ClienteList.
            Find(cliente => cliente.Id == id);

            return cliente;
        }
        public void Remover(int id)
        {
            var clienteARemover = ObterPorId(id);
            ClienteListSingleton.Instancia.ClienteList.Remove(clienteARemover);
        }
        public void Atualizar(Cliente clienteAtualizado)
        {
            var clienteParaAtualizar = ObterPorId(clienteAtualizado.Id);

            clienteParaAtualizar.Nome = clienteAtualizado.Nome;
            clienteParaAtualizar.Email = clienteAtualizado.Email;
            clienteParaAtualizar.CPF = clienteAtualizado.CPF;
            clienteParaAtualizar.DataDeNascimento = clienteAtualizado.DataDeNascimento;
        }

        public bool VerificarCpfNoBancoDeDados(string cpf)
        {
            throw new NotImplementedException();
        }

        public bool VerificarEmailNoBancoDeDados(string email)
        {
            throw new NotImplementedException();
        }

        public Cliente ObterPorCpf(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
