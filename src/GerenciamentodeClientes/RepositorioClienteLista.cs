namespace GerenciamentodeClientes
{
    public class RepositorioClienteLista : ICliente
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
        public void Atualizar(Cliente clienteAtualizada)
        {
            var clienteParaAtualizar = ObterPorId(clienteAtualizada.Id);

            clienteParaAtualizar.Nome = clienteAtualizada.Nome;
            clienteParaAtualizar.Email = clienteAtualizada.Email;
            clienteParaAtualizar.CPF = clienteAtualizada.CPF;
            clienteParaAtualizar.DataDeNascimento = clienteAtualizada.DataDeNascimento;
        }
    }
}
