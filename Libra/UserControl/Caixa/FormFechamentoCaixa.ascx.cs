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
                            lblUnidade.Text = caixa.USUARIO.NOME;
                            lblUsuarioResponsavel.Text = caixa.UNIDADE.APELIDO;
                            //TODO: Campos
                            lblDataHoraAbertura.Text = caixa.DATAHORAABERTURA.ToString();
                            txtValorAbertura.Text = "R$" + string.Format("{0}:C", caixa.VALORABERTURA);
                            txtValorAbertura.Enabled = false;

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
                                lkbSalvar.Visible = false;



                                lblSituacaoCaixa.Text = Enum.GetName(typeof(SituacaoCaixaEnum), caixa.SITUACAO);
                                lblUnidade.Text = caixa.USUARIO.NOME;
                                lblUsuarioResponsavel.Text = caixa.UNIDADE.APELIDO;

                                //TODO: Campos
                                lblDataHoraAbertura.Text = caixa.DATAHORAABERTURA.ToString();
                                txtValorAbertura.Text = "R$" + string.Format("{0}:C", caixa.VALORABERTURA);

                            }
                        }
                    }

                    mpeFechamentoCaixa.Show();
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
                mpeFechamentoCaixa.Hide();
                Response.Redirect("/");
            }
            else
            {
                mpeFechamentoCaixa.Hide();

            }
        }

        protected void lkbFechar_Click(object sender, EventArgs e)
        {
            if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("fechamentocaixa"))
            {
                mpeFechamentoCaixa.Hide();
                Response.Redirect("/");
            }
            else
            {
                mpeFechamentoCaixa.Hide();

            }
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

        }

        public void Salvar()
        {
            try
            {
                MessageBoxSucesso(this.Page, "Salvo com Sucesso!");
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        #endregion


    }
}