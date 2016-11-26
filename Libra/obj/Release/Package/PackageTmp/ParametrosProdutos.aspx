<%@ Page Title="Parâmetros de Produtos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParametrosProdutos.aspx.cs" Inherits="Libra.ParametrosProdutos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnIdProdutos" />
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Parâmetros de Produtos
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
                                    <li role="presentation" class="active" runat="server" id="tabTipoProduto"><a href="#tab_content_tipoProduto" id="tipoProduto-tab" role="tab" data-toggle="tab" aria-expanded="true">Tipo de Produto</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabCategoria"><a href="#tab_content_categoria" id="categoria-tab" role="tab" data-toggle="tab" aria-expanded="true">Categoria</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabTamanho"><a href="#tab_content_tamanho" id="tamanho-tab" role="tab" data-toggle="tab" aria-expanded="true">Tamanho</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabMarca"><a href="#tab_content_marca" id="marca-tab" role="tab" data-toggle="tab" aria-expanded="true">Marca</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabModelo"><a href="#tab_content_modelo" id="modelo-tab" role="tab" data-toggle="tab" aria-expanded="true">Modelo</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabCor"><a href="#tab_content_cor" id="cor-tab" role="tab" data-toggle="tab" aria-expanded="true">Cor</a>
                                    </li>
                                </ul>
                                <div id="myTabContent" class="tab-content">
                                    <div role="tabpanel" class="tab-pane fade active in" id="tab_content_tipoProduto" aria-labelledby="tipoProduto-tab">
                                        <p>
                                            Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                          synth. Cosby sweater eu banh mi, qui irure terr.
                                        </p>
                                    </div>

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_categoria" aria-labelledby="categoria-tab">
                                        <p>
                                            Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                          synth. Cosby sweater eu banh mi, qui irure terr.
                                        </p>
                                    </div>

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_tamanho" aria-labelledby="tamanho-tab">
                                        <p>
                                            Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                          synth. Cosby sweater eu banh mi, qui irure terr.
                                        </p>
                                    </div>

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_marca" aria-labelledby="marca-tab">
                                        <p>
                                            Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                          synth. Cosby sweater eu banh mi, qui irure terr.
                                        </p>
                                    </div>

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_modelo" aria-labelledby="modelo-tab">
                                        <p>
                                            Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                          synth. Cosby sweater eu banh mi, qui irure terr.
                                        </p>
                                    </div>

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_cor" aria-labelledby="cor-tab">
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
