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
using System.Reflection.Metadata;

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
        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            try
            {
                if (ValidacaoGeral())
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

                else
                {
                    MessageBox.Show("Preencha corretamento todos os campos antes de salvar", "AVISO");
                }
            }
            catch (Exception)
            {

                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }

        }
        public bool ValidacaoGeral()
        {          
            var Campo_Email = textEmail.Text;
            if (!Regex.IsMatch(Campo_Email, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$") || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email Inválido. Porfavor insira um endereço de e-mail válido.", "AVISO");
                return false;
            }

            var Campo_DataSelecionada = new DateTime();
            if (!DateTime.TryParse(DateTimeDataDeNascimento.Text, out Campo_DataSelecionada))
            {
                MessageBox.Show("Data de nascimento inválida", "AVISO");
                return false;
            }

            if (DateTime.Now.Year - Campo_DataSelecionada.Year < Pessoa.valorMinimoIdade)
            {
                MessageBox.Show("Você precisa ter mais de 15 anos", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var Campo_Nome = textNome.Text.Trim();
            if (string.IsNullOrEmpty(Campo_Nome) || !Regex.IsMatch(Campo_Nome, @"^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$"))
            {
                MessageBox.Show("O campo nome deve conter apenas letras e espaços.", "AVISO");
                return false;
            }

            var Campo_CPF = mskCPF.Text.Trim();
            if (string.IsNullOrEmpty(Campo_CPF) || !Regex.IsMatch(Campo_CPF, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            {
                MessageBox.Show("CPF inválido.", "AVISO");
                return false;
            }
            return true;
        }
        private void AoClicarEmCancelar(object sender, EventArgs e)
        {
            try
            {
                DialogResult Resposta;
                Resposta = MessageBox.Show("Deseja mesmo cancelar ?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Resposta == DialogResult.Yes)
                {
                    this.Close();
                }

            }
            catch (Exception)
            {

                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }

        }
    }
}
