using Dominio;

namespace Infraestrutura
{
    public class RepositorioClienteLista : ICliente
    {
        public List<Cliente> ObterTodos()
        {
            return ClienteListSingleton.Instancia.ClienteList;
        }
        public int Criar(Cliente clienteNovo)
        {
            ClienteListSingleton.Instancia.ClienteList.Add(clienteNovo);
            return clienteNovo.Id;
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
        public int Atualizar(Cliente clienteAtualizado)
        {
            var clienteParaAtualizar = ObterPorId(clienteAtualizado.Id);

            clienteParaAtualizar.Nome = clienteAtualizado.Nome;
            clienteParaAtualizar.Email = clienteAtualizado.Email;
            clienteParaAtualizar.CPF = clienteAtualizado.CPF;
            clienteParaAtualizar.DataDeNascimento = clienteAtualizado.DataDeNascimento;

            return clienteAtualizado.Id;
        }

        public bool VerificarCpfNoBancoDeDados(string cpf)
        {
            throw new NotImplementedException();
        }

        public bool VerificarEmailNoBancoDeDados(string email)
        {
            throw new NotImplementedException();
        }
    }
}
