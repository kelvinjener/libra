using Libra.Class;
using Libra.Control;
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
            }
        }

        protected void lbFiltroEstoque_Click(object sender, EventArgs e)
        {

        }

        protected void lbVisualizarEstoque_Click(object sender, EventArgs e)
        {

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
                            int ProdutoId = Convert.ToInt32(keys.Values["ProdutoId"]);
                            //if (!usuarioProdutoBll.VerificaProdutoAssociadas(ProdutoId))
                            //{
                            CarregarDadosEdicao(ProdutoId);
                            Edicao(true);
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

        }

        protected void gvResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ldsFiltro_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                //List<bool> disponivel = new List<bool>();

                //if (ckbDisponivelComercioFiltro.Checked)
                //    disponivel.Add(true);

                //if (ckbIndisponivelComercioFiltro.Checked)
                //    disponivel.Add(false);

                //string codigoProduto = (!this.txtCodigoProdutoFiltro.Text.Equals(String.Empty)) ? this.txtCodigoProdutoFiltro.Text : String.Empty;

                //string descricao = (!this.txtDescricaoFiltro.Text.Equals(String.Empty)) ? this.txtDescricaoFiltro.Text : String.Empty;

                //int tipoProduto = 0;
                //if (this.ddlTipoProdutoFiltro.SelectedIndex > 0)
                //    tipoProduto = Convert.ToInt32(this.ddlTipoProdutoFiltro.SelectedValue);

                //int fabricante = 0;
                //if (this.ddlFabricanteFiltro.SelectedIndex > 0)
                //    fabricante = Convert.ToInt32(this.ddlFabricanteFiltro.SelectedValue);

                //int marca = 0;
                //if (this.ddlMarcaFiltro.SelectedIndex > 0)
                //    marca = Convert.ToInt32(this.ddlMarcaFiltro.SelectedValue);

                //int modelo = 0;
                //if (this.ddlModeloFiltro.SelectedIndex > 0)
                //    modelo = Convert.ToInt32(this.ddlModeloFiltro.SelectedValue);

                //int cor = 0;
                //if (this.ddlCorFiltro.SelectedIndex > 0)
                //    cor = Convert.ToInt32(this.ddlCorFiltro.SelectedValue);

                e.Result = estoqueProdutoBll.GetAllEstoqueProdutosGridFiltro();

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Edicao(false);
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

                    }
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

                txtValorVenda.Text = valorVenda.ToString();
            }
        }

        protected void txtValorVenda_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lkCloseVisualizarEstoqueProduto_Click(object sender, EventArgs e)
        {

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

        }

        protected void lkClose_Click(object sender, EventArgs e)
        {

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
            //txtDescricao.Text = string.Empty;
            //txtPeso.Text = string.Empty;
            //ckbDisponivel.Checked = true;
            ////chkAtivo.Checked = true;
            //hdnIdProduto.Value = "0";
            //hdnIdTipoProduto.Value = "0";
            //hdnIdFabricante.Value = "0";
            //hdnIdMarca.Value = "0";
            //hdnIdModelo.Value = "0";
            //hdnIdDimensoes.Value = "0";
            //hdnIdCor.Value = "0";

            //ddlTipoProduto.SelectedIndex = 0;
            //ddlFabricante.SelectedIndex = 0;
            //ddlMarca.SelectedIndex = 0;
            //ddlModelo.SelectedIndex = 0;
            //ddlDimensoes.SelectedIndex = 0;
            //ddlCor.SelectedIndex = 0;
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

        public void CarregarDadosVisualizacao(int idProduto)
        {

            //PRODUTO produto = produtoBll.GetProdutoById(idProduto);

            //if (produto != null)
            //{
            //    lbProduto.Text = produto.DESCRICAO;
            //    lblVisualizarCodigoProduto.Text = produto.CODIGOPRODUTO;
            //    lblVisualizarNomeProduto.Text = produto.DESCRICAO;
            //    lblVisualizarDimensoes.Text = produto.DIMENSOESPRODUTO.DESCRICAO;
            //    lblVisualizarDimensoes.ToolTip = produto.DIMENSOESPRODUTO.ALTURA + " x " + produto.DIMENSOESPRODUTO.LARGURA + " x " + produto.DIMENSOESPRODUTO.COMPRIMENTO;
            //    lblVisualizarDisponivelComercio.Text = produto.DISPONIVELCOMERCIO ? "Sim" : "Não";
            //    lblVisualizarFabricante.Text = produto.FABRICANTEPRODUTO.NOME;
            //    lblVisualizarMarca.Text = produto.MARCAPRODUTO.NOME;
            //    lblVisualizarModelo.Text = produto.MODELOPRODUTO.NOME;
            //    lblVisualizarPeso.Text = produto.PESO.ToString();
            //    lblVisualizarTipoProduto.Text = produto.TIPOPRODUTO.NOME;

            //    mpeVisualizarProduto.Show();
            //}
            //else
            //{
            //    MessageBoxError(this.Page, "Produto não localizada!");
            //}

        }

        public void CarregarDadosEdicao(int idProduto)
        {
            //PRODUTO produto = produtoBll.GetProdutoById(idProduto);

            //hdnIdProduto.Value = idProduto.ToString();

            //if (produto != null)
            //{
            //    ddlTipoProduto.SelectedValue = produto.TIPOPRODUTOID.ToString();
            //    ddlFabricante.SelectedValue = produto.FABRICANTEID.ToString();
            //    ddlMarca.SelectedValue = produto.MARCAID.ToString();
            //    ddlModelo.SelectedValue = produto.MODELOID.ToString();
            //    ddlDimensoes.SelectedValue = produto.DIMENSOESID.ToString();
            //    ddlCor.SelectedValue = produto.CORID.ToString();
            //    txtPeso.Text = produto.PESO.ToString();
            //    lblCodigoProduto.Text = produto.CODIGOPRODUTO.ToString();
            //    txtDescricao.Text = produto.DESCRICAO.ToString();
            //    ckbDisponivel.Checked = produto.DISPONIVELCOMERCIO;
            //}
            //else
            //{
            //    MessageBoxError(this.Page, "Produto não localizado!");
            //}
        }






        #endregion Métodos


    }
}