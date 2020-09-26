using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Xml;
using System.IO;
using Npgsql;


namespace Thor
{
    public partial class SERV : Form
    {
        public SERV()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Variaveis globais
        /// </summary>
        
        //Instanciando Folder Browser
        FolderBrowserDialog OpenDir = new FolderBrowserDialog();

        /// <summary>
        /// String de conexão, utilizando informações definadas no form config
        /// </summary>
        string connstring = string.Format("Server=" + Properties.Settings.Default.host + ";Port=" + Properties.Settings.Default.port + ";" +
         "User Id=" + Properties.Settings.Default.user + ";Password=" + Properties.Settings.Default.pass + ";Database=" + Properties.Settings.Default.bd + ";");
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlTransaction transacao;

        ///<sumary>
        ///Icones de botões
        ///</sumary>

        MessageBoxIcon InfoIcon = MessageBoxIcon.Information;
        MessageBoxIcon WarningIcon = MessageBoxIcon.Warning;

        /// <summary>
        ///Criando funcao para abrir conexão com o banco
        /// </summary>

        /// <summary>
        /// Criando função para abrir uma transação
        /// </summary>
        public void ComecarTransacao()
        {
            conn.Open();
            transacao = (NpgsqlTransaction)conn.BeginTransaction();
        }
       
        
        //SCHEMA
        string schema = Properties.Settings.Default.schema + ".";

        string foldername = null;
        string arquivo_nome = null;



        private void btn_select_Click(object sender, EventArgs e)
        {

                        
            ///<sumary>
            ///Abaixo eu instancio o Open File e seto algumas opções padrões.
            ///</sumary>

            
            OpenDir.ShowNewFolderButton = true;
            OpenDir.RootFolder = Environment.SpecialFolder.MyComputer;


            //instanciando o Dialog result para tratar os arquivos selecionados pelo usuario
            DialogResult dr = OpenDir.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Sorted = true;

                btn_check.Enabled = true;
                btn_uncheck.Enabled = true;
                btn_integrar.Enabled = true;

                ///<sumary>
                /// Realizo parse para cada arquivo  selecionado pelo usuario, 
                /// que é retornado pelo DialogResult.
                ///</sumary>

                 foldername = OpenDir.SelectedPath;
                          
                foreach(string arquivo in Directory.GetFiles(foldername,"*.xml"))

                {
                    
                    checkedListBox1.Items.Add(arquivo.Substring(arquivo.LastIndexOf(@"\")+1));
                }
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void SERV_Load(object sender, EventArgs e)
        {
            btn_check.Enabled = false;
            btn_uncheck.Enabled = false;
            btn_integrar.Enabled = false;
        }

    /// <summary>
    /// BOTÃO DE INTEGRAÇÃO 
    /// </summary>

        private void btn_integrar_Click(object sender, EventArgs e)
        {
            //Log a ser criado
            string log = foldername + "\\log_integra.txt";

            //Criando um novo arquivo na pasta.                  
            if (System.IO.File.Exists(log))
            {
                System.IO.File.Delete(log);
                System.IO.File.Create(log).Close();
            }
            else
            {
                System.IO.File.Create(log).Close();
            }

            System.IO.TextWriter arquivo = System.IO.File.AppendText(log);
            string log_text = null;
            arquivo.WriteLine("Log integracao!\n");


            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            try
            {
                conn.Open();
            }
            catch (Exception msg)
            {
                MessageBox.Show("Não foi possivel conectar ao Banco de Dados ! \n" + msg.ToString(), "Erro", MessageBoxButtons.OK, WarningIcon);
                return;
            }
            
            for (int i = 0; i < checkedListBox1.Items.Count; i ++ )
            {
                XmlDocument XML = new XmlDocument();

                if(checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    XML.Load(OpenDir.SelectedPath+"\\"+checkedListBox1.Items[i].ToString());
                    XmlNode conteudo = XML.SelectSingleNode("/NOTAS_FISCAIS");
                    XmlNodeList inf_notas = conteudo.SelectNodes("NOTA_FISCAL");
                    arquivo_nome = checkedListBox1.Items[i].ToString();
                    foreach (XmlNode nota in inf_notas)
                    {
                        ///<sumary>
                        ///Informações genericas da nota
                        ///</sumary>
                        string num_nota = nota.SelectSingleNode("NUM_NOTA").InnerText;
                        string tipo = nota.SelectSingleNode("TIPO").InnerText;
                        string data_hora_emissao = nota.SelectSingleNode("DATA_HORA_EMISSAO").InnerText;
                        string situacao_nf = nota.SelectSingleNode("SITUACAO_NF").InnerText;
                        string codigo_cidade = nota.SelectSingleNode("CODIGO_CIDADE").InnerText;
                        string valor_nota = nota.SelectSingleNode("VALOR_NOTA").InnerText;
                        string valor_deducao = nota.SelectSingleNode("VALOR_DEDUCAO").InnerText;
                        string valor_servico = nota.SelectSingleNode("VALOR_SERVICO").InnerText;
                        string valor_iss = nota.SelectSingleNode("VALOR_ISS").InnerText;
                        string valor_pis = nota.SelectSingleNode("VALOR_PIS").InnerText;
                        string valor_cofins = nota.SelectSingleNode("VALOR_COFINS").InnerText;
                        string valor_inss = nota.SelectSingleNode("VALOR_INSS").InnerText;
                        string valor_ir = nota.SelectSingleNode("VALOR_IR").InnerText;
                        string valor_csll = nota.SelectSingleNode("VALOR_CSLL").InnerText;
                        string aliquota_pis = nota.SelectSingleNode("ALIQUOTA_PIS").InnerText;
                        string aliquota_cofins = nota.SelectSingleNode("ALIQUOTA_COFINS").InnerText;
                        string aliquota_inss = nota.SelectSingleNode("ALIQUOTA_INSS").InnerText;
                        string aliquota_ir = nota.SelectSingleNode("ALIQUOTA_IR").InnerText;
                        string aliquota_csll = nota.SelectSingleNode("ALIQUOTA_CSLL").InnerText;


                        ///<sumary>
                        ///Informações sobre o usuario do Site, com essa informações irei validar 
                        ///se o cliente está cadastrado na base de dados, e qual o nome dele em nossa
                        ///base, e qual o codigo do Efiscal, para integração.
                        ///</sumary>
                        string usuario_cpf_cnpj = nota.SelectSingleNode("USUARIO_CPF_CNPJ").InnerText;
                        //string usuario_razao_social = nota.SelectSingleNode("USUARIO_RAZAO_SOCIAL").InnerText;
                        string codigo_atividade = nota.SelectSingleNode("CODIGO_ATIVIDADE").InnerText;
                        string descricao_atividade = nota.SelectSingleNode("DESCRICAO_ATIVIDADE").InnerText;
                        string enquadramento_atividade = nota.SelectSingleNode("ENQUADRAMENTO_ATIVIDADE").InnerText;
                        string local_incidencia_atividade = nota.SelectSingleNode("LOCAL_INCIDENCIA_ATIVIDADE").InnerText;
                        string tributavel_atividade = nota.SelectSingleNode("TRIBUTAVEL_ATIVIDADE").InnerText;


                        ///<sumary>
                        ///informações sobre o prestador da NFSe, na integração o prestador será
                        ///levado como fornecedor, e tomador como um cliente.
                        ///</sumary>
                        string prestador_cpf_cnpj = nota.SelectSingleNode("PRESTADOR_CPF_CNPJ").InnerText;
                        string prestador_razao_social = nota.SelectSingleNode("PRESTADOR_RAZAO_SOCIAL").InnerText;
                        string prestador_tipo_logradouro = nota.SelectSingleNode("PRESTADOR_TIPO_LOGRADOURO").InnerText;
                        string prestador_logradouro = nota.SelectSingleNode("PRESTADOR_LOGRADOURO").InnerText;
                        string prestador_tipo_bairro = nota.SelectSingleNode("PRESTADOR_TIPO_BAIRRO").InnerText;
                        string prestador_bairro = nota.SelectSingleNode("PRESTADOR_BAIRRO").InnerText;
                        string prestador_cidade = nota.SelectSingleNode("PRESTADOR_CIDADE").InnerText;
                        string prestador_uf = nota.SelectSingleNode("PRESTADOR_UF").InnerText;
                        string prestador_cep = nota.SelectSingleNode("PRESTADOR_CEP").InnerText;
                        string prestador_numero = nota.SelectSingleNode("PRESTADOR_PREST_NUMERO").InnerText;

                        ///<sumary>
                        ///informações sobre o tomador da NFSe, na integração o prestador será
                        ///levado como fornecedor, e tomador como um cliente.
                        ///</sumary>
                        string tomador_cpf_cnpj = nota.SelectSingleNode("TOMADOR_CPF_CNPJ").InnerText;
                        string tomador_razao_social = nota.SelectSingleNode("TOMADOR_RAZAO_SOCIAL").InnerText;
                        string tomador_logradouro = null;
                        try
                        {
                            tomador_logradouro = nota.SelectSingleNode("TOMADOR_LOGRADOURO").InnerText;
                        }
                        catch { }
                        string tomador_numero = null;
                        try
                        {
                            tomador_numero = nota.SelectSingleNode("TOMADOR_NUMERO").InnerText;
                        }
                        catch { }
                        string tomador_bairro = null;
                        try
                        {
                            tomador_bairro = nota.SelectSingleNode("TOMADOR_BAIRRO").InnerText;
                        }
                        catch { }
                        string tomador_cidade_codigo = nota.SelectSingleNode("TOMADOR_CIDADE_CODIGO").InnerText;
                        string tomador_cidade = null;
                        try
                        {
                            tomador_cidade = nota.SelectSingleNode("TOMADOR_CIDADE").InnerText;
                        }
                        catch { }
                        string tomador_uf = null;
                        try
                        {
                            tomador_uf = nota.SelectSingleNode("TOMADOR_UF").InnerText;
                        }
                        catch {}
                        string tomador_cep = null;
                        try
                        {
                            tomador_cep = nota.SelectSingleNode("TOMADOR_CEP").InnerText;
                        }
                        catch { }
                        //string tomador_email = nota.SelectSingleNode("TOMADOR_EMAIL").InnerText;
                        string tomador_optante_simples = null;
                        try
                        {
                            tomador_optante_simples = nota.SelectSingleNode("TOMADOR_OPTANTE_SIMPLES").InnerText;
                        }
                        catch { }
                        ///<sumary>
                        ///informações sobre o serviço
                        ///</sumary>

                        string cos_servico = null;
                        try
                        {
                            cos_servico = nota.SelectSingleNode("COS_SERVICO").InnerText;
                        }
                        catch { }
                        string descricao_servico = null;
                        try
                        {
                            descricao_servico = nota.SelectSingleNode("DESCRICAO_SERVICO").InnerText;
                        }
                        catch { }
                        string aliquota = nota.SelectSingleNode("ALIQUOTA").InnerText;
                        string tipo_recolhimento = nota.SelectSingleNode("TIPO_RECOLHIMENTO").InnerText;
                        string operacao_tributacao = nota.SelectSingleNode("OPERACAO_TRIBUTACAO").InnerText;
                        string cidade_prestacao = nota.SelectSingleNode("CIDADE_PRESTACAO").InnerText;

                        ///<sumary>
                        ///Informações sobre os items
                        ///</sumary>
                        string tributavel = null;
                        string descricao = null;
                        string quantidade = null;
                        string valor_unitario = null;
                        string valor_total = null;
                        string deducao = null;
                        string valor_iss_unitario = null;

                        XmlNode Itens_nf = nota.SelectSingleNode("ITENS");
                        XmlNodeList inf_item = Itens_nf.SelectNodes("ITEM");

                        foreach (XmlNode item in inf_item)
                        {
                            tributavel = item.SelectSingleNode("TRIBUTAVEL").InnerText;
                            descricao = item.SelectSingleNode("DESCRICAO").InnerText;
                            quantidade = item.SelectSingleNode("QUANTIDADE").InnerText;
                            valor_unitario = item.SelectSingleNode("VALOR_UNITARIO").InnerText;
                            valor_total = item.SelectSingleNode("VALOR_TOTAL").InnerText;
                            deducao = item.SelectSingleNode("DEDUCAO").InnerText;
                            valor_iss_unitario = item.SelectSingleNode("VALOR_ISS_UNITARIO").InnerText;
                        }

                        ///<sumary>
                        ///validação da empresa na base de dados, para inserir corretamente os dados, ou 
                        ///solicitar o cadastramento da emrpesa no banco de dados.
                        ///</sumary>

                        string nro_empresa = null;
                        string nome_empresa = null;
                        string valida_empresa = null;
                        string query_cliente = null;
                        string query_fornec = null;
                        string query_header = null;
                        string query_servicos = null;
                        string query_detail = null;

                        if (tipo == "Prestado Eletrônico")
                        {
                            valida_empresa = prestador_cpf_cnpj;
                        }
                        else
                        {
                            valida_empresa = tomador_cpf_cnpj;
                        }

                        NpgsqlCommand valida = new NpgsqlCommand("SELECT cli_nm_cliente, cli_id_cliente FROM " + schema + "bpm_cli_cliente where cli_cnpj = '" + valida_empresa + "' AND cli_id_cliente LIKE '%NFS%' ", conn);

                        string ret = (string)valida.ExecuteScalar();
                        if (ret == null)
                        {
                            ret = null;
                        }
                        else
                        {
                            NpgsqlDataReader ret_inf = valida.ExecuteReader();
                            while (ret_inf.Read())
                            {
                                
                                nro_empresa = ret_inf[0].ToString().Trim();
                                nome_empresa = ret_inf[1].ToString().Trim();
                                ///<sumary>
                                ///Nesse trecho seto as variaveis com as string de insert de dados na base.
                                ///</sumary>
                                NpgsqlCommand valida_clie = new NpgsqlCommand("SELECT clie_cgc FROM " + schema + "CAD_CLIENTES WHERE BPM_ID_CLIENTE = '" + nome_empresa + "' AND clie_cod_cli = '" + tomador_cpf_cnpj + "'", conn);
                                conn.Close();
                                conn.Open();
                                string sel_clie = (string)valida_clie.ExecuteScalar();
                                if (sel_clie == null)
                                {
                                    //Cadastro de Clientes
                                    query_cliente = "insert into cad_clientes (CLIE_COD_EMPRESA,CLIE_COD_CLI,CLIE_NOME,CLIE_ENDERECO,CLIE_NUMERO,CLIE_MUNICIPIO,CLIE_ESTADO,CLIE_BAIRRO," +
                                                    "CLIE_CEP,CLIE_CGC,CLIE_COD_MUNICIPIO,BPM_ID_CLIENTE,BPM_DT_CRIACAO,CLIE_NATUREZA)" +
                                                                                                "values ('" + nro_empresa + "',		'" + tomador_cpf_cnpj + "', 	    '" + tomador_razao_social + "','"
                                                                                                + tomador_logradouro + "',    '" + tomador_numero + "',		    '" + tomador_cidade + "','"
                                                                                                + tomador_uf + "',		    '" + tomador_bairro + "',		    '" + tomador_cep + "','"
                                                                                                + tomador_cpf_cnpj + "',	    '" + tomador_cidade_codigo + "',    '" + nome_empresa + "','NOW()','NFE_PROG');";
                                    sel_clie = null;
                                }

                                NpgsqlCommand valida_forn = new NpgsqlCommand("SELECT forn_cgc FROM " + schema + "CAD_FORNEC WHERE BPM_ID_CLIENTE = '" + nome_empresa + "' AND forn_cod_cli = '" + prestador_cpf_cnpj + "'", conn);
                                string sel_forn = (string)valida_forn.ExecuteScalar();
                                if (sel_forn == null)
                                {
                                    //Cadastro de Fornecedores
                                    query_fornec = "insert into cad_fornec (FORN_COD_EMPRESA,FORN_COD_CLI,FORN_NOME,FORN_ENDERECO,FORN_NUMERO,FORN_MUNICIPIO,FORN_ESTADO," +
                                                    "FORN_BAIRRO,FORN_CEP,FORN_CGC,BPM_ID_CLIENTE,BPM_DT_CRIACAO,FORN_NATUREZA)"+ 
                                                                                                "values ('" + nro_empresa + "',				'" + prestador_cpf_cnpj + "',	    '" + prestador_razao_social + "',"
                                                                                                       + "'" + prestador_logradouro + "',	'" + prestador_numero + "',		'" + prestador_cidade + "',"
                                                                                                       + "'" + prestador_uf + "',		    '" + prestador_bairro + "',	    '" + prestador_cep + "',"
                                                                                                       + "'" + prestador_cpf_cnpj + "',	'" + nome_empresa + "','NOW()','NFE_PROG');";
                                    sel_forn = null;
                                }


                                NpgsqlCommand valida_nf;
                                /*
                                 * CASO A NOTA SEJA UM SERVIÇO PRESTADO, VERIFICO SE O NUMERO JÁ FOI INSERIDO ATRAVÉS DO CAMPO HNFS_COD_CLI.
                                 * CASO SEJA UM SERVICO TOMADO, ENTÃO VERIFICO O CNPJ DO TOMADOR, PELO CAMPO  HNFS_SERV_FORN.
                                 */
                                if (tipo == "Prestado Eletrônico")
                                {
                                    valida_nf = new NpgsqlCommand("SELECT hnfs_nr_nf FROM " + schema + "header_nfs WHERE BPM_ID_CLIENTE = '" + nome_empresa + "' AND (hnfs_nr_nf = '" + num_nota.Substring(num_nota.Length - 6) + "' or hnfs_nr_nf = '" + num_nota + "' ) AND HNFS_COD_CLI = '" + tomador_cpf_cnpj + "'", conn);
                                }
                                else
                                {
                                    valida_nf = new NpgsqlCommand("SELECT hnfs_nr_nf FROM " + schema + "header_nfs WHERE BPM_ID_CLIENTE = '" + nome_empresa + "' AND (hnfs_nr_nf = '" + num_nota.Substring(num_nota.Length - 6) + "' or hnfs_nr_nf = '" + num_nota + "' ) AND HNFS_SRV_FORN = '" + prestador_cpf_cnpj + "'", conn);
                                }

                                string sel_nf = (string)valida_nf.ExecuteScalar();
                                if (sel_nf == null)
                                {
                                    //Header
                                    query_header = "insert into header_nfs"
                                                    + "(HNFS_COD_EMPRESA,		HNFS_NR_NF,			HNFS_COD_CLI,			HNFS_OBSERVACAO,			HNFS_NF_ESTADO,		HNFS_CFOP,"
                                                    + "HNFS_VALOR_BRUTO,		HNFS_BASE_ISS,		HNFS_VALOR_ISS,         HNFS_VALOR_PIS,			    HNFS_VALOR_COFINS,	HNFS_VALOR_IRRF,	HNFS_VALOR_CSLL,"
                                                    + "HNFS_VALOR_OUTRAS_IPI,	HNFS_NF_CANCELADA,	HNFS_VALOR_TERCEIROS,	HNFS_CHAVE,HNFS_COD_CLI_AUX,HNFS_SRV_FORN,		HNFS_COD_SERVICO, BPM_ID_CLIENTE, BPM_DT_CRIACAO, HNFS_TIPO_NF)"
                                                    + "values ('" + nro_empresa + "',		'" + num_nota + "',				'" + tomador_cpf_cnpj + "',"
                                                                + "'" + data_hora_emissao.Substring(0, 10) + "','" + tomador_uf + "',			'" + aliquota + "',"
                                                                + "'" + valor_nota + "',		                '" + valor_servico + "',		'" + valor_iss + "',"
                                                                + "'" + valor_pis + "',		                '" + valor_cofins + "',		'" + valor_ir + "',"
                                                                + "'" + valor_csll + "',		                '" + valor_inss + "',			'" + situacao_nf + "',"
                                                                + "'" + valor_deducao + "',	                '" + tipo_recolhimento + "',	'" + cidade_prestacao + "',"
                                                                + "'" + prestador_cpf_cnpj + "',               '" + cos_servico + "',		'" + nome_empresa + "','NOW()', 'NFE_PROG');";

                                    //Detail temporario
                                    query_detail = "insert into detail_nfs("
                                                    + "DNFS_COD_EMPRESA,		DNFS_NR_NF,		DNFS_COD_CLI,		DNFS_VALOR_TOTAL,		DNFS_CFOP,"
                                                    + "DNFS_ALIQ_ISS,			DNFS_BASE_ISS,	DNFS_VALOR_ISS,		DNFS_VALOR_PIS,			DNFS_VALOR_COFINS, BPM_ID_CLIENTE, BPM_DT_CRIACAO,DNFS_TIPO)"
                                                    + "values ('" + nro_empresa + "',	    '" + num_nota + "',	'" + tomador_cpf_cnpj + "',"
                                                                + "'" + valor_nota + "',	'5933',				'" + aliquota + "',"
                                                                + "'" + valor_servico + "','" + valor_iss + "',	'" + valor_pis + "',"
                                                                + "'" + valor_cofins + "', '" + nome_empresa + "','NOW()', 'NFE_PROG');";


                                    //Cadastro de Serviço
                                    query_servicos = "insert into cad_servicos (SERV_NR_NF,SERV_DESCR_SERVICO,BPM_ID_CLIENTE,BPM_DT_CRIACAO,SERV_ORIGEM) values('" + num_nota + "','SERVICO PRESTADO','" + nome_empresa + "','NOW()','NFE_PROG');";
                                    
                                    sel_nf = null;
                                }
                                else
                                {
                                    // LEVA INFORMAÇÕES DE NOTAS NÃO INSERIDAS NO LOG
                                    log_text =  "A NOTA FISCAL NRO: " + num_nota + " EMIT: " + tomador_cpf_cnpj + "\nNÃO FOI INSERIDA POIS JÁ EXISTE NA BASE DE DADOS.\n";

                                    sel_nf = null;
                                }


                                
                                ///<sumary>
                                ///Inserindo dados nas tabelas
                                ///</sumary>
                                try
                                {
                                    conn.Close();
                                    NpgsqlCommand qinsert = new NpgsqlCommand(query_cliente + query_detail + query_fornec + query_header + query_servicos, conn);
                                    conn.Open();
                                    qinsert.ExecuteScalar();
                                }
                                catch (Exception msg)
                                {

                                    //Escrevendo Log

                                    log_text = log_text + "\n" + msg;

                                }

                                //ESCREVENDO VARIAVEL DE TEXTO DE LOG NO ARQUIVO
                                arquivo.WriteLine("Arquivo: " + arquivo_nome + "\n" + log_text + "----------------------------------------------------------------------");
                                
                            }

                            ret = null;
                        }

                    }

                }

            }
            //FECHANDO ARQUIVO DE LOG.
            arquivo.Close();

            MessageBox.Show("Concluido", "Aviso", MessageBoxButtons.OK, InfoIcon);
        }

        private void btn_config_Click(object sender, EventArgs e)
        {
            Config frm_config = new Config(this);
            frm_config.Show();
        }

    }
}
