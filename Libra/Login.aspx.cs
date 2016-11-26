using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Libra.Models;
using Libra.Class;
using Libra.Control;
using Libra.Entity;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Libra
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}

            if (!IsPostBack)
            {
                CarregaUnidades();
                RememberMe.CssClass = "js-switch";

            }
        }

        public void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.Add("Unidade");

            List<UNIDADE> perfis = new UnidadeBll().GetAllUnidadeAtivas();
            foreach (var unidade in perfis)
                ddlUnidade.Items.Add(new ListItem(unidade.APELIDO, unidade.UNIDADEID.ToString()));

            ddlUnidade.SelectedIndex = 0;
        }

        protected void LogIn(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {

                    if (ddlUnidade.SelectedIndex > 0 && !new UsuarioUnidadeBll().VerificaUnidadeUsuario(Email.Text, Convert.ToInt32(ddlUnidade.SelectedValue)))
                    {
                        MessageBoxError(this.Page, "Login Inválido!");
                        return;
                    }

                    // Validate the user password
                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();


                    // This doen't count login failures towards account lockout
                    // To enable password failures to trigger lockout, change to shouldLockout: true
                    var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            GetUsuarioInfo(new UsuarioBll().GetUsuarioByLogin(Email.Text).ASPNETUSERID);
                            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                            break;
                        case SignInStatus.LockedOut:
                            Response.Redirect("/Account/Lockout");
                            break;
                        case SignInStatus.RequiresVerification:
                            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                            Request.QueryString["ReturnUrl"],
                                                            RememberMe.Checked),
                                              true);
                            break;
                        case SignInStatus.Failure:
                        default:
                            MessageBoxError(this.Page, "Login Inválido!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        private void GetUsuarioInfo(string aspNetUserId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlUnidade.SelectedValue))
                {
                    UsuarioBll usuarioBll = new UsuarioBll();
                    this.UsuarioInfo = usuarioBll.GetUsuarioInfo(aspNetUserId, Convert.ToInt32(ddlUnidade.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

    }
}