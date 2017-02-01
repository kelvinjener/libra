using Libra.Class;
using Libra.Communs;
using Libra.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libra.Entity;


namespace Libra.Produtos
{
    public partial class CadastroProdutos : BasePage
    {
        #region Atributos
        private ProdutoBll produtoBll = new ProdutoBll();
        //private UsuarioProdutoBll usuarioProdutoBll = new UsuarioProdutoBll();

        #endregion

        #region Properties
        private List<PRODUTO> produtosExclusao
        {
            get
            {
                if (Session["produtosExclusao"] != null)
                    return Session["produtosExclusao"] as List<PRODUTO>;
                else
                    return null;
            }
            set
            {
                Session["produtosExclusao"] = value;
            }
        }

        private List<PRODUTO> produtosExclusaoProibida
        {
            get
            {
                if (Session["produtosExclusaoProibida"] != null)
                    return Session["produtosExclusaoProibida"] as List<PRODUTO>;
                else
                    return null;
            }
            set
            {
                Session["produtosExclusaoProibida"] = value;
            }
        }

        public int HiddenIdTipoProduto
        {
            get { return string.IsNullOrEmpty(this.hdnIdTipoProduto.Value) ? 0 : Convert.ToInt32(this.hdnIdTipoProduto.Value); }
            set { this.hdnIdTipoProduto.Value = value.ToString(); }
        }

        public int HiddenIdFabricante
        {
            get { return string.IsNullOrEmpty(this.hdnIdFabricante.Value) ? 0 : Convert.ToInt32(this.hdnIdFabricante.Value); }
            set { this.hdnIdFabricante.Value = value.ToString(); }
        }

        public int HiddenIdMarca
        {
            get { return string.IsNullOrEmpty(this.hdnIdMarca.Value) ? 0 : Convert.ToInt32(this.hdnIdMarca.Value); }
            set { this.hdnIdMarca.Value = value.ToString(); }
        }

        public int HiddenIdModelo
        {
            get { return string.IsNullOrEmpty(this.hdnIdModelo.Value) ? 0 : Convert.ToInt32(this.hdnIdModelo.Value); }
            set { this.hdnIdModelo.Value = value.ToString(); }
        }

        public int HiddenIdDimensoes
        {
            get { return string.IsNullOrEmpty(this.hdnIdDimensoes.Value) ? 0 : Convert.ToInt32(this.hdnIdDimensoes.Value); }
            set { this.hdnIdDimensoes.Value = value.ToString(); }
        }

        public int HiddenIdCor
        {
            get { return string.IsNullOrEmpty(this.hdnIdCor.Value) ? 0 : Convert.ToInt32(this.hdnIdCor.Value); }
            set { this.hdnIdCor.Value = value.ToString(); }
        }

        #endregion

        #region Page Actions
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Edicao(false);
                CarregaTipoProduto();
                CarregaFabricante();
                CarregaMarca();
                CarregaModelo();
                CarregaDimensoes();
                CarregaCor();

                this.txtPeso.Attributes.Add("onkeypress", "return MascaraMoeda(this, '.', ',', event);");


            }
        }

        protected void gvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idProduto = Convert.ToInt32(this.gvResults.DataKeys[gvResults.SelectedIndex].Values["ProdutoId"]);
                CarregarDadosVisualizacao(idProduto);
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
                    Label lblDisponivelComercio = (Label)(e.Row.FindControl("lblDisponivelComercio"));
                    int ProdutoId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ProdutoId"));

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "DisponivelComercio")))
                        lblDisponivelComercio.Text = "Sim";
                    else
                        lblDisponivelComercio.Text = "Não";

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
                List<bool> disponivel = new List<bool>();

                if (ckbDisponivelComercioFiltro.Checked)
                    disponivel.Add(true);

                if (ckbIndisponivelComercioFiltro.Checked)
                    disponivel.Add(false);

                string codigoProduto = (!this.txtCodigoProdutoFiltro.Text.Equals(String.Empty)) ? this.txtCodigoProdutoFiltro.Text : String.Empty;

                string descricao = (!this.txtDescricaoFiltro.Text.Equals(String.Empty)) ? this.txtDescricaoFiltro.Text : String.Empty;

                int tipoProduto = 0;
                if (this.ddlTipoProdutoFiltro.SelectedIndex > 0)
                    tipoProduto = Convert.ToInt32(this.ddlTipoProdutoFiltro.SelectedValue);

                int fabricante = 0;
                if (this.ddlFabricanteFiltro.SelectedIndex > 0)
                    fabricante = Convert.ToInt32(this.ddlFabricanteFiltro.SelectedValue);

                int marca = 0;
                if (this.ddlMarcaFiltro.SelectedIndex > 0)
                    marca = Convert.ToInt32(this.ddlMarcaFiltro.SelectedValue);

                int modelo = 0;
                if (this.ddlModeloFiltro.SelectedIndex > 0)
                    modelo = Convert.ToInt32(this.ddlModeloFiltro.SelectedValue);

                int cor = 0;
                if (this.ddlCorFiltro.SelectedIndex > 0)
                    cor = Convert.ToInt32(this.ddlCorFiltro.SelectedValue);

                e.Result = produtoBll.GetAllProdutosGridFiltro(codigoProduto, descricao, disponivel, tipoProduto, fabricante, marca, modelo, cor);
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbAddProdutos_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            lblCodigoProduto.Text = "Gerado pelo sistema";
            lbAddEditProduto.Text = "Novo Produto";
            Edicao(true);
        }

        protected void lbEditProdutos_Click(object sender, EventArgs e)
        {
            try
            {
                LimpaCampos();
                lbAddEditProduto.Text = "Editar Produto";

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

        protected void lbFiltroProdutos_Click(object sender, EventArgs e)
        {
            mpeFiltroProdutos.Show();
        }

        protected void lkClose_Click(object sender, EventArgs e)
        {
            mpeFiltroProdutos.Hide();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            gvResults.DataBind();
        }

        protected void lkCloseVisualizarProduto_Click(object sender, EventArgs e)
        {
            mpeVisualizarProduto.Hide();
        }

        protected void lbVisualizarProduto_Click(object sender, EventArgs e)
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
                            int ProdutoId = Convert.ToInt32(keys.Values["ProdutoId"]);
                            CarregarDadosVisualizacao(ProdutoId);
                        }
                    }
                }
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
                int produtoId = Salvar();

                if (produtoId > 0)
                    this.MessageBoxSucesso(this.Page, "Produto salva com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar a produto! Verifique os campos informados.");

                gvResults.DataBind();

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Edicao(false);
            gvResults.DataBind();
        }

        protected void lbDelProdutos_Click(object sender, EventArgs e)
        {
            try
            {
                produtosExclusao = new List<PRODUTO>();
                produtosExclusaoProibida = new List<PRODUTO>();
                btnExcluirProduto.Visible = true;

                int i = 0;
                foreach (GridViewRow row in this.gvResults.Rows)
                {
                    if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                    {
                        i++;
                        break;
                    }

                }
                if (i == 0)
                {
                    this.MessageBoxAtencao(this.Page, "Selecione os itens para excluir.");
                }
                else
                {
                    foreach (GridViewRow row in this.gvResults.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        {
                            DataKey keys = this.gvResults.DataKeys[row.RowIndex];
                            int ProdutoId = Convert.ToInt32(keys.Values["ProdutoId"]);

                            PRODUTO produto = produtoBll.GetProdutoById(ProdutoId);

                            //if (usuarioProdutoBll.VerificaProdutoAssociadas(ProdutoId))
                            //    produtosExclusaoProibida.Add(produto);
                            //else
                            produtosExclusao.Add(produto);
                        }
                    }

                    divExclusaoProduto.Visible = false;
                    divExclusaoProdutoProibida.Visible = false;
                    imgExclusaoProduto.Visible = false;
                    imgExclusaoProdutoProibida.Visible = false;
                    divLinhaExclusaoProduto.Visible = false;
                    divAlgumitem.Visible = false;
                    divOsitem.Visible = false;

                    lblProdutosExclusao.Text = "<br/>";
                    lblProdutosExclusaoProibidas.Text = "<br/>";

                    if (produtosExclusao.Count > 0)
                    {
                        divExclusaoProduto.Visible = true;

                        if (produtosExclusaoProibida.Count == 0)
                            imgExclusaoProduto.Visible = true;

                        btnExcluirProduto.Enabled = true;

                        foreach (var item in produtosExclusao)
                        {
                            lblProdutosExclusao.Text = lblProdutosExclusao.Text + string.Format(" - {0}<br/>", item.DESCRICAO);
                        }
                    }
                    else
                    {
                        btnExcluirProduto.Enabled = false;
                    }

                    if (produtosExclusaoProibida.Count > 0)
                    {
                        divExclusaoProdutoProibida.Visible = true;
                        divAlgumitem.Visible = produtosExclusao.Count > 0;
                        divOsitem.Visible = !divAlgumitem.Visible;

                        if (!imgExclusaoProduto.Visible)
                        {
                            imgExclusaoProdutoProibida.Visible = true;
                            divLinhaExclusaoProduto.Visible = true;
                        }

                        foreach (var item in produtosExclusaoProibida)
                        {
                            lblProdutosExclusaoProibidas.Text = lblProdutosExclusaoProibidas.Text + string.Format(" - {0}<br/>", item.DESCRICAO);
                        }
                        if (produtosExclusao.Count == 0)
                            btnExcluirProduto.Visible = false;
                    }

                    mpeExclusaoProduto.Show();
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            foreach (var item in produtosExclusao)
            {
                produtoBll.Deletar(item);
            }

            mpeExclusaoProduto.Hide();
            gvResults.DataBind();

            if (produtosExclusao.Count > 0)
                this.MessageBoxSucesso(this.Page, "Registros excluídos com sucesso!");
        }

        protected void btnCancelarExclusaoProduto_Click(object sender, EventArgs e)
        {
            mpeExclusaoProduto.Hide();
        }

        #region Parametros

        protected void lbCloseCadastroTipoProduto_Click(object sender, EventArgs e)
        {

            mpeCadastroTipoProdutos.Hide();

        }

        protected void lbCloseCadastroFabricanteProduto_Click(object sender, EventArgs e)
        {
            mpeCadastroFabricanteProduto.Hide();
        }

        protected void lbCloseCadastroMarcaProduto_Click(object sender, EventArgs e)
        {
            mpeCadastroMarcaProduto.Hide();
        }

        protected void lbCloseCadastroModeloProduto_Click(object sender, EventArgs e)
        {
            mpeCadastroModeloProduto.Hide();
        }

        protected void lbCloseCadastroDimensoesProduto_Click(object sender, EventArgs e)
        {
            mpeCadastroDimensoesProduto.Hide();
        }

        protected void lbCloseCadastroCorProduto_Click(object sender, EventArgs e)
        {
            mpeCadastroCorProduto.Hide();
        }

        protected void lkAddTipoProduto_Click(object sender, EventArgs e)
        {
            mpeCadastroTipoProdutos.Show();
            cucCadastroTipoProduto.LimparCamposTiposProdutos();
            cucCadastroTipoProduto.chkAtivo.Enabled = false;
        }

        protected void lkAddFabricante_Click(object sender, EventArgs e)
        {
            mpeCadastroFabricanteProduto.Show();
            cucCadastroTipoProduto.LimparCamposTiposProdutos();
            cucCadastroTipoProduto.chkAtivo.Enabled = false;
        }

        protected void lkAddMarca_Click(object sender, EventArgs e)
        {
            mpeCadastroMarcaProduto.Show();
            cucCadastroMarca.LimparCamposMarcasProdutos();
            cucCadastroMarca.chkAtivo.Enabled = false;
        }

        protected void lkAddModelo_Click(object sender, EventArgs e)
        {
            mpeCadastroModeloProduto.Show();
            cucCadastroModelo.LimparCamposModelosProdutos();
            cucCadastroModelo.chkAtivo.Enabled = false;
        }

        protected void lkAddDimensoes_Click(object sender, EventArgs e)
        {
            mpeCadastroDimensoesProduto.Show();
            cucCadastroDimensoes.LimparCamposDimensoessProdutos();
            cucCadastroDimensoes.chkAtivo.Enabled = false;
        }

        protected void lkAddCor_Click(object sender, EventArgs e)
        {
            mpeCadastroCorProduto.Show();
            cucCadastroCor.LimparCamposCorsProdutos();
            cucCadastroCor.chkAtivo.Enabled = false;
        }

        protected void ddlTipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoProduto.SelectedIndex > 0)
            {
                var TipoProduto = new TipoProdutoBll().GetTipoProdutoById(Convert.ToInt32(this.ddlTipoProduto.SelectedValue));
                txtDescricao.Text = txtDescricao.Text + " " + TipoProduto.NOME.ToUpper();
            }
        }

        protected void ddlFabricante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFabricante.SelectedIndex > 0)
            {
                MARCAPRODUTO Marca = null;

                if (ddlMarca.SelectedIndex > 0)
                {
                    Marca = new MarcaProdutoBll().GetMarcaProdutoById(Convert.ToInt32(ddlMarca.SelectedValue));
                }

                var Fabricante = new FabricanteProdutoBll().GetFabricanteProdutoById(Convert.ToInt32(this.ddlFabricante.SelectedValue));
                if (Marca != null)
                {
                    if (Marca.NOME != Fabricante.NOME)
                        txtDescricao.Text = txtDescricao.Text + " " + Fabricante.NOME.ToUpper();
                }
                else
                    txtDescricao.Text = txtDescricao.Text + " " + Fabricante.NOME.ToUpper();

            }
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMarca.SelectedIndex > 0)
            {
                FABRICANTEPRODUTO Fabricante = null;
                if (ddlFabricante.SelectedIndex > 0)
                {
                    Fabricante = new FabricanteProdutoBll().GetFabricanteProdutoById(Convert.ToInt32(ddlFabricante.SelectedValue));
                }

                var MarcaProduto = new MarcaProdutoBll().GetMarcaProdutoById(Convert.ToInt32(this.ddlMarca.SelectedValue));
                if (Fabricante != null)
                {
                    if (MarcaProduto.NOME != Fabricante.NOME)
                        txtDescricao.Text = txtDescricao.Text + " " + MarcaProduto.NOME.ToUpper();
                }
                else
                    txtDescricao.Text = txtDescricao.Text + " " + MarcaProduto.NOME.ToUpper();

            }
        }

        protected void ddlModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModelo.SelectedIndex > 0)
            {
                var ModeloProduto = new ModeloProdutoBll().GetModeloProdutoById(Convert.ToInt32(this.ddlModelo.SelectedValue));
                txtDescricao.Text = txtDescricao.Text + " " + ModeloProduto.NOME.ToUpper();
            }
        }

        protected void ddlDimensoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDimensoes.SelectedIndex > 0)
            {
                var DimensoesProduto = new DimensoesProdutoBll().GetDimensoesProdutoById(Convert.ToInt32(this.ddlDimensoes.SelectedValue));
                txtDescricao.Text = txtDescricao.Text + " " + DimensoesProduto.DESCRICAO.ToUpper();
            }
        }

        protected void ddlCor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCor.SelectedIndex > 0)
            {
                var CorProduto = new CorProdutoBll().GetCorProdutoById(Convert.ToInt32(this.ddlCor.SelectedValue));
                txtDescricao.Text = txtDescricao.Text + " " + CorProduto.NOME.ToUpper();
            }
        }

        #endregion Parametros

        #endregion

        #region Métodos

        public void Edicao(bool edita)
        {
            divEdicao.Visible = edita;
            divFiltroETabela.Visible = !edita;
        }

        public void CarregarDadosVisualizacao(int idProduto)
        {

            PRODUTO produto = produtoBll.GetProdutoById(idProduto);

            if (produto != null)
            {
                lbProduto.Text = produto.DESCRICAO;
                lblVisualizarCodigoProduto.Text = produto.CODIGOPRODUTO;
                lblVisualizarNomeProduto.Text = produto.DESCRICAO;
                lblVisualizarDimensoes.Text = produto.DIMENSOESPRODUTO.DESCRICAO;
                lblVisualizarDimensoes.ToolTip = produto.DIMENSOESPRODUTO.ALTURA + " x " + produto.DIMENSOESPRODUTO.LARGURA + " x " + produto.DIMENSOESPRODUTO.COMPRIMENTO;
                lblVisualizarDisponivelComercio.Text = produto.DISPONIVELCOMERCIO ? "Sim" : "Não";
                lblVisualizarFabricante.Text = produto.FABRICANTEPRODUTO.NOME;
                lblVisualizarMarca.Text = produto.MARCAPRODUTO.NOME;
                lblVisualizarModelo.Text = produto.MODELOPRODUTO.NOME;
                lblVisualizarPeso.Text = produto.PESO.ToString();
                lblVisualizarTipoProduto.Text = produto.TIPOPRODUTO.NOME;

                mpeVisualizarProduto.Show();
            }
            else
            {
                MessageBoxError(this.Page, "Produto não localizada!");
            }

        }

        public void CarregarDadosEdicao(int idProduto)
        {
            PRODUTO produto = produtoBll.GetProdutoById(idProduto);

            hdnIdProduto.Value = idProduto.ToString();

            if (produto != null)
            {
                ddlTipoProduto.SelectedValue = produto.TIPOPRODUTOID.ToString();
                ddlFabricante.SelectedValue = produto.FABRICANTEID.ToString();
                ddlMarca.SelectedValue = produto.MARCAID.ToString();
                ddlModelo.SelectedValue = produto.MODELOID.ToString();
                ddlDimensoes.SelectedValue = produto.DIMENSOESID.ToString();
                ddlCor.SelectedValue = produto.CORID.ToString();
                txtPeso.Text = produto.PESO.ToString();
                lblCodigoProduto.Text = produto.CODIGOPRODUTO.ToString();
                txtDescricao.Text = produto.DESCRICAO.ToString();
                ckbDisponivel.Checked = produto.DISPONIVELCOMERCIO;
            }
            else
            {
                MessageBoxError(this.Page, "Produto não localizado!");
            }
        }

        public int Salvar()
        {
            try
            {
                PRODUTO produto;

                if (!String.IsNullOrEmpty(hdnIdProduto.Value) && Convert.ToInt32(hdnIdProduto.Value) > 0)
                    produto = produtoBll.GetProdutoById(Convert.ToInt32(hdnIdProduto.Value));
                else {
                    produto = new PRODUTO();

                    string tipoProdutoAux = ddlTipoProduto.SelectedValue.Length == 1 ? ("0" + ddlTipoProduto.SelectedValue) : ddlTipoProduto.SelectedValue;
                    string fabricanteAux = ddlFabricante.SelectedValue.Length == 1 ? ("0" + ddlFabricante.SelectedValue) : ddlFabricante.SelectedValue;
                    string marcaAux = ddlMarca.SelectedValue.Length == 1 ? ("0" + ddlMarca.SelectedValue) : ddlMarca.SelectedValue;
                    string modeloAux = ddlModelo.SelectedValue.Length == 1 ? ("0" + ddlModelo.SelectedValue) : ddlModelo.SelectedValue;
                    string dimensoesAux = ddlDimensoes.SelectedValue.Length == 1 ? ("0" + ddlDimensoes.SelectedValue) : ddlDimensoes.SelectedValue;
                    string corAux = ddlCor.SelectedValue.Length == 1 ? ("0" + ddlCor.SelectedValue) : ddlCor.SelectedValue;

                    produto.CODIGOPRODUTO = (tipoProdutoAux
                      + fabricanteAux
                      + marcaAux
                      + modeloAux
                      + dimensoesAux
                      + corAux
                      );

                    //TODO: Temporário
                    produto.CRIADOPOR = UsuarioInfo.IdUsuario;
                    produto.DATACRIACAO = DateTime.Now;
                }


                produto.TIPOPRODUTOID = Convert.ToInt32(ddlTipoProduto.SelectedValue);
                produto.FABRICANTEID = Convert.ToInt32(ddlFabricante.SelectedValue);
                produto.MARCAID = Convert.ToInt32(ddlMarca.SelectedValue);
                produto.MODELOID = Convert.ToInt32(ddlModelo.SelectedValue);
                produto.DIMENSOESID = Convert.ToInt32(ddlDimensoes.SelectedValue);
                produto.CORID = Convert.ToInt32(ddlCor.SelectedValue);
                produto.PESO = Convert.ToDecimal(ClearCaracter(txtPeso.Text, ".").Trim());
                produto.DESCRICAO = txtDescricao.Text;
                produto.DISPONIVELCOMERCIO = ckbDisponivel.Checked;

                produtoBll.Salvar(produto);

                if (produto.PRODUTOID > 0 && produto.CODIGOPRODUTO.Length == 12)
                {

                    var produtoId = produto.PRODUTOID.ToString().Length == 1 ? ("000" + produto.PRODUTOID.ToString()) :
                                    produto.PRODUTOID.ToString().Length == 2 ? ("00" + produto.PRODUTOID.ToString()) :
                                    produto.PRODUTOID.ToString().Length == 3 ? ("0" + produto.PRODUTOID.ToString()) :
                                    produto.PRODUTOID.ToString();

                    produto.CODIGOPRODUTO = produto.CODIGOPRODUTO + produtoId;
                    produtoBll.Salvar(produto);

                }

                CarregarDadosEdicao(produto.PRODUTOID);

                return produto.PRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }

        public void LimpaCampos()
        {
            txtDescricao.Text = string.Empty;
            txtPeso.Text = string.Empty;
            ckbDisponivel.Checked = true;
            //chkAtivo.Checked = true;
            hdnIdProduto.Value = "0";
            hdnIdTipoProduto.Value = "0";
            hdnIdFabricante.Value = "0";
            hdnIdMarca.Value = "0";
            hdnIdModelo.Value = "0";
            hdnIdDimensoes.Value = "0";
            hdnIdCor.Value = "0";

            ddlTipoProduto.SelectedIndex = 0;
            ddlFabricante.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;
            ddlModelo.SelectedIndex = 0;
            ddlDimensoes.SelectedIndex = 0;
            ddlCor.SelectedIndex = 0;
        }

        public void CarregaTipoProduto()
        {
            ddlTipoProduto.Items.Clear();
            ddlTipoProdutoFiltro.Items.Clear();
            ddlTipoProduto.Items.Add(new ListItem("Selecione...", ""));
            ddlTipoProdutoFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new TipoProdutoBll().GetAllAtivos())
                ddlTipoProduto.Items.Add(new ListItem(item.NOME, item.TIPOPRODUTOID.ToString()));

            foreach (var item in new TipoProdutoBll().GetAllTiposProdutos())
                ddlTipoProdutoFiltro.Items.Add(new ListItem(item.NOME, item.TIPOPRODUTOID.ToString()));
        }

        public void CarregaFabricante()
        {
            ddlFabricante.Items.Clear();
            ddlFabricanteFiltro.Items.Clear();

            ddlFabricante.Items.Add(new ListItem("Selecione...", ""));
            ddlFabricanteFiltro.Items.Add(new ListItem("Selecione...", ""));


            foreach (var item in new FabricanteProdutoBll().GetAllAtivos())
                ddlFabricante.Items.Add(new ListItem(item.NOME, item.FABRICANTEPRODUTOID.ToString()));

            foreach (var item in new FabricanteProdutoBll().GetAllFabricantesProdutos())
                ddlFabricanteFiltro.Items.Add(new ListItem(item.NOME, item.FABRICANTEPRODUTOID.ToString()));
        }

        public void CarregaMarca()
        {
            ddlMarca.Items.Clear();
            ddlMarcaFiltro.Items.Clear();
            ddlMarca.Items.Add(new ListItem("Selecione...", ""));
            ddlMarcaFiltro.Items.Add(new ListItem("Selecione...", ""));


            foreach (var item in new MarcaProdutoBll().GetAllAtivos())
                ddlMarca.Items.Add(new ListItem(item.NOME, item.MARCAPRODUTOID.ToString()));

            foreach (var item in new MarcaProdutoBll().GetAllMarcasProdutos())
                ddlMarcaFiltro.Items.Add(new ListItem(item.NOME, item.MARCAPRODUTOID.ToString()));
        }

        public void CarregaModelo()
        {
            ddlModelo.Items.Clear();
            ddlModeloFiltro.Items.Clear();
            ddlModelo.Items.Add(new ListItem("Selecione...", ""));
            ddlModeloFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new ModeloProdutoBll().GetAllAtivos())
                ddlModelo.Items.Add(new ListItem(item.NOME, item.MODELOPRODUTOID.ToString()));

            foreach (var item in new ModeloProdutoBll().GetAllModeloProdutos())
                ddlModeloFiltro.Items.Add(new ListItem(item.NOME, item.MODELOPRODUTOID.ToString()));
        }

        public void CarregaDimensoes()
        {
            ddlDimensoes.Items.Clear();
            ddlDimensoes.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new DimensoesProdutoBll().GetAllAtivos())
                ddlDimensoes.Items.Add(new ListItem(item.DESCRICAO, item.DIMENSOESPRODUTOID.ToString()));

        }

        public void CarregaCor()
        {
            ddlCor.Items.Clear();
            ddlCorFiltro.Items.Clear();
            ddlCor.Items.Add(new ListItem("Selecione...", ""));
            ddlCorFiltro.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in new CorProdutoBll().GetAllAtivos())
                ddlCor.Items.Add(new ListItem(item.NOME, item.CORPRODUTOID.ToString()));

            foreach (var item in new CorProdutoBll().GetAllCoresProdutos())
                ddlCorFiltro.Items.Add(new ListItem(item.NOME, item.CORPRODUTOID.ToString()));
        }

        public void AutoSelectTipoProduto(int idTipoProduto)
        {
            if (idTipoProduto > 0)
            {
                CarregaTipoProduto();
                ddlTipoProduto.SelectedValue = idTipoProduto.ToString();
                ddlTipoProduto_SelectedIndexChanged(this, null);
            }
        }

        public void AutoSelectFabricante(int idFabricante)
        {

            if (idFabricante > 0)
            {
                CarregaFabricante();
                ddlFabricante.SelectedValue = idFabricante.ToString();
                ddlFabricante_SelectedIndexChanged(this, null);
            }
        }

        public void AutoSelectMarca(int idMarca)
        {

            if (idMarca > 0)
            {
                CarregaMarca();
                ddlMarca.SelectedValue = idMarca.ToString();
                ddlMarca_SelectedIndexChanged(this, null);

            }
        }

        public void AutoSelectModelo(int idModelo)
        {

            if (idModelo > 0)
            {
                CarregaModelo();
                ddlModelo.SelectedValue = idModelo.ToString();
                ddlModelo_SelectedIndexChanged(this, null);
            }
        }

        public void AutoSelectDimensoes(int idDimensoes)
        {

            if (idDimensoes > 0)
            {
                CarregaDimensoes();
                ddlDimensoes.SelectedValue = idDimensoes.ToString();
                ddlDimensoes_SelectedIndexChanged(this, null);
            }
        }

        public void AutoSelectCor(int idCor)
        {

            if (idCor > 0)
            {
                CarregaCor();
                ddlCor.SelectedValue = idCor.ToString();
                ddlCor_SelectedIndexChanged(this, null);

            }
        }


        #endregion
    }
}