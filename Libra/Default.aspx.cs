using Libra.Class;
using Libra.Control;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string AcessoNegado = Request.QueryString["AcessoNegado"];

                if (!String.IsNullOrEmpty(AcessoNegado) && Convert.ToBoolean(AcessoNegado))
                {
                    MessageBoxAtencao(this.Page, "Usuário sem permissão de acesso à funcionalidade!");
                }

                string Mensagem = Request.QueryString["M"];

                if (!String.IsNullOrEmpty(Mensagem))
                {
                    if (Mensagem == "CaixaFechado")
                        MessageBoxAtencao(this.Page, "Não é possível realizar uma venda com o caixa fechado!");
                    else if (Mensagem == "CaixaDeOutroDiaAberto")
                        MessageBoxAtencao(this.Page, "Não é possível realizar vendas em caixas abertos em outro dia. Favor realizar o fechamento do caixa, abrir o do dia atual e realizar a venda novamente.");

                }
            }
        }
    }
}