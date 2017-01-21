using Libra.Class;
using Libra.Control;
using Libra.Entity;
using Libra.Parametros;
using System;
using System.Web.UI;

namespace Libra.UserControl.Parametros
{
    public partial class CadastroCor : BaseUserControl
    {
        #region Attributes
        CorProdutoBll corProdutoBll = new CorProdutoBll();
        #endregion

        #region Properties

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LimparCamposCorsProdutos()
        {
            txtCorProduto.Text = string.Empty;
            chkCorProdutoAtivo.Checked = true;
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.CorProdutoHidden = string.Empty;
        }

        public void CarregaDadosCorsProdutos(int CorProdutoId)
        {
            CORPRODUTO fp = corProdutoBll.GetCorProdutoById(CorProdutoId);

            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.CorProdutoHidden = CorProdutoId.ToString();

            if (fp != null)
            {
                //TODO: Informar campos para Editar.
                txtCorProduto.Text = fp.NOME;
                chkCorProdutoAtivo.Checked = fp.ATIVO;
            }
            else
            {
                MessageBoxError(this.Page, "Cor de Produto não localizado!");
            }
        }

        public int SalvarCorProduto()
        {
            try
            {
                CORPRODUTO fp;
                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //if (!String.IsNullOrEmpty(cad.CorProdutoHidden) && Convert.ToInt32(cad.CorProdutoHidden) > 0)
                //    fp = corProdutoBll.GetCorProdutoById(Convert.ToInt32(cad.CorProdutoHidden));
                //else
                fp = new CORPRODUTO();

                //TODO: informa campos para SalvarCorProduto.
                fp.NOME = txtCorProduto.Text;
                fp.ATIVO = chkCorProdutoAtivo.Checked;

                corProdutoBll.Salvar(fp);

                return fp.CORPRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }
        protected void lbCancelCorProduto_Click(object sender, EventArgs e)
        {
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.ModalCorProduto.Hide();
            LimparCamposCorsProdutos();
        }

        protected void btnSalvarCorProduto_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int corProdutoId = SalvarCorProduto();

                if (corProdutoId > 0)
                    this.MessageBoxSucesso(this.Page, "Cor de Produto salvo com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o Cor de Produto! Verifique os campos informados.");

                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //cad.GridCorProduto.DataBind();
            }
        }


    }
}