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

        BindingList<Pessoa> pessoaList = new BindingList<Pessoa>();
        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            var cadastro = new TeladeCadastro(null);

            var resp = cadastro.ShowDialog(null);
            if (resp == DialogResult.OK)
            {
                pessoaList.Add(cadastro.pessoa);

                dtpData.DataSource = null;
                dtpData.DataSource = pessoaList;
            }
        }
        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            var index = dtpData.CurrentCell.RowIndex;
            var pessoaSelecionada = dtpData.Rows[index].DataBoundItem as Pessoa;

            var telaEdicao = new TeladeCadastro(pessoaSelecionada);
            var resp = telaEdicao.ShowDialog();
            if (resp == DialogResult.OK)
            {
                pessoaList[index] = telaEdicao.pessoa;

                dtpData.DataSource = null;
                dtpData.DataSource = pessoaList;
            }
        }

        private void AoClicarEmExcluir(object sender, EventArgs e)
        {


        }

    }
}
