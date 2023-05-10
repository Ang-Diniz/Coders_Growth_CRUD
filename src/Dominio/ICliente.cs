namespace Dominio
{
    public interface ICliente 
    {
        public List<Cliente> ObterTodos();
        public void Criar(Cliente clienteNovo);
        public Cliente ObterPorId(int id);
        public void Remover(int id);
        public void Atualizar(Cliente clienteAtualizado);
        public bool VerificarCpfNoBancoDeDados(string cpf);
        public bool VerificarEmailNoBancoDeDados(string email);
    }
}
