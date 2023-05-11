using Dominio;
using FluentValidation;
using Infraestrutura;
using LinqToDB;

namespace GerenciamentodeClientes
{
    public partial class TelaInicial : Form
    {
        private static ICliente _repositorioClienteLinq2Db;

        private readonly IValidator<Cliente> _validator;

        DialogResult respostaEventosTelaInicial;
        public TelaInicial(IValidator<Cliente> validator, ICliente repositorioClienteLinq2Db)
        {
            InitializeComponent();
            _validator = validator;
            _repositorioClienteLinq2Db = repositorioClienteLinq2Db;
            DataGridViewTelaInicial.DataSource = _repositorioClienteLinq2Db.ObterTodos();
        }

        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                var cadastro = new TelaDeCadastro(null);
                respostaEventosTelaInicial = cadastro.ShowDialog(null);

                if (respostaEventosTelaInicial == DialogResult.OK)
                {
                    var results = _validator.Validate(cadastro.cliente);

                    if (results.IsValid)
                    {
                        _repositorioClienteLinq2Db.Criar(cadastro.cliente);
                        DataGridViewTelaInicial.DataSource = null;
                        DataGridViewTelaInicial.DataSource = _repositorioClienteLinq2Db.ObterTodos();
                    }
                    else
                    {
                        var erros = results.Errors.Select(erros => erros.ErrorMessage);
                        MessageBox.Show(string.Join("\n", erros), "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                        var clienteSelecionado = _repositorioClienteLinq2Db.ObterPorId(id);
                        var telaEdicao = new TelaDeCadastro(clienteSelecionado);
                        respostaEventosTelaInicial = telaEdicao.ShowDialog();

                        if (respostaEventosTelaInicial == DialogResult.OK)
                        {
                            var results = _validator.Validate(clienteSelecionado);

                            if (results.IsValid)
                            {
                                _repositorioClienteLinq2Db.Atualizar(clienteSelecionado);
                                DataGridViewTelaInicial.DataSource = null;
                                DataGridViewTelaInicial.DataSource = _repositorioClienteLinq2Db.ObterTodos();
                            }
                            else
                            {
                                var erros = results.Errors.Select(erros => erros.ErrorMessage);
                                MessageBox.Show(string.Join("\n", erros), "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

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
                        var clienteSelecionado = _repositorioClienteLinq2Db.ObterPorId(id);
                        respostaEventosTelaInicial = MessageBox.Show("Tem certeza que deseja excluir esse cliente ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (respostaEventosTelaInicial == DialogResult.Yes)
                        {
                            _repositorioClienteLinq2Db.Remover(clienteSelecionado.Id);
                            DataGridViewTelaInicial.DataSource = null;
                            DataGridViewTelaInicial.DataSource = _repositorioClienteLinq2Db.ObterTodos();
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






