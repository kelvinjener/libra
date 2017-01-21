using Libra.Class;
using Libra.Control;
using Libra.Entity;
using Libra.Parametros;
using System;
using System.Web.UI;

namespace Libra.UserControl.Parametros
{
    public partial class CadastroCaracteristicaDiversa : BaseUserControl
    {
        #region Attributes
        CaracteristicasDiversasProdutoBll caracteristicasDiversasProdutoBll = new CaracteristicasDiversasProdutoBll();
        #endregion

        #region Properties

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LimparCamposCaracteristicasDiversassProdutos()
        {
            txtCaracteristicasDiversasProduto.Text = string.Empty;
            chkCaracteristicasDiversasProdutoAtivo.Checked = true;
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.CaracteristicasDiversasProdutoHidden = string.Empty;
        }

        public void CarregaDadosCaracteristicasDiversassProdutos(int CaracteristicasDiversasProdutoId)
        {
            CARACTERISTICASDIVERSASPRODUTO fp = caracteristicasDiversasProdutoBll.GetCaracteristicasDiversasProdutoById(CaracteristicasDiversasProdutoId);

            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.CaracteristicasDiversasProdutoHidden = CaracteristicasDiversasProdutoId.ToString();

            if (fp != null)
            {
                //TODO: Informar campos para Editar.
                txtCaracteristicasDiversasProduto.Text = fp.NOME;
                chkCaracteristicasDiversasProdutoAtivo.Checked = fp.ATIVO;
            }
            else
            {
                MessageBoxError(this.Page, "CaracteristicasDiversas de Produto não localizado!");
            }
        }

        public int SalvarCaracteristicasDiversasProduto()
        {
            try
            {
                CARACTERISTICASDIVERSASPRODUTO fp;
                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //if (!String.IsNullOrEmpty(cad.CaracteristicasDiversasProdutoHidden) && Convert.ToInt32(cad.CaracteristicasDiversasProdutoHidden) > 0)
                //    fp = caracteristicasDiversasProdutoBll.GetCaracteristicasDiversasProdutoById(Convert.ToInt32(cad.CaracteristicasDiversasProdutoHidden));
                //else
                fp = new CARACTERISTICASDIVERSASPRODUTO();

                //TODO: informa campos para SalvarCaracteristicasDiversasProduto.
                fp.NOME = txtCaracteristicasDiversasProduto.Text;
                fp.ATIVO = chkCaracteristicasDiversasProdutoAtivo.Checked;

                caracteristicasDiversasProdutoBll.Salvar(fp);

                return fp.CARACTERISTICASDIVERSASPRODUTOID;

            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }

            return 0;
        }
        protected void lbCancelCaracteristicasDiversasProduto_Click(object sender, EventArgs e)
        {
            //ParametrosProdutos cad = (ParametrosProdutos)Page;
            //cad.ModalCaracteristicasDiversasProduto.Hide();
            LimparCamposCaracteristicasDiversassProdutos();
        }

        protected void btnSalvarCaracteristicasDiversasProduto_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int caracteristicasDiversasProdutoId = SalvarCaracteristicasDiversasProduto();

                if (caracteristicasDiversasProdutoId > 0)
                    this.MessageBoxSucesso(this.Page, "CaracteristicasDiversas de Produto salvo com sucesso!");
                else
                    this.MessageBoxError(this.Page, "Não foi possível salvar o CaracteristicasDiversas de Produto! Verifique os campos informados.");

                //ParametrosProdutos cad = (ParametrosProdutos)Page;
                //cad.GridCaracteristicasDiversasProduto.DataBind();
            }
        }


    }
}