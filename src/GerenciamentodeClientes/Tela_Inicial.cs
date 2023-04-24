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

        List<Pessoa> pessoaList = new List<Pessoa>();
        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                var cadastro = new TeladeCadastro(null);

                var respostaBtnCadastrar = cadastro.ShowDialog(null);
                if (respostaBtnCadastrar == DialogResult.OK)
                {
                    pessoaList.Add(cadastro.pessoa);

                    Data_Grid_View1.DataSource = null;
                    Data_Grid_View1.DataSource = pessoaList;
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }
        }
        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            try
            {
                var index = Data_Grid_View1.CurrentCell.RowIndex;
                var pessoaSelecionada = Data_Grid_View1.Rows[index].DataBoundItem as Pessoa;

                var telaEdicao = new TeladeCadastro(pessoaSelecionada);
                var respostaBtnEditar = telaEdicao.ShowDialog();
                if (respostaBtnEditar == DialogResult.OK)
                {
                    Data_Grid_View1.DataSource = null;
                    Data_Grid_View1.DataSource = pessoaList;
                }

            }
            catch (Exception)
            {
                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }

        }
        private void AoClicarEmExcluir(object sender, EventArgs e)
        {
            try
            {
                var index2 = Data_Grid_View1.CurrentCell.RowIndex;
                var pessoaSelecionada = Data_Grid_View1.Rows[index2].DataBoundItem as Pessoa;

                DialogResult respostaBtnExcluir;
                respostaBtnExcluir = MessageBox.Show("Tem certeza que deseja excluir esse cliente ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respostaBtnExcluir == DialogResult.Yes)
                {
                    pessoaList.Remove(pessoaSelecionada);

                    Data_Grid_View1.DataSource = null;
                    Data_Grid_View1.DataSource = pessoaList;

                }
            }
            catch (Exception)
            {
                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }
        }
    }
}





