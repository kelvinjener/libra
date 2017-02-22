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

        public int HiddenIdCliente
        {
            get { return string.IsNullOrEmpty(this.hdnIdCliente.Value) ? 0 : Convert.ToInt32(this.hdnIdCliente.Value); }
            set { this.hdnIdCliente.Value = value.ToString(); }
        }

        public int HiddenStep0Concluida
        {
            get { return string.IsNullOrEmpty(this.hdnStep0Concluida.Value) ? 0 : Convert.ToInt32(this.hdnStep0Concluida.Value); }
            set { this.hdnStep0Concluida.Value = value.ToString(); }
        }

        public int HiddenStep1Concluida
        {
            get { return string.IsNullOrEmpty(this.hdnStep1Concluida.Value) ? 0 : Convert.ToInt32(this.hdnStep1Concluida.Value); }
            set { this.hdnStep1Concluida.Value = value.ToString(); }
        }

        public int HiddenStep2Concluida
        {
            get { return string.IsNullOrEmpty(this.hdnStep2Concluida.Value) ? 0 : Convert.ToInt32(this.hdnStep2Concluida.Value); }
            set { this.hdnStep2Concluida.Value = value.ToString(); }
        }
        #endregion

        #region Page Actions
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region Verificação Caixa
                var caixaAberto = new CaixaBll().GetCaixaAbertoOutroDiaByUnidade(UsuarioInfo.UnidadeLogada);
                if (caixaAberto != null)
                { Response.Redirect("/?M=CaixaDeOutroDiaAberto"); }
                else { 
                    var caixa = new CaixaBll().GetCaixaByDateNowAndUnidade(UsuarioInfo.UnidadeLogada);
                if (caixa == null || caixa.SITUACAO == Convert.ToInt16(EnumUtils.GetValue(SituacaoCaixaEnum.Fechado)))
                {
                    Response.Redirect("/?M=CaixaFechado");
                }
                }
                #endregion
                CarregarTela();
            }
        }

        protected void gvResultsProdutos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int VendaProdutosId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "VendaProdutosId"));
                    DropDownList ddlProduto = (DropDownList)e.Row.FindControl("ddlProduto");

                    #region CarregaProdutos
                    ddlProduto.Items.Clear();
                    ddlProduto.Items.Add(new ListItem("Selecione...", ""));

                    foreach (var item in new ProdutoBll().GetAllProdutosDisponveis())
                        ddlProduto.Items.Add(new ListItem(item.DESCRICAO, item.PRODUTOID.ToString()));

                    if (VendaProdutosId > 0)
                    {
                        var vp = vendaProdutoBll.GetVendaProdutoById(VendaProdutosId);
                        if (vp != null)
                        {
                            ddlProduto.Items.FindByValue(vp.PRODUTOID.ToString()).Selected = true;
                        }
                    }
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
                Step1AtualizarTotal();
                ddlProduto.Focus();
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
                    Decimal subTotal = Decimal.Round((Convert.ToDecimal(lblValorUnitario.Text) * Convert.ToInt32(txtQTD.Text)), 2);
                    lblSubTotal.Text = subTotal.ToString();
                    AtualizaVendaProduto(VendaProdutosId, null, null, Convert.ToInt32(txtQTD.Text), null, null, null, subTotal);

                }
                Step1AtualizarTotal();

                txtQTD.Focus();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtDesconto = (TextBox)sender;
                var row = (GridViewRow)txtDesconto.NamingContainer;
                //get the Id of the row
                Label lblSubTotal = (Label)row.FindControl("lblSubTotal");
                int VendaProdutosId = (int)this.gvResultsProdutos.DataKeys[row.RowIndex]["VendaProdutosId"];

                if (txtDesconto.Text.Length > 0)
                {
                    Decimal subTotal = Decimal.Round((Convert.ToDecimal(lblSubTotal.Text) - Convert.ToInt32(txtDesconto.Text)), 2);
                    lblSubTotal.Text = subTotal.ToString();
                    AtualizaVendaProduto(VendaProdutosId, null, null, null, null, Convert.ToInt32(txtDesconto.Text), null, subTotal);

                }
                Step1AtualizarTotal();

                txtDesconto.Focus();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void txtDescontoPorcentagem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtDescontoPorcentagem = (TextBox)sender;
                var row = (GridViewRow)txtDescontoPorcentagem.NamingContainer;
                //get the Id of the row
                Label lblSubTotal = (Label)row.FindControl("lblSubTotal");
                int VendaProdutosId = (int)this.gvResultsProdutos.DataKeys[row.RowIndex]["VendaProdutosId"];

                if (txtDescontoPorcentagem.Text.Length > 0)
                {
                    Decimal subTotal = Decimal.Round((Convert.ToDecimal(lblSubTotal.Text) - ((Convert.ToInt32(txtDescontoPorcentagem.Text) * 100) / Convert.ToDecimal(lblSubTotal.Text))), 2);
                    lblSubTotal.Text = subTotal.ToString();
                    AtualizaVendaProduto(VendaProdutosId, null, null, null, null, null, Convert.ToInt32(txtDescontoPorcentagem.Text), subTotal);

                }
                Step1AtualizarTotal();

                txtDescontoPorcentagem.Focus();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void txtAcrescimoPorcentagem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtAcrescimoPorcentagem = (TextBox)sender;
                var row = (GridViewRow)txtAcrescimoPorcentagem.NamingContainer;
                //get the Id of the row
                Label lblSubTotal = (Label)row.FindControl("lblSubTotal");
                int VendaProdutosId = (int)this.gvResultsProdutos.DataKeys[row.RowIndex]["VendaProdutosId"];

                if (txtAcrescimoPorcentagem.Text.Length > 0)
                {
                    Decimal subTotal = Decimal.Round((Convert.ToDecimal(lblSubTotal.Text) + ((Convert.ToInt32(txtAcrescimoPorcentagem.Text) * 100) / Convert.ToDecimal(lblSubTotal.Text))), 2);
                    lblSubTotal.Text = subTotal.ToString();
                    AtualizaVendaProduto(VendaProdutosId, null, null, null, Convert.ToInt32(txtAcrescimoPorcentagem.Text), null, null, subTotal);

                }
                Step1AtualizarTotal();

                txtAcrescimoPorcentagem.Focus();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
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

                        ddlParcelas.Focus();
                    }
                    else
                    {
                        lkbAddPagamento.Focus();
                    }
                }
            }
        }

        protected void ddlParcelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlParcelas.SelectedIndex > 0)
                lblValorParcela.Text = Decimal.Round((Convert.ToDecimal(txtValorPagamento.Text) / Convert.ToInt32(ddlParcelas.SelectedValue)), 2).ToString();

            lkbAddPagamento.Focus();

        }

        protected void btnStep0_Click(object sender, EventArgs e)
        {
            divStep0.Visible = true;
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;

            btnStep0.CssClass = "btn btn-default";
            btnStep1.CssClass = "btn btn-dark";
            btnStep2.CssClass = "btn btn-dark";
            btnStep3.CssClass = "btn btn-dark";

            if (HiddenIdVenda == 0)
            {
                btnStep1.Enabled = false;
                btnStep1.Attributes.Add("disabled", "disabled");
                btnStep2.Enabled = false;
                btnStep2.Attributes.Add("disabled", "disabled");
                btnStep3.Enabled = false;
                btnStep3.Attributes.Add("disabled", "disabled");

            }
        }

        protected void btnStep1_Click(object sender, EventArgs e)
        {
            divStep0.Visible = false;
            divStep1.Visible = true;
            divStep2.Visible = false;
            divStep3.Visible = false;

            btnStep0.CssClass = "btn btn-dark";
            btnStep1.CssClass = "btn btn-default";
            btnStep2.CssClass = "btn btn-dark";
            btnStep3.CssClass = "btn btn-dark";

            if (HiddenIdVenda == 0)
            {
                btnStep2.Enabled = false;
                btnStep2.Attributes.Add("disabled", "disabled");
                btnStep3.Enabled = false;
                btnStep3.Attributes.Add("disabled", "disabled");
            }
        }

        protected void btnStep2_Click(object sender, EventArgs e)
        {
            divStep0.Visible = false;
            divStep1.Visible = false;
            divStep2.Visible = true;
            divStep3.Visible = false;

            btnStep0.CssClass = "btn btn-dark";
            btnStep1.CssClass = "btn btn-dark";
            btnStep2.CssClass = "btn btn-default";
            btnStep3.CssClass = "btn btn-dark";

            if (HiddenIdVenda == 0)
            {
                btnStep3.Enabled = false;
                btnStep3.Attributes.Add("disabled", "disabled");
            }
        }

        protected void btnStep3_Click(object sender, EventArgs e)
        {
            divStep0.Visible = false;
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = true;

            btnStep0.CssClass = "btn btn-dark";
            btnStep1.CssClass = "btn btn-dark";
            btnStep2.CssClass = "btn btn-dark";
            btnStep3.CssClass = "btn btn-default";
        }

        protected void lkbCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }

        protected void lkbSalvarStep0_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SalvarStep0();
            }
        }

        protected void lkbSalvarStep1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SalvarStep1();
            }
        }

        protected void lkbSalvarStep2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SalvarStep2();
            }
        }

        protected void lkbSalvarAvancarStep0_Click(object sender, EventArgs e)
        {
            lkbSalvarStep0_Click(this, null);
            btnStep1_Click(this, null);
        }

        protected void lkbSalvarAvancarStep1_Click(object sender, EventArgs e)
        {
            lkbSalvarStep1_Click(this, null);
            btnStep2_Click(this, null);
        }

        protected void lkbSalvarAvancarStep2_Click(object sender, EventArgs e)
        {
            lkbSalvarStep2_Click(this, null);
            btnStep3_Click(this, null);

        }

        protected void lkbVoltarStep1_Click(object sender, EventArgs e)
        {


            btnStep0_Click(this, null);

        }

        protected void lkbVoltarStep2_Click(object sender, EventArgs e)
        {
            btnStep1_Click(this, null);

        }

        protected void lkbVoltarStep3_Click(object sender, EventArgs e)
        {
            btnStep2_Click(this, null);

        }

        protected void gvResultsPagamento_PreRender(object sender, EventArgs e)
        {
            if (gvResultsPagamento.Rows.Count > 0)
            {
                gvResultsPagamento.UseAccessibleHeader = true;
                gvResultsPagamento.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvResultsProdutos_PreRender(object sender, EventArgs e)
        {
            if (gvResultsProdutos.Rows.Count > 0)
            {
                gvResultsProdutos.UseAccessibleHeader = true;
                gvResultsProdutos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void lbCloseCadastroCliente_Click(object sender, EventArgs e)
        {
            mpeCadastroCliente.Hide();
        }

        protected void lkbAddCliente_Click(object sender, EventArgs e)
        {
            cucCadastroCliente.LimparCampos();
            mpeCadastroCliente.Show();
        }

        protected void lbCloseFiltroCliente_Click(object sender, EventArgs e)
        {
            mpeFiltroCliente.Hide();
        }

        protected void lkFecharFiltroCliente_Click(object sender, EventArgs e)
        {
            mpeFiltroCliente.Hide();
        }

        protected void lkFiltrarCliente_Click(object sender, EventArgs e)
        {
            mpeFiltroCliente.Hide();
        }

        protected void lkbPesquisarCliente_Click(object sender, EventArgs e)
        {
            mpeFiltroCliente.Show();
        }

        #endregion

        #region Methods
        public void CarregarTela()
        {
            CarregaFormaPagamento();
            CarregarClientes();
            LimparDados();
        }

        public void LimparDados()
        {
            HiddenIdVenda = 0;
            HiddenIdCliente = 0;
            HiddenStep0Concluida = 0;
            HiddenStep1Concluida = 0;
            HiddenStep2Concluida = 0;

            LimparAddPagamento();
            LimparAddProduto();

            btnStep1.Enabled = false;
            btnStep1.CssClass = "btn btn-dark";
            btnStep1.Attributes.Add("disabled", "disabled");
            btnStep2.Enabled = false;
            btnStep2.CssClass = "btn btn-dark";
            btnStep2.Attributes.Add("disabled", "disabled");
            btnStep3.Enabled = false;
            btnStep3.CssClass = "btn btn-dark";
            btnStep3.Attributes.Add("disabled", "disabled");

            lblNumeroPedido.Text = "Gerado pelo sistema";
            lblDataHoraCriacao.Text = DateTime.Now.ToString();
            lblSituacao.Text = Enum<SituacaoVendaEnum>.Description((SituacaoVendaEnum)SituacaoVendaEnum.EmAberto);
            lblVendedor.Text = this.UsuarioInfo != null ? this.UsuarioInfo.Nome : "---";
            lblUnidade.Text = this.UsuarioInfo != null ? this.GetUnidadeLogada().APELIDO : "---";
            ddlCliente.SelectedValue = "";

        }

        public void LimparAddProduto()
        {

        }

        public void LimparAddPagamento()
        {
            ddlFormaPagamento.SelectedValue = "";
            txtValorPagamento.Text = string.Empty;
            txtValorPagamento.Focus();

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

        public void CarregarClientes()
        {
            ddlCliente.Items.Clear();
            ddlCliente.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new ClienteBll().GetAllClientes())
                ddlCliente.Items.Add(new ListItem(item.NOME, item.CLIENTEID.ToString()));
        }

        public void AutoSelectCliente(int idCliente)
        {
            if (idCliente > 0)
            {
                CarregarClientes();
                ddlCliente.SelectedValue = idCliente.ToString();
            }
        }

        public void SalvarCliente(int idCliente)
        {
            AutoSelectCliente(idCliente);
            mpeCadastroCliente.Hide();
            MessageBoxSucesso(this.Page, "Cliente salvo com sucesso!");
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

        public void CarregarDados()
        {
            #region Step0
            #endregion

            #region Step1
            lblClienteProdutos.Text = "teste";
            lblNumeroPedidoProdutos.Text = "000000";

            if (HiddenStep0Concluida == 1)
            {
                btnStep1.Enabled = true;
                btnStep1.Attributes.Remove("disabled");
            }
            #endregion

            #region Step2
            lblClientePagamento.Text = "teste";
            lblNumeroPedidoPagamento.Text = "000000";

            if (HiddenStep1Concluida == 1)
            {
                btnStep2.Enabled = true;
                btnStep2.Attributes.Remove("disabled");
            }

            #endregion

            #region Step3
            lblClienteFinalizarVenda.Text = "teste";
            lblNumeroPedidoFinalizarVenda.Text = "000000";

            if (HiddenStep2Concluida == 1)
            {
                btnStep3.Enabled = true;
                btnStep3.Attributes.Remove("disabled");
            }

            #endregion


        }

        public void SalvarStep0()
        {
            try
            {
                HiddenStep0Concluida = 1;
                HiddenIdVenda = 1; //TODO: MUDAR
                HiddenIdCliente = 1; //TODO: MUDAR
                CarregarDados();

                MessageBoxSucesso(this.Page, "Informação salva com sucesso!");
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        public void SalvarStep1()
        {
            try
            {
                HiddenStep1Concluida = 1;

                CarregarDados();
                MessageBoxSucesso(this.Page, "Informação salva com sucesso!");
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        public void SalvarStep2()
        {
            try
            {
                HiddenStep2Concluida = 1;

                CarregarDados();
                MessageBoxSucesso(this.Page, "Informação salva com sucesso!");
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        public void Step1AtualizarTotal()
        {
            //var vp = vendaProdutoBll.GetAllVendaProdutosByIdVenda(HiddenIdVenda);
            var vp = vendaProdutoBll.GetAllVendaProdutosByIdVenda(3);

            if (vp.Count > 0)
            {
                Decimal valorTotal = 0;
                foreach (var item in vp)
                    valorTotal += (decimal)item.SUBTOTAL;

                lbValorTotal.Text = "R$ " + valorTotal;
            }

        }
        #endregion
    }
}