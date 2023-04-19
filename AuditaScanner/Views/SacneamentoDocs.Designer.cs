namespace AuditaScanner.Views
{
    partial class SacneamentoDocs
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
            visualizarScan = new PictureBox();
            groupBox1 = new GroupBox();
            comboBox1 = new ComboBox();
            tipoDocCb = new ComboBox();
            btnNovoScan = new Button();
            btnLocalTemp = new Button();
            nomeArquivo = new TextBox();
            localTemp = new TextBox();
            numeroAtendimento = new TextBox();
            listBox1 = new ListBox();
            label6 = new Label();
            label3 = new Label();
            label7 = new Label();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)visualizarScan).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // visualizarScan
            // 
            visualizarScan.Location = new Point(12, 12);
            visualizarScan.Name = "visualizarScan";
            visualizarScan.Size = new Size(544, 637);
            visualizarScan.TabIndex = 0;
            visualizarScan.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(tipoDocCb);
            groupBox1.Controls.Add(btnNovoScan);
            groupBox1.Controls.Add(btnLocalTemp);
            groupBox1.Controls.Add(nomeArquivo);
            groupBox1.Controls.Add(localTemp);
            groupBox1.Controls.Add(numeroAtendimento);
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(584, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(388, 637);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Opções";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "PNG", "JPEG", "BMP", "GIF", "TIFF", "PDF" });
            comboBox1.Location = new Point(231, 199);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 27);
            comboBox1.TabIndex = 5;
            // 
            // tipoDocCb
            // 
            tipoDocCb.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tipoDocCb.FormattingEnabled = true;
            tipoDocCb.Location = new Point(6, 199);
            tipoDocCb.Name = "tipoDocCb";
            tipoDocCb.Size = new Size(207, 27);
            tipoDocCb.TabIndex = 5;
            // 
            // btnNovoScan
            // 
            btnNovoScan.Font = new Font("Rubik", 17.9999981F, FontStyle.Regular, GraphicsUnit.Point);
            btnNovoScan.Location = new Point(6, 572);
            btnNovoScan.Name = "btnNovoScan";
            btnNovoScan.Size = new Size(376, 59);
            btnNovoScan.TabIndex = 4;
            btnNovoScan.Text = "Novo escaneamento";
            btnNovoScan.UseVisualStyleBackColor = true;
            btnNovoScan.Click += btnNovoScan_Click;
            // 
            // btnLocalTemp
            // 
            btnLocalTemp.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnLocalTemp.Location = new Point(259, 261);
            btnLocalTemp.Name = "btnLocalTemp";
            btnLocalTemp.Size = new Size(123, 25);
            btnLocalTemp.TabIndex = 3;
            btnLocalTemp.Text = "Escolher local";
            btnLocalTemp.UseVisualStyleBackColor = true;
            btnLocalTemp.Click += btnLocalTemp_Click;
            // 
            // nomeArquivo
            // 
            nomeArquivo.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nomeArquivo.Location = new Point(197, 142);
            nomeArquivo.Name = "nomeArquivo";
            nomeArquivo.ReadOnly = true;
            nomeArquivo.Size = new Size(185, 26);
            nomeArquivo.TabIndex = 2;
            // 
            // localTemp
            // 
            localTemp.Font = new Font("Rubik", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            localTemp.Location = new Point(6, 261);
            localTemp.Name = "localTemp";
            localTemp.Size = new Size(247, 25);
            localTemp.TabIndex = 2;
            // 
            // numeroAtendimento
            // 
            numeroAtendimento.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            numeroAtendimento.Location = new Point(6, 142);
            numeroAtendimento.Name = "numeroAtendimento";
            numeroAtendimento.Size = new Size(170, 26);
            numeroAtendimento.TabIndex = 2;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Rubik", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 18;
            listBox1.Location = new Point(6, 41);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(376, 76);
            listBox1.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(197, 181);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(197, 124);
            label3.Name = "label3";
            label3.Size = new Size(114, 15);
            label3.TabIndex = 0;
            label3.Text = "Nome do arquivo:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(231, 181);
            label7.Name = "label7";
            label7.Size = new Size(63, 15);
            label7.TabIndex = 0;
            label7.Text = "Formato:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(6, 181);
            label5.Name = "label5";
            label5.Size = new Size(129, 15);
            label5.TabIndex = 0;
            label5.Text = "Tipo do documento:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(6, 243);
            label4.Name = "label4";
            label4.Size = new Size(207, 15);
            label4.TabIndex = 0;
            label4.Text = "Pasta de salvamento temporario:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(6, 124);
            label2.Name = "label2";
            label2.Size = new Size(142, 15);
            label2.TabIndex = 0;
            label2.Text = "Numero Atendimento:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(161, 19);
            label1.TabIndex = 0;
            label1.Text = "Lista de dispositivos";
            // 
            // SacneamentoDocs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(groupBox1);
            Controls.Add(visualizarScan);
            Name = "SacneamentoDocs";
            Text = "Scanner Audita";
            FormClosed += SacneamentoDocs_FormClosed;
            Load += SacneamentoDocs_Load;
            ((System.ComponentModel.ISupportInitialize)visualizarScan).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox visualizarScan;
        private GroupBox groupBox1;
        private ListBox listBox1;
        private Label label1;
        private Button btnNovoScan;
        private Button btnLocalTemp;
        private TextBox nomeArquivo;
        private TextBox localTemp;
        private TextBox numeroAtendimento;
        private Label label3;
        private Label label4;
        private Label label2;
        private ComboBox tipoDocCb;
        private Label label5;
        private ComboBox comboBox1;
        private Label label6;
        private Label label7;
    }
}