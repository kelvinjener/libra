<%@ Page Title="Usuários" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Libra.Seguranca.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnIdUsuario" />
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Usuários
                </h3>
            </div>
        </div>
        <div class="clearfix"></div>
        <div id="divFiltroETabela" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2><i class="fa fa-bookmark"></i><small>Lista de Usuários cadastrados.</small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li>
                                    <asp:LinkButton runat="server" ID="lbFiltroUsuarios" CssClass="fa fa-filter" OnClick="lbFiltroUsuarios_Click" ToolTip="Filtrar Usuarios"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbVisualizarUsuario" CssClass="fa fa-ellipsis-h" OnClick="lbVisualizarUsuario_Click" ToolTip="Visualizar Usuario"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbAddUsuarios" CssClass="fa fa-plus" OnClick="lbAddUsuarios_Click" ToolTip="Adicionar Usuario"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbEditUsuarios" CssClass="fa fa-pencil" OnClick="lbEditUsuarios_Click" ToolTip="Editar Usuario"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbDelUsuarios" CssClass="fa fa-trash-o" OnClick="lbDelUsuarios_Click" ToolTip="Deletar Usuarios"></asp:LinkButton>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">

                            <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="false" DataKeyNames="UsuarioId"
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
                                    <asp:TemplateField HeaderText="Nome" SortExpression="NOME">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbEditarLoja" runat="server" CommandName="Select" Text='<%# Eval("NOME")%>'
                                                CssClass="gridLink">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CPF" SortExpression="CPF">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCPFUsuario" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" CssClass="even pointer" />
                                        <HeaderStyle CssClass="headings" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Telefone" SortExpression="Telefone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTelefoneUsuario" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" CssClass="even pointer" />
                                        <HeaderStyle CssClass="headings" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Email" HeaderText="E-mail" SortExpression="Email" ItemStyle-Width="20%" HeaderStyle-CssClass="headings" ItemStyle-CssClass="even pointer" />
                                    <asp:TemplateField HeaderText="Ativo" SortExpression="Ativo">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAtiva" />
                                        </ItemTemplate>
                                        <ItemStyle Width="7%" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label runat="server" ID="lblNoResults" Text="Nenhuma usuario encontrado!" />
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
                                <asp:Label ID="lbAddEditUsuario" runat="server" Text="Nova Usuario"></asp:Label>
                            </h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col-md-8 col-sm-8 col-xs-12">
                                    <div>
                                        <label>
                                            Nome Usuário
                                            <asp:Label runat="server" ID="lblNomeUsuarioReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtNomeUsuario" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNome" ControlToValidate="txtNomeUsuario" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <div>
                                        <label>
                                            CPF
                                            <asp:Label runat="server" ID="lblCPFUsuarioReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCPFUsuario" ControlToValidate="txtCPF" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            Data Nascimento
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtenderDataNascimento" runat="server" TargetControlID="txtDataNascimento" Format="dd/MM/yyyy"
                                            Enabled="True" />
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            Sexo
                                        </label>
                                    </div>
                                    <div>
                                        <asp:DropDownList runat="server" ID="ddlSexo" CssClass="form-control" />
                                    </div>
                                </div>


                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-12">
                                    <div>
                                        <label>
                                            Telefone
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-8 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            E-mail
                                            <asp:Label runat="server" ID="lblEmailReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="txtEmail" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="x_panel">
                                        <div class="x_title">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <label>UNIDADES</label>
                                                    <asp:Label runat="server" ID="lblUnidadesReq" Text="*" CssClass="required" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="x_content" style="height: 150px!important;">
                                            <asp:Panel runat="server" ID="pnlContainerUnidades" Width="97%" Height="100%" ScrollBars="Auto">
                                                <asp:CheckBoxList ID="cblUnidades" runat="server" CssClass="cblStyle">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <asp:CustomValidator ID="cvUnidades" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório.<br />"
                                        OnServerValidate="cvUnidades_ServerValidate" ValidationGroup="G1" Display="Dynamic"
                                        CssClass="requerid"></asp:CustomValidator>
                                </div>


                                <div class="col-md-6">
                                    <div class="x_panel">
                                        <div class="x_title">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <label>PERFIS</label>
                                                    <asp:Label runat="server" ID="lblPerfilReq" Text="*" CssClass="required" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="x_content" style="height: 150px!important;">
                                            <asp:Panel runat="server" ID="Panel1" Width="97%" Height="100%" ScrollBars="Auto">

                                                <asp:CheckBoxList ID="cblPerfis" runat="server" CssClass="cblStyle">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <asp:CustomValidator ID="cvPerfis" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório.<br />"
                                        OnServerValidate="cvPerfis_ServerValidate" ValidationGroup="G1" Display="Dynamic"
                                        CssClass="requerid"></asp:CustomValidator>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-xs-12">
                                    <div>
                                        <label>
                                            Ativo?
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
    <asp:ModalPopupExtender ID="mpeFiltroUsuarios" TargetControlID="lbFiltroUsuarios" PopupControlID="pnlFiltroUsuarios"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCancelFiltroUsuarios"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlFiltroUsuarios" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>FILTRO USUÁRIOS</h2>
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
                                                    Usuário
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtUsuarioFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    E-mail
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtEmailFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    CPF
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtCPFFiltro" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="x_panel">
                                                <div class="x_title">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                                            <label>UNIDADES</label>
                                                            &nbsp
                                                            <label>
                                                                <asp:Button ID="btnMarcarTodosUnidades" runat="server" OnClick="btnMarcarTodosUnidades_Click"
                                                                    Text="Marcar Todos" CausesValidation="false" class="btn btn-default btn-xs button-select" />
                                                                <asp:Button ID="btnDesmarcarTodosUnidades" Visible="false" runat="server" OnClick="btnDesmarcarTodosUnidades_Click"
                                                                    Text="Desmarcar Todos" CausesValidation="false" class="btn btn-default btn-xs button-select" />
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="x_content" style="height: 120px!important;">
                                                    <asp:Panel runat="server" ID="Panel2" Width="97%" Height="100%" ScrollBars="Auto">
                                                        <asp:CheckBoxList ID="cblUnidadesFiltro" runat="server" CssClass="cblStyle">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-6">
                                            <div class="x_panel">
                                                <div class="x_title">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                                            <label>PERFIS</label>
                                                            &nbsp
                                                            <label>
                                                                <asp:Button ID="btnMarcarTodosPerfis" runat="server" OnClick="btnMarcarTodosPerfis_Click"
                                                                    Text="Marcar Todos" CausesValidation="false" class="btn btn-default btn-xs button-select" />
                                                                <asp:Button ID="btnDesmarcarTodosPerfis" Visible="false" runat="server" OnClick="btnDesmarcarTodosPerfis_Click"
                                                                    Text="Desmarcar Todos" CausesValidation="false" class="btn btn-default btn-xs button-select" />
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="x_content" style="height: 120px!important;">
                                                    <asp:Panel runat="server" ID="Panel3" Width="97%" Height="100%" ScrollBars="Auto">

                                                        <asp:CheckBoxList ID="cblPerfisFiltro" runat="server" CssClass="cblStyle">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>Ativo</label>
                                            </div>
                                            <div>
                                                <asp:CheckBox runat="server" ID="ckbAtivoFiltro" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Ativo</span>&nbsp;
                                        <asp:CheckBox runat="server" ID="ckbInativoFiltro" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Inativo</span>&nbsp;
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <asp:LinkButton ID="lbCancelFiltroUsuarios" runat="server"
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

    <asp:ModalPopupExtender ID="mpeVisualizarUsuario" TargetControlID="lkbOculto" PopupControlID="pnlVisualizarUsuario"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lnbFecharVisualizarUsuario"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlVisualizarUsuario" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>Usuário
                                        <asp:Label ID="lbUsuario" runat="server"></asp:Label></h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lkCloseVisualizarUsuario" OnClick="lkCloseVisualizarUsuario_Click" CssClass="right"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Nome Usuário:
                                                </label>
                                                <asp:Label ID="lbNomeUsuario" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    CPF:
                                                </label>
                                                <asp:Label ID="lbCPFUsuario" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    Sexo:
                                                </label>
                                                <asp:Label ID="lbSexo" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    Data Nascimento:
                                                </label>
                                                <asp:Label ID="lbDataNascimento" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    Telefone:
                                                </label>
                                                <asp:Label ID="lbTelefone" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    E-mail:
                                                </label>
                                                <asp:Label ID="lbEmail" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    Ativo?
                                                </label>
                                                <asp:Label ID="lbAtiva" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    Data Cadastro:
                                                </label>
                                                <asp:Label ID="lbDataCadastro" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Unidades:
                                                </label>
                                                <asp:Label ID="lbUnidades" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Perfis:
                                                </label>
                                                <asp:Label ID="lbPerfis" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <asp:LinkButton ID="lnbFecharVisualizarUsuario" runat="server"
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
    <asp:ModalPopupExtender ID="mpeExclusaoUsuario" TargetControlID="lkbOculto" PopupControlID="pnlExcluirUsuario"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="btnCancelarExclusaoUsuario"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlExcluirUsuario" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div runat="server" id="divExclusaoUsuarioProibida">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-2 ">
                                    <asp:Image ID="imgExclusaoUsuarioProibida" runat="server" ImageUrl="~/images/alertaExclusao.png"
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
                                                <asp:Label ID="lblUsuariosExclusaoProibidas" runat="server" Text="" /><br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="divExclusaoUsuario">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="linhaExclusao" runat="server" id="divLinhaExclusaoUsuario">
                                </div>
                                <br />
                                <div class="col-md-2 ">
                                    <asp:Image ID="imgExclusaoUsuario" runat="server" ImageUrl="~/images/alertaExclusao.png"
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
                                                <asp:Label ID="lblUsuariosExclusao" runat="server" Text="" />
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
                        <asp:Button ID="btnExcluirUsuario" runat="server" OnClick="btnExcluirUsuario_Click" CausesValidation="false"
                            Text="SIM, EXCLUIR" class="btn btn-primary pull-right" />
                        <asp:Button ID="btnCancelarExclusaoUsuario" runat="server" OnClick="btnCancelarExclusaoUsuario_Click"
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
