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
    public partial class CadastroMarca : BaseUserControl
    {
        #region Attributes
        MarcaProdutoBll marcaProdutoBll = new MarcaProdutoBll();
        #endregion

        #region Properties
        public CheckBox chkAtivo
        {
            set { this.chkMarcaProdutoAtivo = value; }
            get { return this.chkMarcaProdutoAtivo; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LimparCamposMarcasProdutos()
        {
            txtMarcaProduto.Text = string.Empty;
            chkMarcaProdutoAtivo.Checked = true;
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.MarcaProdutoHidden = string.Empty;
        }

        public void CarregaDadosMarcasProdutos(int MarcaProdutoId)
        {
            MARCAPRODUTO fp = marcaProdutoBll.GetMarcaProdutoById(MarcaProdutoId);

            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.MarcaProdutoHidden = MarcaProdutoId.ToString();

            if (fp != null)
            {
                //TODO: Informar campos para Editar.
                txtMarcaProduto.Text = fp.NOME;
                chkMarcaProdutoAtivo.Checked = fp.ATIVO;
            }
            else
            {
                MessageBoxError(this.Page, "Marca de Produto não localizado!");
            }
        }

        public int SalvarMarcaProduto()
        {
            try
            {
                MARCAPRODUTO fp;
                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //if (!String.IsNullOrEmpty(cad.MarcaProdutoHidden) && Convert.ToInt32(cad.MarcaProdutoHidden) > 0)
                //    fp = marcaProdutoBll.GetMarcaProdutoById(Convert.ToInt32(cad.MarcaProdutoHidden));
                //else
                fp = new MARCAPRODUTO();

                //TODO: informa campos para SalvarMarcaProduto.
                fp.NOME = txtMarcaProduto.Text;
                fp.ATIVO = chkMarcaProdutoAtivo.Checked;

                marcaProdutoBll.Salvar(fp);

                return fp.MARCAPRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }
        protected void lbCancelMarcaProduto_Click(object sender, EventArgs e)
        {
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.ModalMarcaProduto.Hide();
            LimparCamposMarcasProdutos();
        }

        protected void btnSalvarMarcaProduto_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int marcaProdutoId = SalvarMarcaProduto();

                if (marcaProdutoId > 0)
                    this.MessageBoxSucesso(this.Page, "Marca de Produto salvo com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o Marca de Produto! Verifique os campos informados.");

                if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("cadastroprodutos"))
                {
                    CadastroProdutos cad = (CadastroProdutos)Page;
                    cad.HiddenIdMarca = marcaProdutoId;
                    cad.AutoSelectMarca(marcaProdutoId);

                }

                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //cad.GridMarcaProduto.DataBind();
            }
        }


    }
}