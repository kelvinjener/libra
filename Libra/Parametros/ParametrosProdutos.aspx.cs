using Libra.Class;
using Libra.Control;
using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra.Parametros
{
    public partial class ParametrosProdutos : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region ## Tipo Produto ##
        #region Properties
        private List<TIPOPRODUTO> tipoProdutosExclusao
        {
            get
            {
                if (Session["tipoProdutosExclusao"] != null)
                    return Session["tipoProdutosExclusao"] as List<TIPOPRODUTO>;
                else
                    return null;
            }
            set
            {
                Session["tipoProdutosExclusao"] = value;
            }
        }

        private List<TIPOPRODUTO> tipoProdutosExclusaoProibida
        {
            get
            {
                if (Session["tipoProdutosExclusaoProibida"] != null)
                    return Session["tipoProdutosExclusaoProibida"] as List<TIPOPRODUTO>;
                else
                    return null;
            }
            set
            {
                Session["tipoProdutosExclusaoProibida"] = value;
            }
        }

        #endregion

        #region Attributes
        TipoProdutoBll tipoProdutoBll = new TipoProdutoBll();
        #endregion

        #region Page Actions
        protected void gvResultsTipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lbTituloModalEditAddTipoProduto.Text = "Editar Tipo de Produto";
                int TipoProdutoId = Convert.ToInt32(this.gvResultsTipoProduto.DataKeys[gvResultsTipoProduto.SelectedIndex].Values["TipoProdutoId"]);
                CarregaDadosTiposProdutos(TipoProdutoId);
                mpeTipoProduto.Show();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void gvResultsTipoProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblAtivo = (Label)(e.Row.FindControl("lblAtivo"));
                    int TipoProdutoId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TipoProdutoId"));

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Ativo")))
                        lblAtivo.Text = "Sim";
                    else
                        lblAtivo.Text = "Não";

                    if (tipoProdutoBll.VerificaProdutoTipoProdutoAssociados(TipoProdutoId))
                    {
                        TableCell tCell = e.Row.Cells[0];
                        tCell.Attributes["style"] = "border-left-color: #FFCC33;";
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void ldsFiltroTipoProduto_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                //List<bool> situacao = new List<bool>();

                //if (ckbAtivoFiltro.Checked)
                //    situacao.Add(true);

                //if (ckbInativoFiltro.Checked)
                //    situacao.Add(false);

                //String txtTipoProduto = (!this.txtTipoProdutoFiltro.Text.Equals(String.Empty)) ? this.txtTipoProdutoFiltro.Text : String.Empty;

                e.Result = tipoProdutoBll.GetAllTiposProdutosGrid();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbAddTipoProduto_Click(object sender, EventArgs e)
        {
            LimparCamposTiposProdutos();
            lbTituloModalEditAddTipoProduto.Text = "Adicionar novo Tipo de Produto";
            mpeTipoProduto.Show();
        }

        protected void lbEditTipoProduto_Click(object sender, EventArgs e)
        {

            try
            {
                lbTituloModalEditAddTipoProduto.Text = "Editar Tipo de Produto";

                int i = 0;
                foreach (GridViewRow row in this.gvResultsTipoProduto.Rows)
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
                    foreach (GridViewRow row in this.gvResultsTipoProduto.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        {
                            DataKey keys = this.gvResultsTipoProduto.DataKeys[row.RowIndex];
                            int TipoProdutoId = Convert.ToInt32(keys.Values["TipoProdutoId"]);
                            CarregaDadosTiposProdutos(TipoProdutoId);
                            mpeTipoProduto.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

        }

        protected void lbDelTipoProduto_Click(object sender, EventArgs e)
        {
            try
            {
                tipoProdutosExclusao = new List<TIPOPRODUTO>();
                tipoProdutosExclusaoProibida = new List<TIPOPRODUTO>();
                btnExcluirTipoProduto.Visible = true;

                int i = 0;
                foreach (GridViewRow row in this.gvResultsTipoProduto.Rows)
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
                    foreach (GridViewRow row in this.gvResultsTipoProduto.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        {
                            DataKey keys = this.gvResultsTipoProduto.DataKeys[row.RowIndex];
                            int TipoProdutoId = Convert.ToInt32(keys.Values["TipoProdutoId"]);

                            TIPOPRODUTO tp = tipoProdutoBll.GetTipoProdutoById(TipoProdutoId);

                            if (tipoProdutoBll.VerificaProdutoTipoProdutoAssociados(TipoProdutoId))
                                tipoProdutosExclusaoProibida.Add(tp);
                            else
                                tipoProdutosExclusao.Add(tp);
                        }
                    }

                    divExclusaoTipoProduto.Visible = false;
                    divExclusaoTipoProdutoProibida.Visible = false;
                    imgExclusaoTipoProduto.Visible = false;
                    imgExclusaoTipoProdutoProibida.Visible = false;
                    divLinhaExclusaoTipoProduto.Visible = false;
                    divAlgumitem.Visible = false;
                    divOsitem.Visible = false;

                    lblTipoProdutosExclusao.Text = "<br/>";
                    lblTipoProdutosExclusaoProibidas.Text = "<br/>";

                    if (tipoProdutosExclusao.Count > 0)
                    {
                        divExclusaoTipoProduto.Visible = true;

                        if (tipoProdutosExclusaoProibida.Count == 0)
                            imgExclusaoTipoProduto.Visible = true;

                        btnExcluirTipoProduto.Enabled = true;

                        foreach (var item in tipoProdutosExclusao)
                        {
                            lblTipoProdutosExclusao.Text = lblTipoProdutosExclusao.Text + string.Format(" - {0}<br/>", item.NOME);
                        }
                    }
                    else
                    {
                        btnExcluirTipoProduto.Enabled = false;
                    }

                    if (tipoProdutosExclusaoProibida.Count > 0)
                    {
                        divExclusaoTipoProdutoProibida.Visible = true;
                        divAlgumitem.Visible = tipoProdutosExclusao.Count > 0;
                        divOsitem.Visible = !divAlgumitem.Visible;

                        if (!imgExclusaoTipoProduto.Visible)
                        {
                            imgExclusaoTipoProdutoProibida.Visible = true;
                            divLinhaExclusaoTipoProduto.Visible = true;
                        }

                        foreach (var item in tipoProdutosExclusaoProibida)
                        {
                            lblTipoProdutosExclusaoProibidas.Text = lblTipoProdutosExclusaoProibidas.Text + string.Format(" - {0}<br/>", item.NOME);
                        }
                        if (tipoProdutosExclusao.Count == 0)
                            btnExcluirTipoProduto.Visible = false;
                    }

                    mpeExclusaoTipoProduto.Show();
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbCancelTipoProduto_Click(object sender, EventArgs e)
        {
            mpeTipoProduto.Hide();
            LimparCamposTiposProdutos();
        }

        protected void btnSalvarTipoProduto_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int tipoProdutoId = SalvarTipoProduto();

                if (tipoProdutoId > 0)
                    this.MessageBoxSucesso(this.Page, "Tipo de Produto salvo com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o Tipo de Produto! Verifique os campos informados.");

                gvResultsTipoProduto.DataBind();
            }
        }

        protected void lkCloseTipoProduto_Click(object sender, EventArgs e)
        {
            mpeTipoProduto.Hide();
            LimparCamposTiposProdutos();
        }

        protected void btnExcluirTipoProduto_Click(object sender, EventArgs e)
        {
            foreach (var item in tipoProdutosExclusao)
            {
                tipoProdutoBll.Deletar(item);
            }

            mpeExclusaoTipoProduto.Hide();
            gvResultsTipoProduto.DataBind();

            if (tipoProdutosExclusao.Count > 0)
                this.MessageBoxSucesso(this.Page, "Registros excluídos com sucesso!");
        }

        protected void btnCancelarExclusaoTipoProduto_Click(object sender, EventArgs e)
        {
            mpeExclusaoTipoProduto.Hide();
        }

        #endregion

        #region Methods

        public void LimparCamposTiposProdutos()
        {
            txtTipoProduto.Text = string.Empty;
            chkTipoProdutoAtivo.Checked = true;
            hdnTipoProdutoId.Value = string.Empty;
        }

        public void CarregaDadosTiposProdutos(int TipoProdutoId)
        {
            TIPOPRODUTO tp = tipoProdutoBll.GetTipoProdutoById(TipoProdutoId);

            hdnTipoProdutoId.Value = TipoProdutoId.ToString();

            if (tp != null)
            {
                //TODO: Informar campos para Editar.
                txtTipoProduto.Text = tp.NOME;
                chkTipoProdutoAtivo.Checked = tp.ATIVO;
            }
            else
            {
                MessageBoxError(this.Page, "Tipo de Produto não localizado!");
            }
        }

        public int SalvarTipoProduto()
        {
            try
            {
                TIPOPRODUTO tp;

                if (!String.IsNullOrEmpty(hdnTipoProdutoId.Value) && Convert.ToInt32(hdnTipoProdutoId.Value) > 0)
                    tp = tipoProdutoBll.GetTipoProdutoById(Convert.ToInt32(hdnTipoProdutoId.Value));
                else
                    tp = new TIPOPRODUTO();

                //TODO: informa campos para SalvarTipoProduto.
                tp.NOME = txtTipoProduto.Text;
                tp.ATIVO = chkTipoProdutoAtivo.Checked;

                tipoProdutoBll.Salvar(tp);

                return tp.TIPOPRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }

        #endregion

        #endregion ## Tipo Produto ##

        #region ## Fabricante ##
        #region Properties
        private List<FABRICANTEPRODUTO> fabricanteProdutosExclusao
        {
            get
            {
                if (Session["fabricanteProdutosExclusao"] != null)
                    return Session["fabricanteProdutosExclusao"] as List<FABRICANTEPRODUTO>;
                else
                    return null;
            }
            set
            {
                Session["fabricanteProdutosExclusao"] = value;
            }
        }

        private List<FABRICANTEPRODUTO> fabricanteProdutosExclusaoProibida
        {
            get
            {
                if (Session["fabricanteProdutosExclusaoProibida"] != null)
                    return Session["fabricanteProdutosExclusaoProibida"] as List<FABRICANTEPRODUTO>;
                else
                    return null;
            }
            set
            {
                Session["fabricanteProdutosExclusaoProibida"] = value;
            }
        }

        #endregion

        #region Attributes
        FabricanteProdutoBll fabricanteProdutoBll = new FabricanteProdutoBll();
        #endregion

        #region Page Actions
        protected void gvResultsFabricanteProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lbTituloModalEditAddFabricanteProduto.Text = "Editar Fabricante de Produto";
                int FabricanteProdutoId = Convert.ToInt32(this.gvResultsFabricanteProduto.DataKeys[gvResultsFabricanteProduto.SelectedIndex].Values["FabricanteProdutoId"]);
                CarregaDadosFabricantesProdutos(FabricanteProdutoId);
                mpeFabricanteProduto.Show();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void gvResultsFabricanteProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblAtivo = (Label)(e.Row.FindControl("lblAtivo"));
                    int FabricanteProdutoId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FabricanteProdutoId"));

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Ativo")))
                        lblAtivo.Text = "Sim";
                    else
                        lblAtivo.Text = "Não";

                    if (fabricanteProdutoBll.VerificaProdutoFabricanteProdutoAssociados(FabricanteProdutoId))
                    {
                        TableCell tCell = e.Row.Cells[0];
                        tCell.Attributes["style"] = "border-left-color: #FFCC33;";
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void ldsFiltroFabricanteProduto_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                //List<bool> situacao = new List<bool>();

                //if (ckbAtivoFiltro.Checked)
                //    situacao.Add(true);

                //if (ckbInativoFiltro.Checked)
                //    situacao.Add(false);

                //String txtFabricanteProduto = (!this.txtFabricanteProdutoFiltro.Text.Equals(String.Empty)) ? this.txtFabricanteProdutoFiltro.Text : String.Empty;

                e.Result = fabricanteProdutoBll.GetAllFabricantesProdutosGrid();
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbAddFabricanteProduto_Click(object sender, EventArgs e)
        {
            LimparCamposFabricantesProdutos();
            lbTituloModalEditAddFabricanteProduto.Text = "Adicionar novo Fabricante de Produto";
            mpeFabricanteProduto.Show();
        }

        protected void lbEditFabricanteProduto_Click(object sender, EventArgs e)
        {

            try
            {
                lbTituloModalEditAddFabricanteProduto.Text = "Editar Fabricante de Produto";

                int i = 0;
                foreach (GridViewRow row in this.gvResultsFabricanteProduto.Rows)
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
                    foreach (GridViewRow row in this.gvResultsFabricanteProduto.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        {
                            DataKey keys = this.gvResultsFabricanteProduto.DataKeys[row.RowIndex];
                            int FabricanteProdutoId = Convert.ToInt32(keys.Values["FabricanteProdutoId"]);
                            CarregaDadosFabricantesProdutos(FabricanteProdutoId);
                            mpeFabricanteProduto.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

        }

        protected void lbDelFabricanteProduto_Click(object sender, EventArgs e)
        {
            try
            {
                fabricanteProdutosExclusao = new List<FABRICANTEPRODUTO>();
                fabricanteProdutosExclusaoProibida = new List<FABRICANTEPRODUTO>();
                btnExcluirFabricanteProduto.Visible = true;

                int i = 0;
                foreach (GridViewRow row in this.gvResultsFabricanteProduto.Rows)
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
                    foreach (GridViewRow row in this.gvResultsFabricanteProduto.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkBxSelect")).Checked)
                        {
                            DataKey keys = this.gvResultsFabricanteProduto.DataKeys[row.RowIndex];
                            int FabricanteProdutoId = Convert.ToInt32(keys.Values["FabricanteProdutoId"]);

                            FABRICANTEPRODUTO fp = fabricanteProdutoBll.GetFabricanteProdutoById(FabricanteProdutoId);

                            if (fabricanteProdutoBll.VerificaProdutoFabricanteProdutoAssociados(FabricanteProdutoId))
                                fabricanteProdutosExclusaoProibida.Add(fp);
                            else
                                fabricanteProdutosExclusao.Add(fp);
                        }
                    }

                    divExclusaoFabricanteProduto.Visible = false;
                    divExclusaoFabricanteProdutoProibida.Visible = false;
                    imgExclusaoFabricanteProduto.Visible = false;
                    imgExclusaoFabricanteProdutoProibida.Visible = false;
                    divLinhaExclusaoFabricanteProduto.Visible = false;
                    divAlgumitem.Visible = false;
                    divOsitem.Visible = false;

                    lblFabricanteProdutosExclusao.Text = "<br/>";
                    lblFabricanteProdutosExclusaoProibidas.Text = "<br/>";

                    if (fabricanteProdutosExclusao.Count > 0)
                    {
                        divExclusaoFabricanteProduto.Visible = true;

                        if (fabricanteProdutosExclusaoProibida.Count == 0)
                            imgExclusaoFabricanteProduto.Visible = true;

                        btnExcluirFabricanteProduto.Enabled = true;

                        foreach (var item in fabricanteProdutosExclusao)
                        {
                            lblFabricanteProdutosExclusao.Text = lblFabricanteProdutosExclusao.Text + string.Format(" - {0}<br/>", item.NOME);
                        }
                    }
                    else
                    {
                        btnExcluirFabricanteProduto.Enabled = false;
                    }

                    if (fabricanteProdutosExclusaoProibida.Count > 0)
                    {
                        divExclusaoFabricanteProdutoProibida.Visible = true;
                        divAlgumitem.Visible = fabricanteProdutosExclusao.Count > 0;
                        divOsitem.Visible = !divAlgumitem.Visible;

                        if (!imgExclusaoFabricanteProduto.Visible)
                        {
                            imgExclusaoFabricanteProdutoProibida.Visible = true;
                            divLinhaExclusaoFabricanteProduto.Visible = true;
                        }

                        foreach (var item in fabricanteProdutosExclusaoProibida)
                        {
                            lblFabricanteProdutosExclusaoProibidas.Text = lblFabricanteProdutosExclusaoProibidas.Text + string.Format(" - {0}<br/>", item.NOME);
                        }
                        if (fabricanteProdutosExclusao.Count == 0)
                            btnExcluirFabricanteProduto.Visible = false;
                    }

                    mpeExclusaoFabricanteProduto.Show();
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbCancelFabricanteProduto_Click(object sender, EventArgs e)
        {
            mpeFabricanteProduto.Hide();
            LimparCamposFabricantesProdutos();
        }

        protected void btnSalvarFabricanteProduto_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int fabricanteProdutoId = SalvarFabricanteProduto();

                if (fabricanteProdutoId > 0)
                    this.MessageBoxSucesso(this.Page, "Fabricante de Produto salvo com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o Fabricante de Produto! Verifique os campos informados.");

                gvResultsFabricanteProduto.DataBind();
            }
        }

        protected void lkCloseFabricanteProduto_Click(object sender, EventArgs e)
        {
            mpeFabricanteProduto.Hide();
            LimparCamposFabricantesProdutos();
        }

        protected void btnExcluirFabricanteProduto_Click(object sender, EventArgs e)
        {
            foreach (var item in fabricanteProdutosExclusao)
            {
                fabricanteProdutoBll.Deletar(item);
            }

            mpeExclusaoFabricanteProduto.Hide();
            gvResultsFabricanteProduto.DataBind();

            if (fabricanteProdutosExclusao.Count > 0)
                this.MessageBoxSucesso(this.Page, "Registros excluídos com sucesso!");
        }

        protected void btnCancelarExclusaoFabricanteProduto_Click(object sender, EventArgs e)
        {
            mpeExclusaoFabricanteProduto.Hide();
        }

        #endregion

        #region Methods

        public void LimparCamposFabricantesProdutos()
        {
            txtFabricanteProduto.Text = string.Empty;
            chkFabricanteProdutoAtivo.Checked = true;
            hdnFabricanteProdutoId.Value = string.Empty;
        }

        public void CarregaDadosFabricantesProdutos(int FabricanteProdutoId)
        {
            FABRICANTEPRODUTO fp = fabricanteProdutoBll.GetFabricanteProdutoById(FabricanteProdutoId);

            hdnFabricanteProdutoId.Value = FabricanteProdutoId.ToString();

            if (fp != null)
            {
                //TODO: Informar campos para Editar.
                txtFabricanteProduto.Text = fp.NOME;
                chkFabricanteProdutoAtivo.Checked = fp.ATIVO;
            }
            else
            {
                MessageBoxError(this.Page, "Fabricante de Produto não localizado!");
            }
        }

        public int SalvarFabricanteProduto()
        {
            try
            {
                FABRICANTEPRODUTO fp;

                if (!String.IsNullOrEmpty(hdnFabricanteProdutoId.Value) && Convert.ToInt32(hdnFabricanteProdutoId.Value) > 0)
                    fp = fabricanteProdutoBll.GetFabricanteProdutoById(Convert.ToInt32(hdnFabricanteProdutoId.Value));
                else
                    fp = new FABRICANTEPRODUTO();

                //TODO: informa campos para SalvarFabricanteProduto.
                fp.NOME = txtFabricanteProduto.Text;
                fp.ATIVO = chkFabricanteProdutoAtivo.Checked;

                fabricanteProdutoBll.Salvar(fp);

                return fp.FABRICANTEPRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }

        #endregion

        #endregion ## Fabricante ##

        #region ## Marca ##
        #region Properties
        #endregion

        #region Attributes
        #endregion

        #region Page Actions
        #endregion

        #region Methods
        #endregion

        #endregion ## Marca ##

        #region ## Modelo ##
        #region Properties
        #endregion

        #region Attributes
        #endregion

        #region Page Actions
        #endregion

        #region Methods
        #endregion

        #endregion ## Modelo ##

        #region ## Dimensões ##
        #region Properties
        #endregion

        #region Attributes
        #endregion

        #region Page Actions
        #endregion

        #region Methods
        #endregion

        #endregion ## Dimensões ##

        #region ## Cores ##
        #region Properties
        #endregion

        #region Attributes
        #endregion

        #region Page Actions
        #endregion

        #region Methods
        #endregion

        #endregion ## Cores ##

        #region ## Características Diversas ##
        #region Properties
        #endregion

        #region Attributes
        #endregion

        #region Page Actions
        #endregion

        #region Methods
        #endregion

        #endregion ## Características Diversas ##


    }
}