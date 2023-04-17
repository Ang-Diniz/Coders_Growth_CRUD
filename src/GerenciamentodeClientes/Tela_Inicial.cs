using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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

        BindingList<Pessoa> pessoaList = new BindingList<Pessoa>();
        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            var cadastro = new TeladeCadastro();

            var resp = cadastro.ShowDialog(null);
            if (resp == DialogResult.OK)
            {
                pessoaList.Add(cadastro.pessoa);

                dtpData.DataSource = null;
                dtpData.DataSource = pessoaList;
            }
        }
        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            /*dtpData.CurrentCell.RowIndex;
            dtpData.Rows.*/
        }

        private void AoClicarEmExcluir(object sender, EventArgs e)
        {
            int index = dtpData.CurrentCell.RowIndex;
            dtpData.Rows.RemoveAt(index);
            MessageBox.Show("Um cliente foi removido", "AVISO", MessageBoxButtons.OK);
        }

    }
}
