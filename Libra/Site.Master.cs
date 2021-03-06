﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Libra.Communs;
using Libra.Control;
using Libra.Communs.Enumerators;
using Libra.Class;

namespace Libra
{
    public partial class SiteMaster : BaseMasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

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

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string UserId = HttpContext.Current.User.Identity.GetUserId();
                if (UsuarioInfo != null && !string.IsNullOrEmpty(UserId))
                {
                    string[] nomeUsuario = UsuarioInfo.Nome.ToString().Split(' ');

                    //usarioLabelLateral.Text = "Bem Vindo, " + nomeUsuario[0] + "!";
                    nomeUsuarioSuperior.Text = nomeUsuario[0];
                    labelUnidadeLogada.Text = new UnidadeBll().GetUnidadeById(UsuarioInfo.UnidadeLogada).APELIDO;

                    CarregaMenu();
                }
                else
                {
                    Response.Redirect("/Login");
                }

                string paginaAtual = Request.Url.LocalPath.ToString().Replace("/", "").Replace(".aspx", "");
                string Home = "/";

                //TODO: Regra de validação de acesso.
                if (paginaAtual != Home && (!new FuncionalidadeBll().CanAcess(UsuarioInfo.IdUsuario, paginaAtual)))
                {
                    Response.Redirect(Home + "?AcessoNegado=true");
                }

                #region Verificação Caixa
                var caixaAberto = new CaixaBll().GetCaixaAbertoOutroDiaByUnidade(UsuarioInfo.UnidadeLogada);
                if (caixaAberto != null)
                {
                    iCaixa.Style.Add("color", "green");
                    iCaixa.Attributes.Add("title", "Caixa Aberto");
                    liCaixaFechado.Visible = false;
                    liCaixaAberto.Visible = true;
                    lbHoraAbertura.Text = caixaAberto.DATAHORAABERTURA.ToString();
                    lbResponsavelCaixa.Text = caixaAberto.USUARIO1.NOME;

                    MessageBoxAtencao(this.Page, string.Format("O caixa do dia {0} consta em aberto! Favor realizar o fechamento do caixa.", caixaAberto.DATAHORAABERTURA.ToShortDateString()));

                }
                else
                {
                    var caixa = new CaixaBll().GetCaixaAbertoByDateNowAndUnidade(UsuarioInfo.UnidadeLogada);
                    if (caixa != null && caixa.SITUACAO == Convert.ToInt16(EnumUtils.GetValueInt(SituacaoCaixaEnum.Aberto)))
                    {
                        iCaixa.Style.Add("color", "green");
                        iCaixa.Attributes.Add("title", "Caixa Aberto");
                        liCaixaFechado.Visible = false;
                        liCaixaAberto.Visible = true;
                        lbHoraAbertura.Text = caixa.DATAHORAABERTURA.ToString();
                        lbResponsavelCaixa.Text = caixa.USUARIO1.NOME;
                    }

                    else
                    {
                        iCaixa.Attributes.Add("title", "Caixa Fechado");
                        iCaixa.Style.Add("color", "red");
                        liCaixaFechado.Visible = true;
                        liCaixaAberto.Visible = false;
                    }
                }
                #endregion
            }


        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public void CarregaMenu()
        {
            #region Menu Pai
            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Vendas"))
                Vendas.Visible = true;

            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Caixa"))
                Caixa.Visible = true;

            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Produtos"))
                Produtos.Visible = true;

            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Logística"))
                Logistica.Visible = true;

            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Clientes"))
                Clientes.Visible = true;

            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Fornecedores"))
                Fornecedores.Visible = true;

            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Relatórios"))
                Relatorios.Visible = true;

            if (!Vendas.Visible &&
                !Caixa.Visible &&
                !Produtos.Visible &&
                !Logistica.Visible &&
                !Clientes.Visible &&
                !Fornecedores.Visible &&
                !Relatorios.Visible)
            {
                General.Visible = false;
            }
            else
            {
                General.Visible = true;
            }

            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Segurança"))
                Seguranca.Visible = true;

            if (new FuncionalidadeBll().ExibeMenuPai(UsuarioInfo.IdUsuario, "Parâmetros"))
                Parametros.Visible = true;

            if (!Seguranca.Visible &&
               !Parametros.Visible)
            {
                Config.Visible = false;
            }
            else
            {
                Config.Visible = true;
            }

            #endregion

            #region Sub Menu

            #region Segurança
            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Usuários"))
                MenuUsuarios.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Unidades"))
                MenuUnidades.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Perfis"))
                MenuPerfis.Visible = true;
            #endregion

            #region Parâmetros
            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Parâmetros de Produtos"))
                MenuParametrosProdutos.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Parâmetros de Vendas"))
                MenuParametrosVendas.Visible = true;
            #endregion

            #region Produtos
            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Cadastro de Produtos"))
                MenuCadastroProdutos.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Cadastro de Estoque"))
                MenuEstoque.Visible = true;
            #endregion

            #region Fornecedores
            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Cadastro de Fornecedores"))
                MenuFornecedores.Visible = true;
            #endregion

            #region Clientes
            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Cadastro de Clientes"))
                MenuCliente.Visible = true;
            #endregion

            #region Relatorios
            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Relatório Consolidado de Vendas"))
                MenuRelatorioVendas.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Relatório Caixa"))
                MenuRelatorioCaixa.Visible = true;
            #endregion

            #region Vendas
            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Venda"))
                MenuVenda.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Troca e Devolução"))
                MenuTrocaDecolucao.Visible = true;



            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Autorização de Vendas"))
                MenuAutorizacaoVendas.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Vendas em Aberto"))
                MenuVendasAberto.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Vendas Realizada"))
                MenuVendasRealizadas.Visible = true;
            #endregion

            #region Caixa
            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Fechamento de Caixa"))
                MenuFechamentoCaixa.Visible = true;

            if (new FuncionalidadeBll().ExibeMenu(UsuarioInfo.IdUsuario, "Sangria/Suprimento de Caixa"))
                MenuMovimentacaoCaixa.Visible = true;
            #endregion

            #endregion
        }

        public void HandlerMessage(string message)
        {
            HandlerMessager.MessageBox(this.Page, message);
        }
    }
}