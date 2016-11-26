<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSwitcherOLD.ascx.cs" Inherits="Libra.ViewSwitcher" %>
<div id="viewSwitcher">
    <%: CurrentView %> view | <a href="<%: SwitchUrl %>" data-ajax="false">Switch to <%: AlternateView %></a>
</div>