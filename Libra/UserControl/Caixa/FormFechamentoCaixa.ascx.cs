using Libra.Class;
using Libra.Communs.Enumerators;
using Libra.Control;
using Libra.Entity;
using System;
using System.Collections.Generic;
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

                        var caixa = caixaBll.GetCaixaByDateNowAndUnidade(UsuarioInfo.UnidadeLogada);
                        if (caixa != null)
                        {
                            lblTitulo.Text = "FECHAMENTO CAIXA";
                            divFechamento.Visible = true;
                            lkbSalvar.Text = "FECHAR CAIXA";

                            lblSituacaoCaixa.Text = Enum.GetName(typeof(SituacaoCaixaEnum), caixa.SITUACAO);
                            lblUnidade.Text = caixa.USUARIO1.NOME;
                            lblUsuarioResponsavel.Text = caixa.UNIDADE.APELIDO;

                            lblDataHoraAbertura.Text = caixa.DATAHORAABERTURA.ToString();
                            txtValorAbertura.Text = string.Format("{0}:C", caixa.VALORABERTURA);
                            txtValorAbertura.Enabled = false;

                            var valorVendas = caixa.TOTALVENDAS == null ? 0 : caixa.TOTALVENDAS;
                            var valorDinheiro = caixa.VALORDINHEIRO == null ? 0 : caixa.VALORDINHEIRO;
                            var valorCredito = caixa.VALORCREDITO == null ? 0 : caixa.VALORCREDITO;
                            var valorDebito = caixa.VALORDEBITO == null ? 0 : caixa.VALORDEBITO;
                            var valorSangria = caixa.TOTALSANGRIA == null ? 0 : caixa.TOTALSANGRIA;
                            var valorSuprimento = caixa.TOTALSUPRIMENTOS == null ? 0 : caixa.TOTALSUPRIMENTOS;
                            var valorFechamento = caixa.VALORFECHAMENTO == null ? 0 : caixa.VALORFECHAMENTO;

                            lbValorTotalVendas.Text = "R$" + string.Format("{0}:C", valorVendas);
                            lblValorDinheiro.Text = "R$" + string.Format("{0}:C", valorDinheiro);
                            lblCartaoDebito.Text = "R$" + string.Format("{0}:C", valorDebito);
                            lblCartaoCredito.Text = "R$" + string.Format("{0}:C", valorCredito);
                            lblValorSangria.Text = "R$" + string.Format("{0}:C", valorSangria);
                            lblValorSuprimentos.Text = "R$" + string.Format("{0}:C", valorSuprimento);
                            lblValorFechamento.Text = "R$" + string.Format("{0}:C", valorFechamento);
                            lblDataHoraFechamento.Text = DateTime.Now.ToString();

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

                                lblSituacaoCaixa.Text = Enum.GetName(typeof(SituacaoCaixaEnum), caixa.SITUACAO);
                                lblUnidade.Text = caixa.USUARIO1.NOME;
                                lblUsuarioResponsavel.Text = caixa.UNIDADE.APELIDO;

                                lblDataHoraAbertura.Text = caixa.DATAHORAABERTURA.ToString();
                                txtValorAbertura.Text = string.Format("{0}:C", caixa.VALORABERTURA);
                                txtValorAbertura.Enabled = false;

                                var valorVendas = caixa.TOTALVENDAS == null ? 0 : caixa.TOTALVENDAS;
                                var valorDinheiro = caixa.VALORDINHEIRO == null ? 0 : caixa.VALORDINHEIRO;
                                var valorCredito = caixa.VALORCREDITO == null ? 0 : caixa.VALORCREDITO;
                                var valorDebito = caixa.VALORDEBITO == null ? 0 : caixa.VALORDEBITO;
                                var valorSangria = caixa.TOTALSANGRIA == null ? 0 : caixa.TOTALSANGRIA;
                                var valorSuprimento = caixa.TOTALSUPRIMENTOS == null ? 0 : caixa.TOTALSUPRIMENTOS;
                                var valorFechamento = caixa.VALORFECHAMENTO == null ? 0 : caixa.VALORFECHAMENTO;

                                lbValorTotalVendas.Text = "R$" + string.Format("{0}:C", valorVendas);
                                lblValorDinheiro.Text = "R$" + string.Format("{0}:C", valorDinheiro);
                                lblCartaoDebito.Text = "R$" + string.Format("{0}:C", valorDebito);
                                lblCartaoCredito.Text = "R$" + string.Format("{0}:C", valorCredito);
                                lblValorSangria.Text = "R$" + string.Format("{0}:C", valorSangria);
                                lblValorSuprimentos.Text = "R$" + string.Format("{0}:C", valorSuprimento);
                                lblValorFechamento.Text = "R$" + string.Format("{0}:C", valorFechamento);
                                lblDataHoraFechamento.Text = caixa.DATAHORAFECHAMENTO.ToString();
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

        public void LimparCapos()
        {
            txtValorAbertura.Text = string.Empty;
        }

        public void Salvar()
        {
            try
            {
                string MensagemSucesso = string.Empty;
                CAIXA caixa = caixaBll.GetCaixaAbertoByDateNowAndUnidade(UsuarioInfo.UnidadeLogada);
                if (caixa != null)
                {
                    caixa.SITUACAO = EnumUtils.GetValueInt(SituacaoCaixaEnum.Fechado);
                    caixa.DATAHORAFECHAMENTO = Convert.ToDateTime(this.lblDataHoraFechamento.Text);
                    caixa.DATAALTERACAO = DateTime.Now;
                    caixa.ALTERADOPOR = UsuarioInfo.IdUsuario;
                    MensagemSucesso = "Caixa fechado com sucesso!";
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

                caixaBll.Salvar(caixa);

                MessageBoxSucesso(this.Page, MensagemSucesso);
                //mpeFechamentoCaixa.Show();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
                //mpeFechamentoCaixa.Show();

            }
        }

        #endregion


    }
}