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

namespace Libra.Seguranca
{
    public partial class Perfis : BasePage
    {
        #region Atributos
        private PerfilBll perfilBll = new PerfilBll();
        private UsuarioPerfilBll usuarioPerfilBll = new UsuarioPerfilBll();
        private FuncionalidadePerfilBll funcionalidadePerfilBll = new FuncionalidadePerfilBll();
        private FuncionalidadeBll funcionalidadeBll = new FuncionalidadeBll();

        #endregion

        #region Properties
        private List<PERFI> perfisExclusao
        {
            get
            {
                if (Session["perfisExclusao"] != null)
                    return Session["perfisExclusao"] as List<PERFI>;
                else
                    return null;
            }
            set
            {
                Session["perfisExclusao"] = value;
            }
        }

        private List<PERFI> perfisExclusaoProibida
        {
            get
            {
                if (Session["perfisExclusaoProibida"] != null)
                    return Session["perfisExclusaoProibida"] as List<PERFI>;
                else
                    return null;
            }
            set
            {
                Session["perfisExclusaoProibida"] = value;
            }
        }

        #endregion

        #region Page Actions
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Edicao(false);
            }
        }

        protected void gvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idPerfil = Convert.ToInt32(this.gvResults.DataKeys[gvResults.SelectedIndex].Values["PerfilId"]);
                CarregarDadosVisualizacao(idPerfil);
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
                    Label lblAtiva = (Label)(e.Row.FindControl("lblAtiva"));
                    Label lblSomenteLeitura = (Label)(e.Row.FindControl("lblSomenteLeitura"));
                    int PerfilId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PerfilId"));

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Ativo")))
                        lblAtiva.Text = "Sim";
                    else
                        lblAtiva.Text = "Não";

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "SomenteLeitura")))
                        lblSomenteLeitura.Text = "Sim";
                    else
                        lblSomenteLeitura.Text = "Não";

                    if (usuarioPerfilBll.VerificaPerfilAssociadas(PerfilId))
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

        protected void ldsFiltro_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                List<bool> situacao = new List<bool>();
                List<bool> somenteLeitura = new List<bool>();

                if (ckbAtivoFiltro.Checked)
                    situacao.Add(true);

                if (ckbInativoFiltro.Checked)
                    situacao.Add(false);

                if (ckbAtivoSomenteLeitura.Checked)
                    somenteLeitura.Add(true);

                if (ckbInativoSomenteLeitura.Checked)
                    somenteLeitura.Add(false);

                String txtPerfil = (!this.txtPerfilFiltro.Text.Equals(String.Empty)) ? this.txtPerfilFiltro.Text : String.Empty;


                e.Result = perfilBll.GetAllPerfisGridFiltro(situacao, txtPerfil, somenteLeitura);
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbAddPerfis_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            lbAddEditPerfil.Text = "Novo Perfil";
            Edicao(true);
            CarregarFucionalidades();
        }

        protected void lbEditPerfis_Click(object sender, EventArgs e)
        {
            try
            {
                LimpaCampos();
                lbAddEditPerfil.Text = "Editar Perfil";

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
                            int PerfilId = Convert.ToInt32(keys.Values["PerfilId"]);
                            //if (!usuarioPerfilBll.VerificaPerfilAssociadas(PerfilId))
                            //{
                            CarregarDadosEdicao(PerfilId);
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



        protected void lbFiltroPerfis_Click(object sender, EventArgs e)
        {
            mpeFiltroPerfis.Show();
        }

        protected void lkClose_Click(object sender, EventArgs e)
        {
            mpeFiltroPerfis.Hide();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            gvResults.DataBind();
        }

        protected void lkCloseVisualizarPerfil_Click(object sender, EventArgs e)
        {
            mpeVisualizarPerfil.Hide();
        }

        protected void lbVisualizarPerfil_Click(object sender, EventArgs e)
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
                            int PerfilId = Convert.ToInt32(keys.Values["PerfilId"]);
                            CarregarDadosVisualizacao(PerfilId);
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
                int perfilId = Salvar();

                if (perfilId > 0)
                    this.MessageBoxSucesso(this.Page, "Perfil salvo com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o perfil! Verifique os campos informados.");


                gvResults.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Edicao(false);
            gvResults.DataBind();
        }

        protected void lbDelPerfis_Click(object sender, EventArgs e)
        {
            try
            {
                perfisExclusao = new List<PERFI>();
                perfisExclusaoProibida = new List<PERFI>();
                btnExcluirPerfil.Visible = true;

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
                            int PerfilId = Convert.ToInt32(keys.Values["PerfilId"]);

                            PERFI perfil = perfilBll.GetPerfilById(PerfilId);

                            if (usuarioPerfilBll.VerificaPerfilAssociadas(PerfilId))
                                perfisExclusaoProibida.Add(perfil);
                            else
                                perfisExclusao.Add(perfil);
                        }
                    }

                    divExclusaoPerfil.Visible = false;
                    divExclusaoPerfilProibida.Visible = false;
                    imgExclusaoPerfil.Visible = false;
                    imgExclusaoPerfilProibida.Visible = false;
                    divLinhaExclusaoPerfil.Visible = false;
                    divAlgumitem.Visible = false;
                    divOsitem.Visible = false;

                    lblPerfisExclusao.Text = "<br/>";
                    lblPerfisExclusaoProibidas.Text = "<br/>";

                    if (perfisExclusao.Count > 0)
                    {
                        divExclusaoPerfil.Visible = true;

                        if (perfisExclusaoProibida.Count == 0)
                            imgExclusaoPerfil.Visible = true;

                        btnExcluirPerfil.Enabled = true;

                        foreach (var item in perfisExclusao)
                        {
                            lblPerfisExclusao.Text = lblPerfisExclusao.Text + string.Format(" - {0}<br/>", item.NOME);
                        }
                    }
                    else
                    {
                        btnExcluirPerfil.Enabled = false;
                    }

                    if (perfisExclusaoProibida.Count > 0)
                    {
                        divExclusaoPerfilProibida.Visible = true;
                        divAlgumitem.Visible = perfisExclusao.Count > 0;
                        divOsitem.Visible = !divAlgumitem.Visible;

                        if (!imgExclusaoPerfil.Visible)
                        {
                            imgExclusaoPerfilProibida.Visible = true;
                            divLinhaExclusaoPerfil.Visible = true;
                        }

                        foreach (var item in perfisExclusaoProibida)
                        {
                            lblPerfisExclusaoProibidas.Text = lblPerfisExclusaoProibidas.Text + string.Format(" - {0}<br/>", item.NOME);
                        }
                        if (perfisExclusao.Count == 0)
                            btnExcluirPerfil.Visible = false;
                    }

                    mpeExclusaoPerfil.Show();
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnExcluirPerfil_Click(object sender, EventArgs e)
        {
            foreach (var item in perfisExclusao)
            {
                perfilBll.Deletar(item);
            }

            mpeExclusaoPerfil.Hide();
            gvResults.DataBind();

            if (perfisExclusao.Count > 0)
                this.MessageBoxSucesso(this.Page, "Registros excluídos com sucesso!");
        }

        protected void btnCancelarExclusaoPerfil_Click(object sender, EventArgs e)
        {
            mpeExclusaoPerfil.Hide();
        }

        protected void btnMarcarTodosFuncionalidades_Click(object sender, EventArgs e)
        {
            btnMarcarTodosFuncionalidades.Visible = false;
            btnDesmarcarTodosFuncionalidades.Visible = true;
            foreach (ListItem i in cblFuncionalidades.Items)
                i.Selected = true;
        }

        protected void btnDesmarcarTodosFuncionalidades_Click(object sender, EventArgs e)
        {
            btnMarcarTodosFuncionalidades.Visible = true;
            btnDesmarcarTodosFuncionalidades.Visible = false;
            foreach (ListItem i in cblFuncionalidades.Items)
                i.Selected = false;
        }

        #endregion

        #region Métodos

        public void Edicao(bool edita)
        {
            divEdicao.Visible = edita;
            divFiltroETabela.Visible = !edita;
        }

        public void CarregarDadosVisualizacao(int idPerfil)
        {

            PERFI perfil = perfilBll.GetPerfilById(idPerfil);

            if (perfil != null)
            {
                lbPerfil.Text = perfil.NOME;
                lbNomePerfil.Text = perfil.NOME;
                lbAtiva.Text = perfil.ATIVO ? "Sim" : "Não";
                lbSomenteLeitura.Text = (bool)perfil.SOMENTELEITURA ? "Sim" : "Não";
                lbFuncionalidades.Text = GetFuncionalidadesPerfil(idPerfil);
                mpeVisualizarPerfil.Show();
            }
            else
            {
                MessageBoxError(this.Page, "Perfil não localizada!");
            }

        }

        public string GetFuncionalidadesPerfil(int idPerfil)
        {
            string funcionalidadesPerfil = string.Empty;
            int contRetorno = 0;
            int contLista = 0;

            foreach (var item in funcionalidadeBll.GetFuncionalidadesByIdPerfis(idPerfil))
            {
                if (contRetorno < 4)
                {
                    if (funcionalidadesPerfil == string.Empty)
                        funcionalidadesPerfil = funcionalidadeBll.GetFuncionalidadeById(item.MENUPAIID).NOME + " > " + item.NOME;
                    else
                        funcionalidadesPerfil += " | " + funcionalidadeBll.GetFuncionalidadeById(item.MENUPAIID).NOME + " > " + item.NOME;

                    contLista++;
                }
                contRetorno++;
            }

            if (!string.IsNullOrEmpty(funcionalidadesPerfil) && contLista == 4)
                funcionalidadesPerfil += "...";


            if (funcionalidadesPerfil == string.Empty)
                funcionalidadesPerfil = "Não existem Funcionalidades associada a este perfil.";

            return funcionalidadesPerfil;
        }

        public void CarregarDadosEdicao(int idPerfil)
        {
            PERFI perfil = perfilBll.GetPerfilById(idPerfil);

            hdnIdPerfil.Value = idPerfil.ToString();

            if (perfil != null)
            {
                CarregarFucionalidades();

                //TODO: Informar campos para Editar.
                txtNomePerfil.Text = perfil.NOME;
                chkAtiva.Checked = perfil.ATIVO;
                chkSomenteLeitura.Checked = (bool)perfil.SOMENTELEITURA;

                foreach (var item in funcionalidadePerfilBll.GetListFuncionalidadePerfilByPerfilId(perfil.PERFILID))
                {
                    if (cblFuncionalidades.Items.FindByValue(item.FUNCIONALIDADEID.ToString()) != null)
                    {
                        cblFuncionalidades.Items.FindByValue(item.FUNCIONALIDADEID.ToString()).Selected = true;
                    }
                    else
                    {
                        FUNCIONALIDADE f = funcionalidadeBll.GetFuncionalidadeById(Convert.ToInt32(item.FUNCIONALIDADEID));
                        cblFuncionalidades.Items.Add(new ListItem(f.NOME, f.FUNCIONALIDADEID.ToString()));
                        cblFuncionalidades.Items.FindByValue(item.FUNCIONALIDADEID.ToString()).Selected = true;
                    }

                }
            }
            else
            {
                MessageBoxError(this.Page, "Perfil não localizada!");
            }
        }

        public int Salvar()
        {
            try
            {
                PERFI perfil;

                if (!String.IsNullOrEmpty(hdnIdPerfil.Value) && Convert.ToInt32(hdnIdPerfil.Value) > 0)
                    perfil = perfilBll.GetPerfilById(Convert.ToInt32(hdnIdPerfil.Value));
                else {
                    perfil = new PERFI();
                    perfil.DATACADASTRO = DateTime.Now;
                }

                //TODO: informa campos para SalvarTipoProduto.
                perfil.NOME = txtNomePerfil.Text;
                perfil.ATIVO = chkAtiva.Checked;
                perfil.SOMENTELEITURA = chkSomenteLeitura.Checked;

                perfilBll.Salvar(perfil);

                //TODO: Campos Funcionalidades
                if (perfil.PERFILID > 0)
                {
                    funcionalidadePerfilBll.DeletarTodosByPerfilId(perfil.PERFILID);
                    foreach (ListItem item in cblFuncionalidades.Items)
                    {
                        if (item.Selected)
                        {
                            FUNCIONALIDADEPERFI fp = new FUNCIONALIDADEPERFI();
                            fp.PERFILID = perfil.PERFILID;
                            fp.FUNCIONALIDADEID = Convert.ToInt32(item.Value);

                            funcionalidadePerfilBll.Salvar(fp);
                        }
                    }
                }

                return perfil.PERFILID;

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
            txtNomePerfil.Text = string.Empty;
            chkAtiva.Checked = false;
            chkSomenteLeitura.Checked = false;
            hdnIdPerfil.Value = "0";

            cblFuncionalidades.Items.Clear();
            btnMarcarTodosFuncionalidades.Visible = true;
            btnDesmarcarTodosFuncionalidades.Visible = false;
        }

        public void CarregarFucionalidades()
        {
            cblFuncionalidades.ClearSelection();
            cblFuncionalidades.Items.Clear();

            List<FuncionalidadesLista> funionalidades = new FuncionalidadeBll().GetAllListaAtivos();
            cblFuncionalidades.Items.Add(new ListItem());

            foreach (var funionalidade in funionalidades)
                cblFuncionalidades.Items.Add(new ListItem(funionalidade.Nome, funionalidade.FuncionalidadeId.ToString()));

            cblFuncionalidades.DataBind();
        }
        #endregion
    }
}