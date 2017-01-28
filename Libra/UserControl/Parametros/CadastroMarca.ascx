<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroMarca.ascx.cs" Inherits="Libra.UserControl.Parametros.CadastroMarca" %>

<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>
                Marca
                                                                                        <asp:Label runat="server" ID="lblMarcaProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtMarcaProduto" runat="server" CssClass="form-control text-text-uppercase" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvMarcaProduto" ControlToValidate="txtMarcaProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Marca" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
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
            <asp:CheckBox runat="server" ID="chkMarcaProdutoAtivo" Checked="true" />
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="lbCancelMarcaProduto" runat="server"
            CssClass="btn btn-default" Text="Fechar" OnClick="lbCancelMarcaProduto_Click">
        </asp:LinkButton>
        <asp:Button runat="server" ID="btnSalvarMarcaProduto" ValidationGroup="G1Marca" CausesValidation="true"
            CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarMarcaProduto_Click" />
    </div>
</div>