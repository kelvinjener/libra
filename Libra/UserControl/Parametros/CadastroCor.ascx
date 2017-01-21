<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroCor.ascx.cs" Inherits="Libra.UserControl.Parametros.CadastroCor" %>


<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>
                Cor
                                                                                        <asp:Label runat="server" ID="lblCorProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtCorProduto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCorProduto" ControlToValidate="txtCorProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Cor" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
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
            <asp:CheckBox runat="server" ID="chkCorProdutoAtivo" />
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="lbCancelCorProduto" runat="server"
            CssClass="btn btn-default" Text="Fechar" OnClick="lbCancelCorProduto_Click">
        </asp:LinkButton>
        <asp:Button runat="server" ID="btnSalvarCorProduto" ValidationGroup="G1Cor" CausesValidation="true"
            CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarCorProduto_Click" />
    </div>
</div>