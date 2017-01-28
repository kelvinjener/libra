<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroTipoProduto.ascx.cs" Inherits="Libra.UserControl.Parametros.CadastroTipoProduto" %>

<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>
                Tipo de Produto
                                                                                        <asp:Label runat="server" ID="lblTipoProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtTipoProduto" runat="server" CssClass="form-control text-text-uppercase" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTipoProduto" ControlToValidate="txtTipoProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Tipo" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>

<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>Ativo?</label>
        </div>
        <div>
            <asp:CheckBox runat="server" ID="chkTipoProdutoAtivo" Checked="true" />
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="lbCancelTipoProduto" runat="server"
            CssClass="btn btn-default" Text="Fechar" OnClick="lbCancelTipoProduto_Click">
        </asp:LinkButton>
        <asp:Button runat="server" ID="btnSalvarTipoProduto" ValidationGroup="G1Tipo" CausesValidation="true"
            CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarTipoProduto_Click" />
    </div>
</div>
