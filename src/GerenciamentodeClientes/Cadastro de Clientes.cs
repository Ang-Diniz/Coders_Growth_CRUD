﻿using System;
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
        public TeladeCadastro(DataGridView dtpData)
        {
            dtp_Data = dtpData;
            InitializeComponent();
            if (pessoa == null)
            {
                pessoa = new Pessoa();
            }
        }

        private void AoClicarEmAdicionar(object sender, EventArgs e)
        {
            pessoa.Id = ObterProximoID(); 
            pessoa.Nome = textNome.Text;
            pessoa.CPF = textCPF.Text;
            pessoa.Email = textEmail.Text;
            pessoa.DatadeNascimento = textDatadeNascimento.Text;

            DialogResult = DialogResult.OK;

        }

        private void GerarID()
        {
            int id = 0;
            ++id;
            return id;
        }

    }
}
