using System.Text.RegularExpressions;

namespace GerenciamentodeClientes
{
    public partial class TelaDeCadastro : Form
    {
        public Pessoa pessoa { get; set; }
        DialogResult respostaEventosCadastroClientes;
        public TelaDeCadastro(Pessoa pessoaSelecionada)
        {
            InitializeComponent();
            if (pessoaSelecionada == null)
            {
                pessoa = new Pessoa();
            }
            else
            {
                pessoa = pessoaSelecionada;
                preencher_Input_Da_Tela(pessoaSelecionada);
            }
        }

        private void preencher_Input_Da_Tela(Pessoa pessoaSelecionada)
        {
            textNome.Text = pessoa.Nome;
            mskCPF.Text = pessoa.CPF;
            textEmail.Text = pessoa.Email;
            dateTimeDataDeNascimento.Value = pessoa.dataDeNascimento;

            DialogResult = DialogResult.OK;
        }

        private void ao_Clicar_Em_Salvar(object sender, EventArgs e)
        {
            try
            {
                if (ValidacaoGeral())
                {
                    if (pessoa.Id == Decimal.Zero)
                    {
                        pessoa.Id = Pessoa.s_gerarID();
                    }
                    pessoa.Nome = textNome.Text;
                    pessoa.CPF = mskCPF.Text;
                    pessoa.Email = textEmail.Text;
                    pessoa.dataDeNascimento = dateTimeDataDeNascimento.Value;

                    DialogResult = DialogResult.OK;
                }

                else
                {
                    MessageBox.Show("Preencha corretamento todos os campos antes de salvar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {

                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }

        }

        public bool ValidacaoGeral()
        {
            var campoNome = textNome.Text.Trim();
            if (string.IsNullOrEmpty(campoNome) || !Regex.IsMatch(campoNome, @"^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$"))
            {
                MessageBox.Show("Nome inválido. O campo nome deve conter apenas letras e espaços.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var campoCPF = mskCPF.Text.Trim();
            if (string.IsNullOrEmpty(campoCPF) || !Regex.IsMatch(campoCPF, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            {
                MessageBox.Show("CPF inválido.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var campoDataSelecionada = new DateTime();
            if (!DateTime.TryParse(dateTimeDataDeNascimento.Text, out campoDataSelecionada))
            {
                return false;
            }

            if (DateTime.Now.Year - campoDataSelecionada.Year < Pessoa.valorMinimoIdade)
            {
                MessageBox.Show("Data Inválida. \nVocê precisa ter mais de 18 anos para se cadastrar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var campoEmail = textEmail.Text;
            if (!Regex.IsMatch(campoEmail, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$") || string.IsNullOrEmpty(campoEmail))
            {
                MessageBox.Show("Email Inválido. Por favor insira um endereço de e-mail válido. \nEx.: seunome@gmail.com"., "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ao_Clicar_Em_Cancelar(object sender, EventArgs e)
        {
            try
            {

                respostaEventosCadastroClientes = MessageBox.Show("Deseja mesmo cancelar ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respostaEventosCadastroClientes == DialogResult.Yes)
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
