<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/SiteAccount.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Libra.ResetPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--   <h2><%: Title %>.</h2>--%>
    <%-- <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>--%>

    <div id="wrapper" class="wrapper_lg">
        <div class="x_panel" id="login">
            <asp:PlaceHolder ID="loginForm" runat="server">
                <div class="x_title">
                    <h2>Nova senha!</h2>
                    <div class="clearfix"></div>

                    <p>Informe seu e-mail, senha e confirme sua senha.</p>
                </div>
                <div class="x_content">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="E-mail" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                            CssClass="text-danger" ErrorMessage="The email field is required." />
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" placeholder="Senha" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                            CssClass="text-danger" ErrorMessage="The password field is required." />
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" placeholder="Confirmar Senha" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                        <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                    </div>
                    <div class="clearfix"></div>

                    <div class="form-group">
                        <asp:Button runat="server" OnClick="Reset_Click" Text="Reset" CssClass="btn btn-primary" />
                        <a runat="server" href="~/Login" title="Cancelar" class="btn btn-default">Cancelar</a>
                    </div>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
