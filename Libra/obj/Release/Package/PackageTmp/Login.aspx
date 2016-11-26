<%@ Page Title="Login" Language="C#" MasterPageFile="~/SiteAccount.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Libra.Login" Async="true" %>

<%@ Register Src="~/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--<h2><%: Title %>.</h2>--%>

    <div class="">
        <div id="wrapper">
            <div class="x_panel">
                <div class="x_title">
                    <div>
                        <img src="../images/logo.png" style="width: 150px!important;" />
                    </div>
                    <div class="clearfix"></div>
                    <br />
                    <%--<h3>Bem vindo!</h3>
                    <p>Insira seu usuário e senha para acessar o sistema.</p>--%>
                </div>
                <div class="x_content">
                    <section id="loginForm">
                        <div class="form-group">
                            <asp:DropDownList runat="server" ID="ddlUnidade" CssClass="form-control" PlaceHolder="Unidade" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUnidade"
                                CssClass="text-danger" ErrorMessage="Unidade obrigatória!" />
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" PlaceHolder="E-mail" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="E-mail obrigatório!" />
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" PlaceHolder="Senha" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Senha obrigatória." />
                        </div>
                        <div class="form-group">
                            <label>
                                <asp:CheckBox runat="server" ID="RememberMe" CssClass="js-switch" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Lembrar-me?</asp:Label>
                            </label>
                        </div>
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                        <div class="form-group">
                            <asp:Button runat="server" OnClick="LogIn" Text="Entrar" CssClass="btn btn-default submit" Width="100%" />
                        </div>

                        <div class="clearfix"></div>
                        <div class="separator">
                            <p class="change_link">
                                <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Esqueceu sua senha?</asp:HyperLink>
                            </p>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolderScripts">
    <script>
        $(document).ready(function () {
            //NotifySucess("Teste", "Funcionou");
            //NotifyInfo("Teste", "Funcionou!!");
            //NotifyRegular("teste", "teste ok");
            //NotifyError("teste", "teste ok");
            //NotifyDark("teste", "teste ok");

            //TabbedNotifySucess("Teste", "Funcionou");
            //TabbedNotifyInfo("Teste", "Funcionou!!");
            //TabbedNotifyRegular("teste", "teste ok");
            //TabbedNotifyError("teste", "teste ok");
            //TabbedNotifyDark("teste", "teste ok");

        });
    </script>
</asp:Content>

