namespace GerenciamentodeClientes
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }
        DialogResult respostaEventosTelaInicial;
        RepositorioClienteLista repositorioClienteLista = new RepositorioClienteLista();
        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                var cadastro = new TelaDeCadastro(null);
                respostaEventosTelaInicial = cadastro.ShowDialog(null);

                if (respostaEventosTelaInicial == DialogResult.OK)
                {
                    repositorioClienteLista.Criar(cadastro.cliente);

                    DataGridViewTelaInicial.DataSource = null;
                    DataGridViewTelaInicial.DataSource = repositorioClienteLista.ObterTodos();
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
                if (ClienteListSingleton.Instancia.ClienteList.Count == Decimal.Zero)
                {
                    MessageBox.Show("Nenhum cliente para exlcuir.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var index = DataGridViewTelaInicial.CurrentCell.RowIndex;
                    var clienteSelecionado = DataGridViewTelaInicial.Rows[index].DataBoundItem as Pessoa;
                    respostaEventosTelaInicial = MessageBox.Show("Tem certeza que deseja excluir esse cliente ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respostaEventosTelaInicial == DialogResult.Yes)
                    {
                        repositorioClienteLista.Remover(clienteSelecionado.Id);

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
    }
}





