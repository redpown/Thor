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
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms.OfType<Integra>().Count() > 0) // Verifica se o form está aberto
            {
                Application.OpenForms["Integra"].BringToFront();//Caso esteja aberto, trago ele para frente.
            }
            else
            {
                Integra frm_integra = new Integra();
                frm_integra.Show();// Abro o form caso ele nao está aberto
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SERV>().Count() > 0) // Verifica se o form está aberto
            {
                Application.OpenForms["SERV"].BringToFront(); //Caso esteja aberto, trago ele para frente.
            }
            else
            {
                SERV frm_serv = new SERV();
                frm_serv.Show(); // Abro o form caso ele nao está aberto
            }
        }

        private void home_Load(object sender, EventArgs e)
        {
            BackgroundImageLayout = ImageLayout.Stretch;
        }

    }
}
