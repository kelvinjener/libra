<%@ Page Title="Parâmetros de Vendas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParametrosVendas.aspx.cs" Inherits="Libra.Parametros.ParametrosVendas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnIdVendas" />
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Parâmetros de Vendas
                </h3>
            </div>
        </div>
        <div class="clearfix"></div>
        <div id="divPrincipal" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2><i class="fa fa-bookmark"></i><small>Selecione a aba do Parâmetro que deseja visualizar/alterar.</small></h2>

                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <div class="" role="tabpanel" data-example-id="togglable-tabs">
                                <ul id="myTab" class="nav nav-tabs bar_tabs" role="tablist">
                                    <li role="presentation" class="active" runat="server" id="tabFormaPgto"><a href="#tab_content_formaPgto" id="formaPgto-tab" role="tab" data-toggle="tab" aria-expanded="true">Forma de Pagamento</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabCartao"><a href="#tab_content_cartao" id="cartao-tab" role="tab" data-toggle="tab" aria-expanded="true">Cartão de Crédito/Débito</a>
                                    </li>
                                   
                                </ul>
                                <div id="myTabContent" class="tab-content">
                                    <div role="tabpanel" class="tab-pane fade active in" id="tab_content_formaPgto" aria-labelledby="formaPgto-tab">
                                        <p>
                                            Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                          synth. Cosby sweater eu banh mi, qui irure terr.
                                        </p>
                                    </div>

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_cartao" aria-labelledby="cartao-tab">
                                        <p>
                                            Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                          synth. Cosby sweater eu banh mi, qui irure terr.
                                        </p>
                                    </div>

                                  
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>
