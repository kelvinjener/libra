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
    public partial class CadastroDimensoes : BaseUserControl
    {
        #region Attributes
        DimensoesProdutoBll dimensoesProdutoBll = new DimensoesProdutoBll();
        #endregion

        #region Properties
        public CheckBox chkAtivo
        {
            set { this.chkDimensoesProdutoAtivo = value; }
            get { return this.chkDimensoesProdutoAtivo; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtAlturaProduto.Attributes.Add("onkeypress", "return FormatMaskOnlyNumbers(event, this, '#####,##');");
                this.txtComprimentoProduto.Attributes.Add("onkeypress", "return FormatMaskOnlyNumbers(event, this, '#####,##');");
                this.txtLarguraProduto.Attributes.Add("onkeypress", "return FormatMaskOnlyNumbers(event, this, '#####,##');");
            }
        }

        public void LimparCamposDimensoessProdutos()
        {
            txtAlturaProduto.Text = string.Empty;
            txtComprimentoProduto.Text = string.Empty;
            txtDescricaoProduto.Text = string.Empty;
            txtLarguraProduto.Text = string.Empty;
            chkDimensoesProdutoAtivo.Checked = true;
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.DimensoesProdutoHidden = string.Empty;
        }

        public void CarregaDadosDimensoessProdutos(int DimensoesProdutoId)
        {
            DIMENSOESPRODUTO fp = dimensoesProdutoBll.GetDimensoesProdutoById(DimensoesProdutoId);

            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.DimensoesProdutoHidden = DimensoesProdutoId.ToString();

            if (fp != null)
            {
                //TODO: Informar campos para Editar.
                txtAlturaProduto.Text = fp.ALTURA.ToString();
                txtComprimentoProduto.Text = fp.COMPRIMENTO.ToString();
                txtDescricaoProduto.Text = fp.DESCRICAO;
                txtLarguraProduto.Text = fp.LARGURA.ToString();
                chkDimensoesProdutoAtivo.Checked = fp.ATIVO;
            }
            else
            {
                MessageBoxError(this.Page, "Dimensoes de Produto não localizado!");
            }
        }

        public int SalvarDimensoesProduto()
        {
            try
            {
                DIMENSOESPRODUTO fp;
                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //if (!String.IsNullOrEmpty(cad.DimensoesProdutoHidden) && Convert.ToInt32(cad.DimensoesProdutoHidden) > 0)
                //    fp = dimensoesProdutoBll.GetDimensoesProdutoById(Convert.ToInt32(cad.DimensoesProdutoHidden));
                //else
                fp = new DIMENSOESPRODUTO();

                //TODO: informa campos para SalvarDimensoesProduto.
                fp.ALTURA = Convert.ToDecimal(txtAlturaProduto.Text);
                fp.LARGURA = Convert.ToDecimal(txtLarguraProduto.Text);
                fp.COMPRIMENTO = Convert.ToDecimal(txtComprimentoProduto.Text);
                fp.DESCRICAO = txtDescricaoProduto.Text;
                fp.ATIVO = chkDimensoesProdutoAtivo.Checked;

                dimensoesProdutoBll.Salvar(fp);

                return fp.DIMENSOESPRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }
        protected void lbCancelDimensoesProduto_Click(object sender, EventArgs e)
        {
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.ModalDimensoesProduto.Hide();
            LimparCamposDimensoessProdutos();
        }

        protected void btnSalvarDimensoesProduto_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int dimensoesProdutoId = SalvarDimensoesProduto();

                if (dimensoesProdutoId > 0)
                    this.MessageBoxSucesso(this.Page, "Dimensões de Produto salvo com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o Dimensões de Produto! Verifique os campos informados.");


                if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("CadastroProdutos"))
                {
                    CadastroProdutos cad = (CadastroProdutos)Page;
                    cad.HiddenIdDimensoes = dimensoesProdutoId;
                    cad.AutoSelectDimensoes(dimensoesProdutoId);

                }

                if (((System.Web.UI.TemplateControl)(Page)).AppRelativeVirtualPath.ToLower().Contains("ParametrosProdutos"))
                {
                    //ParametrosProdutos cad = (ParametrosProdutos)Page;
                    //cad.GridDimensoesProduto.DataBind();
                }
            }
        }


    }
}