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
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }
        List<Pessoa> pessoaList = new List<Pessoa>();
        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            var cadastro = new TeladeCadastro(dtpData);

            var resp = cadastro.ShowDialog();
            if (resp == DialogResult.OK)
            {
                pessoaList.Add(cadastro.pessoa);

                dtpData.DataSource = null;
                dtpData.DataSource = pessoaList;
            }
        }
   
    }
}
