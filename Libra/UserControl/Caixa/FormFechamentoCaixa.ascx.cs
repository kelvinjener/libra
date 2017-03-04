using Libra.Class;
using Libra.Communs.Enumerators;
using Libra.Control;
using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra.UserControl.Caixa
{
    public partial class FormFechamentoCaixa1 : BaseUserControl
    {
        #region Properties
        CaixaBll caixaBll = new CaixaBll();
        VendaBll vendaBll = new VendaBll();
        VendaPagamentoBll vendaPagementoBll = new VendaPagamentoBll();
        CaixaMovimentacaoBll caixaMovimentacaoBll = new CaixaMovimentacaoBll();
        #endregion

        #region Attributes
        #endregion

        #region Page Actions
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtValorAbertura.Attributes.Add("onkeypress", "return MascaraMoeda(this, '.', ',', event);");

                try
                {
                    if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("fechamentocaixa"))
                    {
                        LimparCapos();
                        var caixaAberto = new CaixaBll().GetCaixaAbertoOutroDiaByUnidade(UsuarioInfo.UnidadeLogada);
                        if (caixaAberto != null)
                        {
                            lblTitulo.Text = "FECHAMENTO CAIXA";
                            divFechamento.Visible = true;
                            lkbSalvar.Text = "FECHAR CAIXA";

                            if (caixaAberto.USUARIORESPONSAVELID != UsuarioInfo.IdUsuario)
                            {
                                divFechamento.Visible = true;
                                divBtnSalvar.Visible = false;
                                divBtnFechar.Attributes["class"] = "col-md-offset-10 col-md-2 col-sm-12 col-xs-12";

                                MessageBoxInfo(this.Page, "Somente o usuário responsável pela abertura, tem permissão de fechar o caixa!");
                            }

                            CarregarDados(caixaAberto.CAIXAID);
                        }
                        else
                        {
                            var caixa = caixaBll.GetCaixaAbertoByDateNowAndUnidade(UsuarioInfo.UnidadeLogada);
                            if (caixa != null)
                            {
                                lblTitulo.Text = "FECHAMENTO CAIXA";
                                divFechamento.Visible = true;
                                lkbSalvar.Text = "FECHAR CAIXA";


                                if (caixa.USUARIORESPONSAVELID != UsuarioInfo.IdUsuario)
                                {
                                    divFechamento.Visible = true;
                                    divBtnSalvar.Visible = false;
                                    divBtnFechar.Attributes["class"] = "col-md-offset-10 col-md-2 col-sm-12 col-xs-12";

                                    MessageBoxInfo(this.Page, "Somente o usuário responsável pela abertura, tem permissão de fechar o caixa!");
                                }

                                CarregarDados(caixa.CAIXAID);
                            }
                            else
                            {
                                divFechamento.Visible = false;
                                lkbSalvar.Text = "ABRIR CAIXA";

                                lblSituacaoCaixa.Text = Enum.GetName(typeof(SituacaoCaixaEnum), SituacaoCaixaEnum.Fechado);
                                lblUnidade.Text = UsuarioInfo.UnidadeLogadaDescricao;
                                lblUsuarioResponsavel.Text = UsuarioInfo.Nome;
                                lblDataHoraAbertura.Text = DateTime.Now.ToString();
                            }
                        }
                    }
                    else
                    {
                        var CaixaId = Request.QueryString["Q"];

                        if (!string.IsNullOrEmpty(CaixaId))
                        {
                            var caixa = caixaBll.GetCaixaById(Convert.ToInt32(CaixaId));
                            if (caixa != null)
                            {
                                lblTitulo.Text = "DADOS ABERTURA/FECHAMENTO CAIXA";
                                divFechamento.Visible = true;
                                divBtnSalvar.Visible = false;
                                divBtnFechar.Attributes["class"] = "col-md-offset-10 col-md-2 col-sm-12 col-xs-12";

                                CarregarDados(caixa.CAIXAID);
                            }
                        }
                    }

                    //mpeFechamentoCaixa.Show();
                }
                catch (Exception ex)
                {
                    HandlerException(ex);
                }
            }
        }

        protected void lblCloseFechamentoCaixa_Click(object sender, EventArgs e)
        {
            if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("fechamentocaixa"))
            {
                //mpeFechamentoCaixa.Hide();
                Response.Redirect("/");
            }
            //else
            //{
            //    mpeFechamentoCaixa.Hide();

            //}
        }

        protected void lkbFechar_Click(object sender, EventArgs e)
        {
            if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("fechamentocaixa"))
            {
                //mpeFechamentoCaixa.Hide();
                Response.Redirect("/");
            }
            //else
            //{
            //    mpeFechamentoCaixa.Hide();

            //}
        }

        protected void lkbSalvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Salvar();
            }
        }

        #endregion

        #region Methods

        public void CarregarDados(int id)
        {
            var caixa = caixaBll.GetCaixaById(id);
            if (caixa != null)
            {

                lblSituacaoCaixa.Text = Enum.GetName(typeof(SituacaoCaixaEnum), caixa.SITUACAO);
                lblUnidade.Text = new UnidadeBll().GetUnidadeById(caixa.UNIDADEID).APELIDO;
                lblUsuarioResponsavel.Text = new UsuarioBll().GetUsuarioById(caixa.USUARIORESPONSAVELID).NOME;

                lblDataHoraAbertura.Text = caixa.DATAHORAABERTURA.ToString();
                txtValorAbertura.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", caixa.VALORABERTURA).Replace("R$", "");
                txtValorAbertura.Enabled = false;

                decimal valorVendas = 0;
                decimal valorDinheiro = 0;
                decimal valorCredito = 0;
                decimal valorDebito = 0;
                decimal valorSangria = 0;
                decimal valorSuprimento = 0;

                List<VENDA> vendas = vendaBll.GetListVendaByPeriodoCaixa(caixa.DATAHORAABERTURA, caixa.DATAHORAFECHAMENTO, UsuarioInfo.UnidadeLogada);

                foreach (var venda in vendas)
                {
                    valorVendas = valorVendas + (decimal)venda.VALORTOTAL;

                    foreach (VENDAPAGAMENTO vp in vendaPagementoBll.GetAllVendaPagamentosByIdVenda(venda.VENDAID))
                    {
                        if (vp.VENDAFORMAPAGAMENTO.TIPOPAGAMENTO == EnumUtils.GetValueInt(FormaPagamentoEnum.AVista))
                            valorDinheiro = valorDinheiro + (decimal)vp.VALORTOTAL;
                        else if (vp.VENDAFORMAPAGAMENTO.TIPOPAGAMENTO == EnumUtils.GetValueInt(FormaPagamentoEnum.Debito))
                            valorDebito = valorDebito + (decimal)vp.VALORTOTAL;
                        else if (vp.VENDAFORMAPAGAMENTO.TIPOPAGAMENTO == EnumUtils.GetValueInt(FormaPagamentoEnum.Credito))
                            valorCredito = valorCredito + vp.VALORTOTAL;
                    }
                }

                List<CAIXAMOVIMENTACAO> movimentacoes = caixaMovimentacaoBll.GetAllMovimentacaoesCaixaByCaixaId(caixa.CAIXAID, UsuarioInfo.UnidadeLogada);

                foreach (CAIXAMOVIMENTACAO cm in movimentacoes)
                {
                    if (cm.TIPOMOVIMENTACAO == EnumUtils.GetValueInt(TipoMovimentacaoEnum.Sangria))
                        valorSangria = valorSangria + cm.VALOR;
                    else if (cm.TIPOMOVIMENTACAO == EnumUtils.GetValueInt(TipoMovimentacaoEnum.Suprimento))
                        valorSuprimento = valorSuprimento + cm.VALOR;
                }

                valorVendas = caixa.TOTALVENDAS == null ? valorVendas : (decimal)caixa.TOTALVENDAS;
                valorDinheiro = caixa.VALORDINHEIRO == null ? valorDinheiro : (decimal)caixa.VALORDINHEIRO;
                valorCredito = caixa.VALORCREDITO == null ? valorCredito : (decimal)caixa.VALORCREDITO;
                valorDebito = caixa.VALORDEBITO == null ? valorDebito : (decimal)caixa.VALORDEBITO;
                valorSangria = caixa.TOTALSANGRIA == null ? valorSangria : (decimal)caixa.TOTALSANGRIA;
                valorSuprimento = caixa.TOTALSUPRIMENTOS == null ? valorSuprimento : (decimal)caixa.TOTALSUPRIMENTOS;
                decimal valorFechamento = caixa.VALORFECHAMENTO == null ? (caixa.VALORABERTURA + ((valorVendas + valorSuprimento) - valorSangria)) : (decimal)caixa.VALORFECHAMENTO;

                lbValorTotalVendas.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorVendas);
                lblValorDinheiro.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorDinheiro);
                lblCartaoDebito.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorDebito);
                lblCartaoCredito.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorCredito);
                lblValorSangria.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorSangria);
                lblValorSuprimentos.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorSuprimento);
                lblValorFechamento.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorFechamento);
                lblDataHoraFechamento.Text = caixa.DATAHORAFECHAMENTO == null ? DateTime.Now.ToString() : caixa.DATAHORAFECHAMENTO.ToString();
            }
        }

        public void LimparCapos()
        {
            txtValorAbertura.Text = string.Empty;
        }

        public void Salvar()
        {
            try
            {
                CAIXA caixa = null;

                bool Fechamento = false;

                string MensagemSucesso = string.Empty;
                CAIXA caixaAberto = new CaixaBll().GetCaixaAbertoOutroDiaByUnidade(UsuarioInfo.UnidadeLogada);
                if (caixaAberto != null)
                {

                    caixaAberto.SITUACAO = EnumUtils.GetValueInt(SituacaoCaixaEnum.Fechado);
                    caixaAberto.DATAHORAFECHAMENTO = Convert.ToDateTime(this.lblDataHoraFechamento.Text);
                    caixaAberto.DATAALTERACAO = DateTime.Now;
                    caixaAberto.ALTERADOPOR = UsuarioInfo.IdUsuario;

                    var valorAbertura = caixaAberto.VALORABERTURA;
                    var valorVendas = Convert.ToDecimal(ClearCaracter(lbValorTotalVendas.Text, "R$."));
                    var valorDinheiro = Convert.ToDecimal(ClearCaracter(lblValorDinheiro.Text, "R$."));
                    var valorDebito = Convert.ToDecimal(ClearCaracter(lblCartaoDebito.Text, "R$."));
                    var valorCredito = Convert.ToDecimal(ClearCaracter(lblCartaoCredito.Text, "R$."));
                    var valorSangria = Convert.ToDecimal(ClearCaracter(lblValorSangria.Text, "R$."));
                    var valorSuprimento = Convert.ToDecimal(ClearCaracter(lblValorSuprimentos.Text, "R$."));

                    caixaAberto.TOTALVENDAS = valorVendas;
                    caixaAberto.VALORDINHEIRO = valorDinheiro;
                    caixaAberto.VALORDEBITO = valorDebito;
                    caixaAberto.VALORCREDITO = valorCredito;
                    caixaAberto.TOTALSANGRIA = valorSangria;
                    caixaAberto.TOTALSUPRIMENTOS = valorSuprimento;
                    caixaAberto.VALORFECHAMENTO = valorAbertura + ((valorVendas + valorSuprimento) - valorSangria);

                    caixa = caixaAberto;

                    MensagemSucesso = "Caixa fechado com sucesso!";
                    Fechamento = true;
                }
                else
                {

                    caixa = caixaBll.GetCaixaAbertoByDateNowAndUnidade(UsuarioInfo.UnidadeLogada);
                    if (caixa != null)
                    {
                        caixa.SITUACAO = EnumUtils.GetValueInt(SituacaoCaixaEnum.Fechado);
                        caixa.DATAHORAFECHAMENTO = Convert.ToDateTime(this.lblDataHoraFechamento.Text);
                        caixa.DATAALTERACAO = DateTime.Now;
                        caixa.ALTERADOPOR = UsuarioInfo.IdUsuario;


                        var valorAbertura = caixa.VALORABERTURA;
                        var valorVendas = Convert.ToDecimal(ClearCaracter(lbValorTotalVendas.Text, "R$."));
                        var valorDinheiro = Convert.ToDecimal(ClearCaracter(lblValorDinheiro.Text, "R$."));
                        var valorDebito = Convert.ToDecimal(ClearCaracter(lblCartaoDebito.Text, "R$."));
                        var valorCredito = Convert.ToDecimal(ClearCaracter(lblCartaoCredito.Text, "R$."));
                        var valorSangria = Convert.ToDecimal(ClearCaracter(lblValorSangria.Text, "R$."));
                        var valorSuprimento = Convert.ToDecimal(ClearCaracter(lblValorSuprimentos.Text, "R$."));

                        caixaAberto.TOTALVENDAS = valorVendas;
                        caixaAberto.VALORDINHEIRO = valorDinheiro;
                        caixaAberto.VALORDEBITO = valorDebito;
                        caixaAberto.VALORCREDITO = valorCredito;
                        caixaAberto.TOTALSANGRIA = valorSangria;
                        caixaAberto.TOTALSUPRIMENTOS = valorSuprimento;
                        caixa.VALORFECHAMENTO = valorAbertura + ((valorVendas + valorSuprimento) - valorSangria);

                        MensagemSucesso = "Caixa fechado com sucesso!";
                        Fechamento = true;
                    }
                    else
                    {
                        caixa = new CAIXA();
                        caixa.SITUACAO = EnumUtils.GetValueInt(SituacaoCaixaEnum.Aberto);
                        caixa.USUARIORESPONSAVELID = UsuarioInfo.IdUsuario;
                        caixa.UNIDADEID = UsuarioInfo.UnidadeLogada;
                        caixa.DATAHORAABERTURA = Convert.ToDateTime(this.lblDataHoraAbertura.Text);
                        caixa.COMPETENCIA = DateTime.Now;
                        caixa.VALORABERTURA = Convert.ToDecimal(this.txtValorAbertura.Text);
                        caixa.CRIADOPOR = UsuarioInfo.IdUsuario;
                        caixa.DATACRIACAO = DateTime.Now;
                        MensagemSucesso = "Caixa aberto com sucesso!";


                    }
                }

                caixaBll.Salvar(caixa);
                MessageBoxSucesso(this.Page, MensagemSucesso);
                //if (Fechamento)
                //{
                divBtnSalvar.Visible = false;
                divBtnFechar.Attributes["class"] = "col-md-offset-10 col-md-2 col-sm-12 col-xs-12";
                // }
                CarregarDados(caixa.CAIXAID);
                //mpeFechamentoCaixa.Show();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
                //mpeFechamentoCaixa.Show();

            }
        }


        #endregion

        protected void lkbDetalharEntradas_Click(object sender, EventArgs e)
        {
            mpeEntradas.Show();
        }

        protected void lkDetalharMovimentacoes_Click(object sender, EventArgs e)
        {
            mpeMovimentacoes.Show();
        }
    }
}