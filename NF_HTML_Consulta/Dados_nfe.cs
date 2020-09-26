using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;


namespace Thor
{
    class Dados_nfe
    {
        Boolean SetaUnicos = false;
        Boolean SetaProdutos = false;
        int ContadorProdutosNota = 0;
        //########################################################################################################################################
        //                                                     DADOS DA NF-E
        //########################################################################################################################################

        private string NfModelo;
        private string NfSerie;
        private string NfNumero;
        private string NfDataEmit;
        private string NfValorTotal;
        private string NfChave;

        public string _NfChave
        {
            get { return NfChave; }
        }

        //########################################################################################################################################
        //                                                     DADOS EMITENTE
        //########################################################################################################################################
        private string NomeEmit;                private string MunicipioEmit;
        private string NomeFatasaiaEmit;        private string UFEmit;
        private string CnpjEmit;                private string InscEstadualEmit;
        private string EndEmit;                 private string InscMuniciaplEmit;
        private string BairroEmit;              private string CnaeFiscal;
        private string CepEmit;                 private string TelefoneEmit;
        private string PaisEmit;                private string InscEstadualSubsTributario;
        private string MuniOcorrenciaICMS;      private string CodRegimeTributario;

        public string _CnpjEmit
        {
            get { return CnpjEmit; }
        }
        //########################################################################################################################################
        //                                                   DADOS DESTINATARIO 
        //########################################################################################################################################
        private string NomeDest;                private string CnpjDest;
        private string EndDest;                 private string BairroDest;
        private string CepDest;                 private string MunicipioDest;
        private string TelefoneDest;            private string UFDest;
        private string PaisDest;                private string IndicadorIe;
        private string InscEstadualDest;        private string SuframaDest;
        private string IMDest;                  private string EmailDest;

        public string _CnpjDest
        {
            get { return CnpjDest; }
        }

        //########################################################################################################################################
        //                                                       DADOS TOTAIS
        //########################################################################################################################################
        private string TotBaseICMS;             private string TotValorICMS;        private string TotValorICMSDesonerado;
        private string TotBaseICMSST;           private string TotValotICMSST;      private string TotValorProdutos;
        private string TotValorFrete;           private string TotValorSeguro;      private string TotValorOutrasDespesas;
        private string TotValorIPI;             private string TotValorNFe;         private string TotValorDesconto;
        private string TotValorII;              private string TotValorPis;         private string TotValorCofins;
        private string TotValorAproxTributos;   

        
        //########################################################################################################################################
        //                                                       DADOS PRODUTOS
        //########################################################################################################################################
        //****************************************************************************************************************************************
        //                                                          STRUCT PRODUTOS
        //****************************************************************************************************************************************
        private struct ProdutosNota
        {
            public string ProdNumItem;              public string ProdDescricao;            public string ProdQtd;
            public string ProdUnd;                  public string ProdValor;                public string ProdCodigo;
            public string ProdNcm;                  public string ProdCodExTipi;            public string ProdCfop;
            public string ProdValorOutrasDespesas;  public string ProdValorDesconto;        public string ProdValorFrete;
            public string ProdValorSeguro;          public string ProdIndcCampValor;        public string ProdCodEANComercial;
            public string ProdUnidComercial;        public string ProdQtdComercial;         public string ProdCodEANtributavel;
            public string ProdUndTributavel;        public string ProdQtdTributavel;        public string ProdValorUnitComercial;
            public string ProdValorUnitTributacao;  public string ProdNroPedido;            public string ProdItempedido;
            public string ProdValorAproxTributos;   public string ProdNumFCI;               
            //Icms
            public string ProdTributacaoICMS;       public string ProdModalidadeBaseIcms;   public string ProdBaseICMS;
            public string ProdAliqIcms;             public string ProdValorIcms;            public string ProdOrigemMerc;       
            //Ipi
            public string ProdClasseEnquadramento;  public string ProdCodEnquadramento;     public string ProdCodSelo;
            public string ProdCnpjProdutor;         public string ProdQtdSelo;              public string ProdCST;
            public string ProdQtdTotUndPadrao;      public string ProdValorUnidIpi;         public string ProdValorIpi;
            public string ProdBaseCalculoIPI;       public string ProdAliqIpi;
            //Pis
            public string ProdCstPis;               public string ProdBaseCalculoPis;       public string ProdAliqPis; 
            public string ProdValorPis; 
            //Confins
            public string ProdCstConfins;           public string ProdBaseCalculoCofins;    public string ProdAliqCofins;
            public string ProdValorCofins;
            //IcmsSt
            public string ProdBaseIcmsSt;           public string ProdValorIcmsSt;
        }
        
        
        //########################################################################################################################################
        //                                                     INSTANCIANDO CLASSES / OBJETOS
        //########################################################################################################################################
        
        HtmlAgilityPack.HtmlDocument HtmlDocument = new HtmlAgilityPack.HtmlDocument();

        ProdutosNota ProdutosNotaInstancia = new Dados_nfe.ProdutosNota();

        ProdutosNota[] ProdutosInfo = new ProdutosNota[999];

        //########################################################################################################################################
        //                                                          METODOS PRIVATES
        //########################################################################################################################################
       
        //****************************************************************************************************************************************
        //                                                          METODOS DADOS UNICOS
        //****************************************************************************************************************************************



        /// <summary>
        /// Metodo que faz o Parse do HTML e Label e Span e Retorna Vetor com os dados
        /// </summary>
        /// <param name="html">Parametro contendo somente o bloco HTML a ser utilizado, esse bloco deve conter somente Labels e Spans.</param>
        /// <returns>Retorna Vetor contendo O conteudo dos Labels na primeira posição, e dos Spans na segunda Posição.</returns>
        private string[,] ParseHtml(String html)
        {
            HtmlAgilityPack.HtmlDocument HtmlDocumentLocal = new HtmlAgilityPack.HtmlDocument();

            HtmlDocumentLocal.Load(new StringReader(html));

            HtmlNodeCollection labels = HtmlDocumentLocal.DocumentNode.SelectNodes(".//label");

            string[,] VetParse = new string[labels.Count(), 2];

            int i = 0;
            foreach (HtmlNode label in labels)
            {
                VetParse[i, 0] = label.InnerText.Trim().Replace("&nbsp;"," ");
                i++;
            }

            HtmlNodeCollection spans = HtmlDocumentLocal.DocumentNode.SelectNodes(".//span");


            i = 0;
            foreach (HtmlNode span in spans)
            {   //Construtor para criar um o objeto de regular expression e retirar os espaços a mais.
                Regex regex = new Regex(@"\s{2,}");
                VetParse[i, 1] = regex.Replace( span.InnerText.Trim().Replace("&nbsp;", " ").Replace("\r\n", " "), " ");
                i++;
            }

            return VetParse;
        }

        private void ZeraValoresNota()
        {
            NfModelo = null;
            NfSerie = null;
            NfNumero = null;
            NfDataEmit = null;
            NfValorTotal = null;
            NfChave = null;

            NomeEmit = null;            MunicipioEmit = null;
            NomeFatasaiaEmit = null;    UFEmit = null;
            CnpjEmit = null;            InscEstadualEmit = null;
            EndEmit = null;             InscMuniciaplEmit = null;
            BairroEmit = null;          CnaeFiscal = null;
            CepEmit = null;             TelefoneEmit = null;
            PaisEmit = null;            InscEstadualSubsTributario = null;
            MuniOcorrenciaICMS = null;  CodRegimeTributario = null;

            NomeDest = null;            CnpjDest = null;
            EndDest = null;             BairroDest = null;
            CepDest = null;             MunicipioDest = null;
            TelefoneDest = null;        UFDest = null;
            PaisDest = null;            IndicadorIe = null;
            InscEstadualDest = null;    SuframaDest = null;
            IMDest = null;              EmailDest = null;

            TotBaseICMS = null;         TotValorICMS = null;        TotValorICMSDesonerado = null;
            TotBaseICMSST = null;       TotValotICMSST = null;      TotValorProdutos = null;
            TotValorFrete = null;       TotValorSeguro = null;      TotValorOutrasDespesas = null;
            TotValorIPI = null;         TotValorNFe = null;         TotValorDesconto = null;
            TotValorII = null;          TotValorPis = null;         TotValorCofins = null;
            TotValorAproxTributos = null; 
        }

        /// <summary>
        /// Metodo responsavel por realizar o parse dos dados principais da Nota fiscal. Esses dados ficam no cabeçalho da pagina de impressão, 
        /// geralmente identificado por "Dados da NF-e"
        /// </summary>
        /// <param name="htmlNfDados">HTML contendo o bloco de dados HTML que contem os valores do bloco "Dados da NF-e" </param>
        private void NfDados(HtmlNodeCollection htmlNfDados)
        {
            //Criando Vetor para receber os valores do cabeçalho
            string[,] NFE = ParseHtml(htmlNfDados[0].InnerHtml);
            for (int i = 0; i < (NFE.Length / 2); ++i)
            {
                switch (NFE[i, 0])
                {
                    case "Modelo":
                        NfModelo = NFE[i,1];
                        break;
                    case "Série" :
                        NfSerie = NFE[i, 1];
                        break;
                    case "Número" :
                        NfNumero = NFE[i, 1];
                        break;
                    case "Data de Emissão" :
                        NfDataEmit = NFE[i, 1].Substring(0,10);
                        break;
                    case "Valor Total da Nota Fiscal  " :
                        NfValorTotal = NFE[i, 1];
                        break;
                }
            }
        }


        /// <summary>
        /// Metodo responsavel por realizar o Parse dos dados do Emitente.
        /// </summary>
        /// <param name="HtmlNfEmit">HTML contendo o bloco de dados HTML que contem os valores do bloco "Dados do Emitente"</param>
        private void NfEmit(HtmlNodeCollection HtmlNfEmit)
        {

            //Criando Vetor para receber os valores do Emitente
            string[,] Emit = ParseHtml(HtmlNfEmit[0].InnerHtml);
            for (int i = 0; i < (Emit.Length / 2); i++)
            {
                switch (Emit[i,0])
                {
                    case "Nome / Razão Social" :
                        NomeEmit = Emit[i, 1];
                        break;
                    case "Nome Fantasia" :
                        NomeFatasaiaEmit = Emit[i, 1];
                        break;
                    case "CNPJ":
                        CnpjEmit = Emit[i, 1].Replace("/","").Replace("-","").Replace(".","");
                        break;
                    case "Endereço" :
                        EndEmit = Emit[i, 1];
                        break;
                    case "Bairro / Distrito" :
                        BairroEmit = Emit[i, 1];
                        break;
                    case "CEP":
                        CepEmit = Emit[i, 1];
                        break;
                    case "Município":
                        MunicipioEmit = Emit[i, 1];
                        break;
                    case "Telefone":
                        TelefoneEmit = Emit[i, 1];
                        break;
                    case "UF":
                        UFEmit = Emit[i, 1];
                        break;
                    case "País":
                        PaisEmit = Emit[i, 1];
                        break;
                    case "Inscrição Estadual":
                        InscEstadualEmit = Emit[i, 1];
                        break;
                    case "Inscrição Estadual do Substituto Tributário" :
                        InscEstadualSubsTributario = Emit[i, 1];
                        break;
                    case "Inscrição Municipal":
                        InscMuniciaplEmit = Emit[i, 1];
                        break;
                    case "Município da Ocorrência do Fato Gerador do ICMS":
                        MuniOcorrenciaICMS = Emit[i, 1];
                        break;
                    case "CNAE Fiscal":
                        CnaeFiscal = Emit[i, 1];
                        break;
                    case "Código de Regime Tributário":
                        CodRegimeTributario = Emit[i, 1];
                        break;
                }
            }

                
        }

        /// <summary>
        /// Metodo responsavel por realizar o parse dos dados do Destinatario
        /// </summary>
        /// <param name="HtmlNfDest">HTML contendo o bloco de dados HTML que contem os valores do bloco "Dados do Destinatário"</param>
        private void NfDest(HtmlNodeCollection HtmlNfDest)
        {

            //Criando Vetor para receber os valores do Emitente
            string[,] Dest = ParseHtml(HtmlNfDest[0].InnerHtml);

            for (int i = 0; i < (Dest.Length / 2);i++)
            {
                switch (Dest[i,0])
                {
                    case "Nome / Razão Social":
                        NomeDest = Dest[i, 1];
                        break;
                    case "CNPJ":
                        CnpjDest = Dest[i, 1].Replace("/", "").Replace("-", "").Replace(".", "");
                        break;
                    case "Endereço":
                        EndDest = Dest[i, 1];
                        break;
                    case "Bairro / Distrito":
                        BairroDest = Dest[i, 1];
                        break;
                    case "CEP":
                        CepDest = Dest[i, 1];
                        break;
                    case "Município":
                        MunicipioDest = Dest[i, 1];
                        break;
                    case "Telefone":
                        TelefoneDest = Dest[i, 1];
                        break;
                    case "UF":
                        UFDest = Dest[i, 1];
                        break;
                    case "País":
                        PaisDest = Dest[i, 1];
                        break;
                    case "Indicador IE":
                        IndicadorIe = Dest[i, 1];
                        break;
                    case "Inscrição Estadual":
                        InscEstadualDest = Dest[i, 1];
                        break;
                    case "Inscrição SUFRAMA":
                        SuframaDest = Dest[i, 1];
                        break;
                    case "IM":
                        IMDest = Dest[i, 1];
                        break;
                    case "E-mail":
                        EmailDest = Dest[i, 1];
                        break;
                        
                }
            }

        }

        /// <summary>
        /// Metodo responsavel por carregar os valores Totais da Nota
        /// </summary>
        /// <param name="HtmlNfTotais">HTML contendo o bloco de dados HTML que contem os valores do bloco "Totais"</param>
        private void NfTotais(HtmlNodeCollection HtmlNfTotais)
        {
            //Criando Vetor conforme a quantidade de Labels
            string[,] Totais = ParseHtml(HtmlNfTotais[0].InnerHtml);
            for (int i = 0; i < (Totais.Length / 2); i++)
            {
                switch (Totais[i, 0])
                {
                    case "Base de Cálculo ICMS" :
                        TotBaseICMS = Totais[i, 1];
                        break;
                    case "Valor do ICMS" :
                        TotValorICMS = Totais[i, 1];
                        break;
                    case "Valor do ICMS Desonerado":
                        TotValorICMSDesonerado = Totais[i, 1];
                        break;
                    case "Base de Cálculo ICMS ST":
                        TotBaseICMSST = Totais[i, 1];
                        break;
                    case "Valor ICMS Substituição":
                        TotValotICMSST = Totais[i, 1];
                        break;
                    case "Valor Total dos Produtos":
                        TotValorProdutos = Totais[i, 1];
                        break;
                    case "Valor do Frete":
                        TotValorFrete = Totais[i, 1];
                        break;
                    case "Valor do Seguro":
                         TotValorSeguro = Totais[i, 1];
                        break;
                    case "Outras Despesas Acessórias":
                        TotValorOutrasDespesas = Totais[i, 1];
                        break;
                    case "Valor Total do IPI":
                        TotValorIPI = Totais[i, 1];
                        break;
                    case "Valor Total da NFe":
                        TotValorNFe = Totais[i, 1];
                        break;
                    case "Valor Total dos Descontos":
                        TotValorDesconto = Totais[i, 1];
                        break;
                    case "Valor Total do II":
                        TotValorII = Totais[i, 1];
                        break;
                    case "Valor do PIS":
                        TotValorPis = Totais[i, 1];
                        break;
                    case "Valor da COFINS":
                        TotValorCofins = Totais[i, 1];
                        break;
                    case "Valor Aproximado dos Tributos":
                        TotValorAproxTributos = Totais[i, 1];
                        break;
                        
                }
            }

        }

        /// <summary>
        /// Metodo que starta o processo de parse dos valores, para carregar as variaveis da Classe Dados_nfe, esse metodo chama os outros reponsaveis por fazer
        /// o parse, poré cada metodo de parse é responsavel por realizar uma parte, StartParse tem a função de passar somente o HTML referente aos dados
        /// da qual o metodo é responsavel
        /// </summary>
        /// <param name="html">HTML contendo todos os dados da NF-E</param>
        private void StartParse(string html)
        {

            //Filtrando conteudo do HTML Dos Dados da NF-e
            HtmlDocument.Load(new StringReader(html));
            HtmlNodeCollection DivNfe = HtmlDocument.DocumentNode.SelectNodes("//div[@id='NFe']");
            HtmlNodeCollection FSsNfe = DivNfe[0].SelectNodes("fieldset");
            HtmlNodeCollection FSNfDados = FSsNfe[0].SelectNodes("table");

            //Metodo para carregar os valores do cabeçalho da nota nos parametros da Classe
            NfDados(FSNfDados);

            //Filtrando conteudo do HTML dos Emitentes
            HtmlNodeCollection DivEmit = HtmlDocument.DocumentNode.SelectNodes("//div[@id='Emitente']");
            HtmlNodeCollection FSsEmit = DivEmit[0].SelectNodes("fieldset");
            HtmlNodeCollection FSNfEmit = FSsEmit[0].SelectNodes("table");
            
            //Metodo para carregar os valores do Emitente
            NfEmit(FSNfEmit);

            //Filtrando conteudo do HTML dos Emitentes
            HtmlNodeCollection DivDest = HtmlDocument.DocumentNode.SelectNodes("//div[@id='DestRem']");
            HtmlNodeCollection FSsDest = DivDest[0].SelectNodes("fieldset");
            HtmlNodeCollection FSNfDest = FSsDest[0].SelectNodes("table");

            //Metodo para carregar os valores do Destinatario
            NfDest(FSNfDest);

            //Filtrando conteudo HTML dos Totais da nota
            HtmlNodeCollection DivTotais = HtmlDocument.DocumentNode.SelectNodes("//div[@id='Totais']");
            HtmlNodeCollection FSsTotais = DivTotais[0].SelectNodes("fieldset");

            //Metodo para carregar Valores referente aos Totais.
            NfTotais(FSsTotais);


            //Carregar Chave da Nota fiscal
            HtmlNodeCollection Chave = HtmlDocument.DocumentNode.SelectNodes("//span[@id='lblChaveAcesso']");

            NfChave = Chave[0].InnerText.Replace(".","").Replace("-","").Replace("/","");

            //Setando Variavel como true para evitar usar desnecessariamente o metodo.
            SetaUnicos = true;
        }


        //****************************************************************************************************************************************
        //                                                          METODOS DADOS ITEMS
        //****************************************************************************************************************************************


        /// <summary>
        /// Os Labels dos itens é gerado somente uma vez, portanto existe somente um bloco de HTML referente a ele somente com os Labels,
        /// Esse metodo é responsavel por retornar um Vetor contendo os Labels, para facilitar a identificação dos valores.
        /// </summary>
        /// <param name="HtmlNfProd"></param>
        /// <returns>Retorna Vetor, contendo cabeçalho dos Itens somente, sem nenhuma span.</returns>
        private string[,] ParseItemHeaderLabel(HtmlNodeCollection HtmlNfProd)
        {
            //Labels a ser extraido os valores dos labels
            HtmlNodeCollection labels = HtmlNfProd[0].SelectNodes(".//label");

            //Vetor de retorno para armazenas os valores do label
            string [,] ProdHeader = new string[labels.Count(),2];

            //Inserindo valores dos Labels no Vetor
            int i = 0;
            foreach (HtmlNode label in labels)
            {
                ProdHeader[i,0] = label.InnerText.Trim();
                i++;
            }

            return ProdHeader;
        }

        /// <summary>
        /// Metodo responsavel por alimentar as variaves com os dados do Header dos Items
        /// </summary>
        /// <param name="HtmlNfProdLabel">HTML contendo o cabeçalho do Itens(Labels)</param>
        /// <param name="HtmlNfProdSpan">HTML contendo os dados de Span do Item.</param>
        /// <param name="NumItem">Numero do item que está sendo executado.</param>
        private void ParseItemHeaderSpan(HtmlNodeCollection HtmlNfProdLabel, HtmlNodeCollection HtmlNfProdSpan, int NumItem)
        {
            
            string[,] ProdHeaderDet = ParseItemHeaderLabel(HtmlNfProdLabel);

            HtmlNodeCollection spans = HtmlNfProdSpan[NumItem].SelectNodes(".//span");
            //Inserindo valores dos Labels no Vetor
            int i = 0;

            //Inserindo Valores dos Spans no Vetor
            foreach (HtmlNode span in spans)
            {
                ProdHeaderDet[i, 1] = span.InnerText.Trim().Replace("&nbsp;", " ").Replace("\r\n", " ").Replace("'", " ").Replace("  ", " ");
                i++;
            }

            for (i = 0; i < (ProdHeaderDet.Length / 2); i++)
            {
                switch (ProdHeaderDet[i, 0])
                {
                    case "Num.":
                        ProdutosNotaInstancia.ProdNumItem = ProdHeaderDet[i,1];
                        break;
                    case "Descrição":
                        ProdutosNotaInstancia.ProdDescricao = ProdHeaderDet[i, 1];
                        break;
                    case "Qtd.":
                        ProdutosNotaInstancia.ProdQtd = ProdHeaderDet[i, 1];
                        break;
                    case "Unidade Comercial":
                        ProdutosNotaInstancia.ProdUnidComercial = ProdHeaderDet[i, 1];
                        break;
                    case "Valor(R$)":
                        ProdutosNotaInstancia.ProdValor = ProdHeaderDet[i, 1];
                        break;
                }
            }

        }

        /// <summary>
        /// Parse dos dados do Items, Somente so dados e valores principais.
        /// </summary>
        /// <param name="HtmlNfItem"></param>
        private void ParseItens(String HtmlNfItem)
        {
            string[,] ProdItensDet = ParseHtml(HtmlNfItem);

            for (int i = 0; i < (ProdItensDet.Length / 2); i++)
            {
                switch (ProdItensDet[i, 0])
                {
                    
                    case "Código do Produto":
                        ProdutosNotaInstancia.ProdCodigo = ProdItensDet[i, 1].Substring(0, (ProdItensDet[i, 1].Length > 25 ? 25 : ProdItensDet[i, 1].Length));
                        break;
                    case "Código NCM":
                        ProdutosNotaInstancia.ProdNcm = ProdItensDet[i, 1];
                        break;
                    case "Código EX da TIPI":
                        ProdutosNotaInstancia.ProdCodExTipi = ProdItensDet[i, 1];
                        break;
                    case "CFOP":
                        ProdutosNotaInstancia.ProdCfop = ProdItensDet[i, 1];
                        break;
                    case "Outras Despesas Acessórias":
                        ProdutosNotaInstancia.ProdValorOutrasDespesas = ProdItensDet[i, 1];
                        break;
                    case "Valor do Desconto":
                        ProdutosNotaInstancia.ProdValorDesconto = ProdItensDet[i, 1];
                        break;
                    case "Valor Total do Frete":
                        ProdutosNotaInstancia.ProdValorFrete = ProdItensDet[i, 1];
                        break;
                    case "Valor do Seguro":
                        ProdutosNotaInstancia.ProdValorSeguro = ProdItensDet[i, 1];
                        break;
                }
            }

        }

        /// <summary>
        /// Parse para Dados complementares do produtos
        /// </summary>
        /// <param name="HtmlNfItemDiv"></param>
        private void ParseItensDiv(String HtmlNfItemDiv)
        {

            string[,] ItensDiv = ParseHtml(HtmlNfItemDiv);
            for (int i =0;i < (ItensDiv.Length /2);i++)
            {
                switch (ItensDiv[i, 0])
                {
                    case "Indicador de Composição do Valor Total da NF-e" :
                        ProdutosNotaInstancia.ProdIndcCampValor = ItensDiv[i,1];
                        break;
                    case "":
                        ProdutosNotaInstancia.ProdIndcCampValor = ItensDiv[i, 1];
                        break;
                    case "Código EAN Comercial":
                        ProdutosNotaInstancia.ProdCodEANComercial = ItensDiv[i, 1];
                        break;
                    case "Unidade Comercial":
                        ProdutosNotaInstancia.ProdUnidComercial = ItensDiv[i, 1];
                        break;
                    case "Quantidade Comercial":
                        ProdutosNotaInstancia.ProdQtdComercial = ItensDiv[i, 1];
                        break;
                    case "Código EAN Tributável":
                        ProdutosNotaInstancia.ProdCodEANtributavel = ItensDiv[i, 1];
                        break;
                    case "Unidade Tributável":
                        ProdutosNotaInstancia.ProdUndTributavel = ItensDiv[i, 1];
                        break;
                    case "Valor unitário de comercialização":
                        ProdutosNotaInstancia.ProdValorUnitComercial = ItensDiv[i, 1];
                        break;
                    case "Valor unitário de tributação":
                        ProdutosNotaInstancia.ProdValorUnitTributacao = ItensDiv[i, 1];
                        break;
                    case "Número do pedido de compra":
                        ProdutosNotaInstancia.ProdNroPedido = ItensDiv[i, 1];
                        break;
                    case "Valor Aproximado dos Tributos":
                        ProdutosNotaInstancia.ProdValorAproxTributos = ItensDiv[i, 1];
                        break;
                    case "Número da FCI":
                        ProdutosNotaInstancia.ProdNumFCI = ItensDiv[i, 1];
                        break;
                }
            }
        }

        /// <summary>
        /// Parse dos impostos relacionados ao Icms
        /// </summary>
        /// <param name="HtmlNfImposto"></param>
        private void ParseImpostoIcms(String HtmlNfImposto)
        {
            string[,] ImpostoIcms = ParseHtml(HtmlNfImposto);

            for (int i = 0; i < (ImpostoIcms.Length / 2); i++)
            {
                switch (ImpostoIcms[i,0].Replace("\r\n","").Replace("  ",""))
                {
                        //Base Icms
                    case "Valor da BC do ICMS":
                        ProdutosNotaInstancia.ProdBaseICMS = ImpostoIcms[i, 1];
                        break;
                    case "Base de Cálculo do ICMS Normal":
                        ProdutosNotaInstancia.ProdBaseICMS = ImpostoIcms[i, 1];
                        break;
                    case "Base de Cálculo":
                        ProdutosNotaInstancia.ProdBaseICMS = ImpostoIcms[i, 1];
                        break;
                        //Valor Icms
                    case "Valor do ICMS":
                        ProdutosNotaInstancia.ProdValorIcms = ImpostoIcms[i, 1];
                        break;
                    case "Valor do ICMS Normal":
                        ProdutosNotaInstancia.ProdValorIcms = ImpostoIcms[i, 1];
                        break;
                    case "Valor de crédito do ICMS":
                        ProdutosNotaInstancia.ProdValorIcms = ImpostoIcms[i, 1];
                        break;
                    case "Valor":
                        ProdutosNotaInstancia.ProdValorIcms = ImpostoIcms[i, 1];
                        break;
                        //Aliquota de Icms
                    case "Alíquota do ICMS Normal":
                        ProdutosNotaInstancia.ProdAliqIcms = ImpostoIcms[i, 1];
                        break;
                    case "Alíquota do imposto":
                        ProdutosNotaInstancia.ProdAliqIcms = ImpostoIcms[i, 1];
                        break;
                    case "Alíquota aplicável de cálculo do crédito":
                        ProdutosNotaInstancia.ProdAliqIcms = ImpostoIcms[i, 1];
                        break;
                    case "Alíquota":
                        ProdutosNotaInstancia.ProdAliqIcms = ImpostoIcms[i, 1];
                        break;
                        //Tributação Icms
                    case "Tributação do ICMS":
                        ProdutosNotaInstancia.ProdTributacaoICMS = ImpostoIcms[i, 1];
                        break;
                    case "Código da Situação da Operação":
                        ProdutosNotaInstancia.ProdTributacaoICMS = ImpostoIcms[i, 1];
                        break;
                    case "Código de Situação da Operação":
                        ProdutosNotaInstancia.ProdTributacaoICMS = ImpostoIcms[i, 1];
                        break;
                    case "Código de Situação da Operação ? Simples Nacional":
                        ProdutosNotaInstancia.ProdTributacaoICMS = ImpostoIcms[i, 1];
                        break;
                    case "Código de Situação da Operação - Simples Nacional":
                        ProdutosNotaInstancia.ProdTributacaoICMS = ImpostoIcms[i, 1];
                        break;
                        //Base IcmsST
                    case "Valor da BC do ICMS ST retido":
                        ProdutosNotaInstancia.ProdBaseIcmsSt = ImpostoIcms[i, 1];
                        break;
                    case "Base de Cálculo do ICMS ST":
                        ProdutosNotaInstancia.ProdBaseIcmsSt = ImpostoIcms[i, 1];
                        break;
                    case "Valor da BC do ICMS retido":
                        ProdutosNotaInstancia.ProdBaseIcmsSt = ImpostoIcms[i, 1];
                        break;
                    case "Valor da BC do ICMS ST":
                        ProdutosNotaInstancia.ProdBaseIcmsSt = ImpostoIcms[i, 1];
                        break;
                        //Valor IcmsST
                    case "Valor do ICMS ST":
                        ProdutosNotaInstancia.ProdValorIcmsSt = ImpostoIcms[i, 1];
                        break;
                    case "Valor do ICMS ST retido":
                        ProdutosNotaInstancia.ProdValorIcmsSt = ImpostoIcms[i, 1];
                        break;
                        //Outros
                    case "Origem da Mercadoria":
                        ProdutosNotaInstancia.ProdOrigemMerc = ImpostoIcms[i, 1];
                        break;
                    case "Modalidade Definição da BC ICMS NORMAL" :
                        ProdutosNotaInstancia.ProdModalidadeBaseIcms = ImpostoIcms[i, 1];
                        break;
                }
            }
        }

        /// <summary>
        /// Parse dos impostos relacionados ao IPI
        /// </summary>
        /// <param name="HtmlNfImposto"></param>
        private void ParseImposoIPI(String HtmlNfImposto)
        {
            string[,] ImpostoIPI = ParseHtml(HtmlNfImposto);
            for(int i =0;i < (ImpostoIPI.Length / 2);i++)
            {
                switch (ImpostoIPI[i,0])
                {
                    case "Classe de Enquadramento":
                        ProdutosNotaInstancia.ProdClasseEnquadramento = ImpostoIPI[i,1];
                        break;
                    case "Código de Enquadramento" :
                        ProdutosNotaInstancia.ProdCodEnquadramento = ImpostoIPI[i, 1];
                        break;
                    case "Código do Selo":
                        ProdutosNotaInstancia.ProdCodSelo = ImpostoIPI[i, 1];
                        break;
                    case "CNPJ do Produtor":
                        ProdutosNotaInstancia.ProdCnpjProdutor = ImpostoIPI[i, 1];
                        break;
                    case "Qtd. Selo":
                        ProdutosNotaInstancia.ProdQtdSelo = ImpostoIPI[i, 1];
                        break;
                    case "CST":
                        ProdutosNotaInstancia.ProdCST = ImpostoIPI[i, 1];
                        break;
                    case "Qtd Total Unidade Padrão" :
                        ProdutosNotaInstancia.ProdQtdTotUndPadrao = ImpostoIPI[i, 1];
                        break;
                    case "Valor por Unidade":
                        ProdutosNotaInstancia.ProdValorUnidIpi = ImpostoIPI[i, 1];
                        break;
                    case "Valor IPI" :
                        ProdutosNotaInstancia.ProdValorIpi = ImpostoIPI[i, 1];
                        break;
                    case "Base de Cálculo" :
                        ProdutosNotaInstancia.ProdBaseCalculoIPI = ImpostoIPI[i, 1];
                        break;
                    case "Alíquota" :
                        ProdutosNotaInstancia.ProdAliqIpi = ImpostoIPI[i, 1];
                        break;
                }
            }
        }

        /// <summary>
        /// Parse dos impostos relacionados a PIS
        /// </summary>
        /// <param name="HtmlNfImposto"></param>
        private void ParseImpostoPIS(String HtmlNfImposto)
        {
            string[,] ImpostoPIS = ParseHtml(HtmlNfImposto);
            for(int i = 0 ;i < (ImpostoPIS.Length /2); i++)
            {
                switch(ImpostoPIS[i,0])
                {
                    case "CST":
                        ProdutosNotaInstancia.ProdCstPis = ImpostoPIS[i,1];
                        break;
                    case "Base de Cálculo":
                        ProdutosNotaInstancia.ProdBaseCalculoPis = ImpostoPIS[i, 1];
                        break;
                    case "Alíquota":
                        ProdutosNotaInstancia.ProdAliqPis = ImpostoPIS[i, 1];
                        break;
                    case "Valor":
                        ProdutosNotaInstancia.ProdValorPis = ImpostoPIS[i, 1];
                        break;
                }
            }
        }

        /// <summary>
        /// Parse dos impostos relacionados a COFINS
        /// </summary>
        /// <param name="HtmlNfImposto"></param>
        private void ParseImpostoCOFINS(String HtmlNfImposto)
        {
            string[,] ImpostoCOFINS = ParseHtml(HtmlNfImposto);
            for(int i = 0;i < (ImpostoCOFINS.Length /2);i++)
            {
                switch (ImpostoCOFINS[i, 0])
                {
                    
                    case "CST":
                        ProdutosNotaInstancia.ProdCstConfins = ImpostoCOFINS[i, 1];
                        break;
                    case "Base de Cálculo":
                        ProdutosNotaInstancia.ProdBaseCalculoCofins = ImpostoCOFINS[i, 1];
                        break;
                    case "Alíquota":
                        ProdutosNotaInstancia.ProdAliqCofins = ImpostoCOFINS[i, 1];
                        break;
                    case "Valor":
                        ProdutosNotaInstancia.ProdValorCofins = ImpostoCOFINS[i, 1];
                        break;
                
                }
            }
        }

        /// <summary>
        /// Metodo responsavel por chamar os metodos de Parse de itens
        /// </summary>
        /// <param name="html"></param>
        private ProdutosNota[] StartParseItems(string html)
        {

        
            //Filtrando conteudo do HTML Dos Dados da NF-e
            HtmlDocument.Load(new StringReader(html));

            //Extraindo dados referente aos Items
            //Labels Header Itens
            HtmlNodeCollection ItemsHeaderDetLabel = HtmlDocument.DocumentNode.SelectNodes("//table[@class='prod-serv-header box']");
            //Spans Header Itens
            HtmlNodeCollection ItemsHeaderDetSpan = HtmlDocument.DocumentNode.SelectNodes("//table[@class='toggle box']");
            //Detalhes de todos os itens da nota
            HtmlNodeCollection ItemsDetFull = HtmlDocument.DocumentNode.SelectNodes("//table[@class='toggable box']");

            //Vetor que recebera as informações de cada item
            ProdutosNota[] VetProdutos = new ProdutosNota[ItemsHeaderDetSpan.Count()];

            //Muitas vezes acontece de a nota ter mais blocos de 'toggable box', do que a quantidade de itens,para isso eu crio a variavel  x
            //para ser incrementada somente quando um item for realmente processado, isso foi criado principalmente para notas que tem 
            //detalhamento de medicamentos.
            int CountItem =  0;
            for (int i = 0; CountItem < ItemsHeaderDetSpan.Count(); i++)
            {
                //Parse do Header dos itens.
                ParseItemHeaderSpan(ItemsHeaderDetLabel, ItemsHeaderDetSpan, CountItem);

                /**Quando existe um ou mais detalhamento de medicamentos, o HTML tem uma extrutura de table a mais para cada detalhamento
                para evitar erros, eu verifico se existe a tag Nro. do Lote e  se a tag ICMS Normal e ST não existe, com isso eu tenho que 
                o bloco que está no vetor tem somente detalhamentos de medicamentos. Ao final dos tratamentos eu retorno para o for
                sem prosseguir com o codigo, pulando para o proximo item do vetor, mais sem incrementar a variavel CountItem, pois não 
                foi processado nenhum item.*/
                if (ItemsDetFull[i].InnerHtml.IndexOf("Nro. do Lote") > 0 && ItemsDetFull[i].InnerHtml.IndexOf("ICMS Normal e ST") == -1)
                {
                    //Criar totina para parse de detalhamento de medicamentos.
                    continue;
                }

                HtmlNodeCollection ImpostoFildSet;

                HtmlNodeCollection ItemsDet = ItemsDetFull[i].SelectNodes(".//table[@class='box']");
                ParseItens(ItemsDet[0].InnerHtml);
                
                //Parse das informãções complementares do produtos
                HtmlNodeCollection ItemsDiv = ItemsDet[1].SelectNodes(".//tbody");

                if (ItemsDiv == null)
                {
                    HtmlAgilityPack.HtmlDocument htmlDocumentLocalItem = new HtmlAgilityPack.HtmlDocument();

                    htmlDocumentLocalItem.Load(new StringReader(ItemsDet[1].InnerHtml));

                    ParseItensDiv(ItemsDet[1].InnerHtml);

                    ImpostoFildSet = htmlDocumentLocalItem.DocumentNode.SelectNodes(".//fieldset");
                    
                }
                else
                {
                    ParseItensDiv(ItemsDiv[0].InnerHtml);
                    //Impostos
                    //ICMS e ICMSST
                    ImpostoFildSet = ItemsDiv[0].SelectNodes(".//fieldset");
                }

                HtmlNodeCollection ImpostoIcms = ImpostoFildSet[0].SelectNodes(".//table[@class='box']");
                //Encontrei 
                if (ImpostoIcms != null)
                    ParseImpostoIcms(ImpostoIcms[0].InnerHtml);

                //Verifica se a nota comtem valor de IPI
                if (ImpostoFildSet[1].InnerHtml.IndexOf("Imposto Sobre Produtos Industrializados", 0) > 0)
                {

                    HtmlNodeCollection ImpostoIPI = ImpostoFildSet[1].SelectNodes(".//table[@class='box']");
                    ParseImposoIPI(ImpostoIPI[0].InnerHtml);

                    HtmlNodeCollection ImpostoPIS = ImpostoFildSet[2].SelectNodes(".//table[@class='box']");
                    ParseImpostoPIS(ImpostoPIS[0].InnerHtml);

                    HtmlNodeCollection ImpostoCOFINS = ImpostoFildSet[3].SelectNodes(".//table[@class='box']");
                    ParseImpostoCOFINS(ImpostoCOFINS[0].InnerHtml);
                }
                else
                {//Caso a nota nao tenha IPI
                    HtmlNodeCollection ImpostoPIS = ImpostoFildSet[1].SelectNodes(".//table[@class='box']");
                    ParseImpostoPIS(ImpostoPIS[0].InnerHtml);

                    HtmlNodeCollection ImpostoCOFINS = ImpostoFildSet[2].SelectNodes(".//table[@class='box']");
                    ParseImpostoCOFINS(ImpostoCOFINS[0].InnerHtml);
                }

                //inserindo valores dos itens nas variaveis do vetor de produtos
                VetProdutos[CountItem].ProdNumItem = ProdutosNotaInstancia.ProdNumItem;
                VetProdutos[CountItem].ProdDescricao = ProdutosNotaInstancia.ProdDescricao;
                VetProdutos[CountItem].ProdQtd = ProdutosNotaInstancia.ProdQtd;
                VetProdutos[CountItem].ProdUnd = ProdutosNotaInstancia.ProdUnd;
                VetProdutos[CountItem].ProdValor = ProdutosNotaInstancia.ProdValor;
                VetProdutos[CountItem].ProdCodigo = ProdutosNotaInstancia.ProdCodigo;
                VetProdutos[CountItem].ProdNcm = ProdutosNotaInstancia.ProdNcm;
                VetProdutos[CountItem].ProdCodExTipi = ProdutosNotaInstancia.ProdCodExTipi;
                VetProdutos[CountItem].ProdCfop = ProdutosNotaInstancia.ProdCfop;
                VetProdutos[CountItem].ProdValorOutrasDespesas = ProdutosNotaInstancia.ProdValorOutrasDespesas;
                VetProdutos[CountItem].ProdValorDesconto = ProdutosNotaInstancia.ProdValorDesconto;
                VetProdutos[CountItem].ProdValorFrete = ProdutosNotaInstancia.ProdValorFrete;
                VetProdutos[CountItem].ProdValorSeguro = ProdutosNotaInstancia.ProdValorSeguro;
                VetProdutos[CountItem].ProdIndcCampValor = ProdutosNotaInstancia.ProdIndcCampValor;
                VetProdutos[CountItem].ProdCodEANComercial = ProdutosNotaInstancia.ProdCodEANComercial;
                VetProdutos[CountItem].ProdUnidComercial = ProdutosNotaInstancia.ProdUnidComercial;
                VetProdutos[CountItem].ProdQtdComercial = ProdutosNotaInstancia.ProdQtdComercial;
                VetProdutos[CountItem].ProdCodEANtributavel = ProdutosNotaInstancia.ProdCodEANtributavel;
                VetProdutos[CountItem].ProdUndTributavel = ProdutosNotaInstancia.ProdUndTributavel;
                VetProdutos[CountItem].ProdQtdTributavel = ProdutosNotaInstancia.ProdQtdTributavel;
                VetProdutos[CountItem].ProdValorUnitComercial = ProdutosNotaInstancia.ProdValorUnitComercial;
                VetProdutos[CountItem].ProdValorUnitTributacao = ProdutosNotaInstancia.ProdValorUnitTributacao;
                VetProdutos[CountItem].ProdNroPedido = ProdutosNotaInstancia.ProdNroPedido;
                VetProdutos[CountItem].ProdItempedido = ProdutosNotaInstancia.ProdItempedido;
                VetProdutos[CountItem].ProdValorAproxTributos = ProdutosNotaInstancia.ProdValorAproxTributos;
                VetProdutos[CountItem].ProdNumFCI = ProdutosNotaInstancia.ProdNumFCI;
                //Icms
                VetProdutos[CountItem].ProdTributacaoICMS = ProdutosNotaInstancia.ProdTributacaoICMS;
                VetProdutos[CountItem].ProdModalidadeBaseIcms = ProdutosNotaInstancia.ProdModalidadeBaseIcms;
                VetProdutos[CountItem].ProdBaseICMS = ProdutosNotaInstancia.ProdBaseICMS;
                VetProdutos[CountItem].ProdAliqIcms = ProdutosNotaInstancia.ProdAliqIcms;
                VetProdutos[CountItem].ProdValorIcms = ProdutosNotaInstancia.ProdValorIcms;
                VetProdutos[CountItem].ProdOrigemMerc = ProdutosNotaInstancia.ProdOrigemMerc;
                //Ipi
                VetProdutos[CountItem].ProdClasseEnquadramento = ProdutosNotaInstancia.ProdClasseEnquadramento;
                VetProdutos[CountItem].ProdCodEnquadramento = ProdutosNotaInstancia.ProdCodEnquadramento;
                VetProdutos[CountItem].ProdCodSelo = ProdutosNotaInstancia.ProdCodSelo;
                VetProdutos[CountItem].ProdCnpjProdutor = ProdutosNotaInstancia.ProdCnpjProdutor;
                VetProdutos[CountItem].ProdQtdSelo = ProdutosNotaInstancia.ProdQtdSelo;
                VetProdutos[CountItem].ProdCST = ProdutosNotaInstancia.ProdCST;
                VetProdutos[CountItem].ProdQtdTotUndPadrao = ProdutosNotaInstancia.ProdQtdTotUndPadrao;
                VetProdutos[CountItem].ProdValorUnidIpi = ProdutosNotaInstancia.ProdValorUnidIpi;
                VetProdutos[CountItem].ProdValorIpi = ProdutosNotaInstancia.ProdValorIpi;
                VetProdutos[CountItem].ProdBaseCalculoIPI = ProdutosNotaInstancia.ProdBaseCalculoIPI;
                VetProdutos[CountItem].ProdAliqIpi = ProdutosNotaInstancia.ProdAliqIpi;
                //Pis
                VetProdutos[CountItem].ProdCstPis = ProdutosNotaInstancia.ProdCstPis;
                VetProdutos[CountItem].ProdBaseCalculoPis = ProdutosNotaInstancia.ProdBaseCalculoPis;
                VetProdutos[CountItem].ProdAliqPis = ProdutosNotaInstancia.ProdAliqPis;
                VetProdutos[CountItem].ProdValorPis = ProdutosNotaInstancia.ProdValorPis;
                //Confins
                VetProdutos[CountItem].ProdCstConfins = ProdutosNotaInstancia.ProdCstConfins;
                VetProdutos[CountItem].ProdBaseCalculoCofins = ProdutosNotaInstancia.ProdBaseCalculoCofins;
                VetProdutos[CountItem].ProdAliqCofins = ProdutosNotaInstancia.ProdAliqCofins;
                VetProdutos[CountItem].ProdValorCofins = ProdutosNotaInstancia.ProdValorCofins;
                //IcmsSt
                VetProdutos[CountItem].ProdBaseIcmsSt = ProdutosNotaInstancia.ProdBaseIcmsSt;
                VetProdutos[CountItem].ProdValorIcmsSt = ProdutosNotaInstancia.ProdValorIcmsSt;
                //Contador para validar quantos produtos existem na nota, utilizo esse contador para gerar o insert de produtos e itens.
                ContadorProdutosNota++;

                ZeraValoresItens();
                CountItem++;
            }

            SetaProdutos = true;
            return VetProdutos;
        }

        /// <summary>
        /// Para evitar que valores fiquem nas variaveis a cada execução, essa metodo insere null em todos eles.
        /// </summary>
        private void ZeraValoresItens()
        {
 
                ProdutosNotaInstancia.ProdNumItem = null;
                ProdutosNotaInstancia.ProdDescricao = null;
                ProdutosNotaInstancia.ProdQtd = null;
                ProdutosNotaInstancia.ProdUnd = null;
                ProdutosNotaInstancia.ProdValor = null;
                ProdutosNotaInstancia.ProdCodigo = null;
                ProdutosNotaInstancia.ProdNcm = null;
                ProdutosNotaInstancia.ProdCodExTipi = null;
                ProdutosNotaInstancia.ProdCfop = null;
                ProdutosNotaInstancia.ProdValorOutrasDespesas = null;
                ProdutosNotaInstancia.ProdValorDesconto = null;
                ProdutosNotaInstancia.ProdValorFrete = null;
                ProdutosNotaInstancia.ProdValorSeguro = null;
                ProdutosNotaInstancia.ProdIndcCampValor = null;
                ProdutosNotaInstancia.ProdCodEANComercial = null;
                ProdutosNotaInstancia.ProdUnidComercial = null;
                ProdutosNotaInstancia.ProdQtdComercial = null;
                ProdutosNotaInstancia.ProdCodEANtributavel = null;
                ProdutosNotaInstancia.ProdUndTributavel = null;
                ProdutosNotaInstancia.ProdQtdTributavel = null;
                ProdutosNotaInstancia.ProdValorUnitComercial = null;
                ProdutosNotaInstancia.ProdValorUnitTributacao = null;
                ProdutosNotaInstancia.ProdNroPedido = null;
                ProdutosNotaInstancia.ProdItempedido = null;
                ProdutosNotaInstancia.ProdValorAproxTributos = null;
                ProdutosNotaInstancia.ProdNumFCI = null;
                ProdutosNotaInstancia.ProdTributacaoICMS = null;
                ProdutosNotaInstancia.ProdModalidadeBaseIcms = null;
                ProdutosNotaInstancia.ProdBaseICMS = null;
                ProdutosNotaInstancia.ProdAliqIcms = null;
                ProdutosNotaInstancia.ProdValorIcms = null;
                ProdutosNotaInstancia.ProdOrigemMerc = null;
                ProdutosNotaInstancia.ProdClasseEnquadramento = null;
                ProdutosNotaInstancia.ProdCodEnquadramento = null;
                ProdutosNotaInstancia.ProdCodSelo = null;
                ProdutosNotaInstancia.ProdCnpjProdutor = null;
                ProdutosNotaInstancia.ProdQtdSelo = null;
                ProdutosNotaInstancia.ProdCST = null;
                ProdutosNotaInstancia.ProdQtdTotUndPadrao = null;
                ProdutosNotaInstancia.ProdValorUnidIpi = null;
                ProdutosNotaInstancia.ProdValorIpi = null;
                ProdutosNotaInstancia.ProdBaseCalculoIPI = null;
                ProdutosNotaInstancia.ProdAliqIpi = null;
                ProdutosNotaInstancia.ProdCstPis = null;
                ProdutosNotaInstancia.ProdBaseCalculoPis = null;
                ProdutosNotaInstancia.ProdAliqPis = null;
                ProdutosNotaInstancia.ProdValorPis = null;
                ProdutosNotaInstancia.ProdCstConfins = null;
                ProdutosNotaInstancia.ProdBaseCalculoCofins = null;
                ProdutosNotaInstancia.ProdAliqCofins = null;
                ProdutosNotaInstancia.ProdValorCofins = null;
                ProdutosNotaInstancia.ProdBaseIcmsSt = null;
                ProdutosNotaInstancia.ProdValorIcmsSt = null;
            
        }

        //########################################################################################################################################
        //                                                          METODOS PUBLICOS
        //########################################################################################################################################

        /// <summary>
        /// Metodo Responsavel por resetar as variaves de controle, de modo que o parse seja executado novamente para uma outra nota Fiscal.
        /// </summary>
        public void ResetaDados()
        {
            SetaUnicos = false;
            SetaProdutos = false;
            ContadorProdutosNota = 0;
            ZeraValoresNota();
        }

        /// <summary>
        /// Metodo para Ler os valores da nota e alimentar as variaveis.
        /// </summary>
        /// <param name="html">String contendo html a ser utilizado para extrair os dados.</param>
        public void LerValoresNota(string html)
        {
            // Variavel para evitar rodar desnecessariamente os metodos de parse.
            if (SetaUnicos == false)
                StartParse(html);
        }


        /// <summary>
        /// Metodo responsavel por retornar comando Insert para a tabela HEADER_NFE_THOR;
        /// </summary>
        /// <param name="html">String contendo html a ser utilizado para extrair os dados.</param>
        /// <param name="Data">Data no formato mm/yyyy, essa data será utilizada para ajustar a data de entrada da nota Fiscal.</param>
        /// <param name="Schema">String contendo schema ao qual o insert deve ser direcionado</param>
        /// <param name="NroEmpresa">String contendo numero da empresa com 4 digitos, o mesmo deve estar cadastrado na tabela BPM_CLI_CLIENTES </param>
        /// <param name="NomeEmpresa">Nome da empresa no banco de dados do integra, a mesma deve estar cadastrada na tabela BPM_CLI_CLIENTES.</param>
        /// <returns></returns>
        public string SQLInsertHeader(string html, string Data, string Schema, string NroEmpresa, string NomeEmpresa)
        {
            //Variavel para retornar string de Insert
            string SQLInsert = null;

            // Variavel para evitar rodar desnecessariamente os metodos de parse.
            if (SetaUnicos == false) 
                StartParse(html);

            string DataEntrada = NfDataEmit;
            //Data de referencia para integracao
            if (Data != null)
            {
                if (Convert.ToInt32(NfDataEmit.Substring(3, 2)) < Convert.ToInt32(Data.Substring(0, 2)) || (Convert.ToInt32(NfDataEmit.Substring(6, 4)) < Convert.ToInt32(Data.Substring(3))))
                    DataEntrada = "01/" + Data;
            }


            //Criando String de Insert Header 

            SQLInsert += "insert into " + Schema + "header_nfe_thor" +
                                                            "(hnfe_cod_empresa,			hnfe_nr_nf,			hnfe_serie,				hnfe_cod_forn," +
                                                            "hnfe_dt_emissao,			hnfe_nf_estado,		hnfe_valor_merc,		hnfe_valor_bruto," +
                                                            "hnfe_valor_icms,			hnfe_base_icms,		hnfe_valor_ipi,			hnfe_valor_desc," +
                                                            "hnfe_valor_frete,			hnfe_valor_seguro,	hnfe_outras_despesas,	bpm_dt_criacao," +
                                                            "bpm_id_cliente,			hnfe_nf_especie,	hnfe_base_subst_trib,	hnfe_valor_subst_trib," +
                                                            "hnfe_valor_pis,			hnfe_valor_cofins,	hnfe_dt_entrada,		hnfe_chave, hnfe_tipo_nf )" +
                                                            "Values ('" +
                                                            NroEmpresa + "','" + NfNumero + "','" + NfSerie + "','" + CnpjEmit + "','" + NfDataEmit + "','" +
                                                            UFEmit + "','" + TotValorProdutos + "','" + TotValorNFe + "','" + TotValorICMS + "','" + TotBaseICMS + "','" +
                                                            TotValorIPI + "','" + TotValorDesconto + "','" + TotValorFrete + "','" + TotValorSeguro + "','" + TotValorOutrasDespesas + "'," +
                                                            "now(),'" + NomeEmpresa + "','NF-E','" + TotBaseICMSST + "','" + TotValotICMSST + "','" +
                                                            TotValorPis + "','" + TotValorCofins + "','" + DataEntrada + "','" + NfChave + "','NFE_PROG');";

            //Retorno do Insert
            return SQLInsert;
        }

        /// <summary>
        /// Metodo responsavel por retornar string com o comando insert para a tabela DETAIL_NFE_THOR
        /// </summary>
        /// <param name="html">String contendo html a ser utilizado para extrair os dados.</param>
        /// <param name="Cfop">string contendo cfop com 4 digitos, o mesmo será usado para insert, caso seja null será utilizado o conteudo da NFE</param>
        /// <param name="Schema">String contendo schema ao qual o insert deve ser direcionado</param>
        /// <param name="NroEmpresa">String contendo numero da empresa com 4 digitos, o mesmo deve estar cadastrado na tabela BPM_CLI_CLIENTES </param>
        /// <param name="Data">Data no formato mm/yyyy, essa data será utilizada para ajustar a data de entrada da nota Fiscal.</param>
        /// <param name="NomeEmpresa">Nome da empresa no banco de dados do integra, a mesma deve estar cadastrada na tabela BPM_CLI_CLIENTES.</param>
        /// <returns></returns>
        public string SQLInsertDetail(string html, string Cfop, string Schema, string NroEmpresa, string Data, string NomeEmpresa)
        {
            //Verifica se o parse já não foi executado.
            if (SetaProdutos == false)
                //Objeto contendo os dados de cada item
                ProdutosInfo = StartParseItems(html);

            //Variavel de retorno
           string SQLInsert = null;

           //Variavel para receber somente os dois primeiros numeros da CST do IPI
           string CodCst = null;

           string DataEntrada = NfDataEmit;
           //Data de referencia para integracao
           if (Data != null)
           {
               if (Convert.ToInt32(NfDataEmit.Substring(3, 2)) < Convert.ToInt32(Data.Substring(0, 2)) || (Convert.ToInt32(NfDataEmit.Substring(6, 4)) < Convert.ToInt32(Data.Substring(3))))
                   DataEntrada = "01/" + Data;
           }

           for (int i = 0; i < ContadorProdutosNota; i++)
           {
               //Caso a CST do IPI seja diferente de nulo trago somente os dois primeros digitos
               if (ProdutosInfo[i].ProdCST != null)
                   CodCst = ProdutosInfo[i].ProdCST.Substring(0, 2);


               //Caso o CFOP passado como parametro seja diferente de nulo, uso ele para insert.
               if (Cfop != null)
                   ProdutosInfo[i].ProdCfop = Cfop;
               else
                   //Setand CFOP como o CFOP horiginal da NF
                   Cfop = ProdutosInfo[i].ProdCfop;

               //Por definição do Integra, o Codigo do produto leva os quetro primeiros digitos do Emitente, para evitar codigos iguais no Efiscal.
               ProdutosInfo[i].ProdCodigo = CnpjEmit.Substring(0, 4) + "-" + ProdutosInfo[i].ProdCodigo;

               //Para cada loop do For a variavel de retorno irá receber o insert referente ao item.
               SQLInsert += "INSERT INTO " + Schema + "detail_nfe_thor" +
                                                           "(dnfe_cod_empresa,		    dnfe_nr_nf,			    dnfe_serie,		      dnfe_cod_forn," +
                                                            "dnfe_dt_emissao,			dnfe_class_fiscal,		dnfe_cod_trib,		  dnfe_cfop,			  dnfe_nr_item," +
                                                            "dnfe_cod_prod,				dnfe_cod_descr,			dnfe_unid_prod,		  dnfe_quantidade,		  dnfe_valor_unit," +
                                                            "dnfe_valor_total,			dnfe_valor_ipi,			dnfe_valor_icm,		  dnfe_aliq_ipi,		  dnfe_aliq_icm," +
                                                            "dnfe_base_icm,				dnfe_base_ipi,			bpm_dt_criacao,		  bpm_id_cliente,		  dnfe_base_subst_trib," +
                                                            "dnfe_valor_subst_trib,		hnfe_valor_bruto,		hnfe_valor_merc,	  dnfe_observacao,        dnfe_tipo," +
                                                            "dnfe_dt_entrada ,          dnfe_base_pis ,         dnfe_base_cofins,     dnfe_valor_pis,         dnfe_valor_cofins," +
                                                            "dnfe_valor_desc,           dnfe_valor_frete,       dnfe_outras_despesas, dnfe_ctipi)" +
                                                            "VALUES ('"+ NroEmpresa + "','"                         + NfNumero + "','"                              + NfSerie + "','" + CnpjEmit + "','" +
                                                                         NfDataEmit + "','"                         + ProdutosInfo[i].ProdNcm + "','"               + ProdutosInfo[i].ProdTributacaoICMS + "','"        + Cfop + "','"                              + ProdutosInfo[i].ProdNumItem + "','" +
                                                                         ProdutosInfo[i].ProdCodigo + "','"         + ProdutosInfo[i].ProdDescricao + "','"         + ProdutosInfo[i].ProdUnidComercial + "', '"        + ProdutosInfo[i].ProdQtd + "','"           + ProdutosInfo[i].ProdValorUnitComercial + "','" +
                                                                         ProdutosInfo[i].ProdValor + "','"          + ProdutosInfo[i].ProdValorIpi + "','"          + ProdutosInfo[i].ProdValorIcms + "','"             + ProdutosInfo[i].ProdAliqIpi + "','"       + ProdutosInfo[i].ProdAliqIcms + "','" + 
                                                                         ProdutosInfo[i].ProdBaseICMS + "','"       + ProdutosInfo[i].ProdBaseCalculoIPI + "','now()','"                                                + NomeEmpresa + "','"                       + ProdutosInfo[i].ProdBaseIcmsSt + "','" + 
                                                                         ProdutosInfo[i].ProdValorIcmsSt + "','"    + TotValorNFe + "','"                           + TotValorProdutos + "','"                          + ProdutosInfo[i].ProdCST + "',         'NFE_PROG','" + 
                                                                         DataEntrada + "','"                        + ProdutosInfo[i].ProdBaseCalculoPis + "','"    + ProdutosInfo[i].ProdBaseCalculoCofins + "','"     + ProdutosInfo[i].ProdValorPis + "','"      + ProdutosInfo[i].ProdValorCofins + "','" +
                                                                         ProdutosInfo[i].ProdValorDesconto + "','"  + ProdutosInfo[i].ProdValorFrete + "','"        + ProdutosInfo[i].ProdValorOutrasDespesas + "','"   + CodCst + "');";
           }
           //Retornando variavel de insert
            return SQLInsert;
        }

        /// <summary>
        /// Metodo responsavel por retornar Insert para a Tabela de CAD_FORNEC
        /// </summary>
        /// <param name="html">String contendo html a ser utilizado para extrair os dados.</param>
        /// <param name="Schema">String contendo schema ao qual o insert deve ser direcionado</param>
        /// <param name="NroEmpresa">String contendo numero da empresa com 4 digitos, o mesmo deve estar cadastrado na tabela BPM_CLI_CLIENTES </param>
        /// <param name="NomeEmpresa">Nome da empresa no banco de dados do integra, a mesma deve estar cadastrada na tabela BPM_CLI_CLIENTES.</param>
        /// <returns></returns>
        public string SQLInsertFornecedor(string Html, string Schema, string NroEmpresa, string NomeEmpresa)
        {
            string SQLInsertFornecedor = null;
            // Variavel para evitar rodar desnecessariamente os metodos de parse.
            if (SetaUnicos == false)
                StartParse(Html);

            SQLInsertFornecedor += "INSERT INTO " + Schema + "CAD_FORNEC (" +
                                                    "forn_cod_empresa,			forn_cod_cli,		forn_nome,			forn_nomered," +
                                                    "forn_endereco,				forn_municipio,		forn_estado,		forn_bairro," +
                                                    "forn_cep,					forn_tel,			forn_cgc,			forn_ie," +
                                                    "bpm_dt_criacao,			bpm_id_cliente,     forn_natureza,      forn_cod_pais) Values" +
                                                    "('"+ NroEmpresa + "','"+ CnpjEmit + "','"      + NomeEmit + "','"  + NomeFatasaiaEmit + "','" + 
                                                        EndEmit+ "','"      + MunicipioEmit +"','"  + UFEmit + "','"    + BairroEmit + "','" + 
                                                        CepEmit + "','"     + TelefoneEmit + "','"  + CnpjEmit + "','"  + InscEstadualEmit +
                                                        "','now()','"       + NomeEmpresa + "','NFE_PROG','"            + PaisEmit + "');";

            return SQLInsertFornecedor;
        }

        /// <summary>
        /// Metodo responsavel por retornar vetor, que irá conter o codigo do produto a ser inserido na primeira posição, e o comando de insert na tabela
        /// CAD_PRODUTOS na segunda posição, cabe ao programa verificar se o produtos já está cadastro na tabela antes de executar o insert./// </summary>
        /// <param name="html">String contendo html a ser utilizado para extrair os dados.</param>
        /// <param name="Schema">String contendo schema ao qual o insert deve ser direcionado</param>
        /// <param name="NroEmpresa">String contendo numero da empresa com 4 digitos, o mesmo deve estar cadastrado na tabela BPM_CLI_CLIENTES </param>
        /// <param name="NomeEmpresa">Nome da empresa no banco de dados do integra, a mesma deve estar cadastrada na tabela BPM_CLI_CLIENTES.</param>
        /// <returns></returns>
        public string[,] SQLInsertProdutos(string Html, string Schema, string NroEmpresa, string NomeEmpresa)
        {
            string[,] SQLRetornoProdutos = new string[ContadorProdutosNota,2];

            for (int i = 0; i < ContadorProdutosNota; i++)
            {
                  SQLRetornoProdutos[i, 0] = ProdutosInfo[i].ProdCodigo;
                  SQLRetornoProdutos[i,1] = "INSERT INTO " + Schema + "cad_produtos(prod_cod_empresa,           prod_cod,           prod_descr,         prod_unid," +
                                                                      "prod_ncm,                                prod_class_fiscal,  bpm_dt_criacao,     bpm_id_cliente,     prod_tipo)" +
                                                 "values ('" +  NroEmpresa + "','"              + ProdutosInfo[i].ProdCodigo + "','"        + ProdutosInfo[i].ProdDescricao + "','" + ProdutosInfo[i].ProdUndTributavel + "','" +
                                                                ProdutosInfo[i].ProdNcm + "','" + ProdutosInfo[i].ProdNcm + "','now()','"   + NomeEmpresa + "','NFE_PROG');";
                                             
            }

            return SQLRetornoProdutos;
            
        }
    }
}
