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
using Libra.Communs.Enumerators;

namespace Libra
{
    public partial class Unidades : BasePage
    {
        #region Atributos
        private UnidadeBll unidadeBll = new UnidadeBll();
        private UsuarioUnidadeBll usuarioUnidadeBll = new UsuarioUnidadeBll();

        #endregion

        #region Properties
        private List<UNIDADE> unidadesExclusao
        {
            get
            {
                if (Session["unidadesExclusao"] != null)
                    return Session["unidadesExclusao"] as List<UNIDADE>;
                else
                    return null;
            }
            set
            {
                Session["unidadesExclusao"] = value;
            }
        }

        private List<UNIDADE> unidadesExclusaoProibida
        {
            get
            {
                if (Session["unidadesExclusaoProibida"] != null)
                    return Session["unidadesExclusaoProibida"] as List<UNIDADE>;
                else
                    return null;
            }
            set
            {
                Session["unidadesExclusaoProibida"] = value;
            }
        }

        #endregion

        #region Page Actions
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaTipoUnidade();
                CarregaUFs();
                Edicao(false);

                this.txtTel1.Attributes.Add("onkeypress", "return formatarTelefone(event, this);");
                this.txtTel2.Attributes.Add("onkeypress", "return formatarTelefone(event, this);");
                this.txtFax.Attributes.Add("onkeypress", "return formatarTelefone(event, this);");
                this.txtCEP.Attributes.Add("onkeypress", "return FormatMaskOnlyNumbers(event, this, '##.###-###');");
            }
        }

        protected void gvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idUnidade = Convert.ToInt32(this.gvResults.DataKeys[gvResults.SelectedIndex].Values["UnidadeId"]);
                CarregarDadosVisualizacao(idUnidade);
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
                    int UnidadeId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UnidadeId"));

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Ativo")))
                        lblAtiva.Text = "Sim";
                    else
                        lblAtiva.Text = "Não";

                    if (usuarioUnidadeBll.VerificaUnidadeAssociadas(UnidadeId))
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

                String txtUnidade = (!this.txtUnidadeFiltro.Text.Equals(String.Empty)) ? this.txtUnidadeFiltro.Text : String.Empty;

                string tipoUnidade = string.Empty;
                if (this.ddlTipoUnidadeFiltro.SelectedIndex > 0)
                    tipoUnidade = this.ddlTipoUnidadeFiltro.SelectedValue;

                e.Result = unidadeBll.GetAllUnidadesGridFiltro(situacao, txtUnidade, tipoUnidade);
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void lbAddUnidades_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            lbAddEditUnidade.Text = "Nova Unidade";
            Edicao(true);
        }

        protected void lbEditUnidades_Click(object sender, EventArgs e)
        {
            try
            {
                LimpaCampos();
                lbAddEditUnidade.Text = "Editar Unidade";

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
                            int UnidadeId = Convert.ToInt32(keys.Values["UnidadeId"]);
                            //if (!usuarioUnidadeBll.VerificaUnidadeAssociadas(UnidadeId))
                            //{
                            CarregarDadosEdicao(UnidadeId);
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



        protected void lbFiltroUnidades_Click(object sender, EventArgs e)
        {
            mpeFiltroUnidades.Show();
        }

        protected void lkClose_Click(object sender, EventArgs e)
        {
            mpeFiltroUnidades.Hide();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            gvResults.DataBind();
        }

        protected void lkCloseVisualizarUnidade_Click(object sender, EventArgs e)
        {
            mpeVisualizarUnidade.Hide();
        }

        protected void lbVisualizarUnidade_Click(object sender, EventArgs e)
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
                            int UnidadeId = Convert.ToInt32(keys.Values["UnidadeId"]);
                            CarregarDadosVisualizacao(UnidadeId);
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
                int unidadeId = Salvar();

                if (unidadeId > 0)
                    this.MessageBoxSucesso(this.Page, "Unidade salva com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar a unidade! Verifique os campos informados.");

                gvResults.DataBind();

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Edicao(false);
            gvResults.DataBind();
        }

        protected void lbDelUnidades_Click(object sender, EventArgs e)
        {
            try
            {
                unidadesExclusao = new List<UNIDADE>();
                unidadesExclusaoProibida = new List<UNIDADE>();
                btnExcluirUnidade.Visible = true;

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
                            int UnidadeId = Convert.ToInt32(keys.Values["UnidadeId"]);

                            UNIDADE unidade = unidadeBll.GetUnidadeById(UnidadeId);

                            if (usuarioUnidadeBll.VerificaUnidadeAssociadas(UnidadeId))
                                unidadesExclusaoProibida.Add(unidade);
                            else
                                unidadesExclusao.Add(unidade);
                        }
                    }

                    divExclusaoUnidade.Visible = false;
                    divExclusaoUnidadeProibida.Visible = false;
                    imgExclusaoUnidade.Visible = false;
                    imgExclusaoUnidadeProibida.Visible = false;
                    divLinhaExclusaoUnidade.Visible = false;
                    divAlgumitem.Visible = false;
                    divOsitem.Visible = false;

                    lblUnidadesExclusao.Text = "<br/>";
                    lblUnidadesExclusaoProibidas.Text = "<br/>";

                    if (unidadesExclusao.Count > 0)
                    {
                        divExclusaoUnidade.Visible = true;

                        if (unidadesExclusaoProibida.Count == 0)
                            imgExclusaoUnidade.Visible = true;

                        btnExcluirUnidade.Enabled = true;

                        foreach (var item in unidadesExclusao)
                        {
                            lblUnidadesExclusao.Text = lblUnidadesExclusao.Text + string.Format(" - {0}<br/>", item.APELIDO);
                        }
                    }
                    else
                    {
                        btnExcluirUnidade.Enabled = false;
                    }

                    if (unidadesExclusaoProibida.Count > 0)
                    {
                        divExclusaoUnidadeProibida.Visible = true;
                        divAlgumitem.Visible = unidadesExclusao.Count > 0;
                        divOsitem.Visible = !divAlgumitem.Visible;

                        if (!imgExclusaoUnidade.Visible)
                        {
                            imgExclusaoUnidadeProibida.Visible = true;
                            divLinhaExclusaoUnidade.Visible = true;
                        }

                        foreach (var item in unidadesExclusaoProibida)
                        {
                            lblUnidadesExclusaoProibidas.Text = lblUnidadesExclusaoProibidas.Text + string.Format(" - {0}<br/>", item.APELIDO);
                        }
                        if (unidadesExclusao.Count == 0)
                            btnExcluirUnidade.Visible = false;
                    }

                    mpeExclusaoUnidade.Show();
                }
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        protected void btnExcluirUnidade_Click(object sender, EventArgs e)
        {
            foreach (var item in unidadesExclusao)
            {
                unidadeBll.Deletar(item);
            }

            mpeExclusaoUnidade.Hide();
            gvResults.DataBind();

            if (unidadesExclusao.Count > 0)
                this.MessageBoxSucesso(this.Page, "Registros excluídos com sucesso!");
        }

        protected void btnCancelarExclusaoUnidade_Click(object sender, EventArgs e)
        {
            mpeExclusaoUnidade.Hide();
        }

        #endregion

        #region Métodos

        public void Edicao(bool edita)
        {
            divEdicao.Visible = edita;
            divFiltroETabela.Visible = !edita;
        }
        public void CarregaUFs()
        {
            ddlUF.Items.Clear();
            ddlUF.Items.Add(new ListItem("Selecione...", ""));
            foreach (var item in GetUfs())
                ddlUF.Items.Add(new ListItem(item, item));
        }
        public void CarregaTipoUnidade()
        {
            ddlTipoUnidadeFiltro.Items.Clear();
            ddlTipoUnidade.Items.Clear();
            ddlTipoUnidadeFiltro.Items.Add(new ListItem("Selecione...", ""));
            ddlTipoUnidade.Items.Add(new ListItem("Selecione...", ""));

            foreach (string item in Enum.GetNames(typeof(TipoUnidadeEnum)))
            {
                short value = Convert.ToInt16(Enum.Parse(typeof(TipoUnidadeEnum), item));
                string text = Enum<TipoUnidadeEnum>.Description((TipoUnidadeEnum)Enum.Parse(typeof(TipoUnidadeEnum), item));

                ddlTipoUnidadeFiltro.Items.Add(new ListItem(text, value.ToString()));
                ddlTipoUnidade.Items.Add(new ListItem(text, value.ToString()));
            }

        }

        public void CarregarDadosVisualizacao(int idUnidade)
        {

            UNIDADE unidade = unidadeBll.GetUnidadeById(idUnidade);

            if (unidade != null)
            {
                lbUnidade.Text = unidade.APELIDO;
                lbNomeUnidade.Text = unidade.NOME;
                lbEnderecoUnidade.Text = GetEnderecoUnidade(unidade);
                lbTelefones.Text = GetTelefonesUnidade(unidade);
                lbEmails.Text = GetEmailsUnidade(unidade);
                lbObservacao.Text = unidade.OBSERVACAO == null ? "---" : unidade.OBSERVACAO;
                lbAtiva.Text = unidade.ATIVO ? "Sim" : "Não";
                lbTipoUnidade.Text = Enum<TipoUnidadeEnum>.Description((TipoUnidadeEnum)Enum.Parse(typeof(TipoUnidadeEnum), unidade.TIPOUNIDADE.ToString()));

                mpeVisualizarUnidade.Show();
            }
            else
            {
                MessageBoxError(this.Page, "Unidade não localizada!");
            }

        }

        public void CarregarDadosEdicao(int idUnidade)
        {
            UNIDADE unidade = unidadeBll.GetUnidadeById(idUnidade);

            hdnIdUnidade.Value = idUnidade.ToString();

            if (unidade != null)
            {
                //TODO: Informar campos para Editar.
                txtNomeUnidade.Text = unidade.NOME;
                txtApelidoUnidade.Text = unidade.APELIDO;
                txtLogradouro.Text = unidade.LOGRADOURO;
                txtNumero.Text = unidade.NUMERO;
                txtComplemento.Text = unidade.COMPLEMENTO;
                txtBairro.Text = unidade.BAIRRO;
                txtCidade.Text = unidade.CIDADE;
                if (!String.IsNullOrEmpty(unidade.UF))
                    ddlUF.SelectedValue = unidade.UF;
                txtCEP.Text = FormatarCep(unidade.CEP);
                txtTel1.Text = FormatarTelefone(unidade.TEL1);
                txtTel2.Text = FormatarTelefone(unidade.TEL2);
                txtFax.Text = FormatarTelefone(unidade.FAX);
                txtEmail1.Text = unidade.EMAIL1;
                txtEmail2.Text = unidade.EMAIL2;
                txtObservacao.Text = unidade.OBSERVACAO;
                ddlTipoUnidade.SelectedValue = Convert.ToInt16(Enum.Parse(typeof(TipoUnidadeEnum), unidade.TIPOUNIDADE.ToString())).ToString();
                chkAtiva.Checked = unidade.ATIVO;
            }
            else
            {
                MessageBoxError(this.Page, "Unidade não localizada!");
            }
        }

        public int Salvar()
        {
            try
            {
                UNIDADE unidade;

                if (!String.IsNullOrEmpty(hdnIdUnidade.Value) && Convert.ToInt32(hdnIdUnidade.Value) > 0)
                    unidade = unidadeBll.GetUnidadeById(Convert.ToInt32(hdnIdUnidade.Value));
                else
                    unidade = new UNIDADE();

                //TODO: informa campos para SalvarTipoProduto.
                unidade.NOME = txtNomeUnidade.Text;
                unidade.APELIDO = txtApelidoUnidade.Text;
                unidade.LOGRADOURO = txtLogradouro.Text;
                unidade.NUMERO = txtNumero.Text;
                unidade.COMPLEMENTO = txtComplemento.Text;
                unidade.BAIRRO = txtBairro.Text;
                unidade.CIDADE = txtCidade.Text;
                unidade.UF = ddlUF.SelectedValue;
                unidade.CEP = this.ClearCaracter(txtCEP.Text, ".-");
                unidade.TEL1 = this.ClearCaracter(txtTel1.Text, ".-()");
                unidade.TEL2 = this.ClearCaracter(txtTel2.Text, ".-()");
                unidade.FAX = this.ClearCaracter(txtFax.Text, ".-()");
                unidade.EMAIL1 = txtEmail1.Text;
                unidade.EMAIL2 = txtEmail2.Text;
                unidade.OBSERVACAO = txtObservacao.Text;
                unidade.TIPOUNIDADE = Convert.ToInt16(ddlTipoUnidade.SelectedValue);
                unidade.ATIVO = chkAtiva.Checked;

                unidadeBll.Salvar(unidade);

                return unidade.UNIDADEID;

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
            txtNomeUnidade.Text = string.Empty;
            txtApelidoUnidade.Text = string.Empty;
            txtLogradouro.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            ddlUF.SelectedIndex = 0;
            txtCEP.Text = string.Empty;
            txtTel1.Text = string.Empty;
            txtTel2.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtEmail1.Text = string.Empty;
            txtEmail2.Text = string.Empty;
            txtObservacao.Text = string.Empty;
            ddlTipoUnidade.SelectedIndex = 0;
            chkAtiva.Checked = false;
            hdnIdUnidade.Value = "0";

        }

        public string GetEnderecoUnidade(UNIDADE unidade)
        {
            string endereco = "---";

            if (unidade != null && !String.IsNullOrEmpty(unidade.LOGRADOURO))
            {
                endereco = unidade.LOGRADOURO;

                if (!String.IsNullOrEmpty(unidade.NUMERO))
                    endereco = endereco + ", " + unidade.NUMERO;

                if (!String.IsNullOrEmpty(unidade.COMPLEMENTO))
                    endereco = endereco + ", " + unidade.COMPLEMENTO;

                if (!String.IsNullOrEmpty(unidade.BAIRRO))
                    endereco = endereco + ", B. " + unidade.BAIRRO;

                if (!String.IsNullOrEmpty(unidade.CIDADE))
                    endereco = endereco + ", " + unidade.CIDADE;

                if (!String.IsNullOrEmpty(unidade.UF))
                    endereco = endereco + " - " + unidade.UF;

                if (!String.IsNullOrEmpty(unidade.CEP))
                    endereco = endereco + " - CEP: " + this.FormatarCep(unidade.CEP);

            }

            return endereco;
        }

        public string GetTelefonesUnidade(UNIDADE unidade)
        {
            string telefones = "---";

            if (unidade != null && !String.IsNullOrEmpty(unidade.TEL1))
            {
                telefones = this.FormatarTelefone(unidade.TEL1);

                if (!String.IsNullOrEmpty(unidade.TEL2))
                    telefones = telefones + " | " + this.FormatarTelefone(unidade.TEL2);

                if (!String.IsNullOrEmpty(unidade.FAX))
                    telefones = telefones + " | FAX: " + this.FormatarTelefone(unidade.FAX);
            }

            return telefones;
        }

        public string GetEmailsUnidade(UNIDADE unidade)
        {
            string emails = "---";

            if (unidade != null && !String.IsNullOrEmpty(unidade.EMAIL1))
            {
                emails = unidade.EMAIL1;

                if (!String.IsNullOrEmpty(unidade.EMAIL2))
                    emails = emails + " | " + unidade.EMAIL2;
            }

            return emails;
        }



        #endregion


    }
}