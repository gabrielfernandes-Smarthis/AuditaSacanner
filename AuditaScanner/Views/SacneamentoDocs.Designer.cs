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
            chkDuplex = new CheckBox();
            comboBox1 = new ComboBox();
            tipoDocCb = new ComboBox();
            btnNovoScan = new Button();
            btnReload = new Button();
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
            vScrollBar1 = new VScrollBar();
            ((System.ComponentModel.ISupportInitialize)visualizarScan).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // visualizarScan
            // 
            visualizarScan.Location = new Point(12, 6);
            visualizarScan.Name = "visualizarScan";
            visualizarScan.Size = new Size(551, 637);
            visualizarScan.TabIndex = 0;
            visualizarScan.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chkDuplex);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(tipoDocCb);
            groupBox1.Controls.Add(btnNovoScan);
            groupBox1.Controls.Add(btnReload);
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
            groupBox1.Location = new Point(632, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(467, 637);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Opções";
            // 
            // chkDuplex
            // 
            chkDuplex.AutoSize = true;
            chkDuplex.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            chkDuplex.Location = new Point(373, 183);
            chkDuplex.Name = "chkDuplex";
            chkDuplex.Size = new Size(68, 19);
            chkDuplex.TabIndex = 6;
            chkDuplex.Text = "Duplex";
            chkDuplex.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "PDF", "JPEG", "BMP", "GIF", "TIFF", "PNG" });
            comboBox1.Location = new Point(216, 178);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 27);
            comboBox1.TabIndex = 5;
            // 
            // tipoDocCb
            // 
            tipoDocCb.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tipoDocCb.FormattingEnabled = true;
            tipoDocCb.Location = new Point(6, 178);
            tipoDocCb.Name = "tipoDocCb";
            tipoDocCb.Size = new Size(191, 27);
            tipoDocCb.TabIndex = 5;
            // 
            // btnNovoScan
            // 
            btnNovoScan.Font = new Font("Rubik", 17.9999981F, FontStyle.Regular, GraphicsUnit.Point);
            btnNovoScan.Location = new Point(6, 572);
            btnNovoScan.Name = "btnNovoScan";
            btnNovoScan.Size = new Size(450, 59);
            btnNovoScan.TabIndex = 4;
            btnNovoScan.Text = "Novo escaneamento";
            btnNovoScan.UseVisualStyleBackColor = true;
            btnNovoScan.Click += BtnNovoScan_Click;
            // 
            // btnReload
            // 
            btnReload.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnReload.Location = new Point(216, 24);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(143, 27);
            btnReload.TabIndex = 3;
            btnReload.Text = "Recarregar scanners";
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // btnLocalTemp
            // 
            btnLocalTemp.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnLocalTemp.Location = new Point(313, 297);
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
            nomeArquivo.Location = new Point(197, 235);
            nomeArquivo.Name = "nomeArquivo";
            nomeArquivo.ReadOnly = true;
            nomeArquivo.Size = new Size(239, 26);
            nomeArquivo.TabIndex = 2;
            // 
            // localTemp
            // 
            localTemp.Font = new Font("Rubik", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            localTemp.Location = new Point(6, 297);
            localTemp.Name = "localTemp";
            localTemp.Size = new Size(301, 25);
            localTemp.TabIndex = 2;
            // 
            // numeroAtendimento
            // 
            numeroAtendimento.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            numeroAtendimento.Location = new Point(6, 235);
            numeroAtendimento.Name = "numeroAtendimento";
            numeroAtendimento.Size = new Size(170, 26);
            numeroAtendimento.TabIndex = 2;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Rubik", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 18;
            listBox1.Location = new Point(6, 54);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(353, 76);
            listBox1.TabIndex = 1;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
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
            label3.Location = new Point(197, 217);
            label3.Name = "label3";
            label3.Size = new Size(114, 15);
            label3.TabIndex = 0;
            label3.Text = "Nome do arquivo:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(216, 160);
            label7.Name = "label7";
            label7.Size = new Size(63, 15);
            label7.TabIndex = 0;
            label7.Text = "Formato:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(6, 160);
            label5.Name = "label5";
            label5.Size = new Size(129, 15);
            label5.TabIndex = 0;
            label5.Text = "Tipo do documento:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(6, 279);
            label4.Name = "label4";
            label4.Size = new Size(207, 15);
            label4.TabIndex = 0;
            label4.Text = "Pasta de salvamento temporario:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Rubik", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(6, 217);
            label2.Name = "label2";
            label2.Size = new Size(142, 15);
            label2.TabIndex = 0;
            label2.Text = "Numero Atendimento:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(6, 27);
            label1.Name = "label1";
            label1.Size = new Size(161, 19);
            label1.TabIndex = 0;
            label1.Text = "Lista de dispositivos";
            // 
            // vScrollBar1
            // 
            vScrollBar1.LargeChange = 1;
            vScrollBar1.Location = new Point(566, 9);
            vScrollBar1.Maximum = 2;
            vScrollBar1.Minimum = 1;
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(13, 637);
            vScrollBar1.TabIndex = 3;
            vScrollBar1.Value = 1;
            // 
            // SacneamentoDocs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1111, 661);
            Controls.Add(vScrollBar1);
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
        private Button btnReload;
        private CheckBox chkDuplex;
        private VScrollBar vScrollBar1;
    }
}