using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace Thor
{
    public partial class form_cadastro : Form
    {
        public form_cadastro()
        {
            InitializeComponent();
            btn_limpar.Enabled          = false;
            btn_gravar.Enabled          = false;
            txt_cnpj_empresa.Enabled    = false;
            txt_nome_empresa.Enabled    = false;
            txt_nro_empresa.Enabled     = false;
            txt_regime_especial.Enabled = false;
            chk_ativa.Enabled           = false;
            chk_comercio.Enabled        = false;
            chk_filial.Enabled          = false;
            chk_industria.Enabled       = false;
            chk_itens_prod.Enabled      = false;
            chk_matriz.Enabled          = false;
            chk_presumido.Enabled       = false;
            chk_real.Enabled            = false;
            chk_SIMPLES.Enabled         = false;
            chk_pisconfis.Enabled       = false;

        }

        string connstring = string.Format("Server=" + Properties.Settings.Default.host + ";Port=" + Properties.Settings.Default.port + ";" +
                 "User Id=" + Properties.Settings.Default.user + ";Password=" + Properties.Settings.Default.pass + ";Database=" + Properties.Settings.Default.bd + ";");

        //SCHEMA

        string schema = Properties.Settings.Default.schema + ".";

        string emp_nome = null;
        string emp_nro = null;
        string emp_cnpj = null;
        string emp_ativa = null;
        string emp_comer_industria = null;
        string emp_matriz_filial = null;
        string emp_tributacao = null;
        string emp_tem_prod = null;
        string emp_regime = null;
        string cli_simples = null;
        string msg_valida = null;
        string pisconfis = null;
        int flag_valid = 0;

        private void btn_novo_Click(object sender, EventArgs e)
        {
            btn_limpar.Enabled = true;
            txt_cnpj_empresa.Enabled = true;
            txt_nome_empresa.Enabled = true;
            txt_nro_empresa.Enabled = true;
            txt_regime_especial.Enabled = true;
            txt_regime_especial.Text = "0"; // A MAIORIA DAS EMPRESAS SÃO REGIME ESPECIAL 0, PARA FACILITAR LEVO COMO PADRÃO
            chk_ativa.Enabled = true;
            chk_ativa.Checked = true; //SETO POR PADRÃO COMO CHECADO, PARA FACILIAR O CADASTRO, DIFICILMENTE A EMPRESA SERÁ CADASTRADA SEM  ESTAR ATIVA
            chk_comercio.Enabled = true;
            chk_filial.Enabled = true;
            chk_industria.Enabled = true;
            chk_itens_prod.Enabled = true;
            chk_itens_prod.Checked = true; //SETO POR PADRÃO COMO CHECADO, AS EMPRESAS SEMPRE TEM PRODUTOS A MENOS QUE SEJA CTE
            chk_matriz.Enabled = true;
            chk_presumido.Enabled = true;
            chk_real.Enabled = true;
            chk_SIMPLES.Enabled = true;
            chk_pisconfis.Enabled = true;
            btn_gravar.Enabled = true;
        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {

            txt_cnpj_empresa.Clear();
            txt_nome_empresa.Clear();
            txt_nro_empresa.Clear();
            chk_comercio.Checked = false;
            chk_filial.Checked = false;
            chk_industria.Checked = false;
            chk_matriz.Checked = false;
            chk_presumido.Checked = false;
            chk_real.Checked = false;
            chk_SIMPLES.Checked = false;
            chk_pisconfis.Checked = false;
            
        }

        private void btn_gravar_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            try
            {

                conn.Open();
            }
            catch (Exception msg)
            {
                MessageBox.Show("Não foi possivel conectar com o Banco de Dados ! \n" + msg.ToString());
                return;
            }

            emp_nome    = txt_nome_empresa.Text;
            emp_nro     = txt_nro_empresa.Text;
            emp_cnpj    = txt_cnpj_empresa.Text;
            emp_regime  = txt_regime_especial.Text;

            if (chk_ativa.Checked == true)
            {
                emp_ativa = "A";
            
            }
            else
            {
                emp_ativa = "N";
              
            }
            if (chk_itens_prod.Checked == true)
            {
                emp_tem_prod = "S";
              
            }
            else
            {
                emp_tem_prod = "N";
                
            }

            if (chk_pisconfis.Checked == true)
                pisconfis = "S";

            if (string.IsNullOrEmpty(txt_nome_empresa.Text))
            {
                flag_valid++;
                msg_valida = msg_valida + "NOME EMPRESA \n";
            }
            if (string.IsNullOrEmpty(txt_nro_empresa.Text))
            {
                flag_valid++;
                msg_valida = msg_valida + "NUMERO EMPRESA \n";
            }
            if (string.IsNullOrEmpty(txt_cnpj_empresa.Text))
            {
                flag_valid++;
                msg_valida = msg_valida + "CNPJ EMPRESA \n";
            }
            if (string.IsNullOrEmpty(txt_regime_especial.Text))
            {
                flag_valid++;
                msg_valida = msg_valida + "REGIME ESPECIAL \n";
            }
            if (chk_comercio.Checked == false && chk_industria.Checked == false)
            {
                flag_valid++;
                msg_valida = msg_valida + "COMERCIO/INDUSTRIA \n";
            }
            if (chk_matriz.Checked == false && chk_filial.Checked == false)
            {
                flag_valid++;
                msg_valida = msg_valida + "MATRIZ/FILIAL \n";
            }
            if (chk_presumido.Checked == false && chk_real.Checked == false && chk_SIMPLES.Checked == false)
            {
                flag_valid++;
                msg_valida = msg_valida + "TIPO DE TRIBUTAÇÃO \n";
            }
            

            string insert = "INSERT INTO bpm_cli_cliente(" +
                                                             "cli_id_cliente,         cli_nm_cliente,         cli_cnpj,           cli_regime_especial," +
                                                             "cli_simples_nacional,   cli_comercio_industria, cli_matriz_filial," +
                                                             "cli_itens_prod,         cli_status,             cli_tipo_tributacao, cli_piscofins)" +
             "VALUES ('"+emp_nome+"','"+emp_nro+"','"+emp_cnpj+"','"+emp_regime+"','"+cli_simples+"','"+emp_comer_industria+"','"+emp_matriz_filial+"'," +
                     "'"+emp_tem_prod+"','"+emp_ativa+"','"+emp_tributacao+"','"+pisconfis+"')";
            if (flag_valid == 0)
            {
                try
                {
                    NpgsqlCommand cad = new NpgsqlCommand(insert, conn);
                    object resp = cad.ExecuteScalar();
                    MessageBox.Show("Cadastro efetuado corretamente!");
                }
                catch (Exception ret)
                {
                    MessageBox.Show("Não foi possivel realizar o cadastro ! \n" + ret.ToString());
                    return;
                }


                txt_cnpj_empresa.Clear();
                txt_nome_empresa.Clear();
                txt_nro_empresa.Clear();
                chk_comercio.Checked = false;
                chk_filial.Checked = false;
                chk_industria.Checked = false;
                chk_matriz.Checked = false;
                chk_presumido.Checked = false;
                chk_real.Checked = false;
                chk_SIMPLES.Checked = false;
                chk_pisconfis.Checked = false;

                InitializeComponent();
                btn_limpar.Enabled = false;
                btn_gravar.Enabled = false;
                txt_cnpj_empresa.Enabled = false;
                txt_nome_empresa.Enabled = false;
                txt_nro_empresa.Enabled = false;
                txt_regime_especial.Enabled = false;
                chk_ativa.Enabled = false;
                chk_comercio.Enabled = false;
                chk_filial.Enabled = false;
                chk_industria.Enabled = false;
                chk_itens_prod.Enabled = false;
                chk_matriz.Enabled = false;
                chk_presumido.Enabled = false;
                chk_real.Enabled = false;
                chk_SIMPLES.Enabled = false;
                chk_pisconfis.Enabled = false;
                msg_valida = null;
            }
            else
            {
                MessageBox.Show("Favor preencher os campos abaixo: \n" + msg_valida);
                msg_valida = null;
            }

            

        }

        private void chk_industria_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chk_industria.Checked == true)
            {
                chk_comercio.Checked = false;
                emp_comer_industria = "I";
         

            }
             
        }

        private void chk_matriz_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_matriz.Checked == true)
            {
                chk_filial.Checked = false;
                emp_matriz_filial = "M";
         
            }
             
        }

        private void chk_comercio_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_comercio.Checked == true)
            {
                chk_industria.Checked = false;
                emp_comer_industria = "C";
          
            }
             
        }

        private void chk_filial_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_filial.Checked == true)
            {
                chk_matriz.Checked = false;
                emp_matriz_filial = "F";
             
            }
             
        }

        private void chk_presumido_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chk_presumido.Checked == true)
            {
                chk_real.Checked = false;
                
                    chk_SIMPLES.Checked = false;
                    chk_pisconfis.Checked = false;
                    emp_tributacao = "PRESUMIDO";
             
               
            }
             
        }

        private void chk_real_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chk_real.Checked == true)
            {
                chk_presumido.Checked = false;
                chk_SIMPLES.Checked = false;
                chk_pisconfis.Checked = true;
                emp_tributacao = "REAL";
               
                
            }
             
        }

        private void chk_SIMPLES_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chk_SIMPLES.Checked == true)
            {
                chk_presumido.Checked = false;
                
                    chk_real.Checked = false;
                    chk_pisconfis.Checked = false;
                    emp_tributacao = "SIMPLES";
                    cli_simples = "S";

                        
                
            }
             
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


    }
}
