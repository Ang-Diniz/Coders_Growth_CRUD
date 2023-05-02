namespace GerenciamentodeClientes
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
            DataGridViewTelaInicial.DataSource = repositorioClienteBancoDeDados.ObterTodos();
        }

        DialogResult respostaEventosTelaInicial;
        RepositorioClienteLista repositorioClienteLista = new RepositorioClienteLista();
        RepositorioClienteBancoDeDados repositorioClienteBancoDeDados = new RepositorioClienteBancoDeDados();
        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                var cadastro = new TelaDeCadastro(null);
                respostaEventosTelaInicial = cadastro.ShowDialog(null);
                repositorioClienteBancoDeDados.Criar(cadastro.cliente);

                if (respostaEventosTelaInicial == DialogResult.OK)
                {
                    DataGridViewTelaInicial.DataSource = null;
                    DataGridViewTelaInicial.DataSource = repositorioClienteBancoDeDados.ObterTodos();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado. Contate o administrador do sistema.", ex.Message);
            }
        }
        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            try
            {
                if (ClienteListSingleton.Instancia.ClienteList.Count == Decimal.Zero)
                {
                    MessageBox.Show("Nenhum cliente para editar.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var index = DataGridViewTelaInicial.CurrentCell.RowIndex;
                    var clienteSelecionado = DataGridViewTelaInicial.Rows[index].DataBoundItem as Pessoa;
                    var telaEdicao = new TelaDeCadastro(clienteSelecionado);
                    repositorioClienteLista.Atualizar(clienteSelecionado);
                    respostaEventosTelaInicial = telaEdicao.ShowDialog();

                    if (respostaEventosTelaInicial == DialogResult.OK)
                    {
                        DataGridViewTelaInicial.DataSource = null;
                        DataGridViewTelaInicial.DataSource = repositorioClienteLista.ObterTodos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado. Contate o administrador do sistema.", ex.Message);
            }
        }
        private void AoClicarEmExcluir(object sender, EventArgs e)
        {
            try
            {

                int linhaSelecionada = DataGridViewTelaInicial.SelectedRows.Count;

                if (linhaSelecionada == Decimal.Zero)
                {
                    MessageBox.Show("Nenhum cliente para exlcuir.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var index = DataGridViewTelaInicial.CurrentRow.Index;
                int id = PegarId();
                var clienteSelecionado = repositorioClienteBancoDeDados.ObterPorId(id);
                respostaEventosTelaInicial = MessageBox.Show("Tem certeza que deseja excluir esse cliente ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respostaEventosTelaInicial == DialogResult.Yes)
                {
                    repositorioClienteBancoDeDados.Remover(clienteSelecionado.Id);
                    DataGridViewTelaInicial.DataSource = null;
                    DataGridViewTelaInicial.DataSource = repositorioClienteBancoDeDados.ObterTodos();
                }

                int PegarId()
                {
                    return int.Parse(DataGridViewTelaInicial.SelectedRows[0].Cells[0].Value.ToString());
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado. Contate o administrador do sistema.", ex.Message);
            }
        }
    }
}





