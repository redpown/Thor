using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using Npgsql;
using System.Transactions;
using HtmlAgilityPack;
using System.Runtime.InteropServices;
using System.Xml;
using System.Security.Cryptography.X509Certificates;



namespace Thor
{
    public partial class Integra : Form
    {

        public Integra()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Integra == true)
                DoVisible();
            else
                NoVisible();

            webBrowser1.Focus();
            webBrowser1.ScriptErrorsSuppressed = true; 
            
        }

        MessageBoxIcon InfoIcon = MessageBoxIcon.Information;
        MessageBoxIcon ErrorIcon = MessageBoxIcon.Hand;
        MessageBoxIcon WarningIcon = MessageBoxIcon.Warning;
       
        

        //CODIGO PARA ENCERRAR SESSÃO
        private const int INTERNET_OPTION_END_BROWSER_SESSION = 42;

        //IMPORTANDO DLL PARA ENCERRAR SESSÃO DO NAVEGADOR
        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        //STRING DE CONEXÃO
        string connstring = string.Format("Server=" + Properties.Settings.Default.host + ";Port=" + Properties.Settings.Default.port + ";" +
                 "User Id=" + Properties.Settings.Default.user + ";Password=" + Properties.Settings.Default.pass + ";Database=" + Properties.Settings.Default.bd + ";Enlist=true");

        //SCHEMA

        string schema = Properties.Settings.Default.schema + ".";
        

        private void button1_Click(object sender, EventArgs e)

        {
            Dados_nfe DadosNfe = new Dados_nfe();

            string html = null;
            //STRING DE CONEXÃO 
            
            NpgsqlConnection conn = new NpgsqlConnection(connstring);

            try
            {

                conn.Open();
            }
            catch (Exception msg)
            {
                MessageBox.Show("Não foi possivel conectar com o Banco de Dados ! \n" + msg.ToString(),"Erro",MessageBoxButtons.OK,ErrorIcon);
                return;
            }

            
          
            //VERIFICAÇÃO PARA SABE SE EXISTEM DADOS A SEREM IMPORTADOS
            if (webBrowser1.Url.ToString() != "http://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=")
            {

                try
                {
                    webBrowser1.Navigate("http://www.nfe.fazenda.gov.br/portal/consultaImpressao.aspx?tipoConsulta=completa");


                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }


                    //CORREÇÃO DE ENCODING DO CONTEUDO HTML RETORNADO
                    Stream documentStream = webBrowser1.DocumentStream;
                    StreamReader reader = new StreamReader(documentStream, Encoding.GetEncoding(28591));
                    documentStream.Position = 0;
                    html = reader.ReadToEnd();

                    //Metodo para ler os valores da Nota fiscal utilizando a Classe Dados_Nfe
                    DadosNfe.LerValoresNota(html);


                    NpgsqlCommand valida = new NpgsqlCommand("SELECT  cli_nm_cliente, cli_id_cliente FROM " + schema + "bpm_cli_cliente where cli_cnpj = '" + DadosNfe._CnpjDest + "' AND cli_id_cliente NOT LIKE '%NFS%' ", conn);
                    //NpgsqlDataReader sel = valida.ExecuteReader();

                    string sel;
                    try //BLOCO PARA VALIDAR SE O CLIENTE E/OU A NOTA FISCAL ESTÃO CADASTRADOS NO BANCO DE DADOS
                    {
                        sel = (string)valida.ExecuteScalar();
                        if (sel == null) //SE O RETORNO DO SELECT QUE VERIFICA O CNPJ DO DESTINATARIO FOR NULO, ENTÃO A EMPRESA NÃO ESTÁ CADASTRADA NA BASE
                        // E NÃO REALIZO O INSERT.
                        {
                            webBrowser1.GoBack();
                            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0); //COM ESSE COMANDO EU ENCERRO A SESSÃO DO NAVEGADOR, SE MANTER A SESSÃO ABERTA, OCORRE ERROS NA CHAVE.
                            MessageBox.Show("Empresa não cadastrada", "Aviso", MessageBoxButtons.OK, ErrorIcon);

                            //Resetando dados da Classe para realizar parse para as proximas notas.
                            DadosNfe.ResetaDados();
                        }
                        else
                        {

                            NpgsqlDataReader sel_dat = valida.ExecuteReader();
                            while (sel_dat.Read()) //REALIZO NOVAMENTE O SELECT DESSA VEZ COM O ExecuteReader() QUE TRARA OS DOIS CAMPOS NUMERO DA EMPRESA E NOME 
                            //USO ESSES CAMPOS PARA VALIDAÇÃO E PARA INSERT NO BANCO.
                            {

                                string nr_empresa = sel_dat[0].ToString().Trim();
                                string nm_empresa = sel_dat[1].ToString().Trim();
                                NpgsqlCommand valida_nf = new NpgsqlCommand("SELECT HNFE_NR_NF FROM " + schema + "header_nfe_thor WHERE BPM_ID_CLIENTE = '" + sel_dat[1].ToString().Trim() + "' AND HNFE_CHAVE = '" + DadosNfe._NfChave + "'", conn);
                                conn.Close(); //É NECESSARIO FECHAR A CONEXÃO E REABRIR PARA REALIZAR UM SELECT DENTRO DO OUTRO;
                                conn.Open();
                                string sel_nf = (string)valida_nf.ExecuteScalar();
                                conn.Close();
                                if (sel_nf == null) //SE O RETORNO DO SELECT FOR NULO, A NOTA FISCAL NÃO EXISTE NA BASE, E COM ISSO EU REALIZO OS INSERTS
                                {
                                    string Parametro_cfop = null;
                                    string Parametro_data = null;

                                    if (chbx_cfop.Checked != true && txt_cfop.Text.Length != 0)
                                        Parametro_cfop = txt_cfop.Text;

                                    if (txt_data_ref.Text != "  /" && txt_data_ref.Text != "  -")
                                    {
                                        Parametro_data = txt_data_ref.Text;
                                    }



                                    string InsertHeaderCommand = DadosNfe.SQLInsertHeader(html, Parametro_data, schema, nr_empresa, nm_empresa);

                                    string InsertDetailCommand = DadosNfe.SQLInsertDetail(html, Parametro_cfop, schema, nr_empresa, Parametro_data, nm_empresa);

                                    string InsertFornecedorCommand = DadosNfe.SQLInsertFornecedor(html, schema, nr_empresa, nm_empresa);

                                    string[,] SQLInsertProdutosCommand = DadosNfe.SQLInsertProdutos(html, schema, nr_empresa, nm_empresa);


                                    using (TransactionScope Transaction = new TransactionScope()){

                                        //Abrinco conexão com o Banco
                                        conn.Open();

                                        //Inserindo Header
                                        NpgsqlCommand InsertHeader = new NpgsqlCommand(InsertHeaderCommand, conn);
                                        object resp_InsertHeader = InsertHeader.ExecuteScalar();

                                        //Antes de Inserir o Fornecedore, verifico se o mesmo já existe na Base.
                                        NpgsqlCommand valida_forn = new NpgsqlCommand("SELECT forn_cgc FROM " + schema + "CAD_FORNEC WHERE BPM_ID_CLIENTE = '" + nm_empresa + "' AND forn_cod_cli = '" + DadosNfe._CnpjEmit + "'", conn);
                                        string sel_forn = (string)valida_forn.ExecuteScalar();
                                        if (sel_forn == null)
                                        {
                                            NpgsqlCommand InsertFornecedor = new NpgsqlCommand(InsertFornecedorCommand, conn);
                                            object resp_InsertFornecedor = InsertFornecedor.ExecuteScalar();
                                        }

                                        //Inserindo Detail
                                        NpgsqlCommand InsertDetail = new NpgsqlCommand(InsertDetailCommand, conn);
                                        object resp_InsertDetail = InsertDetail.ExecuteScalar();

                                        //Antes de inserir os produtos com a query retornada pelo metodo da classe, verifico se o mesmo já exste.
                                        for (int x = 0; x < (SQLInsertProdutosCommand.Length / 2); x++)
                                        {
                                            NpgsqlCommand valida_prod = new NpgsqlCommand("SELECT  prod_cod FROM " + schema + "cad_produtos WHERE BPM_ID_CLIENTE = '" + nm_empresa + "' AND prod_cod = '" + SQLInsertProdutosCommand[x, 0] + "'", conn);
                                            string select_prod = (string)valida_prod.ExecuteScalar();
                                            if (select_prod == null)
                                            {

                                                NpgsqlCommand InsertProdutos = new NpgsqlCommand(SQLInsertProdutosCommand[x, 1], conn);
                                                object resp_InsertProdutos = InsertProdutos.ExecuteScalar();

                                            }
                                        }

                                        //Resetando os dados da Classe para realizar o parse para outras Notas.
                                        DadosNfe.ResetaDados();

                                        webBrowser1.GoBack();
                                        while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                                        {
                                            Application.DoEvents();
                                        }
                                        InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);

                                        MessageBox.Show("Nota inserida corretamente", "Aviso", MessageBoxButtons.OK, InfoIcon);

                                        Transaction.Complete();
                                        
                                    }
                                    

                                }


                                else //CASO O SELECT RETORNE ALGO, EU INFORMO QUE A NOTA JÁ EXISTE NA BASE DE DADOS.
                                {

                                    webBrowser1.GoBack();
                                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                                    {
                                        Application.DoEvents();
                                    }

                                    MessageBox.Show("Nota já existe na base de dados", "Aviso", MessageBoxButtons.OK, WarningIcon);

                                    InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
                                    conn.Close();

                                    //Resetando dados da Classe para realizar parse para as proximas notas.
                                    DadosNfe.ResetaDados();
                                }

                            }
                        }
                    }
                    finally
                    {
                        conn.Clone();
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao tentar ler os dados do site da receita,\n tente novamente ou entre em contato com o administrador!", "Erro", MessageBoxButtons.OK, ErrorIcon);
                    webBrowser1.GoBack();
                    InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
                    conn.Close();
                }
            }

            else
            {
                MessageBox.Show("Não foram encontrados dados para importar, por favor insira a chave e o captcha para importar os dados", "Aviso", MessageBoxButtons.OK, WarningIcon);
                return;
            }

    
            webBrowser1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Config frmconfig = new Config(this);
            frmconfig.Show();
        }


        public void NoVisible()
        {
            lbl_cfop.Visible = false;
            txt_cfop.Visible = false;
            lbl_mes.Visible = false;
            txt_data_ref.Visible = false;
            chbx_cfop.Visible = false;
            btn_ler_nota.Enabled = true;
        }

        public void DoVisible()
        {
            lbl_cfop.Visible = true;
            txt_cfop.Visible = true;
            lbl_mes.Visible = true;
            txt_data_ref.Visible = true;
            chbx_cfop.Visible = true;
            if (txt_data_ref.Text != "  /" && txt_data_ref.Text != "  -")
                btn_ler_nota.Enabled = true;
            else
                btn_ler_nota.Enabled = false;
        }

        private void chbx_cfop_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_cfop.Checked == true)
            {
                txt_cfop.Enabled = false;
            }
            else
            {
                txt_cfop.Enabled = true;
            }
        }

        private void txt_cfop_Leave(object sender, EventArgs e)
        {
            if (txt_cfop.Text.Length != 0)
            {
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string chk_cfop = "select cfop from cfop_thor where cfop =" + txt_cfop.Text + ";";
                NpgsqlCommand valida_cfop = new NpgsqlCommand(chk_cfop, conn);
                string valid = (string)valida_cfop.ExecuteScalar();
                if (valid == null)
                {
                    MessageBox.Show("CFOP INVALIDO!", "Aviso", MessageBoxButtons.OK, WarningIcon);
                    txt_cfop.Clear();
                    txt_cfop.Focus();
                }
            }
        }

        private void txt_data_ref_Leave_1(object sender, EventArgs e)
        {
            int v_mes = 0;
            int v_ano = 0;
            if (txt_data_ref.Text != "  /" && txt_data_ref.Text != "  -" )
            {
                if (txt_data_ref.Text.Length < 7)
                {
                    MessageBox.Show("DATA INVALIDA!", "AVISO", MessageBoxButtons.OK, WarningIcon);
                    txt_data_ref.Clear();
                    txt_data_ref.Focus();
                    btn_ler_nota.Enabled = false;
                }
                else
                {
                    v_mes = Convert.ToInt32(txt_data_ref.Text.Substring(0, 2));
                    v_ano = Convert.ToInt32(txt_data_ref.Text.Substring(3));

                    if (txt_data_ref.Text.Length != 7 && txt_data_ref != null)
                    {
                        MessageBox.Show("DATA INVALIDA!", "AVISO", MessageBoxButtons.OK, WarningIcon);
                        txt_data_ref.Clear();
                        txt_data_ref.Focus();
                        btn_ler_nota.Enabled = false;
                    }
                    else if (v_mes < 1 || v_mes > 12)
                    {
                        MessageBox.Show("DATA INVALIDA!", "AVISO", MessageBoxButtons.OK, WarningIcon);
                        txt_data_ref.Clear();
                        txt_data_ref.Focus();
                        btn_ler_nota.Enabled = false;
                    }
                    else
                    {
                        btn_ler_nota.Enabled = true;
                    }
                }

            }
            
        }

        private void btn_cadastro_Click(object sender, EventArgs e)
        {
            form_cadastro frm_cadastro = new form_cadastro();
            frm_cadastro.Show();
        }
        
    }
}
