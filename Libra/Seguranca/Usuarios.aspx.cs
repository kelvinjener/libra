using Libra.Class;
using Libra.Communs;
using Libra.Communs.Enumerators;
using Libra.Control;
using Libra.Entity;
using Libra.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra.Seguranca
{
    public partial class Usuarios : BasePage
    {
        #region Atributos
        private UsuarioBll usuarioBll = new UsuarioBll();
        private AspNetUserBll aspNetUserBll = new AspNetUserBll();
        private LogBll logBll = new LogBll();
        private UnidadeBll unidadeBll = new UnidadeBll();
        private PerfilBll perfilBll = new PerfilBll();
        private UsuarioPerfilBll usuarioPerfilBll = new UsuarioPerfilBll();
        private UsuarioUnidadeBll usuarioUnidadeBll = new UsuarioUnidadeBll();

        #endregion

        #region Properties
        private List<USUARIO> usuariosExclusao
        {
            get
            {
                if (Session["usuariosExclusao"] != null)
                    return Session["usuariosExclusao"] as List<USUARIO>;
                else
                    return null;
            }
            set
            {
                Session["usuariosExclusao"] = value;
            }
        }

        private List<USUARIO> usuariosExclusaoProibida
        {
            get
            {
                if (Session["usuariosExclusaoProibida"] != null)
                    return Session["usuariosExclusaoProibida"] as List<USUARIO>;
                else
                    return null;
            }
            set
            {
                Session["usuariosExclusaoProibida"] = value;
            }
        }

        #endregion

        #region Page Actions
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaSexo();
                CarregarUnidadesFiltro();
                CarregarPerfisFiltro();
                Edicao(false);

                this.txtTelefone.Attributes.Add("onkeypress", "return formatarTelefone(event, this);");
                this.txtCPFFiltro.Attributes.Add("onkeypress", "return FormatMaskOnlyNumbers(event, this, '###.###.###-##');");
                this.txtCPF.Attributes.Add("onkeypress", "return FormatMaskOnlyNumbers(event, this, '###.###.###-##');");
                this.txtDataNascimento.Attributes.Add("onkeypress", "return FormatMaskOnlyNumbers(event, this, '##/##/####');");


            }
        }

        protected void gvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = Convert.ToInt32(this.gvResults.DataKeys[gvResults.SelectedIndex].Values["UsuarioId"]);
                CarregarDadosVisualizacao(idUsuario);
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
                    int UsuarioId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UsuarioId"));

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Ativo")))
                        lblAtiva.Text = "Sim";
                    else
                        lblAtiva.Text = "Não";

                    Label lblCPFUsuario = (Label)e.Row.FindControl("lblCPFUsuario");
                    lblCPFUsuario.Text = this.FormatarCpf(DataBinder.Eval(e.Row.DataItem, "CPF").ToString());

                    Label lblTelefoneUsuario = (Label)e.Row.FindControl("lblTelefoneUsuario");
                    lblTelefoneUsuario.Text = this.FormatarTelefone(DataBinder.Eval(e.Row.DataItem, "Telefone").ToString());

                    if (this.VerificaUsuarioAssociado(UsuarioId))
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

                if (ckbAtivoFiltro.Checked)
                    situacao.Add(true);

                if (ckbInativoFiltro.Checked)
                    situacao.Add(false);

                List<int> idsPerfis = new List<int>();
                foreach (ListItem item in cblPerfisFiltro.Items)
                    if (item.Selected)
                        idsPerfis.Add(Convert.ToInt32(item.Value));

                List<int> idsUnidades = new List<int>();
                foreach (ListItem item in cblUnidadesFiltro.Items)
                    if (item.Selected)
                        idsUnidades.Add(Convert.ToInt32(item.Value));

                String txtUsuario = (!this.txtUsuarioFiltro.Text.Equals(String.Empty)) ? this.txtUsuarioFiltro.Text : String.Empty;
                String txtEmail = (!this.txtEmailFiltro.Text.Equals(String.Empty)) ? this.txtEmailFiltro.Text : String.Empty;
                String txtCPF = (!this.txtCPFFiltro.Text.Equals(String.Empty)) ? this.txtCPFFiltro.Text : String.Empty;


                e.Result = usuarioBll.GetAllUsuariosGridFiltro(situacao, txtUsuario, txtEmail, txtCPF, idsUnidades, idsPerfis);
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbAddUsuarios_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            CarregarUnidades();
            CarregarPerfis();
            lbAddEditUsuario.Text = "Novo Usuario";
            Edicao(true);
        }

        protected void lbEditUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                LimpaCampos();
                lbAddEditUsuario.Text = "Editar Usuario";

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
                            int UsuarioId = Convert.ToInt32(keys.Values["UsuarioId"]);
                            //if (!usuarioUsuarioBll.VerificaUsuarioAssociadas(UsuarioId))
                            //{
                            CarregarDadosEdicao(UsuarioId);
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

        protected void lbFiltroUsuarios_Click(object sender, EventArgs e)
        {
            mpeFiltroUsuarios.Show();
        }

        protected void lkClose_Click(object sender, EventArgs e)
        {
            mpeFiltroUsuarios.Hide();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            gvResults.DataBind();
        }

        protected void lkCloseVisualizarUsuario_Click(object sender, EventArgs e)
        {
            mpeVisualizarUsuario.Hide();
        }

        protected void lbVisualizarUsuario_Click(object sender, EventArgs e)
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
                            int UsuarioId = Convert.ToInt32(keys.Values["UsuarioId"]);
                            CarregarDadosVisualizacao(UsuarioId);
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
                int usuarioId = 0;

                if (!String.IsNullOrEmpty(hdnIdUsuario.Value) && Convert.ToInt32(hdnIdUsuario.Value) == 0)
                {
                    if ((usuarioBll.GetUsuarioByCPF(txtCPF.Text) == null && aspNetUserBll.GetANUsuerByUserName(txtEmail.Text) == null) ||
                        (aspNetUserBll.GetANUsuerByUserName(txtEmail.Text) != null && usuarioBll.GetUsuarioByCPF(txtCPF.Text) == null))
                    {
                        usuarioId = Salvar();
                    }
                    else
                    {
                        this.MessageBoxAtencao(this.Page, "Já existe usuário com o CPF e/ou E-mail informado! Verifique os dados informados.");
                        return;
                    }
                }
                else
                    usuarioId = Salvar();

                if (usuarioId > 0)
                {
                    this.MessageBoxSucesso(this.Page, "Usuário salvo com sucesso!");
                    hdnIdUsuario.Value = usuarioId.ToString();
                }
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o usuário! Verifique os campos informados. Caso o erro persista, informe o administrador do sistema.");

                gvResults.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Edicao(false);
            gvResults.DataBind();
        }

        protected void lbDelUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                usuariosExclusao = new List<USUARIO>();
                usuariosExclusaoProibida = new List<USUARIO>();
                btnExcluirUsuario.Visible = true;

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
                            int UsuarioId = Convert.ToInt32(keys.Values["UsuarioId"]);

                            USUARIO usuario = usuarioBll.GetUsuarioById(UsuarioId);

                            if (this.VerificaUsuarioAssociado(UsuarioId))
                                usuariosExclusaoProibida.Add(usuario);
                            else
                                usuariosExclusao.Add(usuario);
                        }
                    }

                    divExclusaoUsuario.Visible = false;
                    divExclusaoUsuarioProibida.Visible = false;
                    imgExclusaoUsuario.Visible = false;
                    imgExclusaoUsuarioProibida.Visible = false;
                    divLinhaExclusaoUsuario.Visible = false;
                    divAlgumitem.Visible = false;
                    divOsitem.Visible = false;

                    lblUsuariosExclusao.Text = "<br/>";
                    lblUsuariosExclusaoProibidas.Text = "<br/>";

                    if (usuariosExclusao.Count > 0)
                    {
                        divExclusaoUsuario.Visible = true;

                        if (usuariosExclusaoProibida.Count == 0)
                            imgExclusaoUsuario.Visible = true;

                        btnExcluirUsuario.Enabled = true;

                        foreach (var item in usuariosExclusao)
                        {
                            lblUsuariosExclusao.Text = lblUsuariosExclusao.Text + string.Format(" - {0}<br/>", item.NOME);
                        }
                    }
                    else
                    {
                        btnExcluirUsuario.Enabled = false;
                    }

                    if (usuariosExclusaoProibida.Count > 0)
                    {
                        divExclusaoUsuarioProibida.Visible = true;
                        divAlgumitem.Visible = usuariosExclusao.Count > 0;
                        divOsitem.Visible = !divAlgumitem.Visible;

                        if (!imgExclusaoUsuario.Visible)
                        {
                            imgExclusaoUsuarioProibida.Visible = true;
                            divLinhaExclusaoUsuario.Visible = true;
                        }

                        foreach (var item in usuariosExclusaoProibida)
                        {
                            lblUsuariosExclusaoProibidas.Text = lblUsuariosExclusaoProibidas.Text + string.Format(" - {0}<br/>", item.NOME);
                        }
                        if (usuariosExclusao.Count == 0)
                            btnExcluirUsuario.Visible = false;
                    }

                    mpeExclusaoUsuario.Show();
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnExcluirUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in usuariosExclusao)
                {
                    string ASPNETUSERID = item.ASPNETUSERID;

                    usuarioPerfilBll.DeletarTodosByUsuarioId(item.USUARIOID);
                    usuarioUnidadeBll.DeletarTodosByUsuarioId(item.USUARIOID);

                    usuarioBll.Deletar(item);
                    AspNetUser anu = aspNetUserBll.GetUNUsuerById(ASPNETUSERID);
                    if (anu != null)
                        aspNetUserBll.Deletar(anu);
                }

                mpeExclusaoUsuario.Hide();
                gvResults.DataBind();

                if (usuariosExclusao.Count > 0)
                    this.MessageBoxSucesso(this.Page, "Registros excluídos com sucesso!");
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnCancelarExclusaoUsuario_Click(object sender, EventArgs e)
        {
            mpeExclusaoUsuario.Hide();
        }

        protected void cvUnidades_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                bool isValid = false;

                foreach (ListItem item in this.cblUnidades.Items)
                    if (item.Selected)
                    {
                        isValid = true;
                        break;
                    }

                args.IsValid = isValid;
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void cvPerfis_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                bool isValid = false;

                foreach (ListItem item in this.cblPerfis.Items)
                    if (item.Selected)
                    {
                        isValid = true;
                        break;
                    }

                args.IsValid = isValid;
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnMarcarTodosUnidades_Click(object sender, EventArgs e)
        {
            btnMarcarTodosUnidades.Visible = false;
            btnDesmarcarTodosUnidades.Visible = true;
            foreach (ListItem i in cblUnidadesFiltro.Items)
                i.Selected = true;

            mpeFiltroUsuarios.Show();
        }

        protected void btnDesmarcarTodosUnidades_Click(object sender, EventArgs e)
        {
            btnMarcarTodosUnidades.Visible = true;
            btnDesmarcarTodosUnidades.Visible = false;
            foreach (ListItem i in cblUnidadesFiltro.Items)
                i.Selected = false;

            mpeFiltroUsuarios.Show();
        }

        protected void btnMarcarTodosPerfis_Click(object sender, EventArgs e)
        {
            btnMarcarTodosPerfis.Visible = false;
            btnDesmarcarTodosPerfis.Visible = true;
            foreach (ListItem i in cblPerfisFiltro.Items)
                i.Selected = true;

            mpeFiltroUsuarios.Show();
        }

        protected void btnDesmarcarTodosPerfis_Click(object sender, EventArgs e)
        {
            btnMarcarTodosPerfis.Visible = true;
            btnDesmarcarTodosPerfis.Visible = false;
            foreach (ListItem i in cblPerfisFiltro.Items)
                i.Selected = false;

            mpeFiltroUsuarios.Show();
        }
        #endregion

        #region Métodos

        public void Edicao(bool edita)
        {

            divEdicao.Visible = edita;
            divFiltroETabela.Visible = !edita;
        }

        public void CarregaSexo()
        {
            ddlSexo.Items.Clear();
            ddlSexo.Items.Add(new ListItem("Selecione...", ""));

            foreach (string item in Enum.GetNames(typeof(SexoEnum)))
            {
                short value = Convert.ToInt16(Enum.Parse(typeof(SexoEnum), item));
                string text = Enum<SexoEnum>.Description((SexoEnum)Enum.Parse(typeof(SexoEnum), item));

                ddlSexo.Items.Add(new ListItem(text, value.ToString()));
            }

        }

        public void CarregarUnidadesFiltro()
        {
            cblUnidadesFiltro.ClearSelection();
            cblUnidadesFiltro.Items.Clear();

            List<UNIDADE> unidades = new UnidadeBll().GetAllUnidades();
            foreach (var unidade in unidades)
                cblUnidadesFiltro.Items.Add(new ListItem(unidade.APELIDO, unidade.UNIDADEID.ToString()));

            cblUnidadesFiltro.DataBind();
        }

        public void CarregarPerfisFiltro()
        {
            cblPerfisFiltro.ClearSelection();
            cblPerfisFiltro.Items.Clear();

            List<PERFI> perfis = new PerfilBll().GetAllPerfis();
            foreach (var perfil in perfis)
                cblPerfisFiltro.Items.Add(new ListItem(perfil.NOME, perfil.PERFILID.ToString()));

            cblPerfisFiltro.DataBind();
        }

        public void CarregarUnidades()
        {
            cblUnidades.ClearSelection();
            cblUnidades.Items.Clear();

            List<UNIDADE> unidades = new UnidadeBll().GetAllUnidadeAtivas();
            cblUnidades.Items.Add(new ListItem());

            foreach (var unidade in unidades)
                cblUnidades.Items.Add(new ListItem(unidade.APELIDO, unidade.UNIDADEID.ToString()));

            cblUnidades.DataBind();
        }

        public void CarregarPerfis()
        {
            cblPerfis.ClearSelection();
            cblPerfis.Items.Clear();

            List<PERFI> perfis = new PerfilBll().GetAllPerfilAtivos();
            foreach (var perfil in perfis)
                cblPerfis.Items.Add(new ListItem(perfil.NOME, perfil.PERFILID.ToString()));

            cblPerfis.DataBind();
        }

        public void CarregarDadosVisualizacao(int idUsuario)
        {
            try
            {
                USUARIO usuario = usuarioBll.GetUsuarioById(idUsuario);

                if (usuario != null)
                {
                    lbUsuario.Text = usuario.NOME;
                    lbNomeUsuario.Text = usuario.NOME;
                    lbCPFUsuario.Text = !string.IsNullOrEmpty(usuario.CPF) ? this.FormatarCpf(usuario.CPF) : "---";
                    lbSexo.Text = Enum<SexoEnum>.Description((SexoEnum)Enum.Parse(typeof(SexoEnum), usuario.SEXO.ToString()));
                    lbDataNascimento.Text = !string.IsNullOrEmpty(usuario.DATANASCIMENTO.ToString()) ? Convert.ToDateTime(usuario.DATANASCIMENTO).ToShortDateString() : "---";
                    lbDataCadastro.Text = usuario.DATACADASTRO.ToShortDateString();
                    lbTelefone.Text = !string.IsNullOrEmpty(usuario.TELEFONE) ? this.FormatarTelefone(usuario.TELEFONE) : "---";
                    lbEmail.Text = !string.IsNullOrEmpty(usuario.AspNetUser.Email) ? usuario.AspNetUser.Email : "---";
                    lbAtiva.Text = usuario.ATIVO ? "Sim" : "Não";
                    lbUnidades.Text = GetUsuarioUnidades(idUsuario);
                    lbPerfis.Text = GetUsuarioPerfis(idUsuario);

                    mpeVisualizarUsuario.Show();
                }
                else
                {
                    MessageBoxError(this.Page, "Usuario não localizada!");
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

        }

        public string GetUsuarioUnidades(int idUsuario)
        {
            string usuarioUnidades = string.Empty;

            foreach (var item in unidadeBll.GetUnidadesByIdUsuario(idUsuario))
            {
                if (usuarioUnidades == string.Empty)
                    usuarioUnidades = item.APELIDO;
                else
                    usuarioUnidades += " / " + item.APELIDO;
            }

            if (usuarioUnidades == string.Empty)
                usuarioUnidades = "Não existem Unidades associada a este usuário.";

            return usuarioUnidades;
        }

        public string GetUsuarioPerfis(int idUsuario)
        {
            string usuarioPerfis = string.Empty;

            foreach (var item in perfilBll.GetPerfisByIdUsuario(idUsuario))
            {
                if (usuarioPerfis == string.Empty)
                    usuarioPerfis = item.NOME;
                else
                    usuarioPerfis += " / " + item.NOME;
            }

            if (usuarioPerfis == string.Empty)
                usuarioPerfis = "Não existem Perfis associado a este usuário.";

            return usuarioPerfis;
        }

        public void CarregarDadosEdicao(int idUsuario)
        {
            USUARIO usuario = usuarioBll.GetUsuarioById(idUsuario);

            if (usuario != null)
            {
                CarregarUnidades();
                CarregarPerfis();

                hdnIdUsuario.Value = idUsuario.ToString();

                //TODO: Informar campos para Editar.
                txtNomeUsuario.Text = usuario.NOME;
                txtCPF.Text = this.FormatarCpf(usuario.CPF);
                ddlSexo.SelectedValue = Convert.ToInt16(Enum.Parse(typeof(SexoEnum), usuario.SEXO.ToString())).ToString();
                txtDataNascimento.Text = Convert.ToDateTime(usuario.DATANASCIMENTO).ToShortDateString();
                txtTelefone.Text = FormatarTelefone(usuario.TELEFONE);
                txtEmail.Text = usuario.AspNetUser.Email;
                chkAtiva.Checked = usuario.ATIVO;

                foreach (var item in usuarioUnidadeBll.GetListUsuarioUnidadesByUsuarioId(idUsuario))
                {
                    if (cblUnidades.Items.FindByValue(item.UNIDADEID.ToString()) != null)
                    {
                        cblUnidades.Items.FindByValue(item.UNIDADEID.ToString()).Selected = true;
                    }
                    else
                    {
                        UNIDADE un = unidadeBll.GetUnidadeById(Convert.ToInt32(item.UNIDADEID));
                        cblUnidades.Items.Add(new ListItem(un.APELIDO, un.UNIDADEID.ToString()));
                        cblUnidades.Items.FindByValue(item.UNIDADEID.ToString()).Selected = true;
                    }

                }

                foreach (var item in usuarioPerfilBll.GetListUsuarioPerfilByUsuarioId(idUsuario))
                {
                    if (cblPerfis.Items.FindByValue(item.PERFILID.ToString()) != null)
                    {
                        cblPerfis.Items.FindByValue(item.PERFILID.ToString()).Selected = true;

                    }
                    else
                    {
                        PERFI un = perfilBll.GetPerfilById(Convert.ToInt32(item.PERFILID));
                        cblPerfis.Items.Add(new ListItem(un.NOME, un.PERFILID.ToString()));
                        cblPerfis.Items.FindByValue(item.PERFILID.ToString()).Selected = true;
                    }
                }
            }
            else
            {
                MessageBoxError(this.Page, "Usuario não localizado!");
            }
        }

        public int Salvar()
        {
            try
            {
                USUARIO usuario = null;
                AspNetUser anUser = null;

                //TODO: Campos ASPNETUSERS
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    anUser = aspNetUserBll.GetANUsuerByUserName(txtEmail.Text);
                }

                if (!string.IsNullOrEmpty(txtEmail.Text) && anUser == null)
                {
                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                    var user = new ApplicationUser() { UserName = txtEmail.Text, Email = txtEmail.Text };
                    IdentityResult result = manager.Create(user, "Novo@123456");

                    if (result.Succeeded)
                    {
                        anUser = aspNetUserBll.GetUNUsuerById(user.Id);
                        anUser.EmailConfirmed = true;

                        aspNetUserBll.Salvar(anUser);
                    }
                    else
                    {
                        return 0;
                    }
                }

                if (anUser != null)
                {

                    //TODO: informa campos para SalvarTipoProduto.
                    if (Convert.ToInt32(hdnIdUsuario.Value) > 0)
                        usuario = usuarioBll.GetUsuarioById(Convert.ToInt32(hdnIdUsuario.Value));
                    else
                        usuario = new USUARIO();

                    usuario.NOME = txtNomeUsuario.Text;
                    usuario.CPF = this.ClearCaracter(txtCPF.Text, ".-");

                    if (ddlSexo.SelectedIndex > 0)
                        usuario.SEXO = Convert.ToInt16(ddlSexo.SelectedValue);

                    if (!string.IsNullOrEmpty(txtDataNascimento.Text))
                        usuario.DATANASCIMENTO = Convert.ToDateTime(txtDataNascimento.Text);

                    usuario.TELEFONE = this.ClearCaracter(txtTelefone.Text, "()- ");
                    usuario.ATIVO = chkAtiva.Checked;
                    usuario.ASPNETUSERID = anUser.Id;
                    usuario.DATACADASTRO = DateTime.Now;

                    usuarioBll.Salvar(usuario);

                    //TODO: Campos Perfis
                    if (usuario.USUARIOID > 0)
                    {
                        usuarioPerfilBll.DeletarTodosByUsuarioId(usuario.USUARIOID);
                        foreach (ListItem item in cblPerfis.Items)
                        {
                            if (item.Selected)
                            {
                                USUARIOPERFI up = new USUARIOPERFI();
                                up.USUARIOID = usuario.USUARIOID;
                                up.PERFILID = Convert.ToInt32(item.Value);

                                usuarioPerfilBll.Salvar(up);
                            }
                        }
                    }

                    //TODO: Campos Unidades
                    if (usuario.USUARIOID > 0)
                    {
                        usuarioUnidadeBll.DeletarTodosByUsuarioId(usuario.USUARIOID);
                        foreach (ListItem item in cblUnidades.Items)
                        {
                            if (item.Selected)
                            {
                                USUARIOUNIDADE uu = new USUARIOUNIDADE();
                                uu.USUARIOID = usuario.USUARIOID;
                                uu.UNIDADEID = Convert.ToInt32(item.Value);

                                usuarioUnidadeBll.Salvar(uu);
                            }
                        }
                    }

                    return usuario.USUARIOID;

                }
                return 0;
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
            txtNomeUsuario.Text = string.Empty;
            txtCPF.Text = string.Empty;
            ddlSexo.SelectedIndex = 0;
            txtDataNascimento.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            chkAtiva.Checked = false;
            hdnIdUsuario.Value = "0";
            cblUnidades.ClearSelection();
            cblUnidades.Items.Clear();
            btnMarcarTodosUnidades.Visible = true;
            btnDesmarcarTodosUnidades.Visible = false;

            cblPerfis.ClearSelection();
            cblPerfis.Items.Clear();
            btnMarcarTodosPerfis.Visible = true;
            btnDesmarcarTodosPerfis.Visible = false;
        }

        public bool VerificaUsuarioAssociado(int usuarioId)
        {
            bool associado = false;

            if (UsuarioInfo != null && (UsuarioInfo.IdUsuario == usuarioId || logBll.VerificaUsuarioAssociado(usuarioId)))
                associado = true;

            return associado;
        }



        #endregion
    }
}