<%@ Page Title="Perfis" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfis.aspx.cs" Inherits="Libra.Perfis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnIdPerfil" />
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Perfis
                </h3>
            </div>
        </div>
        <div class="clearfix"></div>
        <div id="divFiltroETabela" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2><i class="fa fa-bookmark"></i><small>Lista de Perfis cadastrados.</small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li>
                                    <asp:LinkButton runat="server" ID="lbFiltroPerfis" CssClass="fa fa-filter" OnClick="lbFiltroPerfis_Click" ToolTip="Filtrar Perfis"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbVisualizarPerfil" CssClass="fa fa-ellipsis-h" OnClick="lbVisualizarPerfil_Click" ToolTip="Visualizar Perfil"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbAddPerfis" CssClass="fa fa-plus" OnClick="lbAddPerfis_Click" ToolTip="Adicionar Perfil"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbEditPerfis" CssClass="fa fa-pencil" OnClick="lbEditPerfis_Click" ToolTip="Editar Perfil"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbDelPerfis" CssClass="fa fa-trash-o" OnClick="lbDelPerfis_Click" ToolTip="Deletar Perfis"></asp:LinkButton>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">

                            <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="false" DataKeyNames="PerfilId"
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
                                    <asp:TemplateField HeaderText="Nome" SortExpression="Nome">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbEditarNome" runat="server" CommandName="Select" Text='<%# Eval("Nome")%>'
                                                CssClass="gridLink">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="75%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Somente Leitura" SortExpression="SomenteLeitura">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSomenteLeitura" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ativo" SortExpression="Ativo">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAtiva" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label runat="server" ID="lblNoResults" Text="Nenhum perfil encontrado!" />
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
                                <asp:Label ID="lbAddEditPerfil" runat="server" Text="Novo Perfil"></asp:Label>
                            </h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div>
                                        <label>
                                            Nome Perfil
                                            <asp:Label runat="server" ID="lblNomePerfilReq" Text="*" CssClass="requerid" />
                                        </label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtNomePerfil" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNome" ControlToValidate="txtNomePerfil" SetFocusOnError="True" CssClass="requerid"
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
                                                    <label>FUNCIONALIDADES</label>
                                                    &nbsp
                                                            <label>
                                                                <asp:Button ID="btnMarcarTodosFuncionalidades" runat="server" OnClick="btnMarcarTodosFuncionalidades_Click"
                                                                    Text="Marcar Todos" CausesValidation="false" class="btn btn-default btn-xs button-select" />
                                                                <asp:Button ID="btnDesmarcarTodosFuncionalidades" Visible="false" runat="server" OnClick="btnDesmarcarTodosFuncionalidades_Click"
                                                                    Text="Desmarcar Todos" CausesValidation="false" class="btn btn-default btn-xs button-select" />
                                                            </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="x_content" style="height: 120px!important;">
                                            <asp:Panel runat="server" ID="Panel2" Width="97%" Height="100%" ScrollBars="Auto">
                                                <asp:CheckBoxList ID="cblFuncionalidades" runat="server" CssClass="cblStyle">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-xs-12">
                                    <div>
                                        <label>
                                            Somente Leitura?
                                        </label>
                                    </div>
                                    <div>
                                        <asp:CheckBox runat="server" ID="chkSomenteLeitura" />
                                    </div>
                                </div>
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
    <asp:ModalPopupExtender ID="mpeFiltroPerfis" TargetControlID="lbFiltroPerfis" PopupControlID="pnlFiltroPerfis"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lbCancelFiltroPerfis"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlFiltroPerfis" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>FILTRO PERFIS</h2>
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
                                                    Nome Perfil
                                                </label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtPerfilFiltro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>Somente Leitura?</label>
                                            </div>
                                            <div>
                                                <asp:CheckBox runat="server" ID="ckbAtivoSomenteLeitura" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Sim</span>&nbsp;
                                        <asp:CheckBox runat="server" ID="ckbInativoSomenteLeitura" Style="vertical-align: middle" Checked="true" />
                                                <span class="labelInfo fonteCinza">Não</span>&nbsp;
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 col-xs-12">
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
                                            <asp:LinkButton ID="lbCancelFiltroPerfis" runat="server"
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

    <asp:ModalPopupExtender ID="mpeVisualizarPerfil" TargetControlID="lkbOculto" PopupControlID="pnlVisualizarPerfil"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lnbFecharVisualizarPerfil"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlVisualizarPerfil" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>Perfil
                                        <asp:Label ID="lbPerfil" runat="server"></asp:Label></h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lkCloseVisualizarPerfil" OnClick="lkCloseVisualizarPerfil_Click" CssClass="right"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Nome Perfil:
                                                </label>
                                                <asp:Label ID="lbNomePerfil" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Funcionalidades Acessíveis:
                                                </label>
                                                <asp:Label ID="lbFuncionalidades" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    Somente Leitura?
                                                </label>
                                                <asp:Label ID="lbSomenteLeitura" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    Ativo?
                                                </label>
                                                <asp:Label ID="lbAtiva" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <asp:LinkButton ID="lnbFecharVisualizarPerfil" runat="server"
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
    <asp:ModalPopupExtender ID="mpeExclusaoPerfil" TargetControlID="lkbOculto" PopupControlID="pnlExcluirPerfil"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="btnCancelarExclusaoPerfil"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlExcluirPerfil" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div runat="server" id="divExclusaoPerfilProibida">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-2 ">
                                    <asp:Image ID="imgExclusaoPerfilProibida" runat="server" ImageUrl="~/images/alertaExclusao.png"
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
                                                <asp:Label ID="lblPerfisExclusaoProibidas" runat="server" Text="" /><br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="divExclusaoPerfil">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="linhaExclusao" runat="server" id="divLinhaExclusaoPerfil">
                                </div>
                                <br />
                                <div class="col-md-2 ">
                                    <asp:Image ID="imgExclusaoPerfil" runat="server" ImageUrl="~/images/alertaExclusao.png"
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
                                                <asp:Label ID="lblPerfisExclusao" runat="server" Text="" />
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
                        <asp:Button ID="btnExcluirPerfil" runat="server" OnClick="btnExcluirPerfil_Click" CausesValidation="false"
                            Text="SIM, EXCLUIR" class="btn btn-primary pull-right" />
                        <asp:Button ID="btnCancelarExclusaoPerfil" runat="server" OnClick="btnCancelarExclusaoPerfil_Click"
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
