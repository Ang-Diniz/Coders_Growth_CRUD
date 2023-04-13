namespace GerenciamentodeClientes
{
    partial class TeladeCadastro
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            maskedTextBox1 = new MaskedTextBox();
            dateTimePicker1 = new DateTimePicker();
            textBox2 = new TextBox();
            btnCadastrar2 = new Button();
            btnCancelar = new Button();
            label5 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(12, 74);
            label1.Name = "label1";
            label1.Size = new Size(55, 22);
            label1.TabIndex = 0;
            label1.Text = "Nome:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(12, 138);
            label2.Name = "label2";
            label2.Size = new Size(38, 22);
            label2.TabIndex = 1;
            label2.Text = "CPF:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Location = new Point(12, 269);
            label3.Name = "label3";
            label3.Size = new Size(57, 22);
            label3.TabIndex = 2;
            label3.Text = "E-mail:";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Location = new Point(12, 203);
            label4.Name = "label4";
            label4.Size = new Size(147, 22);
            label4.TabIndex = 3;
            label4.Text = "Data de nascimento:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 99);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(373, 27);
            textBox1.TabIndex = 4;
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Location = new Point(12, 163);
            maskedTextBox1.Mask = "999,999,999-99";
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(125, 27);
            maskedTextBox1.TabIndex = 5;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(12, 228);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 294);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(373, 27);
            textBox2.TabIndex = 7;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // btnCadastrar2
            // 
            btnCadastrar2.Location = new Point(10, 386);
            btnCadastrar2.Name = "btnCadastrar2";
            btnCadastrar2.Size = new Size(127, 46);
            btnCadastrar2.TabIndex = 8;
            btnCadastrar2.Text = "Cadastrar";
            btnCadastrar2.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.ForeColor = Color.Red;
            btnCancelar.Location = new Point(258, 386);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(127, 46);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BorderStyle = BorderStyle.Fixed3D;
            label5.ForeColor = SystemColors.HotTrack;
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(124, 22);
            label5.TabIndex = 10;
            label5.Text = "Dados do cliente";
            // 
            // TeladeCadastro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 444);
            Controls.Add(label5);
            Controls.Add(btnCancelar);
            Controls.Add(btnCadastrar2);
            Controls.Add(textBox2);
            Controls.Add(dateTimePicker1);
            Controls.Add(maskedTextBox1);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "TeladeCadastro";
            Text = "Tela de Cadastro";
            Load += TeladeCadastro_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private MaskedTextBox maskedTextBox1;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox2;
        private Button btnCadastrar2;
        private Button btnCancelar;
        private Label label5;
    }
}