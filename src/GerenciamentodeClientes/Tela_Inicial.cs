﻿namespace GerenciamentodeClientes
{
    public partial class TelaInicial : Form
    {
        private readonly ICliente _repositorio;
        public TelaInicial(ICliente repositorio )
        {
            InitializeComponent();
            _repositorio = repositorio;
            DataGridViewTelaInicial.DataSource = repositorioClienteBancoDeDados.ObterTodos();
        }

        DialogResult respostaEventosTelaInicial;

        RepositorioClienteBancoDeDados repositorioClienteBancoDeDados = new RepositorioClienteBancoDeDados();

        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                var cadastro = new TelaDeCadastro(null);
                respostaEventosTelaInicial = cadastro.ShowDialog(null);

                if (respostaEventosTelaInicial == DialogResult.OK)
                {
                    _repositorio.Criar(cadastro.cliente);
                    DataGridViewTelaInicial.DataSource = null;
                    DataGridViewTelaInicial.DataSource = _repositorio.ObterTodos();
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
                if (DataGridViewTelaInicial.SelectedRows.Count == Decimal.Zero)
                {
                    MessageBox.Show("Nenhum cliente foi selecionado.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    var index = DataGridViewTelaInicial.CurrentRow.Index;

                    if (index != null)
                    {
                        var id = PegarId();
                        var clienteSelecionado = repositorioClienteBancoDeDados.ObterPorId(id);
                        var telaEdicao = new TelaDeCadastro(clienteSelecionado);
                        respostaEventosTelaInicial = telaEdicao.ShowDialog();
                       

                        if (respostaEventosTelaInicial == DialogResult.OK)
                        {
                            _repositorio.Atualizar(clienteSelecionado);
                            DataGridViewTelaInicial.DataSource = null;
                            DataGridViewTelaInicial.DataSource = _repositorio.ObterTodos();
                        }
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
                if (DataGridViewTelaInicial.SelectedRows.Count == Decimal.Zero)
                {
                    MessageBox.Show("Nenhum cliente foi selecionado.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var index = DataGridViewTelaInicial.CurrentRow.Index;

                    if (index != null)
                    {
                        var id = PegarId();
                        var clienteSelecionado = repositorioClienteBancoDeDados.ObterPorId(id);
                        respostaEventosTelaInicial = MessageBox.Show("Tem certeza que deseja excluir esse cliente ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (respostaEventosTelaInicial == DialogResult.Yes)
                        {
                            _repositorio.Remover(clienteSelecionado.Id);
                            DataGridViewTelaInicial.DataSource = null;
                            DataGridViewTelaInicial.DataSource = _repositorio.ObterTodos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado. Contate o administrador do sistema.", ex.Message);
            }
        }

        private int PegarId()
        {
            return int.Parse(DataGridViewTelaInicial.SelectedRows[0].Cells[0].Value.ToString());
        }

    }
}






