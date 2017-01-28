<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroCaracteristicaDiversa.ascx.cs" Inherits="Libra.UserControl.Parametros.CadastroCaracteristicaDiversa" %>

<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>
                Caracteristica Diversa 
                                                                                                        <asp:Label runat="server" ID="lblCaracteristicasDiversasProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtCaracteristicasDiversasProduto" runat="server" CssClass="form-control text-text-uppercase" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCaracteristicasDiversasProduto" ControlToValidate="txtCaracteristicasDiversasProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1CaracteristicasDiversas" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>

<div class="row" runat="server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>Ativo?</label>
        </div>
        <div>
            <asp:CheckBox runat="server" ID="chkCaracteristicasDiversasProdutoAtivo" Checked="true" />
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="lbCancelCaracteristicasDiversasProduto" runat="server"
            CssClass="btn btn-default" Text="Fechar" OnClick="lbCancelCaracteristicasDiversasProduto_Click">
        </asp:LinkButton>
        <asp:Button runat="server" ID="btnSalvarCaracteristicasDiversasProduto" ValidationGroup="G1CaracteristicasDiversas" CausesValidation="true"
            CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarCaracteristicasDiversasProduto_Click" />
    </div>
</div>
