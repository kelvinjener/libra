<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroModelo.ascx.cs" Inherits="Libra.UserControl.Parametros.CadastroModelo" %>

<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>
                Modelo
                                                                                        <asp:Label runat="server" ID="lblModeloProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtModeloProduto" runat="server" CssClass="form-control text-uppercase" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvModeloProduto" ControlToValidate="txtModeloProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Modelo" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
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
            <asp:CheckBox runat="server" ID="chkModeloProdutoAtivo" Checked="true" />
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="lbCancelModeloProduto" runat="server"
            CssClass="btn btn-default" Text="Fechar" OnClick="lbCancelModeloProduto_Click">
        </asp:LinkButton>
        <asp:Button runat="server" ID="btnSalvarModeloProduto" ValidationGroup="G1Modelo" CausesValidation="true"
            CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarModeloProduto_Click" />
    </div>
</div>