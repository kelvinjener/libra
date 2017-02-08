using Libra.Class;
using Libra.Communs.Enumerators;
using Libra.Control;
using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra.Vendas
{
    public partial class Venda : BasePage
    {
        #region Attributes
        VendaProdutoBll vendaProdutoBll = new VendaProdutoBll();
        VendaPagamentoBll vendaPagamentoBll = new VendaPagamentoBll();
        #endregion

        #region Properties
        public int HiddenIdVenda
        {
            get { return string.IsNullOrEmpty(this.hdnIdVenda.Value) ? 0 : Convert.ToInt32(this.hdnIdVenda.Value); }
            set { this.hdnIdVenda.Value = value.ToString(); }
        }
        #endregion

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
            CarregaFormaPagamento();
            LimparAddProduto();
            LimparAddPagamento();
        }

        public void LimparAddProduto()
        {

        }

        public void LimparAddPagamento()
        {
            ddlFormaPagamento.SelectedValue = "";
            txtValorPagamento.Text = string.Empty;
            if (divParcelas.Visible && ddlParcelas.Items.Count > 0)
                ddlParcelas.SelectedValue = "";
            lblValorParcela.Text = string.Empty;

            divParcelas.Visible = false;
            divValorParcelas.Visible = false;
        }

        public void CarregaFormaPagamento()
        {
            ddlFormaPagamento.Items.Clear();
            ddlFormaPagamento.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new VendaFormaPagamentoBll().GetAllFormasPagamento())
                ddlFormaPagamento.Items.Add(new ListItem(item.DESCRICAO, item.FORMAPAGAMENTOID.ToString()));
        }

        public void Edicao(bool edita)
        {
            ddlCliente.Enabled = edita;
        }

        public void AtualizaVendaProduto(int VendaProdutoID, int? ProdutoId, decimal? ValorUnitario, int? Qtd, decimal? ValorAcrescimo, decimal? ValorDesconto, decimal? PercentDesconto, decimal? SubTotal)
        {
            try
            {
                VENDAPRODUTO vp = vendaProdutoBll.GetVendaProdutoById(VendaProdutoID);
                if (vp != null)
                {
                    if (ProdutoId != null)
                        vp.PRODUTOID = ProdutoId;

                    if (Qtd != null)
                        vp.QTD = (int)Qtd;

                    if (ValorUnitario != null)
                        vp.VALORUNITARIO = ValorUnitario;

                    if (ValorAcrescimo != null)
                        vp.VALORACRESCIMO = ValorAcrescimo;

                    if (ValorDesconto != null)
                        vp.VALORDESCONTO = ValorDesconto;

                    if (PercentDesconto != null)
                        vp.PERCENTDESCONTO = PercentDesconto;

                    if (SubTotal != null)
                        vp.SUBTOTAL = SubTotal;

                    vendaProdutoBll.Salvar(vp);
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

        }
        #endregion

        protected void gvResultsProdutos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlProduto = (DropDownList)e.Row.FindControl("ddlProduto");

                    #region CarregaProdutos
                    ddlProduto.Items.Clear();
                    ddlProduto.Items.Add(new ListItem("Selecione...", ""));

                    foreach (var item in new ProdutoBll().GetAllProdutosDisponveis())
                        ddlProduto.Items.Add(new ListItem(item.DESCRICAO, item.PRODUTOID.ToString()));
                    #endregion
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void ddlProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlProduto = (DropDownList)sender;
                var row = (GridViewRow)ddlProduto.NamingContainer;
                //get the Id of the row
                Label lblValorUnitario = (Label)row.FindControl("lblValorUnitario");
                int VendaProdutosId = (int)this.gvResultsProdutos.DataKeys[row.RowIndex]["VendaProdutosId"];
                if (ddlProduto.SelectedIndex > 0)
                {
                    ESTOQUEPRODUTO p = new EstoqueProdutoBll().GetEstoqueByIdProdutoAndUnidade(Convert.ToInt32(ddlProduto.SelectedValue), UsuarioInfo.UnidadeLogada);
                    lblValorUnitario.Text = p.VALORCUSTO.ToString();

                    AtualizaVendaProduto(VendaProdutosId, Convert.ToInt32(ddlProduto.SelectedValue), p.VALORCUSTO, null, null, null, null, null);
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }


        }

        protected void txtQtd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtQTD = (TextBox)sender;
                var row = (GridViewRow)txtQTD.NamingContainer;
                //get the Id of the row
                Label lblSubTotal = (Label)row.FindControl("lblSubTotal");
                Label lblValorUnitario = (Label)row.FindControl("lblValorUnitario");
                DropDownList ddlProduto = (DropDownList)row.FindControl("ddlProduto");
                int VendaProdutosId = (int)this.gvResultsProdutos.DataKeys[row.RowIndex]["VendaProdutosId"];

                if (txtQTD.Text.Length > 0 && lblValorUnitario.Text.Length > 0)
                {
                    Decimal subTotal = Decimal.Round((Convert.ToDecimal(lblValorUnitario.Text) * Convert.ToInt32(txtQTD.Text)),2);
                    lblSubTotal.Text = subTotal.ToString();
                    AtualizaVendaProduto(VendaProdutosId, null, null, Convert.ToInt32(txtQTD.Text), null, null, null, subTotal);

                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void txtDesconto_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDescontoPorcentagem_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtAcrescimoPorcentagem_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ldsFiltroProdutos_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                //e.Result = vendaProdutoBll.GetAllVendaProdutosByIdVendaForGrid(Convert.ToInt32(HiddenIdVenda));
                e.Result = vendaProdutoBll.GetAllVendaProdutosByIdVendaForGrid(Convert.ToInt32(3));
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lkbAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (HiddenIdVenda > 0)
                //{
                //Variáveis Publicas a todo o formulario....
                VENDAPRODUTO vp = new VENDAPRODUTO();
                vp.ITEM = (vendaProdutoBll.GetAllVendaProdutosByIdVenda(HiddenIdVenda).OrderByDescending(i => i.ITEM).Select(i => i.ITEM).FirstOrDefault() + 1);
                //vp.VENDAID = HiddenIdVenda;
                vp.VENDAID = 3;
                vp.CRIADOPOR = UsuarioInfo.IdUsuario;
                vp.DATACRIACAO = DateTime.Now;
                vendaProdutoBll.Salvar(vp);


                gvResultsProdutos.DataBind();
                //}
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnDelProdutoVenda_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int? RowItem = Int32.Parse(e.CommandArgument.ToString());
                //if (HiddenIdVenda > 0 && RowItem != null)
                //{
                //vendaProdutoBll.DeletarProduto(HiddenIdVenda, (int)RowItem);
                vendaProdutoBll.DeletarProduto(3, (int)RowItem);
                MessageBoxSucesso(this.Page, "Produto excluído com sucesso!");

                //}
                gvResultsProdutos.DataBind();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void ldsFiltroPagamento_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                //e.Result = vendaPagamentoBll.GetAllVendaPagamentosByIdVendaPagamento(HiddenIdVenda);
                e.Result = vendaPagamentoBll.GetAllVendaPagamentosByIdVendaPagamento(Convert.ToInt32(3));
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnDelPagamento_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int? RowItem = Int32.Parse(e.CommandArgument.ToString());
                //if (HiddenIdVenda > 0 && RowItem != null)
                //{
                vendaPagamentoBll.DeletarPagamento((int)RowItem);
                MessageBoxSucesso(this.Page, "Pagamento excluído com sucesso!");

                //}
                gvResultsPagamento.DataBind();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lkbAddPagamento_Click(object sender, EventArgs e)
        {
            try
            {
                //if (HiddenIdVenda > 0 && ddlFormaPagamento.SelectedIndex > 0)
                //{

                VENDAPAGAMENTO vendaPagamento = new VENDAPAGAMENTO();

                //vendaPagamento.VENDAID = HiddenIdVenda;
                vendaPagamento.VENDAID = 3;
                vendaPagamento.FORMAPAGAMENTOID = Convert.ToInt32(ddlFormaPagamento.SelectedValue);
                vendaPagamento.VALORTOTAL = Convert.ToDecimal(txtValorPagamento.Text);
                vendaPagamento.NUMEROPARCELA = divParcelas.Visible ? ddlParcelas.SelectedIndex == 0 ? 1 : Convert.ToInt32(ddlParcelas.SelectedValue) : 1;
                vendaPagamento.VALORPARCELA = Decimal.Round(((decimal)vendaPagamento.VALORTOTAL / (decimal)vendaPagamento.NUMEROPARCELA), 2);
                vendaPagamento.CRIADOPOR = UsuarioInfo.IdUsuario;
                vendaPagamento.DATACRIACAO = DateTime.Now;

                vendaPagamentoBll.Salvar(vendaPagamento);
                MessageBoxSucesso(this.Page, "Pagamento inserido com sucesso!");

                gvResultsPagamento.DataBind();
                formAddPagamento.Visible = false;

                //}
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

        }

        protected void lkbAddNovoPagamento_Click(object sender, EventArgs e)
        {
            LimparAddPagamento();
            formAddPagamento.Visible = true;
        }

        protected void ddlFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlFormaPagamento.SelectedIndex > 0)
            {
                VENDAFORMAPAGAMENTO vfp = new VendaFormaPagamentoBll().GetFormaPagamentoById(Convert.ToInt32(ddlFormaPagamento.SelectedValue));
                if (vfp != null)
                {
                    if (vfp.TIPOPAGAMENTO == EnumUtils.GetValueInt(TipoPagamentoEnum.CreditoParcelado))
                    {
                        divParcelas.Visible = true;
                        divValorParcelas.Visible = true;

                        ddlParcelas.Items.Clear();
                        ddlParcelas.Items.Add(new ListItem("Selecione...", ""));
                        int qtdParcelas = Convert.ToInt32(vfp.QTDPARCELAS);
                        for (int cont = 0; cont <= qtdParcelas; cont++)
                        {
                            ddlParcelas.Items.Add(new ListItem(cont.ToString(), cont.ToString()));
                        }
                    }
                }
            }
        }

        protected void ddlParcelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlParcelas.SelectedIndex > 0)
            {
                lblValorParcela.Text = Decimal.Round((Convert.ToDecimal(txtValorPagamento.Text) / Convert.ToInt32(ddlParcelas.SelectedValue)), 2).ToString();
            }
        }


    }
}