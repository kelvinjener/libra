using Libra.Class;
using Libra.Communs.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra.Vendas
{
    public partial class Venda : BasePage
    {
        #region Page Actions
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregarTela();
            }
        }
        #endregion

        #region Methods
        public void CarregarTela()
        {
            lblNumeroPedido.Text = "Gerado pelo sistema";
            lblDataHoraCriacao.Text = DateTime.Now.ToString();
            lblSituacao.Text = Enum<SituacaoVendaEnum>.Description((SituacaoVendaEnum)SituacaoVendaEnum.EmAberto);
            lblVendedor.Text = this.UsuarioInfo != null ? this.UsuarioInfo.Nome : "---";
            lblUnidade.Text = this.UsuarioInfo != null ? this.GetUnidadeLogada().APELIDO : "---";
            ddlCliente.SelectedValue = "";
        }

        public void Edicao(bool edita)
        {
            ddlCliente.Enabled = edita;
        }
        #endregion
    }
}