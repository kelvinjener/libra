using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Libra.Models;
using Libra.Class;

namespace Libra
{
    public partial class ResetPassword : BasePage
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                string code = IdentityHelper.GetCodeFromRequest(Request);
                if (code != null)
                {
                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                    var user = manager.FindByName(Email.Text);
                    if (user == null)
                    {
                        MessageBoxError(this.Page, "Usuário não encontrado!");
                        return;
                    }
                    var result = manager.ResetPassword(user.Id, code, Password.Text);
                    if (result.Succeeded)
                    {
                        Response.Redirect("~/ResetPasswordConfirmation");
                        return;
                    }
                    MessageBoxError(this.Page, result.Errors.FirstOrDefault());
                    return;
                }

                MessageBoxError(this.Page, "Algum erro ocorreu!");
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }
    }
}