using Libra.Class;
using Libra.Controllers;
using Libra.Controllers.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra
{
    public partial class CadastroFornecedores : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaCRT();
        }

        public void CarregaCRT()
        {
            var instancia = new CRTController();
            var itens = instancia.RetornaTodos();

            ddlCRT.Items.Clear();
            ddlCRT.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in itens)
            {
                string value = Convert.ToString(item.ID);
                string text = item.DESCRICAO;

                ddlCRT.Items.Add(new ListItem(text, value));
            }
        }
    }
}