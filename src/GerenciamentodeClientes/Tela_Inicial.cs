using FluentValidation;
using Microsoft.Extensions.Options;

namespace GerenciamentodeClientes
{
    public partial class TelaInicial : Form
    {
        ICliente _repositorioCliente;
        public TelaInicial( ICliente repositorioCliente)
        {
            InitializeComponent();
            _repositorioCliente = repositorioCliente;
            
            DataGridViewTelaInicial.DataSource = _repositorioCliente.ObterTodos();
        }

        DialogResult respostaEventosTelaInicial;

        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                var cadastro = new TelaDeCadastro(null);
                respostaEventosTelaInicial = cadastro.ShowDialog(null);

                if (respostaEventosTelaInicial == DialogResult.OK)
                {
                    _repositorioCliente.Criar(cadastro.cliente);
                    DataGridViewTelaInicial.DataSource = null;
                    DataGridViewTelaInicial.DataSource = _repositorioCliente.ObterTodos();
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
                        var clienteSelecionado = _repositorioCliente.ObterPorId(id);
                        var telaEdicao = new TelaDeCadastro(clienteSelecionado);
                        respostaEventosTelaInicial = telaEdicao.ShowDialog();
                       

                        if (respostaEventosTelaInicial == DialogResult.OK)
                        {
                            _repositorioCliente.Atualizar(clienteSelecionado);
                            DataGridViewTelaInicial.DataSource = null;
                            DataGridViewTelaInicial.DataSource = _repositorioCliente.ObterTodos();
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
                        var clienteSelecionado = _repositorioCliente.ObterPorId(id);
                        respostaEventosTelaInicial = MessageBox.Show("Tem certeza que deseja excluir esse cliente ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (respostaEventosTelaInicial == DialogResult.Yes)
                        {
                            _repositorioCliente.Remover(clienteSelecionado.Id);
                            DataGridViewTelaInicial.DataSource = null;
                            DataGridViewTelaInicial.DataSource = _repositorioCliente.ObterTodos();
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






