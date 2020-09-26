namespace Thor
{
    partial class Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.txthost = new System.Windows.Forms.TextBox();
            this.txtport = new System.Windows.Forms.TextBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.txtdb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_salvar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_schema = new System.Windows.Forms.Label();
            this.txt_schema = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txthost
            // 
            this.txthost.Location = new System.Drawing.Point(123, 30);
            this.txthost.Name = "txthost";
            this.txthost.Size = new System.Drawing.Size(109, 20);
            this.txthost.TabIndex = 0;
            this.txthost.Text = "localhost";
            this.txthost.TextChanged += new System.EventHandler(this.txthost_TextChanged);
            // 
            // txtport
            // 
            this.txtport.Location = new System.Drawing.Point(123, 88);
            this.txtport.Name = "txtport";
            this.txtport.Size = new System.Drawing.Size(71, 20);
            this.txtport.TabIndex = 1;
            this.txtport.Text = "5432";
            this.txtport.TextChanged += new System.EventHandler(this.txtport_TextChanged);
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(123, 147);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(191, 20);
            this.txtuser.TabIndex = 2;
            this.txtuser.Text = "Postgre";
            this.txtuser.TextChanged += new System.EventHandler(this.txtuser_TextChanged);
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(123, 213);
            this.txtpass.Name = "txtpass";
            this.txtpass.PasswordChar = '*';
            this.txtpass.Size = new System.Drawing.Size(191, 20);
            this.txtpass.TabIndex = 3;
            this.txtpass.TextChanged += new System.EventHandler(this.txtpass_TextChanged);
            // 
            // txtdb
            // 
            this.txtdb.Location = new System.Drawing.Point(123, 287);
            this.txtdb.Name = "txtdb";
            this.txtdb.Size = new System.Drawing.Size(226, 20);
            this.txtdb.TabIndex = 4;
            this.txtdb.Text = "postgre";
            this.txtdb.TextChanged += new System.EventHandler(this.txtdb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Host";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "User";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Database";
            // 
            // btn_salvar
            // 
            this.btn_salvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_salvar.Location = new System.Drawing.Point(205, 442);
            this.btn_salvar.Name = "btn_salvar";
            this.btn_salvar.Size = new System.Drawing.Size(75, 23);
            this.btn_salvar.TabIndex = 10;
            this.btn_salvar.Text = "Salvar";
            this.btn_salvar.UseVisualStyleBackColor = true;
            this.btn_salvar.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(205, 492);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(439, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "2.0.0.02";
            // 
            // lbl_schema
            // 
            this.lbl_schema.AutoSize = true;
            this.lbl_schema.Location = new System.Drawing.Point(15, 354);
            this.lbl_schema.Name = "lbl_schema";
            this.lbl_schema.Size = new System.Drawing.Size(46, 13);
            this.lbl_schema.TabIndex = 13;
            this.lbl_schema.Text = "Schema";
            // 
            // txt_schema
            // 
            this.txt_schema.Location = new System.Drawing.Point(123, 351);
            this.txt_schema.Name = "txt_schema";
            this.txt_schema.Size = new System.Drawing.Size(100, 20);
            this.txt_schema.TabIndex = 14;
            this.txt_schema.TextChanged += new System.EventHandler(this.txt_schema_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_schema);
            this.groupBox1.Controls.Add(this.lbl_schema);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txthost);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtport);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtdb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtuser);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtpass);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 413);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Banco";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(546, 528);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_salvar);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Config";
            this.Text = "Config";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox txthost;
        public System.Windows.Forms.TextBox txtport;
        public System.Windows.Forms.TextBox txtuser;
        public System.Windows.Forms.TextBox txtpass;
        public System.Windows.Forms.TextBox txtdb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_salvar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_schema;
        public System.Windows.Forms.TextBox txt_schema;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}