namespace GerenciamentodeClientes
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
            DataGridViewTelaInicial = new DataGridView();
            btnCadastrar = new Button();
            btnEditar = new Button();
            btnExcluir = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)DataGridViewTelaInicial).BeginInit();
            SuspendLayout();
            // 
            // DataGridViewTelaInicial
            // 
            DataGridViewTelaInicial.AllowUserToAddRows = false;
            DataGridViewTelaInicial.AllowUserToOrderColumns = true;
            DataGridViewTelaInicial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGridViewTelaInicial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewTelaInicial.Location = new Point(12, 44);
            DataGridViewTelaInicial.Name = "DataGridViewTelaInicial";
            DataGridViewTelaInicial.RowHeadersWidth = 51;
            DataGridViewTelaInicial.RowTemplate.Height = 29;
            DataGridViewTelaInicial.Size = new Size(974, 379);
            DataGridViewTelaInicial.TabIndex = 0;
            // 
            // btnCadastrar
            // 
            btnCadastrar.BackColor = SystemColors.HotTrack;
            btnCadastrar.ForeColor = Color.White;
            btnCadastrar.Location = new Point(469, 434);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(133, 60);
            btnCadastrar.TabIndex = 1;
            btnCadastrar.Text = "Cadastro";
            btnCadastrar.UseVisualStyleBackColor = false;
            btnCadastrar.Click += AoClicarEmCadastrar;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = SystemColors.HotTrack;
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(632, 434);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(133, 60);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += AoClicarEmEditar;
            // 
            // btnExcluir
            // 
            btnExcluir.BackColor = Color.FromArgb(192, 0, 0);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.ImageAlign = ContentAlignment.MiddleLeft;
            btnExcluir.Location = new Point(797, 434);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(133, 60);
            btnExcluir.TabIndex = 3;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = false;
            btnExcluir.Click += AoClicarEmExcluir;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Light", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Image = Properties.Resources.pagina_inicial;
            label1.ImageAlign = ContentAlignment.MiddleRight;
            label1.Location = new Point(439, 9);
            label1.Name = "label1";
            label1.Size = new Size(145, 31);
            label1.TabIndex = 4;
            label1.Text = "Tela Inicial      ";
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
            Controls.Add(DataGridViewTelaInicial);
            Name = "TelaInicial";
            Text = "Tela_Inicial";
            ((System.ComponentModel.ISupportInitialize)DataGridViewTelaInicial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DataGridViewTelaInicial;
        private Button btnCadastrar;
        private Button btnEditar;
        private Button btnExcluir;
        private Label label1;
    }
}