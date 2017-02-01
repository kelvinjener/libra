<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroFabricante.ascx.cs" Inherits="Libra.UserControl.Parametros.CadastroFabricante" %>

<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>
                Fabricante
                 <asp:Label runat="server" ID="lblFabricanteProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtFabricanteProduto" runat="server" CssClass="form-control text-uppercase" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFabricanteProduto" ControlToValidate="txtFabricanteProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Fabricante" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
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
            <asp:CheckBox runat="server" ID="chkFabricanteProdutoAtivo"  Checked="true" />
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="lbCancelFabricanteProduto" runat="server"
            CssClass="btn btn-default" Text="Fechar" OnClick="lbCancelFabricanteProduto_Click">
        </asp:LinkButton>
        <asp:Button runat="server" ID="btnSalvarFabricanteProduto" ValidationGroup="G1Fabricante" CausesValidation="true"
            CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarFabricanteProduto_Click" />
    </div>
</div>

