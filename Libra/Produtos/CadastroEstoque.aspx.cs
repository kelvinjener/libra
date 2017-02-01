using Libra.Class;
using Libra.Control;
using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra.Produtos
{
    public partial class CadastroEstoque : BasePage
    {

        #region Atributos
        private EstoqueProdutoBll estoqueProdutoBll = new EstoqueProdutoBll();
        private EstoqueProdutoHistoricoBll estoqueProdutoHistoricoBll = new EstoqueProdutoHistoricoBll();
        private ProdutoBll produtoBll = new ProdutoBll();
        private UnidadeBll unidadeBll = new UnidadeBll();
        #endregion Atributos

        #region Properties

        #endregion Properties

        #region Page Actions

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Edicao(false);
                CarregaProdutos();
                CarregaUnidades();
                CarregaTipoProduto();
                CarregaFabricante();
                CarregaMarca();
                CarregaModelo();
                CarregaDimensoes();
                CarregaCor();

                this.txtValorCusto.Attributes.Add("onkeypress", "return MascaraMoeda(this, '.', ',', event);");
                this.txtValorVenda.Attributes.Add("onkeypress", "return MascaraMoeda(this, '.', ',', event);");
                btnVisualizarHistorico.Visible = false;
            }
        }

        protected void lbFiltroEstoque_Click(object sender, EventArgs e)
        {
            mpeFiltroEstoqueProdutos.Show();
        }

        protected void lbVisualizarEstoque_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                foreach (GridViewRow row in this.gvResults.Rows)
                {
                    if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        i++;
                }
                if (i > 1)
                {
                    MessageBoxAtencao(this.Page, "Selecione apenas um item para visualizar.");
                }
                else if (i == 0)
                {
                    MessageBoxAtencao(this.Page, "Selecione um item para visualizar.");
                }
                else
                {
                    foreach (GridViewRow row in this.gvResults.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        {
                            DataKey keys = this.gvResults.DataKeys[row.RowIndex];
                            int EstoqueId = Convert.ToInt32(keys.Values["EstoqueId"]);
                            CarregarDadosVisualizacao(EstoqueId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbAddEstoque_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            lblCodigoEstoque.Text = "Gerado pelo sistema";
            lbAddEditEstoqueProduto.Text = "Adicionar Produtos ao Estoque";
            Edicao(true);
        }

        protected void lbEditEstoque_Click(object sender, EventArgs e)
        {
            try
            {
                LimpaCampos();
                lbAddEditEstoqueProduto.Text = "Editar Estoque de Produtos";

                int i = 0;
                foreach (GridViewRow row in this.gvResults.Rows)
                {
                    if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        i++;
                }
                if (i > 1)
                {
                    MessageBoxAtencao(this.Page, "Selecione apenas um item para editar.");
                }
                else if (i == 0)
                {
                    MessageBoxAtencao(this.Page, "Selecione um item para editar.");
                }
                else
                {
                    foreach (GridViewRow row in this.gvResults.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        {
                            DataKey keys = this.gvResults.DataKeys[row.RowIndex];
                            int EstoqueId = Convert.ToInt32(keys.Values["EstoqueId"]);
                            //if (!usuarioProdutoBll.VerificaProdutoAssociadas(ProdutoId))
                            //{
                            CarregarDadosEdicao(EstoqueId);
                            Edicao(true);
                            btnVisualizarHistorico.Visible = true;

                            //}
                            //else
                            //    MessageBoxAtencao(this.Page, "O item selecionado não pode ser editado!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        //protected void lbDelEstoque_Click(object sender, EventArgs e)
        //{

        //}

        protected void gvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idEstoque = Convert.ToInt32(this.gvResults.DataKeys[gvResults.SelectedIndex].Values["EstoqueId"]);
                CarregarDadosVisualizacao(idEstoque);
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void gvResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int EstoqueId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "EstoqueId"));

                    //if (usuarioProdutoBll.VerificaProdutoAssociadas(ProdutoId))
                    //{
                    TableCell tCell = e.Row.Cells[0];
                    tCell.Attributes["style"] = "border-left-color: #FFCC33;";
                    //}
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void ldsFiltro_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                string codigoEstoqueProduto = (!this.txtCodigoEstoqueProdutoFiltro.Text.Equals(String.Empty)) ? this.txtCodigoEstoqueProdutoFiltro.Text : String.Empty;
                string codigoProduto = (!this.txtCodigoProdutoFiltro.Text.Equals(String.Empty)) ? this.txtCodigoProdutoFiltro.Text : String.Empty;
                string nomeProduto = (!this.txtDescricaoProdutoFiltro.Text.Equals(String.Empty)) ? this.txtDescricaoProdutoFiltro.Text : String.Empty;
                int tipoProduto = 0;
                int fabricante = 0;
                int marca = 0;
                int modelo = 0;
                int tamanho = 0;
                int cor = 0;

                if (this.ddlTipoProdutoFiltro.SelectedIndex > 0)
                    tipoProduto = Convert.ToInt32(this.ddlTipoProdutoFiltro.SelectedValue);

                if (this.ddlFabricanteFiltro.SelectedIndex > 0)
                    fabricante = Convert.ToInt32(this.ddlFabricanteFiltro.SelectedValue);

                if (this.ddlMarcaFiltro.SelectedIndex > 0)
                    marca = Convert.ToInt32(this.ddlMarcaFiltro.SelectedValue);

                if (this.ddlModeloFiltro.SelectedIndex > 0)
                    modelo = Convert.ToInt32(this.ddlModeloFiltro.SelectedValue);

                if (this.ddlTamanhoFiltro.SelectedIndex > 0)
                    tamanho = Convert.ToInt32(this.ddlTamanhoFiltro.SelectedValue);

                if (this.ddlCorFiltro.SelectedIndex > 0)
                    cor = Convert.ToInt32(this.ddlCorFiltro.SelectedValue);

                e.Result = estoqueProdutoBll.GetAllEstoqueProdutosGridFiltro(codigoEstoqueProduto, codigoProduto, nomeProduto, tipoProduto, fabricante, marca, modelo, tamanho, cor);

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int EstoqueId = Salvar();

                if (EstoqueId > 0)
                    this.MessageBoxSucesso(this.Page, "Estoque de Produto salvo com sucesso!");
                else if (EstoqueId == -1)
                    mpeConfirmarEdicaoRegistro.Show();
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar estoque de produto! Verifique os campos informados.");

                gvResults.DataBind();

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Edicao(false);
            btnVisualizarHistorico.Visible = false;

            gvResults.DataBind();
        }

        protected void ddlProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProduto.SelectedIndex > 0)
                {
                    var produto = produtoBll.GetProdutoById(Convert.ToInt32(ddlProduto.SelectedValue));

                    if (produto != null)
                    {
                        lblCodigoProduto.Text = produto.CODIGOPRODUTO;
                        lblProduto.Text = produto.DESCRICAO;
                        lblTipoProduto.Text = produto.TIPOPRODUTO.NOME;
                        lblFabricante.Text = produto.FABRICANTEPRODUTO.NOME;
                        lblMarca.Text = produto.MARCAPRODUTO.NOME;
                        lblModelo.Text = produto.MODELOPRODUTO.NOME;
                        lblDimensoes.Text = produto.DIMENSOESPRODUTO.DESCRICAO;
                        lblCor.Text = produto.CORPRODUTO.NOME;
                        lblPeso.Text = produto.PESO.ToString();
                    }
                }
                else
                {
                    lblCodigoProduto.Text = "---";
                    lblProduto.Text = "---";
                    lblTipoProduto.Text = "---";
                    lblFabricante.Text = "---";
                    lblMarca.Text = "---";
                    lblModelo.Text = "---";
                    lblDimensoes.Text = "---";
                    lblCor.Text = "---";
                    lblPeso.Text = "---";
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void txtMargemLucro_TextChanged(object sender, EventArgs e)
        {
            if (txtMargemLucro.Text.Length > 0 && txtValorCusto.Text.Length > 0)
            {
                decimal valorVenda = ((Convert.ToDecimal(txtValorCusto.Text) * Convert.ToDecimal(txtMargemLucro.Text) / 100) + Convert.ToDecimal(txtValorCusto.Text));

                txtValorVenda.Text = Decimal.Round(valorVenda, 2).ToString();
            }
        }

        protected void txtValorVenda_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lkCloseVisualizarEstoqueProduto_Click(object sender, EventArgs e)
        {
            mpeFiltroEstoqueProdutos.Hide();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            gvResults.DataBind();
        }

        protected void lkClose_Click(object sender, EventArgs e)
        {
            mpeFiltroEstoqueProdutos.Hide();
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            if (ddlProduto.SelectedIndex > 0 && ddlUnidade.SelectedIndex > 0)
            {
                var estoque = estoqueProdutoBll.GetEstoqueByIdProdutoAndUnidade(Convert.ToInt32(ddlProduto.SelectedValue), Convert.ToInt32(ddlUnidade.SelectedValue));
                if (estoque != null)
                    CarregarDadosEdicao(estoque.ESTOQUEID);
            }
        }

        protected void ldsHistorico_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                int EstoqueId = 0;
                if (hdnIdEstoque.Value != string.Empty)
                    EstoqueId = Convert.ToInt32(hdnIdEstoque.Value);

                e.Result = estoqueProdutoHistoricoBll.GetAllEstoqueProdutosGridByEstoqueId(EstoqueId);
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

        }

        protected void btnVisualizarHistorico_Click(object sender, EventArgs e)
        {
            int EstoqueId = 0;
            if (hdnIdEstoque.Value != string.Empty)
                EstoqueId = Convert.ToInt32(hdnIdEstoque.Value);

            var estoque = estoqueProdutoBll.GetEstoqueById(EstoqueId);
            if (estoque != null)
            {
                lblEstoqueProdutoHistorico.Text = estoque.PRODUTO.DESCRICAO;
                lblEstoqueUnidadeProdutoHistorico.Text = estoque.UNIDADE.APELIDO;
            }

            gvResult_Historico.DataBind();

            mpeHistorico.Show();
        }

        protected void lkCloseHistoricoX_Click(object sender, EventArgs e)
        {
            mpeHistorico.Hide();
        }
        #endregion Page Actions

        #region Métodos
        public void Edicao(bool edita)
        {
            divEdicao.Visible = edita;
            divFiltroETabela.Visible = !edita;
        }

        public void LimpaCampos()
        {
            hdnIdProduto.Value = "0";
            hdnIdEstoque.Value = "0";

            lblCodigoEstoque.Text = string.Empty;
            ddlUnidade.SelectedIndex = 0;
            ddlUnidade.Enabled = true;
            ddlProduto.SelectedIndex = 0;
            ddlProduto.Enabled = true;
            txtValorCusto.Text = string.Empty;
            txtMargemLucro.Text = string.Empty;
            txtValorVenda.Text = string.Empty;
            lblQtdAtualEstoque.Text = "0";
            txtQuatidadeAddEstoque.Text = string.Empty;
            lblDataUltimaAlteracaoEstoque.Text = "---";


            ddlProduto.SelectedValue = "";
            ddlProduto_SelectedIndexChanged(this, null);
            ddlUnidade.SelectedValue = "";
            ddlProduto.Attributes["tabindex"] = "-1";
            ddlUnidade.Attributes["tabindex"] = "-1";

        }

        public void CarregaProdutos()
        {
            ddlProduto.Items.Clear();

            ddlProduto.Items.Add(new ListItem("Selecione...", ""));

            var produtos = produtoBll.GetAllProdutos().Where(p => p.DISPONIVELCOMERCIO);

            foreach (var item in produtos)
                ddlProduto.Items.Add(new ListItem(item.DESCRICAO, item.PRODUTOID.ToString()));


        }

        public void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.Add(new ListItem("Selecione...", ""));

            var unidades = unidadeBll.GetAllUnidadeAtivas();

            foreach (var item in unidades)
                ddlUnidade.Items.Add(new ListItem(item.APELIDO, item.UNIDADEID.ToString()));


        }

        public void CarregaTipoProduto()
        {
            ddlTipoProdutoFiltro.Items.Clear();
            ddlTipoProdutoFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new TipoProdutoBll().GetAllTiposProdutos())
                ddlTipoProdutoFiltro.Items.Add(new ListItem(item.NOME, item.TIPOPRODUTOID.ToString()));
        }

        public void CarregaFabricante()
        {
            ddlFabricanteFiltro.Items.Clear();
            ddlFabricanteFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new FabricanteProdutoBll().GetAllFabricantesProdutos())
                ddlFabricanteFiltro.Items.Add(new ListItem(item.NOME, item.FABRICANTEPRODUTOID.ToString()));
        }

        public void CarregaMarca()
        {
            ddlMarcaFiltro.Items.Clear();
            ddlMarcaFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new MarcaProdutoBll().GetAllMarcasProdutos())
                ddlMarcaFiltro.Items.Add(new ListItem(item.NOME, item.MARCAPRODUTOID.ToString()));
        }

        public void CarregaModelo()
        {
            ddlModeloFiltro.Items.Clear();
            ddlModeloFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new ModeloProdutoBll().GetAllModeloProdutos())
                ddlModeloFiltro.Items.Add(new ListItem(item.NOME, item.MODELOPRODUTOID.ToString()));
        }

        public void CarregaDimensoes()
        {
            ddlTamanhoFiltro.Items.Clear();
            ddlTamanhoFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new DimensoesProdutoBll().GetAllAtivos())
                ddlTamanhoFiltro.Items.Add(new ListItem(item.DESCRICAO, item.DIMENSOESPRODUTOID.ToString()));

        }

        public void CarregaCor()
        {
            ddlCorFiltro.Items.Clear();
            ddlCorFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new CorProdutoBll().GetAllCoresProdutos())
                ddlCorFiltro.Items.Add(new ListItem(item.NOME, item.CORPRODUTOID.ToString()));
        }

        public void CarregarDadosVisualizacao(int idEstoque)
        {
            try
            {
                ESTOQUEPRODUTO e = estoqueProdutoBll.GetEstoqueById(idEstoque);

                if (e != null)
                {
                    lbEstoqueProduto.Text = e.PRODUTO.DESCRICAO;
                    lblVisualizarCodigoEstoque.Text = e.CODIGOESTOQUE;
                    lblVisualizarUnidade.Text = e.UNIDADE.APELIDO;
                    lblVisualizarCodigoProduto.Text = e.PRODUTO.CODIGOPRODUTO;
                    lblVisualizarProduto.Text = e.PRODUTO.DESCRICAO;
                    lblVisualizarDimensoes.Text = e.PRODUTO.DIMENSOESPRODUTO.DESCRICAO;
                    lblVisualizarDimensoes.ToolTip = e.PRODUTO.DIMENSOESPRODUTO.ALTURA + " X " + e.PRODUTO.DIMENSOESPRODUTO.LARGURA + " X " + e.PRODUTO.DIMENSOESPRODUTO.COMPRIMENTO;
                    lblVisualizarFabricante.Text = e.PRODUTO.FABRICANTEPRODUTO.NOME;
                    lblVisualizarMarca.Text = e.PRODUTO.MARCAPRODUTO.NOME;
                    lblVisualizarModelo.Text = e.PRODUTO.MODELOPRODUTO.NOME;
                    lblVisualizarPeso.Text = e.PRODUTO.PESO.ToString();
                    lblVisualizarTipoProduto.Text = e.PRODUTO.TIPOPRODUTO.NOME;
                    lblVisualizarCor.Text = e.PRODUTO.TIPOPRODUTO.NOME;
                    lblVisualizarValorCusto.Text = string.Format("{0:c}", e.VALORCUSTO);
                    lblVisualizarMargemLucro.Text = e.MARGEMLUCRO.ToString();
                    lblVisualizarValorVenda.Text = string.Format("{0:c}", e.VALORVENDA);
                    lblVisualizarQtdAtualEstoque.Text = e.QTDESTOQUE.ToString();
                    lblVisualizarDataUltimaAlteracaoEstoque.Text = e.DATAENTRADA.ToString();

                    mpeVisualizarEstoqueProduto.Show();
                }
                else
                {
                    MessageBoxError(this.Page, "Produto não localizada!");
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

        }

        public void CarregarDadosEdicao(int idEstoque)
        {
            try
            {
                ESTOQUEPRODUTO e = estoqueProdutoBll.GetEstoqueById(idEstoque);

                if (e != null)
                {
                    hdnIdProduto.Value = e.PRODUTOID.ToString();
                    hdnIdEstoque.Value = e.ESTOQUEID.ToString();

                    lblCodigoEstoque.Text = e.CODIGOESTOQUE;
                    ddlUnidade.SelectedValue = e.UNIDADEID.ToString();
                    ddlProduto.SelectedValue = e.PRODUTOID.ToString();
                    ddlProduto_SelectedIndexChanged(this, null);
                    txtValorCusto.Text = e.VALORCUSTO.ToString();
                    txtMargemLucro.Text = e.MARGEMLUCRO.ToString();
                    txtValorVenda.Text = e.VALORVENDA.ToString();
                    lblQtdAtualEstoque.Text = e.QTDESTOQUE.ToString();
                    txtQuatidadeAddEstoque.Text = e.QTDENTRADA.ToString();
                    lblDataUltimaAlteracaoEstoque.Text = e.DATAENTRADA.ToString();

                    ddlUnidade.Enabled = false;
                    ddlProduto.Enabled = false;
                    btnVisualizarHistorico.Visible = true;

                }
                else
                {
                    MessageBoxError(this.Page, "Produto não localizado!");
                }

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        public int Salvar()
        {
            try
            {
                ESTOQUEPRODUTO e = new ESTOQUEPRODUTO();

                if (!String.IsNullOrEmpty(hdnIdEstoque.Value) && Convert.ToInt32(hdnIdEstoque.Value) > 0)
                    e = estoqueProdutoBll.GetEstoqueById(Convert.ToInt32(hdnIdEstoque.Value));
                else {
                    if (ddlProduto.SelectedIndex > 0 && ddlUnidade.SelectedIndex > 0)
                    {
                        e = estoqueProdutoBll.GetEstoqueByIdProdutoAndUnidade(Convert.ToInt32(ddlProduto.SelectedValue), Convert.ToInt32(ddlUnidade.SelectedValue));
                        if (e != null)
                            return -1;
                        else {
                            e = new ESTOQUEPRODUTO();

                            var produto = produtoBll.GetProdutoById(Convert.ToInt32(ddlProduto.SelectedValue));
                            var unidadeId = ddlUnidade.SelectedValue.Length == 1 ? ("0" + ddlUnidade.SelectedValue) : ddlUnidade.SelectedValue;

                            if (produto != null && unidadeId.Length > 0)
                            {
                                e.CODIGOESTOQUE = unidadeId + produto.CODIGOPRODUTO;

                                e.UNIDADEID = Convert.ToInt32(ddlUnidade.SelectedValue);
                                e.PRODUTOID = produto.PRODUTOID;

                                //TODO: Temporário
                                e.CRIADOPOR = UsuarioInfo.IdUsuario;
                                e.DATACRIACAO = DateTime.Now;
                            }
                        }
                    }
                }

                e.QTDENTRADA = Convert.ToInt32(txtQuatidadeAddEstoque.Text);
                e.DATAENTRADA = DateTime.Now;
                e.QTDESTOQUE = Convert.ToInt32(lblQtdAtualEstoque.Text) + Convert.ToInt32(txtQuatidadeAddEstoque.Text);
                e.VALORCUSTO = Convert.ToDecimal(txtValorCusto.Text);
                e.MARGEMLUCRO = Convert.ToDecimal(txtMargemLucro.Text);
                e.VALORVENDA = Convert.ToDecimal(txtValorVenda.Text);

                estoqueProdutoBll.Salvar(e);

                if (e.ESTOQUEID > 0)
                {
                    ESTOQUEPRODUTOSHISTORICO h = new ESTOQUEPRODUTOSHISTORICO();

                    h.ESTOQUEID = e.ESTOQUEID;
                    h.QTDENTRADA = e.QTDENTRADA;
                    h.QTDESTOQUE = e.QTDESTOQUE;
                    h.VALORCUSTO = e.VALORCUSTO;
                    h.MARGEMLUCRO = e.MARGEMLUCRO;
                    h.VALORVENDA = e.VALORVENDA;
                    h.ALTERADOPOR = UsuarioInfo.IdUsuario;
                    h.DATAALTERACAO = e.DATAENTRADA;

                    estoqueProdutoHistoricoBll.Salvar(h);


                    CarregarDadosEdicao(e.ESTOQUEID);

                    return e.ESTOQUEID;
                }

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }

        #endregion Métodos


    }
}