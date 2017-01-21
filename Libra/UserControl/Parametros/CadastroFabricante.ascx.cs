using Libra.Class;
using Libra.Control;
using Libra.Entity;
using Libra.Parametros;
using System;
using System.Web.UI;

namespace Libra.UserControl.Parametros
{
    public partial class CadastroFabricante : BaseUserControl
    {
        #region Attributes
        FabricanteProdutoBll fabricanteProdutoBll = new FabricanteProdutoBll();
        #endregion

        #region Properties

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LimparCamposFabricantesProdutos()
        {
            txtFabricanteProduto.Text = string.Empty;
            chkFabricanteProdutoAtivo.Checked = true;
            ParametrosProdutos cad = (ParametrosProdutos)Page;
            cad.FabricanteProdutoHidden = string.Empty;
        }

        public void CarregaDadosFabricantesProdutos(int FabricanteProdutoId)
        {
            FABRICANTEPRODUTO fp = fabricanteProdutoBll.GetFabricanteProdutoById(FabricanteProdutoId);

            ParametrosProdutos cad = (ParametrosProdutos)Page;
            cad.FabricanteProdutoHidden = FabricanteProdutoId.ToString();

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
                ParametrosProdutos cad = (ParametrosProdutos)Page;
                if (!String.IsNullOrEmpty(cad.FabricanteProdutoHidden) && Convert.ToInt32(cad.FabricanteProdutoHidden) > 0)
                    fp = fabricanteProdutoBll.GetFabricanteProdutoById(Convert.ToInt32(cad.FabricanteProdutoHidden));
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
        protected void lbCancelFabricanteProduto_Click(object sender, EventArgs e)
        {
            ParametrosProdutos cad = (ParametrosProdutos)Page;
            cad.ModalFabricanteProduto.Hide();
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

                ParametrosProdutos cad = (ParametrosProdutos)Page;
                cad.GridFabricanteProduto.DataBind();
            }
        }

       
    }
}