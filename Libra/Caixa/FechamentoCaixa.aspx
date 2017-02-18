<%@ Page Title="Abertura/Fechamento de Caixa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FechamentoCaixa.aspx.cs" Inherits="Libra.Caixa.FechamentoCaixa" %>

<%@ Register Src="~/UserControl/Caixa/FormFechamentoCaixa.ascx" TagName="FormFechamentoCaixa"
    TagPrefix="cuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <cuc:FormFechamentoCaixa ID="cucFormFechamentoCaixa" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>
