using System.Text.RegularExpressions;

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
            DateTimeDataDeNascimento.Value = pessoa.dataDeNascimento;

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
                    pessoa.dataDeNascimento = DateTimeDataDeNascimento.Value;

                    DialogResult = DialogResult.OK;
                }

                else
                {
                    MessageBox.Show("Preencha corretamento todos os campos antes de salvar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {

                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }

        }

        public bool ValidacaoGeral()
        {
            var campo_Nome = textNome.Text.Trim();
            if (string.IsNullOrEmpty(campo_Nome) || !Regex.IsMatch(campo_Nome, @"^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$"))
            {
                MessageBox.Show("Nome inválido. O campo nome deve conter apenas letras e espaços.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var campo_CPF = mskCPF.Text.Trim();
            if (string.IsNullOrEmpty(campo_CPF) || !Regex.IsMatch(campo_CPF, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            {
                MessageBox.Show("CPF inválido.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var campo_dataSelecionada = new DateTime();
            if (!DateTime.TryParse(DateTimeDataDeNascimento.Text, out campo_dataSelecionada))
            {
                return false;
            }

            if (DateTime.Now.Year - campo_dataSelecionada.Year < Pessoa.valorMinimoIdade)
            {
                MessageBox.Show("Data Inválida. \nVocê precisa ter mais de 15 anos para se cadastrar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var campo_Email = textEmail.Text;
            if (!Regex.IsMatch(campo_Email, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$") || string.IsNullOrEmpty(campo_Email))
            {
                MessageBox.Show("Email Inválido. Por favor insira um endereço de e-mail válido. \nEx.: seunome@gmail.com", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void AoClicarEmCancelar(object sender, EventArgs e)
        {
            try
            {
                DialogResult respostaBtnCancelar;
                respostaBtnCancelar = MessageBox.Show("Deseja mesmo cancelar ?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respostaBtnCancelar == DialogResult.Yes)
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
