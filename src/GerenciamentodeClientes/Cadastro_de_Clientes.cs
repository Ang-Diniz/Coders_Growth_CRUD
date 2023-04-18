using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciamentodeClientes
{
    public partial class TeladeCadastro : Form
    {
        public Pessoa pessoa { get; set; }
        public TeladeCadastro(Pessoa pessoaSelecionada)
        {
            InitializeComponent();
            if (pessoaSelecionada == null)
            {
                pessoa = new Pessoa();
            }
            else
            {
                pessoa = pessoaSelecionada;
                preencherInputDaTela(pessoaSelecionada);
            }
        }
        private void preencherInputDaTela(Pessoa pessoaSelecionada)
        {
            textNome.Text = pessoa.Nome;
            textCPF.Text = pessoa.CPF;
            textEmail.Text = pessoa.Email;
            textDatadeNascimento.Text = pessoa.DatadeNascimento;

            DialogResult = DialogResult.OK;
        }
        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            if (pessoa.Id == Decimal.Zero)
            {
                pessoa.Id = Pessoa.GerarID();
            }
            pessoa.Nome = textNome.Text;
            pessoa.CPF = textCPF.Text;
            pessoa.Email = textEmail.Text;
            pessoa.DatadeNascimento = textDatadeNascimento.Text;

            DialogResult = DialogResult.OK;

        }

        private void AoClicarEmCancelar(object sender, EventArgs e)
        {
            DialogResult Resposta;
            Resposta = MessageBox.Show("Deseja mesmo cancelar ?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Resposta == DialogResult.Yes)
            {
                this.Close();
            }

        }

    }
}
