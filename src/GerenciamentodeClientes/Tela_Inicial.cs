using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciamentodeClientes
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }
        DialogResult respostaEventosTelaInicial;
        RepositorioPessoaLista repositorioPessoaLista = new RepositorioPessoaLista();
        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                var cadastro = new TelaDeCadastro(null);
                respostaEventosTelaInicial = cadastro.ShowDialog(null);

                if (respostaEventosTelaInicial == DialogResult.OK)
                {
                    repositorioPessoaLista.Criar(cadastro.pessoa);

                    DataGridViewTelaInicial.DataSource = null;
                    DataGridViewTelaInicial.DataSource = repositorioPessoaLista.ObterTodos();
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
                if (PessoaListSingleton.Instancia.PessoaList.Count == Decimal.Zero)
                {
                    MessageBox.Show("Nenhum cliente para editar.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var index = DataGridViewTelaInicial.CurrentCell.RowIndex;
                    var pessoaSelecionada = DataGridViewTelaInicial.Rows[index].DataBoundItem as Pessoa;
                    var telaEdicao = new TelaDeCadastro(pessoaSelecionada);
                    repositorioPessoaLista.Atualizar(pessoaSelecionada);
                    respostaEventosTelaInicial = telaEdicao.ShowDialog();

                    if (respostaEventosTelaInicial == DialogResult.OK)
                    {
                        DataGridViewTelaInicial.DataSource = null;
                        DataGridViewTelaInicial.DataSource = repositorioPessoaLista.ObterTodos();
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
                if (PessoaListSingleton.Instancia.PessoaList.Count == Decimal.Zero)
                {
                    MessageBox.Show("Nenhum cliente para exlcuir.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var index = DataGridViewTelaInicial.CurrentCell.RowIndex;
                    var pessoaSelecionada = DataGridViewTelaInicial.Rows[index].DataBoundItem as Pessoa;
                    respostaEventosTelaInicial = MessageBox.Show("Tem certeza que deseja excluir esse cliente ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respostaEventosTelaInicial == DialogResult.Yes)
                    {
                        repositorioPessoaLista.Remover(pessoaSelecionada.Id);

                        DataGridViewTelaInicial.DataSource = null;
                        DataGridViewTelaInicial.DataSource = repositorioPessoaLista.ObterTodos();
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





