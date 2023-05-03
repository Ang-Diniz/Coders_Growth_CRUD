namespace GerenciamentodeClientes
{
    public interface ICliente 
    {
        public List<Pessoa> ObterTodos();
        public void Criar(Pessoa clienteNovo);
        public Pessoa ObterPorId(int id);
        public void Remover(int id);
        public void Atualizar(Pessoa clienteAtualizado);
    }
}
