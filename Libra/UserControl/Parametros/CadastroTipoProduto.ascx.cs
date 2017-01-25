using Libra.Class;
using Libra.Control;
using Libra.Entity;
using Libra.Parametros;
using Libra.Produtos;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libra.UserControl.Parametros
{
    public partial class CadastroTipoProduto : BaseUserControl
    {
        #region Attributes
        TipoProdutoBll tipoProdutoBll = new TipoProdutoBll();
        #endregion

        #region Properties
        public CheckBox chkAtivo
        {
            set { this.chkTipoProdutoAtivo = value; }
            get { return this.chkTipoProdutoAtivo; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LimparCamposTiposProdutos()
        {
            txtTipoProduto.Text = string.Empty;
            chkTipoProdutoAtivo.Checked = true;
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.TipoProdutoHidden = string.Empty;
        }

        public void CarregaDadosTiposProdutos(int TipoProdutoId)
        {
            TIPOPRODUTO fp = tipoProdutoBll.GetTipoProdutoById(TipoProdutoId);

            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.TipoProdutoHidden = TipoProdutoId.ToString();

            if (fp != null)
            {
                //TODO: Informar campos para Editar.
                txtTipoProduto.Text = fp.NOME;
                chkTipoProdutoAtivo.Checked = fp.ATIVO;
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
                TIPOPRODUTO fp;
                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //if (!String.IsNullOrEmpty(cad.TipoProdutoHidden) && Convert.ToInt32(cad.TipoProdutoHidden) > 0)
                //    fp = tipoProdutoBll.GetTipoProdutoById(Convert.ToInt32(cad.TipoProdutoHidden));
                //else
                    fp = new TIPOPRODUTO();

                //TODO: informa campos para SalvarTipoProduto.
                fp.NOME = txtTipoProduto.Text;
                fp.ATIVO = chkTipoProdutoAtivo.Checked;

                tipoProdutoBll.Salvar(fp);

                return fp.TIPOPRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }
        protected void lbCancelTipoProduto_Click(object sender, EventArgs e)
        {
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.ModalTipoProduto.Hide();
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


                if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("CadastroProdutos"))
                {
                    CadastroProdutos cad = (CadastroProdutos)Page;
                    cad.HiddenIdTipoProduto = tipoProdutoId;
                    cad.AutoSelectTipoProduto(tipoProdutoId);
                }
                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //cad.GridTipoProduto.DataBind();
            }
        }


    }
}