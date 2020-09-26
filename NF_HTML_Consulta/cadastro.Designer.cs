namespace Thor
{
    partial class form_cadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_cadastro));
            this.btn_limpar = new System.Windows.Forms.Button();
            this.btn_gravar = new System.Windows.Forms.Button();
            this.txt_nome_empresa = new System.Windows.Forms.TextBox();
            this.txt_nro_empresa = new System.Windows.Forms.TextBox();
            this.btn_novo = new System.Windows.Forms.Button();
            this.txt_cnpj_empresa = new System.Windows.Forms.TextBox();
            this.lbl_nome_empresa = new System.Windows.Forms.Label();
            this.lbl_cod = new System.Windows.Forms.Label();
            this.lbl_cnpj = new System.Windows.Forms.Label();
            this.grp_empresa = new System.Windows.Forms.GroupBox();
            this.chk_filial = new System.Windows.Forms.CheckBox();
            this.chk_matriz = new System.Windows.Forms.CheckBox();
            this.chk_SIMPLES = new System.Windows.Forms.CheckBox();
            this.chk_real = new System.Windows.Forms.CheckBox();
            this.chk_presumido = new System.Windows.Forms.CheckBox();
            this.chk_comercio = new System.Windows.Forms.CheckBox();
            this.chk_industria = new System.Windows.Forms.CheckBox();
            this.txt_regime_especial = new System.Windows.Forms.TextBox();
            this.chk_ativa = new System.Windows.Forms.CheckBox();
            this.lbl_regime_especial = new System.Windows.Forms.Label();
            this.chk_itens_prod = new System.Windows.Forms.CheckBox();
            this.grp_cadastro = new System.Windows.Forms.GroupBox();
            this.chk_pisconfis = new System.Windows.Forms.CheckBox();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.grp_empresa.SuspendLayout();
            this.grp_cadastro.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_limpar
            // 
            this.btn_limpar.Enabled = false;
            this.btn_limpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_limpar.Location = new System.Drawing.Point(329, 381);
            this.btn_limpar.Name = "btn_limpar";
            this.btn_limpar.Size = new System.Drawing.Size(75, 23);
            this.btn_limpar.TabIndex = 0;
            this.btn_limpar.Text = "Limpar";
            this.btn_limpar.UseVisualStyleBackColor = true;
            this.btn_limpar.Click += new System.EventHandler(this.btn_limpar_Click);
            // 
            // btn_gravar
            // 
            this.btn_gravar.Enabled = false;
            this.btn_gravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gravar.Location = new System.Drawing.Point(516, 381);
            this.btn_gravar.Name = "btn_gravar";
            this.btn_gravar.Size = new System.Drawing.Size(75, 23);
            this.btn_gravar.TabIndex = 7;
            this.btn_gravar.Text = "Gravar";
            this.btn_gravar.UseVisualStyleBackColor = true;
            this.btn_gravar.Click += new System.EventHandler(this.btn_gravar_Click);
            // 
            // txt_nome_empresa
            // 
            this.txt_nome_empresa.AllowDrop = true;
            this.txt_nome_empresa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nome_empresa.Location = new System.Drawing.Point(64, 65);
            this.txt_nome_empresa.Name = "txt_nome_empresa";
            this.txt_nome_empresa.Size = new System.Drawing.Size(100, 20);
            this.txt_nome_empresa.TabIndex = 1;
            // 
            // txt_nro_empresa
            // 
            this.txt_nro_empresa.Location = new System.Drawing.Point(217, 65);
            this.txt_nro_empresa.MaxLength = 4;
            this.txt_nro_empresa.Name = "txt_nro_empresa";
            this.txt_nro_empresa.Size = new System.Drawing.Size(55, 20);
            this.txt_nro_empresa.TabIndex = 2;
            // 
            // btn_novo
            // 
            this.btn_novo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_novo.Location = new System.Drawing.Point(120, 381);
            this.btn_novo.Name = "btn_novo";
            this.btn_novo.Size = new System.Drawing.Size(75, 23);
            this.btn_novo.TabIndex = 0;
            this.btn_novo.Text = "Novo";
            this.btn_novo.UseVisualStyleBackColor = true;
            this.btn_novo.Click += new System.EventHandler(this.btn_novo_Click);
            // 
            // txt_cnpj_empresa
            // 
            this.txt_cnpj_empresa.Location = new System.Drawing.Point(318, 53);
            this.txt_cnpj_empresa.MaxLength = 14;
            this.txt_cnpj_empresa.Name = "txt_cnpj_empresa";
            this.txt_cnpj_empresa.Size = new System.Drawing.Size(100, 20);
            this.txt_cnpj_empresa.TabIndex = 3;
            // 
            // lbl_nome_empresa
            // 
            this.lbl_nome_empresa.AutoSize = true;
            this.lbl_nome_empresa.Location = new System.Drawing.Point(61, 49);
            this.lbl_nome_empresa.Name = "lbl_nome_empresa";
            this.lbl_nome_empresa.Size = new System.Drawing.Size(94, 13);
            this.lbl_nome_empresa.TabIndex = 4;
            this.lbl_nome_empresa.Text = "NOME EMPRESA";
            // 
            // lbl_cod
            // 
            this.lbl_cod.AutoSize = true;
            this.lbl_cod.Location = new System.Drawing.Point(214, 49);
            this.lbl_cod.Name = "lbl_cod";
            this.lbl_cod.Size = new System.Drawing.Size(79, 13);
            this.lbl_cod.TabIndex = 5;
            this.lbl_cod.Text = "COD. EFISCAL";
            // 
            // lbl_cnpj
            // 
            this.lbl_cnpj.AutoSize = true;
            this.lbl_cnpj.Location = new System.Drawing.Point(320, 49);
            this.lbl_cnpj.Name = "lbl_cnpj";
            this.lbl_cnpj.Size = new System.Drawing.Size(34, 13);
            this.lbl_cnpj.TabIndex = 6;
            this.lbl_cnpj.Text = "CNPJ";
            // 
            // grp_empresa
            // 
            this.grp_empresa.Controls.Add(this.chk_filial);
            this.grp_empresa.Controls.Add(this.chk_matriz);
            this.grp_empresa.Controls.Add(this.chk_SIMPLES);
            this.grp_empresa.Controls.Add(this.chk_real);
            this.grp_empresa.Controls.Add(this.chk_presumido);
            this.grp_empresa.Controls.Add(this.chk_comercio);
            this.grp_empresa.Controls.Add(this.chk_industria);
            this.grp_empresa.Location = new System.Drawing.Point(64, 112);
            this.grp_empresa.Name = "grp_empresa";
            this.grp_empresa.Size = new System.Drawing.Size(805, 123);
            this.grp_empresa.TabIndex = 7;
            this.grp_empresa.TabStop = false;
            this.grp_empresa.Text = "Tipo empresa";
            // 
            // chk_filial
            // 
            this.chk_filial.AutoSize = true;
            this.chk_filial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_filial.Location = new System.Drawing.Point(125, 61);
            this.chk_filial.Name = "chk_filial";
            this.chk_filial.Size = new System.Drawing.Size(54, 17);
            this.chk_filial.TabIndex = 5;
            this.chk_filial.Text = "FILIAL";
            this.chk_filial.UseVisualStyleBackColor = true;
            this.chk_filial.CheckedChanged += new System.EventHandler(this.chk_filial_CheckedChanged);
            // 
            // chk_matriz
            // 
            this.chk_matriz.AutoSize = true;
            this.chk_matriz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_matriz.Location = new System.Drawing.Point(12, 61);
            this.chk_matriz.Name = "chk_matriz";
            this.chk_matriz.Size = new System.Drawing.Size(64, 17);
            this.chk_matriz.TabIndex = 5;
            this.chk_matriz.Text = "MATRIZ";
            this.chk_matriz.UseVisualStyleBackColor = true;
            this.chk_matriz.CheckedChanged += new System.EventHandler(this.chk_matriz_CheckedChanged);
            // 
            // chk_SIMPLES
            // 
            this.chk_SIMPLES.AutoSize = true;
            this.chk_SIMPLES.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_SIMPLES.Location = new System.Drawing.Point(216, 100);
            this.chk_SIMPLES.Name = "chk_SIMPLES";
            this.chk_SIMPLES.Size = new System.Drawing.Size(69, 17);
            this.chk_SIMPLES.TabIndex = 6;
            this.chk_SIMPLES.Text = "SIMPLES";
            this.chk_SIMPLES.UseVisualStyleBackColor = true;
            this.chk_SIMPLES.CheckedChanged += new System.EventHandler(this.chk_SIMPLES_CheckedChanged);
            // 
            // chk_real
            // 
            this.chk_real.AutoSize = true;
            this.chk_real.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_real.Location = new System.Drawing.Point(125, 100);
            this.chk_real.Name = "chk_real";
            this.chk_real.Size = new System.Drawing.Size(51, 17);
            this.chk_real.TabIndex = 6;
            this.chk_real.Text = "REAL";
            this.chk_real.UseVisualStyleBackColor = true;
            this.chk_real.CheckedChanged += new System.EventHandler(this.chk_real_CheckedChanged);
            // 
            // chk_presumido
            // 
            this.chk_presumido.AutoSize = true;
            this.chk_presumido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_presumido.Location = new System.Drawing.Point(12, 100);
            this.chk_presumido.Name = "chk_presumido";
            this.chk_presumido.Size = new System.Drawing.Size(88, 17);
            this.chk_presumido.TabIndex = 6;
            this.chk_presumido.Text = "PRESUMIDO";
            this.chk_presumido.UseVisualStyleBackColor = true;
            this.chk_presumido.CheckedChanged += new System.EventHandler(this.chk_presumido_CheckedChanged);
            // 
            // chk_comercio
            // 
            this.chk_comercio.AutoSize = true;
            this.chk_comercio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_comercio.Location = new System.Drawing.Point(125, 19);
            this.chk_comercio.Name = "chk_comercio";
            this.chk_comercio.Size = new System.Drawing.Size(80, 17);
            this.chk_comercio.TabIndex = 4;
            this.chk_comercio.Text = "COMERCIO";
            this.chk_comercio.UseVisualStyleBackColor = true;
            this.chk_comercio.CheckedChanged += new System.EventHandler(this.chk_comercio_CheckedChanged);
            // 
            // chk_industria
            // 
            this.chk_industria.AutoSize = true;
            this.chk_industria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_industria.Location = new System.Drawing.Point(12, 19);
            this.chk_industria.Name = "chk_industria";
            this.chk_industria.Size = new System.Drawing.Size(82, 17);
            this.chk_industria.TabIndex = 4;
            this.chk_industria.Text = "INDUSTRIA";
            this.chk_industria.UseVisualStyleBackColor = true;
            this.chk_industria.CheckedChanged += new System.EventHandler(this.chk_industria_CheckedChanged);
            // 
            // txt_regime_especial
            // 
            this.txt_regime_especial.Location = new System.Drawing.Point(483, 280);
            this.txt_regime_especial.MaxLength = 1;
            this.txt_regime_especial.Name = "txt_regime_especial";
            this.txt_regime_especial.Size = new System.Drawing.Size(17, 20);
            this.txt_regime_especial.TabIndex = 8;
            // 
            // chk_ativa
            // 
            this.chk_ativa.AutoSize = true;
            this.chk_ativa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_ativa.Location = new System.Drawing.Point(717, 68);
            this.chk_ativa.Name = "chk_ativa";
            this.chk_ativa.Size = new System.Drawing.Size(109, 17);
            this.chk_ativa.TabIndex = 9;
            this.chk_ativa.Text = "EMPRESA ATIVA";
            this.chk_ativa.UseVisualStyleBackColor = true;
            // 
            // lbl_regime_especial
            // 
            this.lbl_regime_especial.AutoSize = true;
            this.lbl_regime_especial.Location = new System.Drawing.Point(466, 273);
            this.lbl_regime_especial.Name = "lbl_regime_especial";
            this.lbl_regime_especial.Size = new System.Drawing.Size(103, 13);
            this.lbl_regime_especial.TabIndex = 10;
            this.lbl_regime_especial.Text = "REGIME ESPECIAL";
            // 
            // chk_itens_prod
            // 
            this.chk_itens_prod.AutoSize = true;
            this.chk_itens_prod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_itens_prod.Location = new System.Drawing.Point(228, 279);
            this.chk_itens_prod.Name = "chk_itens_prod";
            this.chk_itens_prod.Size = new System.Drawing.Size(165, 17);
            this.chk_itens_prod.TabIndex = 11;
            this.chk_itens_prod.Text = "EMPRESA TEM PRODUTOS";
            this.chk_itens_prod.UseVisualStyleBackColor = true;
            // 
            // grp_cadastro
            // 
            this.grp_cadastro.Controls.Add(this.chk_pisconfis);
            this.grp_cadastro.Controls.Add(this.chk_itens_prod);
            this.grp_cadastro.Controls.Add(this.txt_regime_especial);
            this.grp_cadastro.Controls.Add(this.txt_cnpj_empresa);
            this.grp_cadastro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grp_cadastro.Location = new System.Drawing.Point(11, 12);
            this.grp_cadastro.Name = "grp_cadastro";
            this.grp_cadastro.Size = new System.Drawing.Size(897, 340);
            this.grp_cadastro.TabIndex = 12;
            this.grp_cadastro.TabStop = false;
            this.grp_cadastro.Text = "Cadastro";
            // 
            // chk_pisconfis
            // 
            this.chk_pisconfis.AutoSize = true;
            this.chk_pisconfis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_pisconfis.Location = new System.Drawing.Point(78, 279);
            this.chk_pisconfis.Name = "chk_pisconfis";
            this.chk_pisconfis.Size = new System.Drawing.Size(84, 17);
            this.chk_pisconfis.TabIndex = 9;
            this.chk_pisconfis.Text = "PIS/COFINS";
            this.chk_pisconfis.UseVisualStyleBackColor = true;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Location = new System.Drawing.Point(688, 381);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 13;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // form_cadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(920, 432);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.lbl_regime_especial);
            this.Controls.Add(this.chk_ativa);
            this.Controls.Add(this.grp_empresa);
            this.Controls.Add(this.lbl_cnpj);
            this.Controls.Add(this.lbl_cod);
            this.Controls.Add(this.lbl_nome_empresa);
            this.Controls.Add(this.btn_novo);
            this.Controls.Add(this.txt_nro_empresa);
            this.Controls.Add(this.txt_nome_empresa);
            this.Controls.Add(this.btn_gravar);
            this.Controls.Add(this.btn_limpar);
            this.Controls.Add(this.grp_cadastro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "form_cadastro";
            this.Text = "CADASTRO";
            this.grp_empresa.ResumeLayout(false);
            this.grp_empresa.PerformLayout();
            this.grp_cadastro.ResumeLayout(false);
            this.grp_cadastro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_limpar;
        private System.Windows.Forms.Button btn_gravar;
        private System.Windows.Forms.TextBox txt_nome_empresa;
        private System.Windows.Forms.TextBox txt_nro_empresa;
        private System.Windows.Forms.Button btn_novo;
        private System.Windows.Forms.TextBox txt_cnpj_empresa;
        private System.Windows.Forms.Label lbl_nome_empresa;
        private System.Windows.Forms.Label lbl_cod;
        private System.Windows.Forms.Label lbl_cnpj;
        private System.Windows.Forms.GroupBox grp_empresa;
        private System.Windows.Forms.CheckBox chk_filial;
        private System.Windows.Forms.CheckBox chk_matriz;
        private System.Windows.Forms.CheckBox chk_SIMPLES;
        private System.Windows.Forms.CheckBox chk_real;
        private System.Windows.Forms.CheckBox chk_presumido;
        private System.Windows.Forms.CheckBox chk_comercio;
        private System.Windows.Forms.CheckBox chk_industria;
        private System.Windows.Forms.TextBox txt_regime_especial;
        private System.Windows.Forms.CheckBox chk_ativa;
        private System.Windows.Forms.Label lbl_regime_especial;
        private System.Windows.Forms.CheckBox chk_itens_prod;
        private System.Windows.Forms.GroupBox grp_cadastro;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.CheckBox chk_pisconfis;
    }
}