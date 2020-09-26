using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Thor
{
    public partial class Config : Form
    {
        private Integra frm;
        private SERV frm_serv;

        public Config(Integra frm)
        {

            InitializeComponent();
            this.frm = frm;
            txtport.Text = Properties.Settings.Default.port;
            txtuser.Text = Properties.Settings.Default.user;
            txtpass.Text = Properties.Settings.Default.pass;
            txtdb.Text = Properties.Settings.Default.bd;
            txthost.Text = Properties.Settings.Default.host;
            txt_schema.Text = Properties.Settings.Default.schema;
            btn_salvar.Enabled = false;
        }


        public Config(SERV frm_serv)
        {

            InitializeComponent();
            this.frm_serv = frm_serv;
            txtport.Text = Properties.Settings.Default.port;
            txtuser.Text = Properties.Settings.Default.user;
            txtpass.Text = Properties.Settings.Default.pass;
            txtdb.Text = Properties.Settings.Default.bd;
            txthost.Text = Properties.Settings.Default.host;
            txt_schema.Text = Properties.Settings.Default.schema;
            btn_salvar.Enabled = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void txthost_TextChanged(object sender, EventArgs e)
        {
            btn_salvar.Enabled = true;
        }

        public void txtport_TextChanged(object sender, EventArgs e)
        {
            btn_salvar.Enabled = true;
        }

        public void txtuser_TextChanged(object sender, EventArgs e)
        {
            btn_salvar.Enabled = true;
        }

        public void txtpass_TextChanged(object sender, EventArgs e)
        {
            btn_salvar.Enabled = true;
        }

        public void txtdb_TextChanged(object sender, EventArgs e)
        {
            btn_salvar.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.host = txthost.Text;
            Properties.Settings.Default.port = txtport.Text;
            Properties.Settings.Default.user = txtuser.Text;
            Properties.Settings.Default.pass = txtpass.Text;
            Properties.Settings.Default.bd = txtdb.Text;
            Properties.Settings.Default.schema = txt_schema.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Salvo com sucesso!", "Aviso!");
            btn_salvar.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btn_salvar.Enabled == false)
            {
                this.Visible = false;
            }
            else if (btn_salvar.Enabled == true && MessageBox.Show("Deseja realmente sair sem salvar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Visible = false;
            }

            else
            {
                return;
            }



        }

        private void txt_schema_TextChanged(object sender, EventArgs e)
        {
            btn_salvar.Enabled = true;
        }


    }
}
