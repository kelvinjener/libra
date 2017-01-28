<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroDimensoes.ascx.cs" Inherits="Libra.UserControl.Parametros.CadastroDimensoes" %>

<div class="row">
    <div class="col-md-6 col-sm-12 col-xs-12">
        <div>
            <label>
                Largura
                <asp:Label runat="server" ID="lblLarguraProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtLarguraProduto" runat="server" CssClass="form-control text-text-uppercase" MaxLength="9"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLarguraProduto" ControlToValidate="txtLarguraProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Dimensoes" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-md-6 col-sm-12 col-xs-12">
        <div>
            <label>
                Altura
                <asp:Label runat="server" ID="lblAlturaProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtAlturaProduto" runat="server" CssClass="form-control" MaxLength="9"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAlturaProduto" ControlToValidate="txtAlturaProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Dimensoes" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>
                Comprimento
                <asp:Label runat="server" ID="lblComprimentoProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtComprimentoProduto" runat="server" CssClass="form-control" MaxLength="9"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvComprimentoProduto" ControlToValidate="txtComprimentoProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Dimensoes" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div>
            <label>
                Descrição
                <asp:Label runat="server" ID="lblDescricaoProdutoReq" Text="*" CssClass="requerid" />
            </label>
        </div>
        <div>
            <asp:TextBox ID="txtDescricaoProduto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricaoProduto" ControlToValidate="txtDescricaoProduto" SetFocusOnError="True" CssClass="requerid"
                ValidationGroup="G1Dimensoes" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
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
            <asp:CheckBox runat="server" ID="chkDimensoesProdutoAtivo" Checked="true" />
        </div>
    </div>
</div>
<div class="row">&nbsp;</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:LinkButton ID="lbCancelDimensoesProduto" runat="server"
            CssClass="btn btn-default" Text="Fechar" OnClick="lbCancelDimensoesProduto_Click">
        </asp:LinkButton>
        <asp:Button runat="server" ID="btnSalvarDimensoesProduto" ValidationGroup="G1Dimensoes" CausesValidation="true"
            CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarDimensoesProduto_Click" />
    </div>
</div>
