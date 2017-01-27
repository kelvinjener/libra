<%@ Page Title="Parâmetros de Produtos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParametrosProdutos.aspx.cs" Inherits="Libra.Parametros.ParametrosProdutos" %>

<%@ Register Src="~/UserControl/Parametros/CadastroTipoProduto.ascx" TagName="CadastroTipoProduto"
    TagPrefix="cuc" %>
<%@ Register Src="~/UserControl/Parametros/CadastroFabricante.ascx" TagName="CadastroFabricante"
    TagPrefix="cuc" %>
<%@ Register Src="~/UserControl/Parametros/CadastroMarca.ascx" TagName="CadastroMarca"
    TagPrefix="cuc" %>
<%@ Register Src="~/UserControl/Parametros/CadastroModelo.ascx" TagName="CadastroModelo"
    TagPrefix="cuc" %>
<%@ Register Src="~/UserControl/Parametros/CadastroDimensoes.ascx" TagName="CadastroDimensoes"
    TagPrefix="cuc" %>
<%@ Register Src="~/UserControl/Parametros/CadastroCor.ascx" TagName="CadastroCor"
    TagPrefix="cuc" %>
<%@ Register Src="~/UserControl/Parametros/CadastroCaracteristicaDiversa.ascx" TagName="CadastroCaracteristicaDiversa"
    TagPrefix="cuc" %>

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
                            <asp:LinkButton ID="lkbOculto" runat="server" Text="" Style="display: none"></asp:LinkButton>

                            <div class="" role="tabpanel" data-example-id="togglable-tabs">
                                <ul id="myTab" class="nav nav-tabs bar_tabs" role="tablist">
                                    <li role="presentation" class="active" runat="server" id="tabTipoProduto"><a href="#tab_content_tipoProduto" id="tipoProduto-tab" role="tab" data-toggle="tab" aria-expanded="true">Tipo de Produto</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabFabricante"><a href="#tab_content_fabricante" id="fabricante-tab" role="tab" data-toggle="tab" aria-expanded="true">Fabricante</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabMarca"><a href="#tab_content_marca" id="marca-tab" role="tab" data-toggle="tab" aria-expanded="true">Marca</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabModelo"><a href="#tab_content_modelo" id="modelo-tab" role="tab" data-toggle="tab" aria-expanded="true">Modelo</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabDimensoes"><a href="#tab_content_dimensoes" id="dimensoes-tab" role="tab" data-toggle="tab" aria-expanded="true">Dimensões</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabCor"><a href="#tab_content_cor" id="cor-tab" role="tab" data-toggle="tab" aria-expanded="true">Cor</a>
                                    </li>
                                    <li role="presentation" class="" runat="server" id="tabCaracteristicasDiversas"><a href="#tab_content_caracteristicasDiversas" id="caracteristicasDiversas-tab" role="tab" data-toggle="tab" aria-expanded="true">Características Diversas</a>
                                    </li>
                                </ul>
                                <div id="myTabContent" class="tab-content">
                                    <div role="tabpanel" class="tab-pane fade active in" id="tab_content_tipoProduto" aria-labelledby="tipoProduto-tab">
                                        <asp:HiddenField runat="server" ID="hdnTipoProdutoId" />
                                        <div class="">
                                            <div id="divFiltroETabela" runat="server">
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div class="x_panel">
                                                            <div class="x_title">
                                                                <ul class="nav navbar-left panel_toolbox">
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="lbAddTipoProduto" CssClass="fa fa-plus" OnClick="lbAddTipoProduto_Click" ToolTip="Adicionar TipoProduto"></asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="lbEditTipoProduto" CssClass="fa fa-pencil" OnClick="lbEditTipoProduto_Click" ToolTip="Editar TipoProduto"></asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="lbDelTipoProduto" CssClass="fa fa-trash-o" OnClick="lbDelTipoProduto_Click" ToolTip="Deletar Tipos de Produto"></asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                                <div class="clearfix"></div>
                                                            </div>
                                                            <div class="x_content">

                                                                <asp:GridView ID="gvResultsTipoProduto" runat="server" AutoGenerateColumns="false" DataKeyNames="TipoProdutoId"
                                                                    AllowPaging="true" AllowSorting="true" Width="100%" DataSourceID="ldsFiltroTipoProduto" CssClass="table table-striped responsive-utilities jambo_table gvResults table-bordered dt-responsive nowrap"
                                                                    OnSelectedIndexChanged="gvResultsTipoProduto_SelectedIndexChanged" OnRowDataBound="gvResultsTipoProduto_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderStyle CssClass="gridCheckBox" />
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkBxHeader" CssClass="tableflat" onclick="javascript:HeaderClick(this);" runat="server" />
                                                                            </HeaderTemplate>
                                                                            <ItemStyle CssClass="gridCheckBox" />
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkBxSelect" runat="server" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="5%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tipo do Produto" SortExpression="Nome">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lkbEditNome" runat="server" CommandName="Select" Text='<%# Eval("Nome")%>'
                                                                                    CssClass="gridLink">
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="85%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ativo" SortExpression="Ativo">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAtivo" Text="Inativa" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label runat="server" ID="lblNoResults" Text="Nenhuma informação encontrada!" />
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                                <asp:LinqDataSource ID="ldsFiltroTipoProduto" runat="server" ContextTypeName="Libra.Entity.LibraDataContext"
                                                                    OnSelecting="ldsFiltroTipoProduto_Selecting" AutoSort="true" AutoGenerateWhereClause="true">
                                                                </asp:LinqDataSource>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:ModalPopupExtender ID="mpeTipoProduto" TargetControlID="lkbOculto" PopupControlID="pnlTipoProduto"
                                            BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCancelTipoProduto"
                                            ClientIDMode="AutoID">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel ID="pnlTipoProduto" runat="server" Style="display: none">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-body">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div class="x_panel">
                                                                    <div class="x_title">
                                                                        <div class="row">
                                                                            <div class="col-md-11 col-sm-11 col-xs-11">
                                                                                <h2>
                                                                                    <asp:Label ID="lbTituloModalEditAddTipoProduto" runat="server" Text="Adicionar Novo Tipo de Produto"></asp:Label></h2>
                                                                            </div>
                                                                            <div class="col-md-1 col-sm-1 col-xs-1">
                                                                                <asp:LinkButton runat="server" ID="lkCloseTipoProduto" OnClick="lkCloseTipoProduto_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="clearfix"></div>
                                                                    </div>
                                                                    <div class="x_content">
                                                                        <div class="row">
                                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                                <div>
                                                                                    <label>
                                                                                        Tipo de Produto
                                                                                        <asp:Label runat="server" ID="lblTipoProdutoReq" Text="*" CssClass="requerid" />
                                                                                    </label>
                                                                                </div>
                                                                                <div>
                                                                                    <asp:TextBox ID="txtTipoProduto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvTipoProduto" ControlToValidate="txtTipoProduto" SetFocusOnError="True" CssClass="requerid"
                                                                                        ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
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
                                                                                    <asp:CheckBox runat="server" ID="chkTipoProdutoAtivo" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">&nbsp;</div>
                                                                        <div class="row">
                                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                                <asp:LinkButton ID="lbCancelTipoProduto" runat="server"
                                                                                    CssClass="btn btn-default" Text="Fechar" OnClick="lbCancelTipoProduto_Click">
                                                                                </asp:LinkButton>
                                                                                <asp:Button runat="server" ID="btnSalvarTipoProduto" ValidationGroup="G1" CausesValidation="true"
                                                                                    CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarTipoProduto_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <!--Inicio Confirmação Exclusão Área-->
                                        <asp:ModalPopupExtender ID="mpeExclusaoTipoProduto" TargetControlID="lkbOculto" PopupControlID="pnlExcluirTipoProduto"
                                            BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="btnCancelarExclusaoTipoProduto"
                                            ClientIDMode="AutoID">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel ID="pnlExcluirTipoProduto" runat="server" Style="display: none">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-body">
                                                        <div runat="server" id="divExclusaoTipoProdutoProibida">
                                                            <div class="container-fluid">
                                                                <div class="row">
                                                                    <div class="col-md-2 ">
                                                                        <asp:Image ID="imgExclusaoTipoProdutoProibida" runat="server" ImageUrl="~/images/alertaExclusao.png"
                                                                            class="center-block" />
                                                                    </div>
                                                                    <div class="col-md-10 ">
                                                                        <div class="container-fluid">
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <div runat="server" id="divAlgumitem" visible="false">
                                                                                        <span class="titleExclusaoProibida">Alguns itens selecionados não podem ser excluídos</span><br />
                                                                                    </div>
                                                                                    <div runat="server" id="divOsitem" visible="false">
                                                                                        <span class="titleExclusaoProibida">Os itens selecionados não podem ser excluídos</span><br />
                                                                                    </div>
                                                                                    <span class="titleExclusaoEmUso">Eles já estão em uso em outros cadastros no sistema.</span>&nbsp<br />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <asp:Label ID="lblTipoProdutosExclusaoProibidas" runat="server" Text="" /><br />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="divExclusaoTipoProduto">
                                                            <div class="container-fluid">
                                                                <div class="row">
                                                                    <div class="linhaExclusao" runat="server" id="divLinhaExclusaoTipoProduto">
                                                                    </div>
                                                                    <br />
                                                                    <div class="col-md-2 ">
                                                                        <asp:Image ID="imgExclusaoTipoProduto" runat="server" ImageUrl="~/images/alertaExclusao.png"
                                                                            class="center-block" />
                                                                    </div>
                                                                    <div class="col-md-10 ">
                                                                        <div class="container-fluid">
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <span class="titleExclusao">Deseja excluir os itens abaixo?</span> &nbsp<br />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <asp:Label ID="lblTipoProdutosExclusao" runat="server" Text="" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <div class="panelButtons">
                                                            <asp:Button ID="btnExcluirTipoProduto" runat="server" OnClick="btnExcluirTipoProduto_Click" CausesValidation="false"
                                                                Text="SIM, EXCLUIR" class="btn btn-primary pull-right" />
                                                            <asp:Button ID="btnCancelarExclusaoTipoProduto" runat="server" OnClick="btnCancelarExclusaoTipoProduto_Click"
                                                                Text="FECHAR" CausesValidation="false" class="btn btn-default pull-right" />
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_fabricante" aria-labelledby="fabricante-tab">
                                        <asp:HiddenField runat="server" ID="hdnFabricanteProdutoId" />
                                        <div class="">
                                            <div id="div1" runat="server">
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div class="x_panel">
                                                            <div class="x_title">
                                                                <ul class="nav navbar-left panel_toolbox">
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="lbAddFabricanteProduto" CssClass="fa fa-plus" OnClick="lbAddFabricanteProduto_Click" ToolTip="Adicionar FabricanteProduto"></asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="lbEditFabricanteProduto" CssClass="fa fa-pencil" OnClick="lbEditFabricanteProduto_Click" ToolTip="Editar FabricanteProduto"></asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="lbDelFabricanteProduto" CssClass="fa fa-trash-o" OnClick="lbDelFabricanteProduto_Click" ToolTip="Deletar Fabricantes de Produto"></asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                                <div class="clearfix"></div>
                                                            </div>
                                                            <div class="x_content">

                                                                <asp:GridView ID="gvResultsFabricanteProduto" runat="server" AutoGenerateColumns="false" DataKeyNames="FabricanteProdutoId"
                                                                    AllowPaging="true" AllowSorting="true" Width="100%" DataSourceID="ldsFiltroFabricanteProduto" CssClass="table table-striped responsive-utilities jambo_table gvResults table-bordered dt-responsive nowrap"
                                                                    OnSelectedIndexChanged="gvResultsFabricanteProduto_SelectedIndexChanged" OnRowDataBound="gvResultsFabricanteProduto_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderStyle CssClass="gridCheckBox" />
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkBxHeader" CssClass="tableflat" onclick="javascript:HeaderClickFabricanteProduto(this);" runat="server" />
                                                                            </HeaderTemplate>
                                                                            <ItemStyle CssClass="gridCheckBox" />
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkBxSelectFabricanteProduto" runat="server" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="5%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fabricante do Produto" SortExpression="Nome">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lkbEditNome" runat="server" CommandName="Select" Text='<%# Eval("Nome")%>'
                                                                                    CssClass="gridLink">
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="85%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ativo" SortExpression="Ativo">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAtivo" Text="Inativa" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label runat="server" ID="lblNoResults" Text="Nenhuma informação encontrada!" />
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                                <asp:LinqDataSource ID="ldsFiltroFabricanteProduto" runat="server" ContextTypeName="Libra.Entity.LibraDataContext"
                                                                    OnSelecting="ldsFiltroFabricanteProduto_Selecting" AutoSort="true" AutoGenerateWhereClause="true">
                                                                </asp:LinqDataSource>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:ModalPopupExtender ID="mpeFabricanteProduto" TargetControlID="lkbOculto" PopupControlID="pnlFabricanteProduto"
                                            BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lkCloseFabricanteProduto"
                                            ClientIDMode="AutoID">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel ID="pnlFabricanteProduto" runat="server" Style="display: none">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-body">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div class="x_panel">
                                                                    <div class="x_title">
                                                                        <div class="row">
                                                                            <div class="col-md-11 col-sm-11 col-xs-11">
                                                                                <h2>
                                                                                    <asp:Label ID="lbTituloModalEditAddFabricanteProduto" runat="server" Text="Adicionar Novo Fabricante de Produto"></asp:Label></h2>
                                                                            </div>
                                                                            <div class="col-md-1 col-sm-1 col-xs-1">
                                                                                <asp:LinkButton runat="server" ID="lkCloseFabricanteProduto" OnClick="lkCloseFabricanteProduto_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="clearfix"></div>
                                                                    </div>
                                                                    <div class="x_content">
                                                                        <cuc:CadastroFabricante ID="cucCadastroFabricante" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <!--Inicio Confirmação Exclusão Área-->
                                        <asp:ModalPopupExtender ID="mpeExclusaoFabricanteProduto" TargetControlID="lkbOculto" PopupControlID="pnlExcluirFabricanteProduto"
                                            BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="btnCancelarExclusaoFabricanteProduto"
                                            ClientIDMode="AutoID">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel ID="pnlExcluirFabricanteProduto" runat="server" Style="display: none">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-body">
                                                        <div runat="server" id="divExclusaoFabricanteProdutoProibida">
                                                            <div class="container-fluid">
                                                                <div class="row">
                                                                    <div class="col-md-2 ">
                                                                        <asp:Image ID="imgExclusaoFabricanteProdutoProibida" runat="server" ImageUrl="~/images/alertaExclusao.png"
                                                                            class="center-block" />
                                                                    </div>
                                                                    <div class="col-md-10 ">
                                                                        <div class="container-fluid">
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <div runat="server" id="div2" visible="false">
                                                                                        <span class="titleExclusaoProibida">Alguns itens selecionados não podem ser excluídos</span><br />
                                                                                    </div>
                                                                                    <div runat="server" id="div3" visible="false">
                                                                                        <span class="titleExclusaoProibida">Os itens selecionados não podem ser excluídos</span><br />
                                                                                    </div>
                                                                                    <span class="titleExclusaoEmUso">Eles já estão em uso em outros cadastros no sistema.</span>&nbsp<br />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <asp:Label ID="lblFabricanteProdutosExclusaoProibidas" runat="server" Text="" /><br />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="divExclusaoFabricanteProduto">
                                                            <div class="container-fluid">
                                                                <div class="row">
                                                                    <div class="linhaExclusao" runat="server" id="divLinhaExclusaoFabricanteProduto">
                                                                    </div>
                                                                    <br />
                                                                    <div class="col-md-2 ">
                                                                        <asp:Image ID="imgExclusaoFabricanteProduto" runat="server" ImageUrl="~/images/alertaExclusao.png"
                                                                            class="center-block" />
                                                                    </div>
                                                                    <div class="col-md-10 ">
                                                                        <div class="container-fluid">
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <span class="titleExclusao">Deseja excluir os itens abaixo?</span> &nbsp<br />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <asp:Label ID="lblFabricanteProdutosExclusao" runat="server" Text="" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <div class="panelButtons">
                                                            <asp:Button ID="btnExcluirFabricanteProduto" runat="server" OnClick="btnExcluirFabricanteProduto_Click" CausesValidation="false"
                                                                Text="SIM, EXCLUIR" class="btn btn-primary pull-right" />
                                                            <asp:Button ID="btnCancelarExclusaoFabricanteProduto" runat="server" OnClick="btnCancelarExclusaoFabricanteProduto_Click"
                                                                Text="FECHAR" CausesValidation="false" class="btn btn-default pull-right" />
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
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

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_dimensoes" aria-labelledby="dimensoes-tab">
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

                                    <div role="tabpanel" class="tab-pane fade" id="tab_content_caracteristicasDiversas" aria-labelledby="caracteristicasDiversas-tab">
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

    <%-- Tipo Produto --%>
    <script type="text/javascript" charset="utf-8">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvResultsTipoProduto.Rows.Count %>');

            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
       document.getElementById('<%= this.gvResultsTipoProduto.ClientID %>');
            var TargetChildControl = "chkBxSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter; 
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }

    </script>

    <%-- Fabricante --%>
    <script type="text/javascript" charset="utf-8">
        var TotalChkBxFabricante;
        var CounterFabricante;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBxFabricante = parseInt('<%= this.gvResultsFabricanteProduto.Rows.Count %>');

            //Get total no. of checked CheckBoxes in side the GridView.
            CounterFabricante = 0;
        }

        function HeaderClickFabricante(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
       document.getElementById('<%= this.gvResultsFabricanteProduto.ClientID %>');
            var TargetChildControl = "chkBxSelectFabricante";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset CounterFabricante
            CounterFabricante = CheckBox.checked ? TotalChkBxFabricante : 0;
        }

        function ChildClickFabricante(CheckBox, HCheckBox) {
            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy CounterFabricante; 
            if (CheckBox.checked && CounterFabricante < TotalChkBxFabricante)
                CounterFabricante++;
            else if (CounterFabricante > 0)
                CounterFabricante--;

            //Change state of the header CheckBox.
            if (CounterFabricante < TotalChkBxFabricante)
                HeaderCheckBox.checked = false;
            else if (CounterFabricante == TotalChkBxFabricante)
                HeaderCheckBox.checked = true;
        }

    </script>
</asp:Content>
