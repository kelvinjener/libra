<%@ Page Title="Lembrar senha" Language="C#" MasterPageFile="~/SiteAccount.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="Libra.Account.ForgotPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--<h2><%: Title %>.</h2>--%>
    <div id="wrapper" class="wrapper_lg">
        <div class="x_panel" id="login">
            <asp:PlaceHolder ID="loginForm" runat="server">
                <div class="x_title">
                    <h2>Olá! Será que já nos conhecemos?</h2>
                    <div class="clearfix"></div>
                    <p>Insira seu e-mail.</p>
                </div>
                <div class="x_content">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="E-mail" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                            CssClass="text-danger" ErrorMessage="The email field is required." />
                    </div>
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Button runat="server" OnClick="Forgot" Text="Enviar" CssClass="btn btn-primary" />
                        <a runat="server" href="~/Account/Login" title="Cancelar" class="btn btn-default">Cancelar</a>
                    </div>

                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">
                <div class="x_content">
                    <p class="text-info">
                        Por favor, verifique seu e-mail!
                    </p>

                    <div class="form-group">
                        <a runat="server" href="~/Account/Login" title="Ok!" class="btn btn-default">Ok!</a>
                    </div>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
