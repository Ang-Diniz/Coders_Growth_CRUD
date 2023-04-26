using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace GerenciamentodeClientes
{
    public partial class TelaDeCadastro : Form
    {
        public Pessoa pessoa { get; set; }
        DialogResult respostaEventosCadastroClientes;
        public Pessoa PessoaSelecionada { get; set; }
        public TelaDeCadastro(Pessoa PessoaSelecionada)
        {
            InitializeComponent();
            if (PessoaSelecionada == null)
            {
                pessoa = new Pessoa();
            }
            else
            {
                pessoa = PessoaSelecionada;
                PreencherInputDaTela(PessoaSelecionada);
            }
        }
        private void PreencherInputDaTela(Pessoa PessoaSelecionada)
        {
            textNome.Text = pessoa.Nome;
            mskCPF.Text = pessoa.CPF;
            textEmail.Text = pessoa.Email;
            dateTimeDataDeNascimento.Value = pessoa.DataDeNascimento;

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
                    pessoa.DataDeNascimento = dateTimeDataDeNascimento.Value;

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Preencha corretamento todos os campos antes de salvar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado. Contate o administrador do sistema.", ex.Message);
            }
        }
        public bool ValidacaoGeral()
        {
            var erros = new List<string>();

            var campoNome = textNome.Text.Trim();
            var campoCPF = mskCPF.Text.Trim();
            var campoDataSelecionada = new DateTime();
            var campoEmail = textEmail.Text;

            if (string.IsNullOrEmpty(campoNome) || !Regex.IsMatch(campoNome, @"^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$"))
            {
                erros.Add("Nome inválido. O campo nome deve conter apenas letras e espaços.\n");
            }
            
            if (string.IsNullOrEmpty(campoCPF) || !Regex.IsMatch(campoCPF, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            {
                erros.Add("CPF inválido. Por favor insira um CPF válido.\n");
            }
            
            if (!DateTime.TryParse(dateTimeDataDeNascimento.Text, out campoDataSelecionada))
            {
                return false;
            }

            if (DateTime.Now.Year - campoDataSelecionada.Year < Pessoa.valorMinimoIdade)
            {
                erros.Add("Data Inválida. \nVocê precisa ter mais de 18 anos para se cadastrar.\n");
            }
            
            if (string.IsNullOrEmpty(campoEmail) || !Regex.IsMatch(campoEmail, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$"))
            {
                erros.Add("Email Inválido. Por favor insira um endereço de e-mail válido. \nExemplo: seunome@gmail.com\n");
            }

            if (erros.Any())
            {
                MessageBox.Show(string.Join("\n", erros), "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void AoClicarEmCancelar(object sender, EventArgs e)
        {
            try
            {
                respostaEventosCadastroClientes = MessageBox.Show("Deseja mesmo cancelar ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respostaEventosCadastroClientes == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado. Contate o administrador do sistema.", ex.Message);
            }
        }
    }
}
