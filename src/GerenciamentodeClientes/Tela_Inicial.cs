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
        DialogResult respostaEventosTelaInicial;
        private void ao_Clicar_Em_Cadastrar(object sender, EventArgs e)
        {
            try
            {
                var cadastro = new TelaDeCadastro(null);
                respostaEventosTelaInicial = cadastro.ShowDialog(null);

                if (respostaEventosTelaInicial == DialogResult.OK)
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
        private void ao_Clicar_Em_Editar(object sender, EventArgs e)
        {
            try
            {
                var index = Data_Grid_View1.CurrentCell.RowIndex;
                var pessoaSelecionada = Data_Grid_View1.Rows[index].DataBoundItem as Pessoa;
                var telaEdicao = new TelaDeCadastro(pessoaSelecionada);
                respostaEventosTelaInicial = telaEdicao.ShowDialog();

                if (respostaEventosTelaInicial == DialogResult.OK)
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
        private void ao_Clicar_Em_Excluir(object sender, EventArgs e)
        {
            try
            {
                var index = Data_Grid_View1.CurrentCell.RowIndex;
                var pessoaSelecionada = Data_Grid_View1.Rows[index].DataBoundItem as Pessoa;
                respostaEventosTelaInicial = MessageBox.Show("Tem certeza que deseja excluir esse cliente ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respostaEventosTelaInicial == DialogResult.Yes)
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





