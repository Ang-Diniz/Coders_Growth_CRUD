using System.Text.RegularExpressions;

namespace GerenciamentodeClientes
{
    public partial class TelaDeCadastro : Form
    {
        public Pessoa cliente { get; set; }
        DialogResult respostaEventosCadastroClientes;
        public TelaDeCadastro(Pessoa ClienteSelecionada)
        {
            InitializeComponent();
            if (ClienteSelecionada == null)
            {
                cliente = new Pessoa();
            }
            else
            {
                cliente = ClienteSelecionada;
                PreencherInputDaTela();
            }
        }

        private void PreencherInputDaTela()
        {
            textNome.Text = cliente.Nome;
            mskCPF.Text = cliente.CPF;
            textEmail.Text = cliente.Email;
            dateTimeDataDeNascimento.Value = cliente.DataDeNascimento;

            DialogResult = DialogResult.OK;
        }

        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            try
            {
                if (ValidacaoGeral())
                {
                    cliente.Nome = textNome.Text;
                    cliente.CPF = mskCPF.Text;
                    cliente.Email = textEmail.Text;
                    cliente.DataDeNascimento = dateTimeDataDeNascimento.Value;

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
            var campoEmail = textEmail.Text;

            if (string.IsNullOrEmpty(campoNome) || !Regex.IsMatch(campoNome, @"^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$"))
            {
                erros.Add("Nome inválido. O campo nome deve conter apenas letras e espaços.\n");
            }

            if (string.IsNullOrEmpty(campoCPF) || !Regex.IsMatch(campoCPF, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            {
                erros.Add("CPF inválido. Por favor insira um CPF válido.\n");
            }

            if (DateTime.Now.Year - dateTimeDataDeNascimento.Value.Year < Pessoa.valorMinimoIdade)
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
                respostaEventosCadastroClientes = MessageBox.Show("Deseja mesmo cancelar ?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respostaEventosCadastroClientes == DialogResult.Yes)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado. Contate o administrador do sistema.", ex.Message);
            }
        }
    }
}
