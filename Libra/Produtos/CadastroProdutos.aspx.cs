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

                this.txtPeso.Attributes.Add("onkeypress", "return FormatMaskOnlyNumbers(event, this, '#.###,##');");


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
                    Label lblAtivo = (Label)(e.Row.FindControl("lblAtivo"));
                    int ProdutoId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ProdutoId"));

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Ativo")))
                        lblAtivo.Text = "Sim";
                    else
                        lblAtivo.Text = "Não";

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
                List<bool> situacao = new List<bool>();

                if (ckbAtivoFiltro.Checked)
                    situacao.Add(true);

                if (ckbInativoFiltro.Checked)
                    situacao.Add(false);

                String txtProduto = String.Empty;

                string tipoProduto = string.Empty;
                if (this.ddlTipoProdutoFiltro.SelectedIndex > 0)
                    tipoProduto = this.ddlTipoProdutoFiltro.SelectedValue;

                e.Result = produtoBll.GetAllProdutosGridFiltro(txtProduto);
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbAddProdutos_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            lbAddEditProduto.Text = "Nova Produto";
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

            //PRODUTO produto = produtoBll.GetProdutoById(idProduto);

            //if (produto != null)
            //{
            //    lbProduto.Text = produto.APELIDO;
            //    lbNomeProduto.Text = produto.NOME;
            //    lbEnderecoProduto.Text = GetEnderecoProduto(produto);
            //    lbTelefones.Text = GetTelefonesProduto(produto);
            //    lbEmails.Text = GetEmailsProduto(produto);
            //    lbObservacao.Text = produto.OBSERVACAO == null ? "---" : produto.OBSERVACAO;
            //    lbAtiva.Text = produto.ATIVO ? "Sim" : "Não";
            //    lbTipoProduto.Text = Enum<TipoProduto>.Description((TipoProduto)Enum.Parse(typeof(TipoProduto), produto.TIPOPRODUTO.ToString()));

            //    mpeVisualizarProduto.Show();
            //}
            //else
            //{
            //    MessageBoxError(this.Page, "Produto não localizada!");
            //}

        }

        public void CarregarDadosEdicao(int idProduto)
        {
            PRODUTO produto = produtoBll.GetProdutoById(idProduto);

            hdnIdProduto.Value = idProduto.ToString();

            //if (produto != null)
            //{
            //    //TODO: Informar campos para Editar.
            //    txtNomeProduto.Text = produto.NOME;
            //    txtApelidoProduto.Text = produto.APELIDO;
            //    txtLogradouro.Text = produto.LOGRADOURO;
            //    txtNumero.Text = produto.NUMERO;
            //    txtComplemento.Text = produto.COMPLEMENTO;
            //    txtBairro.Text = produto.BAIRRO;
            //    txtCidade.Text = produto.CIDADE;
            //    if (!String.IsNullOrEmpty(produto.UF))
            //        ddlUF.SelectedValue = produto.UF;
            //    txtCEP.Text = FormatarCep(produto.CEP);
            //    txtTel1.Text = FormatarTelefone(produto.TEL1);
            //    txtTel2.Text = FormatarTelefone(produto.TEL2);
            //    txtFax.Text = FormatarTelefone(produto.FAX);
            //    txtEmail1.Text = produto.EMAIL1;
            //    txtEmail2.Text = produto.EMAIL2;
            //    txtObservacao.Text = produto.OBSERVACAO;
            //    ddlTipoProduto.SelectedValue = Convert.ToInt16(Enum.Parse(typeof(TipoProduto), produto.TIPOPRODUTO.ToString())).ToString();
            //    chkAtiva.Checked = produto.ATIVO;
            //}
            //else
            //{
            //    MessageBoxError(this.Page, "Produto não localizada!");
            //}
        }

        public int Salvar()
        {
            try
            {
                PRODUTO produto;

                if (!String.IsNullOrEmpty(hdnIdProduto.Value) && Convert.ToInt32(hdnIdProduto.Value) > 0)
                    produto = produtoBll.GetProdutoById(Convert.ToInt32(hdnIdProduto.Value));
                else
                    produto = new PRODUTO();

                //TODO: informa campos para SalvarTipoProduto.
                //produto.NOME = txtNomeProduto.Text;
                //produto.APELIDO = txtApelidoProduto.Text;
                //produto.LOGRADOURO = txtLogradouro.Text;
                //produto.NUMERO = txtNumero.Text;
                //produto.COMPLEMENTO = txtComplemento.Text;
                //produto.BAIRRO = txtBairro.Text;
                //produto.CIDADE = txtCidade.Text;
                //produto.UF = ddlUF.SelectedValue;
                //produto.CEP = this.ClearCaracter(txtCEP.Text, ".-");
                //produto.TEL1 = this.ClearCaracter(txtTel1.Text, ".-()");
                //produto.TEL2 = this.ClearCaracter(txtTel2.Text, ".-()");
                //produto.FAX = this.ClearCaracter(txtFax.Text, ".-()");
                //produto.EMAIL1 = txtEmail1.Text;
                //produto.EMAIL2 = txtEmail2.Text;
                //produto.OBSERVACAO = txtObservacao.Text;
                //produto.TIPOPRODUTO = (TipoProduto)Enum.Parse(typeof(TipoProduto), ddlTipoProduto.SelectedValue);
                //produto.ATIVO = chkAtiva.Checked;

                //produtoBll.Salvar(produto);

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
            //TODO: Informar campos para limpar.
            //txtNomeProduto.Text = string.Empty;
            //txtApelidoProduto.Text = string.Empty;
            //txtLogradouro.Text = string.Empty;
            //txtNumero.Text = string.Empty;
            //txtComplemento.Text = string.Empty;
            //txtBairro.Text = string.Empty;
            //txtCidade.Text = string.Empty;
            //ddlUF.SelectedIndex = 0;
            //txtCEP.Text = string.Empty;
            //txtTel1.Text = string.Empty;
            //txtTel2.Text = string.Empty;
            //txtFax.Text = string.Empty;
            //txtEmail1.Text = string.Empty;
            //txtEmail2.Text = string.Empty;
            //txtObservacao.Text = string.Empty;
            //ddlTipoProduto.SelectedIndex = 0;
            //chkAtiva.Checked = false;
            hdnIdProduto.Value = "0";
            hdnIdTipoProduto.Value = "0";
            hdnIdFabricante.Value = "0";
            hdnIdMarca.Value = "0";
            hdnIdModelo.Value = "0";
            hdnIdDimensoes.Value = "0";
            hdnIdCor.Value = "0";
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

            var TipoProduto = new TipoProdutoBll().GetTipoProdutoById(idTipoProduto);

            if (TipoProduto != null)
            {
                ddlTipoProduto.Items.Add(new ListItem(TipoProduto.NOME, TipoProduto.TIPOPRODUTOID.ToString()));
                ddlTipoProduto.DataBind();

                ddlTipoProduto.Items.FindByValue(TipoProduto.TIPOPRODUTOID.ToString()).Selected = true;
            }
        }

        public void AutoSelectFabricante(int idFabricante)
        {

            var Fabricante = new FabricanteProdutoBll().GetFabricanteProdutoById(idFabricante);

            if (Fabricante != null)
            {
                ddlFabricante.Items.Add(new ListItem(Fabricante.NOME, Fabricante.FABRICANTEPRODUTOID.ToString()));
                ddlFabricante.DataBind();

                ddlFabricante.Items.FindByValue(Fabricante.FABRICANTEPRODUTOID.ToString()).Selected = true;

            }
        }

        public void AutoSelectMarca(int idMarca)
        {

            var Marca = new MarcaProdutoBll().GetMarcaProdutoById(idMarca);

            if (Marca != null)
            {
                ddlMarca.Items.Add(new ListItem(Marca.NOME, Marca.MARCAPRODUTOID.ToString()));
                ddlMarca.DataBind();

                ddlMarca.Items.FindByValue(Marca.MARCAPRODUTOID.ToString()).Selected = true;

            }
        }

        public void AutoSelectModelo(int idModelo)
        {

            var Modelo = new ModeloProdutoBll().GetModeloProdutoById(idModelo);

            if (Modelo != null)
            {
                ddlModelo.Items.Add(new ListItem(Modelo.NOME, Modelo.MODELOPRODUTOID.ToString()));
                ddlModelo.DataBind();

                ddlModelo.Items.FindByValue(Modelo.MODELOPRODUTOID.ToString()).Selected = true;

            }
        }

        public void AutoSelectDimensoes(int idDimensoes)
        {

            var Dimensoes = new DimensoesProdutoBll().GetDimensoesProdutoById(idDimensoes);

            if (Dimensoes != null)
            {
                ddlDimensoes.Items.Add(new ListItem(Dimensoes.DESCRICAO, Dimensoes.DIMENSOESPRODUTOID.ToString()));
                ddlDimensoes.DataBind();

                ddlDimensoes.Items.FindByValue(Dimensoes.DIMENSOESPRODUTOID.ToString()).Selected = true;
            }
        }

        public void AutoSelectCor(int idCor)
        {

            var Cor = new CorProdutoBll().GetCorProdutoById(idCor);

            if (Cor != null)
            {
                ddlCor.Items.Add(new ListItem(Cor.NOME, Cor.CORPRODUTOID.ToString()));
                ddlCor.DataBind();

                ddlCor.Items.FindByValue(Cor.CORPRODUTOID.ToString()).Selected = true;
            }
        }

        #endregion


    }
}