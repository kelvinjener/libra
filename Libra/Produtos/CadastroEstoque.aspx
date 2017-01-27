<%@ Page Title="Cadastro de Estoque" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroEstoque.aspx.cs" Inherits="Libra.Produtos.CadastroEstoque" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnIdEstoque" runat="server" />
    <asp:HiddenField ID="hdnIdProduto" runat="server" />

    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Estoque de Produtos
                </h3>
            </div>
        </div>
        <div class="clearfix"></div>
        <div id="divFiltroETabela" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2><i class="fa fa-bookmark"></i><small>Lista de estoque cadastrados.</small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li>
                                    <asp:LinkButton runat="server" ID="lbFiltroEstoque" CssClass="fa fa-filter" OnClick="lbFiltroEstoque_Click" ToolTip="Filtrar"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbVisualizarEstoque" CssClass="fa fa-ellipsis-h" OnClick="lbVisualizarEstoque_Click" ToolTip="Visualizar Estoque de Produto"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbAddEstoque" CssClass="fa fa-plus" OnClick="lbAddEstoque_Click" ToolTip="Adicionar Produto ao Estoque"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbEditEstoque" CssClass="fa fa-pencil" OnClick="lbEditEstoque_Click" ToolTip="Editar Estoque de Produto"></asp:LinkButton>
                                </li>
                                <%-- <li>
                                    <asp:LinkButton runat="server" ID="lbDelEstoque" CssClass="fa fa-trash-o" OnClick="lbDelEstoque_Click" ToolTip="Deletar Estoque de Produto"></asp:LinkButton>
                                </li>--%>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">

                            <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="false" DataKeyNames="EstoqueId, ProdutoId, UnidadeId"
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
                                    <asp:TemplateField HeaderText="Código do Estoque" SortExpression="CodigoEstoque">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbCodigoEstoque" runat="server" CommandName="Select" Text='<%# Eval("CodigoEstoque")%>'
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
                                        <ItemStyle Width="50%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor Compra" SortExpression="ValorCompra">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblValorCompra" Text='<%# Eval("ValorCompra")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor Venda" SortExpression="ValorVenda">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblValorVenda" Text='<%# Eval("ValorVenda")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unidade" SortExpression="Unidade">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblUnidade" Text='<%# Eval("Unidade")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtd. Disponível" SortExpression="QtdDisponivel">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblQtdDisponivel" Text='<%# Eval("QtdDisponivel")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
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
                                <asp:Label ID="lbAddEditEstoqueProduto" runat="server" Text="Adicionar Produto ao Estoque"></asp:Label>
                            </h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Código do Estoque
                                        </label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblCodigoEstoque" Text="Gerado pelo sistema" CssClass="labelInfo" />

                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Unidade
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList runat="server" ID="ddlUnidade" CssClass="form-control" />

                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Produto
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList runat="server" ID="ddlProduto" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlProduto_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div>
                                        <div class="x_panel">
                                            <div class="x_title">
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <label>Informações do Produto</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="x_content">
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>
                                                                Código do produto: 
                                                            </label>

                                                            <asp:Label runat="server" ID="lblCodigoProduto" CssClass="labelInfo" Text="---" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>
                                                                Produto: 
                                                            </label>

                                                            <asp:Label runat="server" ID="lblProduto" CssClass="labelInfo" Text="---" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>
                                                                Tipo produto: 
                                                            </label>

                                                            <asp:Label runat="server" ID="lblTipoProduto" CssClass="labelInfo" Text="---" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>
                                                                Fabricante: 
                                                            </label>

                                                            <asp:Label runat="server" ID="lblFabricante" CssClass="labelInfo" Text="---" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>
                                                                Marca: 
                                                            </label>

                                                            <asp:Label runat="server" ID="lblMarca" CssClass="labelInfo" Text="---" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>
                                                                Modelo: 
                                                            </label>

                                                            <asp:Label runat="server" ID="lblModelo" CssClass="labelInfo" Text="---" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>
                                                                Dimensões: 
                                                            </label>

                                                            <asp:Label runat="server" ID="lblDimensoes" CssClass="labelInfo" Text="---" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>
                                                                Cor: 
                                                            </label>

                                                            <asp:Label runat="server" ID="lblCor" CssClass="labelInfo" Text="---" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Valor Custo</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtValorCusto" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Margem de Lucro</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtMargemLucro" runat="server" CssClass="form-control"
                                                    OnTextChanged="txtMargemLucro_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Valor Venda</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtValorVenda" runat="server" CssClass="form-control" 
                                                    OnTextChanged="txtValorVenda_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Quantidade Atual em Estoque</label>
                                            </div>
                                            <div>
                                                <asp:Label runat="server" ID="lblQtdAtualEstoque" CssClass="labelInfo" Text="0" />
                                            </div>
                                        </div>

                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Quantidade Adicionada ao Estoque</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtQuatidadeAddEstoque" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Data da Última Alteração do Estoque</label>
                                            </div>
                                            <div>
                                                <asp:Label runat="server" ID="lblDataUltimaAlteracaoEstoque" CssClass="labelInfo" Text="---" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-xs-6">
                                    <asp:Button ID="btnCancelar" runat="server" Width="100%"
                                        CssClass="btn btn-default" Text="Cancelar" OnClick="btnCancelar_Click" />
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-6">
                                    <asp:Button runat="server" ID="btnSalvar" ValidationGroup="G1"
                                        CssClass="btn btn-primary" Width="100%" Text="Salvar" OnClick="btnSalvar_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="lkbOculto" runat="server" Text="" Style="display: none"></asp:LinkButton>

    <asp:ModalPopupExtender ID="mpeVisualizarEstoqueProduto" TargetControlID="lkbOculto" PopupControlID="pnlVisualizarEstoqueProduto"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lnbFecharVisualizarProduto"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlVisualizarEstoqueProduto" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>Estoque do Produto
                                        <asp:Label ID="lbEstoqueProduto" runat="server"></asp:Label></h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lkCloseVisualizarEstoqueProduto" OnClick="lkCloseVisualizarEstoqueProduto_Click" CssClass="right"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-4 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Código do Estoque
                                                </label>
                                            </div>
                                            <div>
                                                <asp:Label runat="server" ID="lblVisualizarCodigoEstoque" Text="Gerado pelo sistema" CssClass="labelInfo" />

                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Unidade
                                                </label>
                                            </div>
                                            <div>
                                                <asp:Label runat="server" ID="lblVisualizarUnidade" CssClass="labelInfo" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Produto
                                                </label>
                                            </div>
                                            <div>
                                                <asp:Label runat="server" ID="lblVisualizarProduto" CssClass="labelInfo" />
                                            </div>
                                            <div class="row">&nbsp;</div>

                                            <div>
                                                <div class="x_panel">
                                                    <div class="x_title">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <label>Informações do Produto</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="x_content">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Código do produto: 
                                                                    </label>

                                                                    <asp:Label runat="server" ID="lblVisualizarCodigoProduto" CssClass="labelInfo" Text="---" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Produto: 
                                                                    </label>

                                                                    <asp:Label runat="server" ID="Label1" CssClass="labelInfo" Text="---" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Tipo produto: 
                                                                    </label>

                                                                    <asp:Label runat="server" ID="lblVisualizarTipoProduto" CssClass="labelInfo" Text="---" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Fabricante: 
                                                                    </label>

                                                                    <asp:Label runat="server" ID="lblVisualizarFabricante" CssClass="labelInfo" Text="---" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Marca: 
                                                                    </label>

                                                                    <asp:Label runat="server" ID="lblVisualizarMarca" CssClass="labelInfo" Text="---" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Modelo: 
                                                                    </label>

                                                                    <asp:Label runat="server" ID="lblVisualizarModelo" CssClass="labelInfo" Text="---" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Dimensões: 
                                                                    </label>

                                                                    <asp:Label runat="server" ID="lblVisualizarDimensoes" CssClass="labelInfo" Text="---" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Cor: 
                                                                    </label>

                                                                    <asp:Label runat="server" ID="lblVisualizarCor" CssClass="labelInfo" Text="---" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Valor Custo</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblVisualizarValorCusto" CssClass="labelInfo" Text="---" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">&nbsp;</div>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Margem de Lucro</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblVisualizarMargemLucro" CssClass="labelInfo" Text="---" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">&nbsp;</div>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Valor Venda</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblVisualizarValorVenda" CssClass="labelInfo" Text="---" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">&nbsp;</div>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Quantidade Atual em Estoque</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblVisualizarQtdAtualEstoque" CssClass="labelInfo" Text="0" />
                                                    </div>
                                                </div>

                                                <div class="col-md-6 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Quantidade Adicionada ao Estoque</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblVisualizarQtdAddEstoque" CssClass="labelInfo" Text="0" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">&nbsp;</div>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Data da Última Alteração do Estoque</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblVisualizarDataUltimaAlteracaoEstoque" CssClass="labelInfo" Text="---" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

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

    <asp:ModalPopupExtender ID="mpeFiltroEstoqueProdutos" TargetControlID="lbFiltroEstoque" PopupControlID="pnlFiltroEstoqueProdutos"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCancelFiltroEstoqueProdutos"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlFiltroEstoqueProdutos" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>FILTRO ESTOQUE DE PRODUTOS</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lkClose" OnClick="lkClose_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Código do Estoque Produto
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtCodigoEstoqueProdutoFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Código do Produto
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtCodigoProdutoFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Descrição Produto 
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtDescricaoProdutoFiltro" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                    Tamanho
                                                </label>
                                            </div>
                                            <div>
                                                <asp:DropDownList runat="server" ID="ddlTamanhoFiltro" CssClass="form-control" />
                                            </div>
                                        </div>
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
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <asp:LinkButton ID="lbCancelFiltroEstoqueProdutos" runat="server"
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
