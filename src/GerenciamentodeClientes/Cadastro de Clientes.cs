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
        DataGridView dtp_Data = new DataGridView();
        TelaInicial t1 = new TelaInicial();
        public TeladeCadastro(DataGridView dtpData)
        {
            dtp_Data = dtpData;
            InitializeComponent();
            if (pessoa == null)
            {
                pessoa = new Pessoa();
            }
        }

        private void AoClicarEmSalvar(object sender, EventArgs e)
        {
            
            int IDE = t1.GerarID();
            pessoa.Id = IDE; 
            pessoa.Nome = textNome.Text;
            pessoa.CPF = textCPF.Text;
            pessoa.Email = textEmail.Text;
            pessoa.DatadeNascimento = textDatadeNascimento.Text;

            DialogResult = DialogResult.OK;

        }

        private void AoClicarEmCancelar (object sender, EventArgs e)
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
