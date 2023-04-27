namespace GerenciamentodeClientes
{
    public class RepositorioClienteLista : ICliente
    {
        public List<Pessoa> ObterTodos()
        {
            return ClienteListSingleton.Instancia.ClienteList;
        }
        public void Criar(Pessoa clienteNovo)
        {
            ClienteListSingleton.Instancia.ClienteList.Add(clienteNovo);
        }
        public Pessoa ObterPorId(int id)
        {
            Pessoa cliente = ClienteListSingleton.Instancia.ClienteList.
            Find(cliente => cliente.Id == id);

            return cliente;
        }
        public void Remover(int id)
        {
            var clienteARemover = ObterPorId(id);
            ClienteListSingleton.Instancia.ClienteList.Remove(clienteARemover);
        }
        public void Atualizar(Pessoa clienteAtualizada)
        {
            var clienteParaAtualizar = ObterPorId(clienteAtualizada.Id);

            clienteParaAtualizar.Nome = clienteAtualizada.Nome;
            clienteParaAtualizar.Email = clienteAtualizada.Email;
            clienteParaAtualizar.CPF = clienteAtualizada.CPF;
            clienteParaAtualizar.DataDeNascimento = clienteAtualizada.DataDeNascimento;
        }
    }
}
