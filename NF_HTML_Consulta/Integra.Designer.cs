namespace Thor
{
    partial class Integra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Integra));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_cadastro = new System.Windows.Forms.Button();
            this.txt_data_ref = new System.Windows.Forms.MaskedTextBox();
            this.chbx_cfop = new System.Windows.Forms.CheckBox();
            this.txt_cfop = new System.Windows.Forms.TextBox();
            this.lbl_cfop = new System.Windows.Forms.Label();
            this.lbl_mes = new System.Windows.Forms.Label();
            this.btn_config = new System.Windows.Forms.Button();
            this.btn_ler_nota = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.btn_cadastro);
            this.groupBox1.Controls.Add(this.txt_data_ref);
            this.groupBox1.Controls.Add(this.chbx_cfop);
            this.groupBox1.Controls.Add(this.txt_cfop);
            this.groupBox1.Controls.Add(this.lbl_cfop);
            this.groupBox1.Controls.Add(this.lbl_mes);
            this.groupBox1.Controls.Add(this.btn_config);
            this.groupBox1.Controls.Add(this.btn_ler_nota);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1028, 61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btn_cadastro
            // 
            this.btn_cadastro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cadastro.Location = new System.Drawing.Point(825, 21);
            this.btn_cadastro.Name = "btn_cadastro";
            this.btn_cadastro.Size = new System.Drawing.Size(75, 23);
            this.btn_cadastro.TabIndex = 7;
            this.btn_cadastro.Text = "Cadastro";
            this.btn_cadastro.UseVisualStyleBackColor = true;
            this.btn_cadastro.Click += new System.EventHandler(this.btn_cadastro_Click);
            // 
            // txt_data_ref
            // 
            this.txt_data_ref.Location = new System.Drawing.Point(255, 21);
            this.txt_data_ref.Mask = "00/0000";
            this.txt_data_ref.Name = "txt_data_ref";
            this.txt_data_ref.Size = new System.Drawing.Size(63, 20);
            this.txt_data_ref.TabIndex = 1;
            this.txt_data_ref.Leave += new System.EventHandler(this.txt_data_ref_Leave_1);
            // 
            // chbx_cfop
            // 
            this.chbx_cfop.AutoSize = true;
            this.chbx_cfop.Location = new System.Drawing.Point(545, 25);
            this.chbx_cfop.Name = "chbx_cfop";
            this.chbx_cfop.Size = new System.Drawing.Size(105, 17);
            this.chbx_cfop.TabIndex = 3;
            this.chbx_cfop.Text = "CFOP ORGINAL";
            this.chbx_cfop.UseVisualStyleBackColor = true;
            this.chbx_cfop.CheckedChanged += new System.EventHandler(this.chbx_cfop_CheckedChanged);
            // 
            // txt_cfop
            // 
            this.txt_cfop.Location = new System.Drawing.Point(443, 22);
            this.txt_cfop.MaxLength = 4;
            this.txt_cfop.Name = "txt_cfop";
            this.txt_cfop.Size = new System.Drawing.Size(59, 20);
            this.txt_cfop.TabIndex = 2;
            this.txt_cfop.Leave += new System.EventHandler(this.txt_cfop_Leave);
            // 
            // lbl_cfop
            // 
            this.lbl_cfop.AutoSize = true;
            this.lbl_cfop.Location = new System.Drawing.Point(390, 24);
            this.lbl_cfop.Name = "lbl_cfop";
            this.lbl_cfop.Size = new System.Drawing.Size(35, 13);
            this.lbl_cfop.TabIndex = 4;
            this.lbl_cfop.Text = "CFOP";
            // 
            // lbl_mes
            // 
            this.lbl_mes.AutoSize = true;
            this.lbl_mes.Location = new System.Drawing.Point(130, 26);
            this.lbl_mes.Name = "lbl_mes";
            this.lbl_mes.Size = new System.Drawing.Size(119, 13);
            this.lbl_mes.TabIndex = 2;
            this.lbl_mes.Text = "MÊS DE REFERÊNCIA";
            // 
            // btn_config
            // 
            this.btn_config.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_config.Location = new System.Drawing.Point(940, 19);
            this.btn_config.Name = "btn_config";
            this.btn_config.Size = new System.Drawing.Size(75, 23);
            this.btn_config.TabIndex = 4;
            this.btn_config.Text = "&Config";
            this.btn_config.UseVisualStyleBackColor = true;
            this.btn_config.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_ler_nota
            // 
            this.btn_ler_nota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ler_nota.Location = new System.Drawing.Point(12, 19);
            this.btn_ler_nota.Name = "btn_ler_nota";
            this.btn_ler_nota.Size = new System.Drawing.Size(75, 23);
            this.btn_ler_nota.TabIndex = 6;
            this.btn_ler_nota.Text = "&Ler Nota";
            this.btn_ler_nota.UseVisualStyleBackColor = true;
            this.btn_ler_nota.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.webBrowser1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1028, 527);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 16);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1022, 508);
            this.webBrowser1.TabIndex = 5;
            this.webBrowser1.Url = new System.Uri("http://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoCont" +
                    "eudo=XbSeqxE8pl8=", System.UriKind.Absolute);
            // 
            // Integra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(1028, 588);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Integra";
            this.Text = "THOR 2.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_ler_nota;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btn_config;
        private System.Windows.Forms.Label lbl_mes;
        private System.Windows.Forms.Label lbl_cfop;
        private System.Windows.Forms.TextBox txt_cfop;
        private System.Windows.Forms.CheckBox chbx_cfop;
        private System.Windows.Forms.MaskedTextBox txt_data_ref;
        private System.Windows.Forms.Button btn_cadastro;
    }
}

