using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            mskCPF.Text = pessoa.CPF;
            textEmail.Text = pessoa.Email;
            DateTimeDataDeNascimento.Value = pessoa.DatadeNascimento;

            DialogResult = DialogResult.OK;
        }
        public bool ValidaoGeral()
        {

            //ValidarEmail
            string email = textEmail.Text;
            var rgx = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            try
            {
                if (!rgx.IsMatch(email))
                {
                    throw new Exception("ERRO");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Email Inválido", ex.Message);
                return false;
            }

            //ValidarData
            DateTime DataSelecionada = new DateTime();
            DateTime DataAtual = DateTime.Now;
            TimeSpan DataLimite = DataSelecionada - DataAtual;

            if (DataLimite.TotalDays < (15*365))
            {
                MessageBox.Show("Você precisa ter mais de 15 anos", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                pessoa.DatadeNascimento = DateTimeDataDeNascimento.Value;
            }
            return true;
        }

        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            var result = ValidaoGeral();

            if (result == true)
            {
                if (pessoa.Id == Decimal.Zero)
                {
                    pessoa.Id = Pessoa.GerarID();
                }
                pessoa.Nome = textNome.Text;
                pessoa.CPF = mskCPF.Text;
                pessoa.Email = textEmail.Text;
                pessoa.DatadeNascimento = DateTimeDataDeNascimento.Value;

                DialogResult = DialogResult.OK;
            }

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
