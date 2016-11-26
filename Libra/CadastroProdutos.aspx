<%@ Page Title="Cadastro de Produtos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProdutos.aspx.cs" Inherits="Libra.CadastroProdutos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnIdProduto" />
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
                                    <asp:TemplateField HeaderText="Loja" SortExpression="Apelido">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbEditarLoja" runat="server" CommandName="Select" Text='<%# Eval("Apelido")%>'
                                                CssClass="gridLink">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" ItemStyle-Width="15%" HeaderStyle-CssClass="headings" ItemStyle-CssClass="even pointer" />
                                    <asp:BoundField DataField="Bairro" HeaderText="Bairro" SortExpression="Bairro" ItemStyle-Width="15%" HeaderStyle-CssClass="headings" ItemStyle-CssClass="even pointer" />
                                    <asp:BoundField DataField="Telefone1" HeaderText="Telefone" SortExpression="Telefone1" ItemStyle-Width="10%" HeaderStyle-CssClass="headings" ItemStyle-CssClass="even pointer" />
                                    <asp:BoundField DataField="Email1" HeaderText="E-mail" SortExpression="Email1" ItemStyle-Width="20%" HeaderStyle-CssClass="headings" ItemStyle-CssClass="even pointer" />
                                    <asp:TemplateField HeaderText="Ativa" SortExpression="Ativo">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAtiva" Text="Inativa" />
                                        </ItemTemplate>
                                        <ItemStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TipoProduto" HeaderText="Tipo de Produto" SortExpression="TipoProduto" ItemStyle-Width="13%" HeaderStyle-CssClass="headings" ItemStyle-CssClass="even pointer" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label runat="server" ID="lblNoResults" Text="Nenhuma produto encontrado!" />
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:LinqDataSource ID="ldsFiltro" runat="server" ContextTypeName="RM.Cst.Sebrae.SGCTEC.Entity.SGCTECDataContext"
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
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            Nome Produto
                                            <asp:Label runat="server" ID="lblNomeProdutoReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtNomeProduto" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNome" ControlToValidate="txtNomeProduto" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            Apelido Produto
                                            <asp:Label runat="server" ID="lblApelidoProdutoReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtApelidoProduto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvApelidoProduto" ControlToValidate="txtApelidoProduto" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-8 col-sm-8 col-xs-12">
                                    <div>
                                        <label>
                                            Logradouro
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtLogradouro" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <div>
                                        <label>
                                            Número
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <div>
                                        <label>
                                            Complemento
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtComplemento" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            Bairro
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtBairro" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>


                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            Cidade
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtCidade" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-xs-12">
                                    <div>
                                        <label>
                                            UF
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlUF" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <div>
                                        <label>
                                            CEP
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <div>
                                        <label>
                                            Telefone 1
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtTel1" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <div>
                                        <label>
                                            Telefone 2
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtTel2" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <div>
                                        <label>
                                            FAX
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtFax" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            E-mail 1
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            E-mail 2
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>


                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div>
                                        <label>
                                            Observação
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" MaxLength="8000" onKeyDown="atualizarContadorComentario()" onKeyUp="atualizarContadorComentario()"
                                            onFocus="atualizarContadorComentario(); exibeContadorComentario(true);  " onBlur="exibeContadorComentario(false)"></asp:TextBox>
                                        <label id="lblCaracteresRestantesComentario" style="visibility: hidden">
                                            Caracteres restantes:</label>&nbsp;<label id="lblCaracteresRestantesValueComentario"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            Tipo Produto
                                            <asp:Label runat="server" ID="lblTipoProdutoReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList runat="server" ID="ddlTipoProduto" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvTipoProduto" ControlToValidate="ddlTipoProduto" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-xs-12">
                                    <div>
                                        <label>
                                            Ativa?
                                        </label>
                                    </div>
                                    <div>
                                        <asp:CheckBox runat="server" ID="chkAtiva" />
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
                                    <asp:Button runat="server" ID="btnSalvar" ValidationGroup="G1" CssClass="btn btn-primary" Width="100%" Text="Salvar" OnClick="btnSalvar_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
                                            <h2>FILTRO UNIDADES</h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lkClose" OnClick="lkClose_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Produto
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtProdutoFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Tipo 
                                                </label>
                                            </div>
                                            <div>
                                                <asp:DropDownList runat="server" ID="ddlTipoProdutoFiltro" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Ativa</label>
                                            </div>
                                            <div>
                                                <asp:CheckBox runat="server" ID="ckbAtivoFiltro" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Ativa</span>&nbsp;
                                        <asp:CheckBox runat="server" ID="ckbInativoFiltro" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Inativa</span>&nbsp;
                                            </div>
                                        </div>
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
                                                    Nome Produto:
                                                </label>
                                                <asp:Label ID="lbNomeProduto" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Endereço:
                                                </label>
                                                <asp:Label ID="lbEnderecoProduto" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Telefone(s):
                                                </label>
                                                <asp:Label ID="lbTelefones" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    E-mail(s):
                                                </label>
                                                <asp:Label ID="lbEmails" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Observação:
                                                </label>
                                                <asp:Label ID="lbObservacao" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Ativa?
                                                </label>
                                                <asp:Label ID="lbAtiva" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Tipo Produto:
                                                </label>
                                                <asp:Label ID="lbTipoProduto" runat="server" CssClass="labelInfo"></asp:Label>
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

        function atualizarContadorComentario() {
            atualizarCaracteresRestantes(document.getElementById('<%=txtObservacao.ClientID%>'),
                      document.getElementById('lblCaracteresRestantesValueComentario'), 8000)
        }

        function exibeContadorComentario(estado) {
            document.getElementById('lblCaracteresRestantesComentario').style.visibility = estado ? "visible" : "hidden";
            document.getElementById('lblCaracteresRestantesValueComentario').style.visibility = estado ? "visible" : "hidden";
        }

        function atualizarCaracteresRestantes(textBox, label, limite) {
            var restantes = limite - textBox.value.length;

            if (restantes <= 0) {
                textBox.value = textBox.value.substr(0, limite);

                restantes = 0;
            }

            label.innerHTML = restantes;
        }

    </script>
</asp:Content>
