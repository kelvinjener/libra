using Libra.Communs;
using Libra.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Libra.Class
{
    public class BasePage : Page
    {

      

        public UsuarioInfo UsuarioInfo
        {
            get
            {
                if (Session["USUARIOINFO"] != null)
                {
                    return Session["USUARIOINFO"] as UsuarioInfo;
                }
                else
                {
                    return null;
                }
            }
            set { Session["USUARIOINFO"] = value; }
        }

        public void MessageBoxAtencao(Page page, string message)
        {
            HandlerMessagerBootbox.MessageBoxAtencao(page, message);
        }

        public void MessageBoxError(Page page, string message)
        {
            HandlerMessagerBootbox.MessageBoxError(page, message);
        }

        public void MessageBoxInfo(Page page, string message)
        {
            HandlerMessagerBootbox.MessageBoxInfo(page, message);
        }

        public void MessageBoxSucesso(Page page, string message)
        {
            HandlerMessagerBootbox.MessageBoxSucesso(page, message);
        }

        public void MessageTabbedAtencao(Page page, string message)
        {
            HandlerMessagerBootbox.MessageTabbedAtencao(page, message);
        }

        public void MessageTabbedError(Page page, string message)
        {
            HandlerMessagerBootbox.MessageTabbedError(page, message);
        }

        public void MessageTabbedInfo(Page page, string message)
        {
            HandlerMessagerBootbox.MessageTabbedInfo(page, message);
        }

        public void MessageTabbedSucesso(Page page, string message)
        {
            HandlerMessagerBootbox.MessageTabbedSucesso(page, message);
        }

        public void MessageBox(Page page, string message, string urlRedirect)
        {
            HandlerMessagerBootbox.MessageBox(page, message, urlRedirect);
        }

        public void MessageBoxAtencao(UpdatePanel updatePanel, string message)
        {
            HandlerMessagerBootbox.MessageBoxAtencao(updatePanel, message);
        }

        public void MessageBoxError(UpdatePanel updatePanel, string message)
        {
            HandlerMessagerBootbox.MessageBoxError(updatePanel, message);
        }

        public void MessageBoxInfo(UpdatePanel updatePanel, string message)
        {
            HandlerMessagerBootbox.MessageBoxInfo(updatePanel, message);
        }

        public void MessageBoxSucesso(UpdatePanel updatePanel, string message)
        {
            HandlerMessagerBootbox.MessageBoxSucesso(updatePanel, message);
        }

        public void MessageBox(UpdatePanel updatePanel, string message, string urlRedirect)
        {
            HandlerMessagerBootbox.MessageBox(updatePanel, message, urlRedirect);
        }

        public string GetLoginHttpContext()
        {
            if (Session["LOGON_USER"] != null)
                return Session["LOGON_USER"].ToString();
            else
                return string.Empty;
        }

        public int GetIntByText(String value)
        {
            int result;
            int.TryParse(value, out result);
            return result;
        }

        public short GetShortByText(String value)
        {
            short result;
            short.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Formata o CNAE para o padrão ##.##.#-##
        /// </summary>
        /// <param name="cnae">CNAE a ser formatado</param>
        /// <returns>Retorna o CNAE formatado</returns>
        public string FormatarCnae(string cnae)
        {
            if (string.IsNullOrEmpty(cnae)) return "";

            cnae = cnae.Replace("-", "").Replace("/", "").Replace(".", "");
            return Formatar(cnae, "##.##.#-##");
        }

        /// <summary>
        /// Formata o CPF para o padrão ###.###.###-##
        /// </summary>
        /// <param name="cpf">CPF a ser formatado</param>
        /// <returns>Retorna o CPF formatado</returns>
        public string FormatarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return "";

            cpf = cpf.Replace("-", "").Replace("/", "").Replace(".", "");
            return Formatar(cpf, "###.###.###-##");
        }
        /// <summary>
        /// Formata o CNPJ para o padrão ##.###.###/####-##
        /// </summary>
        /// <param name="cnpj">CNPJ a ser formatado</param>
        /// <returns>Retorna o CNPJ formatado</returns>
        public string FormatarCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj)) return "";

            cnpj = cnpj.Replace("-", "").Replace("/", "").Replace(".", "");
            return Formatar(cnpj, "##.###.###/####-##");
        }

        /// <summary>
        /// Formata o CEP para o padrão #####-###
        /// </summary>
        /// <param name="cnpj">CEP a ser formatado</param>
        /// <returns>Retorna o CEP formatado</returns>
        public string FormatarCep(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return "";

            cep = cep.Replace("-", "").Replace("/", "").Replace(".", "");
            return Formatar(cep, "#####-###");
        }

        /// <summary>
        /// Formata o telefone para o padrão (##) #####-#### ou (##) ####-####
        /// </summary>
        /// <param name="telefone">Telefone a ser formatado</param>
        /// <returns>Retorna o Telefone formatado</returns>
        public string FormatarTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone)) return "";

            telefone = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace("/", "").Replace(".", "").Replace(" ", "");
            if (telefone.Length == 10)
                return "(" + Formatar(telefone, "##) ####-####");
            else if (telefone.Length == 11)
                return "(" + Formatar(telefone, "##) #####-####");
            else
                return telefone;
        }

        public string Formatar(string valor, string mascara)
        {
            System.Text.StringBuilder dado = new System.Text.StringBuilder();
            // remove caracteres nao numericos
            foreach (char c in valor)
            {
                if (Char.IsNumber(c))
                    dado.Append(c);
            }

            int indMascara = mascara.Length;
            int indCampo = dado.Length;

            for (; indCampo > 0 && indMascara > 0;)
            {
                if (mascara[--indMascara] == '#')
                    indCampo--;
            }

            System.Text.StringBuilder saida = new System.Text.StringBuilder();
            for (; indMascara < mascara.Length; indMascara++)
            {
                saida.Append((mascara[indMascara] == '#') ? dado[indCampo++] : mascara[indMascara]);
            }
            return saida.ToString();
        }

        private string GetjQueryCode(string jsCodetoRun)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine(jsCodetoRun);
            sb.AppendLine(" });");

            return sb.ToString();
        }

        /// <summary>
        /// Run custom jquery code when page is loaded on browser.
        /// </summary>
        /// <param name="jsCodetoRun"> Jquery code to run. </param>
        protected void RunjQueryCode(string jsCodetoRun)
        {

            ScriptManager requestSM = ScriptManager.GetCurrent(this);
            if (requestSM != null && requestSM.IsInAsyncPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this,
                                    typeof(Page),
                                    Guid.NewGuid().ToString(),
                                    GetjQueryCode(jsCodetoRun),
                                    true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(Page),
                                     Guid.NewGuid().ToString(),
                                     GetjQueryCode(jsCodetoRun),
                                     true);
            }
        }

        public void HandlerException(Exception error)
        {
            //LogException.Log(error, "Default");
            //LogException.NotifySystemOps(error);
            HandlerMessager.MessageBox(this.Page, error.Message.Replace("{", " ").Replace("}", " ").Replace("\"", "").Replace("\'", "").Replace("\r", " ").Replace("\n", " "));
        }

        public void ShowReport(int? codigoReport, string parametros, int index)
        {
            ShowReport(codigoReport, parametros, "1", "Relatorio.pdf", index);
        }

        public void ShowReport(int? codigoReport, string parametros)
        {
            ShowReport(codigoReport, parametros, "1", "Relatorio.pdf", 0);
        }

        public void ShowReportWord(int? codigoReport, string parametros)
        {
            ShowReport(codigoReport, parametros, "3", "Relatorio.rtf", 0);
        }

        public void ShowReportWord(int? codigoReport, string parametros, int index)
        {
            ShowReport(codigoReport, parametros, "3", "Relatorio.rtf", index);
        }

        public void ShowReport(int? codigoReport, string parametros, string Excel)
        {
            ShowReport(codigoReport, parametros, Excel, "Relatorio.xls", 0);
        }

        public void ShowReport(int? codigoReport, string parametros, string reportNumber, string nomeArquivo, int index)
        {
            try
            {
                //FORMATO DOS PARAMETROS:
                //REL. SOMENTE COM 1 PARAMETRO =                                                           *ID1,*ID2,*ID3,*ID4,*ID5,
                //REL. COM 1 PARAMETRO E ESPERANDO 3 IDS NO PARAMETRO =                                    *ID1-ID2-ID3,*ID1-ID2-ID3, 
                //REL. COM 2 PARAMETROS E ESPERANDO 3 ID EM CADA PARAMETRO (";" = QUEBRA DE PARAMETROS) =  *ID1-ID2-ID3,*ID1-ID2-ID3,;*ID1-ID2-ID3,*ID1-ID2-ID3,

                //Busca o valor do diretório virual do Site.
                //O valor buscado é o definido na chave "SGCAdminUrl" do arquivo Web.Config
                //A linha referente no Web.Config é: <add key="SGCAdminUrl" value="http://localhost/admin"/>
                //Verificar qual é a URL no momento em que rodar a aplicação e realizar a alteração se necessário.   
                //Se fez necessário esta alteração devido a chamadas realizadas por páginas em níveis diferentes da página "ReportOutput.aspx"
                string diretorioRaiz = System.Configuration.ConfigurationManager.AppSettings["SGCTECAdminUrlReport"].ToString();

                //Verifica se o relatório está parametrizado na ZPARAMSGC
                if (codigoReport == null || codigoReport == -1)
                    throw new Exception("O relatório não está parametrizado.");

                //RelatorioBll relatorioBll = new RelatorioBll();
                //Verifica se o relatório parametrizado existe na tabela do Gerador de relatório(RRPTREPORT)
                //if (!relatorioBll.VerificaRelatorioParametrizado(codigoReport.Value))
                //throw new Exception("O relatório parametrizado não existe na base de dados.");

                Session["ParametrosReport" + index] = parametros;

                bool ie11 = this.Request.UserAgent.Contains("Trident/7") || this.Request.UserAgent.Contains("IE 9.0");
                if (ie11)
                {
                    //Conteudo cont = (Conteudo)Master;
                    //cont.ShowReportPopUp(codigoReport, parametros, reportNumber, nomeArquivo, index);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,
                        this.Page.GetType(),
                        "OpenReportWindow" + codigoReport + parametros,
                        "window.open('" + diretorioRaiz + "/ReportOutput.aspx?IDRELATORIO=" + codigoReport + "&reportnumber=" + reportNumber + "&index=" + index
                                        + "', '_blank', 'toolbar=yes, location=yes, directories=no, status=no, menubar=yes, scrollbars=yes, resizable=yes, copyhistory=yes, width=700, height=500');",
                        true);
                }
            }
            catch (Exception ex)
            {
                this.HandlerException(ex);
            }
        }


        /// <summary>
        /// Utilizado para retornar uma lista das UF´s 
        /// </summary>
        /// <returns>Lista de Uf´s</returns>
        public List<string> GetUfs()
        {
            List<string> ufs = new List<string>(Enum.GetNames(typeof(UnidadeFederativa)));
            ufs.Sort();

            return ufs;
        }

        public string ClearCaracter(string texto, string caracteres)
        {
            string newText = texto;
            for (int i = 0; i < caracteres.Length; i++)
            {
                newText = newText.Replace(caracteres.Substring(i, 1), "");
            }

            return newText;
        }

    }
}
