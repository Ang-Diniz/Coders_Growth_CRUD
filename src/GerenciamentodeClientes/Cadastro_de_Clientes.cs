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

        /*public static bool validarCPF(string mskCPF )
        {
            if (valor.Length != 11)

                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")

                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)

                    return false;
            }
            else if (numeros[9] != 11 - resultado)

                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[10] != 0)

                    return false;
            }
            else

                if (numeros[10] != 11 - resultado)

                return false;

            return true;

        }*/

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
            mskCPF.Text = pessoa.CPF;
            textEmail.Text = pessoa.Email;
            textDatadeNascimento.Text = pessoa.DatadeNascimento;

            DialogResult = DialogResult.OK;
        }
        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            /*{
                if (TeladeCadastro.validarCPF(mskCPF.Text))

                    MessageBox.Show("CPF Válido!");
                else
                    MessageBox.Show("CPF Inválido!");
            }*/

            if (pessoa.Id == Decimal.Zero)
            {
                pessoa.Id = Pessoa.GerarID();
            }
            pessoa.Nome = textNome.Text;
            pessoa.CPF = mskCPF.Text;
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
