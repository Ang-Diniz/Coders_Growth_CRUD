namespace GerenciamentodeClientes
{
    partial class TelaDeCadastro
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
            label5 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textNome = new TextBox();
            mskCPF = new MaskedTextBox();
            dateTimeDataDeNascimento = new DateTimePicker();
            textEmail = new TextBox();
            btnSalvar = new Button();
            btnCancelar = new Button();
            label6 = new Label();
            label7 = new Label();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ControlDarkDark;
            label5.Location = new Point(119, 9);
            label5.Name = "label5";
            label5.Size = new Size(143, 20);
            label5.TabIndex = 10;
            label5.Text = "Cadastro de clientes";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 74);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 138);
            label2.Name = "label2";
            label2.Size = new Size(36, 20);
            label2.TabIndex = 1;
            label2.Text = "CPF:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 269);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 2;
            label3.Text = "E-mail:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 203);
            label4.Name = "label4";
            label4.Size = new Size(0, 20);
            label4.TabIndex = 3;
            // 
            // textNome
            // 
            textNome.Location = new Point(11, 97);
            textNome.Name = "textNome";
            textNome.Size = new Size(374, 27);
            textNome.TabIndex = 0;
            // 
            // mskCPF
            // 
            mskCPF.Location = new Point(12, 163);
            mskCPF.Mask = "999,999,999-99";
            mskCPF.Name = "mskCPF";
            mskCPF.Size = new Size(125, 27);
            mskCPF.TabIndex = 5;
            // 
            // dateTimeDataDeNascimento
            // 
            dateTimeDataDeNascimento.Format = DateTimePickerFormat.Short;
            dateTimeDataDeNascimento.Location = new Point(12, 226);
            dateTimeDataDeNascimento.MaxDate = new DateTime(2023, 12, 31, 0, 0, 0, 0);
            dateTimeDataDeNascimento.MinDate = new DateTime(1940, 1, 1, 0, 0, 0, 0);
            dateTimeDataDeNascimento.Name = "dateTimeDataDeNascimento";
            dateTimeDataDeNascimento.Size = new Size(157, 27);
            dateTimeDataDeNascimento.TabIndex = 6;
            // 
            // textEmail
            // 
            textEmail.Location = new Point(12, 294);
            textEmail.Name = "textEmail";
            textEmail.Size = new Size(373, 27);
            textEmail.TabIndex = 7;
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = SystemColors.HotTrack;
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(10, 386);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(127, 46);
            btnSalvar.TabIndex = 8;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += AoClicarEmSalvar;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(192, 0, 0);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(258, 386);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(127, 46);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += AoClicarEmCancelar;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 74);
            label6.Name = "label6";
            label6.Size = new Size(53, 20);
            label6.TabIndex = 11;
            label6.Text = "Nome:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 203);
            label7.Name = "label7";
            label7.Size = new Size(148, 20);
            label7.TabIndex = 14;
            label7.Text = "Data de Nascimento:";
            // 
            // TelaDeCadastro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 444);
            Controls.Add(dateTimeDataDeNascimento);
            Controls.Add(label7);
            Controls.Add(textNome);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(textEmail);
            Controls.Add(mskCPF);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "TelaDeCadastro";
            Text = "Tela de Cadastro";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textNome;
        private MaskedTextBox mskCPF;
        private TextBox textEmail;
        private Button btnSalvar;
        private Button btnCancelar;
        private Label label6;
        private Label label7;
        private DateTimePicker dateTimeDataDeNascimento;
    }
}