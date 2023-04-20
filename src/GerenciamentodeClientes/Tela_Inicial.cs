﻿using System;
using System.Collections;
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
            try
            {
                var cadastro = new TeladeCadastro(null);

                var resp = cadastro.ShowDialog(null);
                if (resp == DialogResult.OK)
                {
                    pessoaList.Add(cadastro.pessoa);

                    Data_Grid_View1.DataSource = null;
                    Data_Grid_View1.DataSource = pessoaList;
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }
        }
        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            try
            {
                var index = Data_Grid_View1.CurrentCell.RowIndex;
                var pessoaSelecionada = Data_Grid_View1.Rows[index].DataBoundItem as Pessoa;

                var telaEdicao = new TeladeCadastro(pessoaSelecionada);
                var resp = telaEdicao.ShowDialog();
                if (resp == DialogResult.OK)
                {
                    Data_Grid_View1.DataSource = null;
                    Data_Grid_View1.DataSource = pessoaList;
                }

            }
            catch (Exception)
            {
                throw new Exception("Erro inesperado, contate o administrador do sistema.");
            }

        }
    }
}





