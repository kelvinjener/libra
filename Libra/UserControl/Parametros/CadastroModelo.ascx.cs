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
    public partial class CadastroModelo : BaseUserControl
    {
        #region Attributes
        ModeloProdutoBll modeloProdutoBll = new ModeloProdutoBll();
        #endregion

        #region Properties
        public CheckBox chkAtivo
        {
            set { this.chkModeloProdutoAtivo = value; }
            get { return this.chkModeloProdutoAtivo; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LimparCamposModelosProdutos()
        {
            txtModeloProduto.Text = string.Empty;
            chkModeloProdutoAtivo.Checked = true;
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.ModeloProdutoHidden = string.Empty;
        }

        public void CarregaDadosModelosProdutos(int ModeloProdutoId)
        {
            MODELOPRODUTO fp = modeloProdutoBll.GetModeloProdutoById(ModeloProdutoId);

            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.ModeloProdutoHidden = ModeloProdutoId.ToString();

            if (fp != null)
            {
                //TODO: Informar campos para Editar.
                txtModeloProduto.Text = fp.NOME;
                chkModeloProdutoAtivo.Checked = fp.ATIVO;
            }
            else
            {
                MessageBoxError(this.Page, "Modelo de Produto não localizado!");
            }
        }

        public int SalvarModeloProduto()
        {
            try
            {
                MODELOPRODUTO fp;
                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //if (!String.IsNullOrEmpty(cad.ModeloProdutoHidden) && Convert.ToInt32(cad.ModeloProdutoHidden) > 0)
                //    fp = modeloProdutoBll.GetModeloProdutoById(Convert.ToInt32(cad.ModeloProdutoHidden));
                //else
                fp = new MODELOPRODUTO();

                //TODO: informa campos para SalvarModeloProduto.
                fp.NOME = txtModeloProduto.Text;
                fp.ATIVO = chkModeloProdutoAtivo.Checked;

                modeloProdutoBll.Salvar(fp);

                return fp.MODELOPRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }
        protected void lbCancelModeloProduto_Click(object sender, EventArgs e)
        {
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.ModalModeloProduto.Hide();
            LimparCamposModelosProdutos();
        }

        protected void btnSalvarModeloProduto_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int modeloProdutoId = SalvarModeloProduto();

                if (modeloProdutoId > 0)
                    this.MessageBoxSucesso(this.Page, "Modelo de Produto salvo com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o Modelo de Produto! Verifique os campos informados.");


                if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("CadastroProdutos"))
                {
                    CadastroProdutos cad = (CadastroProdutos)Page;
                    cad.HiddenIdModelo = modeloProdutoId;
                    cad.AutoSelectModelo(modeloProdutoId);
                }
                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //cad.GridModeloProduto.DataBind();
            }
        }


    }
}