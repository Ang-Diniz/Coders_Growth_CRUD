﻿namespace GerenciamentodeClientes
{
    partial class TelaInicial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Data_Grid_View1 = new DataGridView();
            btnCadastrar = new Button();
            btnEditar = new Button();
            btnExcluir = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)Data_Grid_View1).BeginInit();
            SuspendLayout();
            // 
            // Data_Grid_View1
            // 
            Data_Grid_View1.AllowUserToAddRows = false;
            Data_Grid_View1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Data_Grid_View1.Location = new Point(12, 44);
            Data_Grid_View1.Name = "dtpData";
            Data_Grid_View1.RowHeadersWidth = 51;
            Data_Grid_View1.RowTemplate.Height = 29;
            Data_Grid_View1.Size = new Size(974, 379);
            Data_Grid_View1.TabIndex = 0;
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(469, 434);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(133, 60);
            btnCadastrar.TabIndex = 1;
            btnCadastrar.Text = "Cadastro";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += AoClicarEmCadastrar;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(632, 434);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(133, 60);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += AoClicarEmEditar;
            // 
            // btnExcluir
            // 
            btnExcluir.ForeColor = Color.Red;
            btnExcluir.Location = new Point(797, 434);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(133, 60);
            btnExcluir.TabIndex = 3;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = true;
            
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Location = new Point(469, 9);
            label1.Name = "label1";
            label1.Size = new Size(81, 22);
            label1.TabIndex = 4;
            label1.Text = "Tela Inicial";
            // 
            // TelaInicial
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 506);
            Controls.Add(label1);
            Controls.Add(btnExcluir);
            Controls.Add(btnEditar);
            Controls.Add(btnCadastrar);
            Controls.Add(Data_Grid_View1);
            Name = "TelaInicial";
            Text = "Tela_Inicial";
            ((System.ComponentModel.ISupportInitialize)Data_Grid_View1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Data_Grid_View1;
        private Button btnCadastrar;
        private Button btnEditar;
        private Button btnExcluir;
        private Label label1;
    }
}