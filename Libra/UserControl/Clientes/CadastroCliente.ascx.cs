using Libra.Class;
using Libra.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra.UserControl.Clientes
{
    public partial class CadastroCliente : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LimparCampos()
        {

        }

        public void FinalizarCadastroPorVenda(int idCliente)
        {
            if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("venda"))
            {
                Venda cad = (Venda)Page;
                cad.HiddenIdCliente = idCliente;
                cad.AutoSelectCliente(idCliente);
            }
        }
    }
}