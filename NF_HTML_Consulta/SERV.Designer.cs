namespace Thor
{
    partial class SERV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SERV));
            this.btn_select = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.btn_check = new System.Windows.Forms.Button();
            this.btn_uncheck = new System.Windows.Forms.Button();
            this.btn_integrar = new System.Windows.Forms.Button();
            this.btn_config = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_select
            // 
            this.btn_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_select.Location = new System.Drawing.Point(581, 35);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(104, 47);
            this.btn_select.TabIndex = 0;
            this.btn_select.Text = "SELECIONAR";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(310, 244);
            this.checkedListBox1.TabIndex = 1;
            // 
            // btn_check
            // 
            this.btn_check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_check.Location = new System.Drawing.Point(27, 278);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(93, 23);
            this.btn_check.TabIndex = 2;
            this.btn_check.Text = "Selecionar tudo";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_uncheck
            // 
            this.btn_uncheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_uncheck.Location = new System.Drawing.Point(172, 278);
            this.btn_uncheck.Name = "btn_uncheck";
            this.btn_uncheck.Size = new System.Drawing.Size(98, 23);
            this.btn_uncheck.TabIndex = 3;
            this.btn_uncheck.Text = "Deselecionar";
            this.btn_uncheck.UseVisualStyleBackColor = true;
            this.btn_uncheck.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_integrar
            // 
            this.btn_integrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_integrar.Location = new System.Drawing.Point(581, 136);
            this.btn_integrar.Name = "btn_integrar";
            this.btn_integrar.Size = new System.Drawing.Size(104, 47);
            this.btn_integrar.TabIndex = 4;
            this.btn_integrar.Text = "INTEGRAR";
            this.btn_integrar.UseVisualStyleBackColor = true;
            this.btn_integrar.Click += new System.EventHandler(this.btn_integrar_Click);
            // 
            // btn_config
            // 
            this.btn_config.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_config.Location = new System.Drawing.Point(581, 232);
            this.btn_config.Name = "btn_config";
            this.btn_config.Size = new System.Drawing.Size(104, 34);
            this.btn_config.TabIndex = 5;
            this.btn_config.Text = "CONFIG";
            this.btn_config.UseVisualStyleBackColor = true;
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // SERV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(788, 355);
            this.Controls.Add(this.btn_config);
            this.Controls.Add(this.btn_integrar);
            this.Controls.Add(this.btn_uncheck);
            this.Controls.Add(this.btn_check);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.btn_select);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SERV";
            this.Text = "Servicos";
            this.Load += new System.EventHandler(this.SERV_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button btn_check;
        private System.Windows.Forms.Button btn_uncheck;
        private System.Windows.Forms.Button btn_integrar;
        private System.Windows.Forms.Button btn_config;
    }
}