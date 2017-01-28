<%@ Page Title="Cadastro de Produtos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProdutos.aspx.cs" Inherits="Libra.Produtos.CadastroProdutos" %>

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
    <asp:HiddenField ID="hdnIdProduto" runat="server" />
    <asp:HiddenField ID="hdnIdTipoProduto" runat="server" />
    <asp:HiddenField ID="hdnIdFabricante" runat="server" />
    <asp:HiddenField ID="hdnIdMarca" runat="server" />
    <asp:HiddenField ID="hdnIdModelo" runat="server" />
    <asp:HiddenField ID="hdnIdCor" runat="server" />
    <asp:HiddenField ID="hdnIdDimensoes" runat="server" />

    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Produtos
                </h3>
            </div>
        </div>
        <div class="clearfix"></div>
        <div id="divFiltroETabela" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2><i class="fa fa-bookmark"></i><small>Lista de Produtos cadastrados.</small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li>
                                    <asp:LinkButton runat="server" ID="lbFiltroProdutos" CssClass="fa fa-filter" OnClick="lbFiltroProdutos_Click" ToolTip="Filtrar Produtos"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbVisualizarProduto" CssClass="fa fa-ellipsis-h" OnClick="lbVisualizarProduto_Click" ToolTip="Visualizar Produto"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbAddProdutos" CssClass="fa fa-plus" OnClick="lbAddProdutos_Click" ToolTip="Adicionar Produto"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbEditProdutos" CssClass="fa fa-pencil" OnClick="lbEditProdutos_Click" ToolTip="Editar Produto"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbDelProdutos" CssClass="fa fa-trash-o" OnClick="lbDelProdutos_Click" ToolTip="Deletar Produtos"></asp:LinkButton>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">

                            <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="false" DataKeyNames="ProdutoId"
                                AllowPaging="true" AllowSorting="true" Width="100%" DataSourceID="ldsFiltro" CssClass="table table-striped responsive-utilities jambo_table gvResults table-bordered dt-responsive nowrap"
                                OnSelectedIndexChanged="gvResults_SelectedIndexChanged" OnRowDataBound="gvResults_RowDataBound">
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
                                    <asp:TemplateField HeaderText="Código do Produto" SortExpression="CodigoProduto">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbCodigoProduto" runat="server" CommandName="Select" Text='<%# Eval("CodigoProduto")%>'
                                                CssClass="gridLink">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descrição" SortExpression="Descricao">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescricaoProduto" runat="server" Text='<%# Eval("Descricao")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disponível para Comércio" SortExpression="DisponivelComercio">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDisponivelComercio" />
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label runat="server" ID="lblNoResults" Text="Nenhuma informação encontrada!" />
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:LinqDataSource ID="ldsFiltro" runat="server" ContextTypeName="Libra.Entity.LibraDataContext"
                                OnSelecting="ldsFiltro_Selecting" AutoSort="true" AutoGenerateWhereClause="true">
                            </asp:LinqDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divEdicao" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>
                                <asp:Label ID="lbAddEditProduto" runat="server" Text="Novo Produto"></asp:Label>
                            </h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Código do Produto
                                        </label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblCodigoProduto" Text="Gerado pelo sistema" CssClass="labelInfo" />

                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Tipo de Produto
                                            <asp:Label runat="server" ID="lblTipoProdutoReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlTipoProduto" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTipoProduto_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:LinkButton runat="server" ID="lkAddTipoProduto" CssClass="fa fa-plus" OnClick="lkAddTipoProduto_Click" ToolTip="Adicionar Tipo Produto"></asp:LinkButton>
                                        <asp:RequiredFieldValidator ID="rfvTipoProduto" ControlToValidate="ddlTipoProduto" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Fabricante
                                            <asp:Label runat="server" ID="lblFabricanteReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlFabricante" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFabricante_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:LinkButton runat="server" ID="lkAddFabricante" CssClass="fa fa-plus" OnClick="lkAddFabricante_Click" ToolTip="Adicionar Fabricante"></asp:LinkButton>
                                        <asp:RequiredFieldValidator ID="rfvFabricante" ControlToValidate="ddlFabricante" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Marca
                                            <asp:Label runat="server" ID="lblMarcaReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:LinkButton runat="server" ID="lkAddMarca" CssClass="fa fa-plus" OnClick="lkAddMarca_Click" ToolTip="Adicionar Marca"></asp:LinkButton>
                                        <asp:RequiredFieldValidator ID="rfvMarca" ControlToValidate="ddlMarca" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Modelo
                                            <asp:Label runat="server" ID="lblModeloReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlModelo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlModelo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:LinkButton runat="server" ID="lkAddModelo" CssClass="fa fa-plus" OnClick="lkAddModelo_Click" ToolTip="Adicionar Modelo"></asp:LinkButton>

                                        <asp:RequiredFieldValidator ID="rfvModelo" ControlToValidate="ddlModelo" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Dimensões
                                            <asp:Label runat="server" ID="lblDimensoesReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlDimensoes" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDimensoes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:LinkButton runat="server" ID="lkAddDimensoes" CssClass="fa fa-plus" OnClick="lkAddDimensoes_Click" ToolTip="Adicionar Dimensões"></asp:LinkButton>

                                        <asp:RequiredFieldValidator ID="rfvDimensoes" ControlToValidate="ddlDimensoes" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Cor
                                            <asp:Label runat="server" ID="lblCorReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlCor" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCor_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:LinkButton runat="server" ID="lkAddCor" CssClass="fa fa-plus" OnClick="lkAddCor_Click" ToolTip="Adicionar Cor"></asp:LinkButton>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlMarca" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Peso (Kg)
                                            <asp:Label runat="server" ID="lblPesoReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control text-text-uppercase" MaxLength="8"></asp:TextBox>


                                        <asp:RequiredFieldValidator ID="rfvPeso" ControlToValidate="txtPeso" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>


                            <div class="row">
                                <div class="col-md-8 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Descrição
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control text-uppercase" MaxLength="255"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">&nbsp;</div>


                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-xs-12">
                                    <div>
                                        <label>
                                            Disponível Comércio?
                                        </label>
                                    </div>
                                    <div>
                                        <asp:CheckBox runat="server" ID="ckbDisponivel" />
                                    </div>
                                </div>
                                <%-- <div class="col-md-2 col-sm-2 col-xs-12">
                                    <div>
                                        <label>
                                            Ativo?
                                        </label>
                                    </div>
                                    <div>
                                        <asp:CheckBox runat="server" ID="chkAtivo" />
                                    </div>
                                </div>--%>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-xs-6">
                                    <asp:Button ID="btnCancelar" runat="server" Width="100%"
                                        CssClass="btn btn-default" Text="Cancelar" OnClick="btnCancelar_Click" />
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-6">
                                    <asp:Button runat="server" ID="btnSalvar" ValidationGroup="G1" CssClass="btn btn-primary" Width="100%" Text="Salvar" OnClick="btnSalvar_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:ModalPopupExtender ID="mpeCadastroTipoProdutos" TargetControlID="lkbOculto" PopupControlID="pnlCadastroTipoProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCloseCadastroTipoProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCadastroTipoProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>CADASTRO DE TIPO DE PRODUTO</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lbCloseCadastroTipoProduto" OnClick="lbCloseCadastroTipoProduto_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <cuc:CadastroTipoProduto ID="cucCadastroTipoProduto" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpeCadastroFabricanteProduto" TargetControlID="lkbOculto" PopupControlID="pnlCadastroFabricateProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCloseCadastroFabricanteProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCadastroFabricateProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>CADASTRO DE FABRICANTE DE PRODUTO</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lbCloseCadastroFabricanteProduto" OnClick="lbCloseCadastroFabricanteProduto_Click"><i class="fa fa-close"></i></asp:LinkButton>
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

    <asp:ModalPopupExtender ID="mpeCadastroMarcaProduto" TargetControlID="lkbOculto" PopupControlID="pnlCadastroMarcaProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCloseCadastroMarcaProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCadastroMarcaProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>CADASTRO DE MARCA DE PRODUTO</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lbCloseCadastroMarcaProduto" OnClick="lbCloseCadastroMarcaProduto_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <cuc:CadastroMarca ID="cucCadastroMarca" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpeCadastroModeloProduto" TargetControlID="lkbOculto" PopupControlID="pnlCadastroModeloProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCloseCadastroModeloProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCadastroModeloProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>CADASTRO DE MODELO DE PRODUTO</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lbCloseCadastroModeloProduto" OnClick="lbCloseCadastroModeloProduto_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <cuc:CadastroModelo ID="cucCadastroModelo" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpeCadastroDimensoesProduto" TargetControlID="lkbOculto" PopupControlID="pnlCadastroDimensoesProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCloseCadastroDimensoesProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCadastroDimensoesProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>CADASTRO DE DIMENSÕES DE PRODUTO</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lbCloseCadastroDimensoesProduto" OnClick="lbCloseCadastroDimensoesProduto_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <cuc:CadastroDimensoes ID="cucCadastroDimensoes" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpeCadastroCorProduto" TargetControlID="lkbOculto" PopupControlID="pnlCadastroCorProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCloseCadastroCorProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCadastroCorProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>CADASTRO DE COR DE PRODUTO</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lbCloseCadastroCorProduto" OnClick="lbCloseCadastroCorProduto_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <cuc:CadastroCor ID="cucCadastroCor" runat="server" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpeFiltroProdutos" TargetControlID="lbFiltroProdutos" PopupControlID="pnlFiltroProdutos"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCancelFiltroProdutos"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlFiltroProdutos" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>FILTRO PRODUTOS</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lkClose" OnClick="lkClose_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-4 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Código do Produto
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtCodigoProdutoFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-8 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Descrição 
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtDescricaoFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Tipo Produto
                                                </label>
                                            </div>
                                            <div>
                                                <asp:DropDownList runat="server" ID="ddlTipoProdutoFiltro" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Fabricante 
                                                </label>
                                            </div>
                                            <div>
                                                <asp:DropDownList runat="server" ID="ddlFabricanteFiltro" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Marca
                                                </label>
                                            </div>
                                            <div>
                                                <asp:DropDownList runat="server" ID="ddlMarcaFiltro" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Modelo 
                                                </label>
                                            </div>
                                            <div>
                                                <asp:DropDownList runat="server" ID="ddlModeloFiltro" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Cor
                                                </label>
                                            </div>
                                            <div>
                                                <asp:DropDownList runat="server" ID="ddlCorFiltro" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Disponível Comércio</label>
                                            </div>
                                            <div>
                                                <asp:CheckBox runat="server" ID="ckbDisponivelComercioFiltro" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Disponível</span>&nbsp;
                                        <asp:CheckBox runat="server" ID="ckbIndisponivelComercioFiltro" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Indisponível</span>&nbsp;
                                            </div>
                                        </div>
                                        <%-- <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Ativo</label>
                                            </div>
                                            <div>
                                                <asp:CheckBox runat="server" ID="ckbAtivoFiltro" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Ativo</span>&nbsp;
                                        <asp:CheckBox runat="server" ID="ckbInativoFiltro" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Inativo</span>&nbsp;
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <asp:LinkButton ID="lbCancelFiltroProdutos" runat="server"
                                                CssClass="btn btn-default" Text="Fechar">
                                            </asp:LinkButton>
                                            <asp:Button runat="server" ID="btnFiltrar" CssClass="btn btn-primary" Text="Filtrar" OnClick="btnFiltrar_Click" />
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
    <asp:LinkButton ID="lkbOculto" runat="server" Text="" Style="display: none"></asp:LinkButton>

    <asp:ModalPopupExtender ID="mpeVisualizarProduto" TargetControlID="lkbOculto" PopupControlID="pnlVisualizarProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lnbFecharVisualizarProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlVisualizarProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>Produto
                                        <asp:Label ID="lbProduto" runat="server"></asp:Label></h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lkCloseVisualizarProduto" OnClick="lkCloseVisualizarProduto_Click" CssClass="right"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Código do Produto:
                                                </label>
                                                <asp:Label ID="lblVisualizarCodigoProduto" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Produto:
                                                </label>
                                                <asp:Label ID="lblVisualizarNomeProduto" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-4 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Tipo Produto:
                                                </label>
                                                <asp:Label ID="lblVisualizarTipoProduto" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Dimensões:
                                                </label>
                                                <asp:Label ID="lblVisualizarDimensoes" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Peso:
                                                </label>
                                                <asp:Label ID="lblVisualizarPeso" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-4 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Fabricante:
                                                </label>
                                                <asp:Label ID="lblVisualizarFabricante" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Marca:
                                                </label>
                                                <asp:Label ID="lblVisualizarMarca" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Modelo:
                                                </label>
                                                <asp:Label ID="lblVisualizarModelo" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Disponível Comércio?
                                                </label>
                                                <asp:Label ID="lblVisualizarDisponivelComercio" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">&nbsp;</div>

                                    <%--<div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Ativo?
                                                </label>
                                                <asp:Label ID="lblAtivo" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>--%>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <asp:LinkButton ID="lnbFecharVisualizarProduto" runat="server"
                                                CssClass="btn btn-default" Text="Fechar">
                                            </asp:LinkButton>
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
    <asp:ModalPopupExtender ID="mpeExclusaoProduto" TargetControlID="lkbOculto" PopupControlID="pnlExcluirProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="btnCancelarExclusaoProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlExcluirProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div runat="server" id="divExclusaoProdutoProibida">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-2 ">
                                    <asp:Image ID="imgExclusaoProdutoProibida" runat="server" ImageUrl="~/images/alertaExclusao.png"
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
                                                <asp:Label ID="lblProdutosExclusaoProibidas" runat="server" Text="" /><br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="divExclusaoProduto">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="linhaExclusao" runat="server" id="divLinhaExclusaoProduto">
                                </div>
                                <br />
                                <div class="col-md-2 ">
                                    <asp:Image ID="imgExclusaoProduto" runat="server" ImageUrl="~/images/alertaExclusao.png"
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
                                                <asp:Label ID="lblProdutosExclusao" runat="server" Text="" />
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
                        <asp:Button ID="btnExcluirProduto" runat="server" OnClick="btnExcluirProduto_Click" CausesValidation="false"
                            Text="SIM, EXCLUIR" class="btn btn-primary pull-right" />
                        <asp:Button ID="btnCancelarExclusaoProduto" runat="server" OnClick="btnCancelarExclusaoProduto_Click"
                            Text="FECHAR" CausesValidation="false" class="btn btn-default pull-right" />
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <script type="text/javascript" charset="utf-8">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvResults.Rows.Count %>');

            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
       document.getElementById('<%= this.gvResults.ClientID %>');
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
</asp:Content>
