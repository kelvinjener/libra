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
                                <li>
                                    <asp:LinkButton runat="server" ID="lbDelEstoque" CssClass="fa fa-trash-o" OnClick="lbDelEstoque_Click" ToolTip="Deletar Estoque de Produto"></asp:LinkButton>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">

                            <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="false" DataKeyNames="EstoqueId, ProdutoId"
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
                                    <asp:TemplateField HeaderText="Valor Compra" SortExpression="valorCompra">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblValorCompra" Text='<%# Eval("valorCompra")%>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor Venda" SortExpression="valorVenda">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblValorVenda" Text='<%# Eval("valorCompra")%>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unidade" SortExpression="unidade">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblUnidade" Text='<%# Eval("unidade")%>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtd. Disponível" SortExpression="qtdDisponivel">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblQtdDisponivel" Text='<%# Eval("qtdDisponivel")%>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label runat="server" ID="lblNoResults" Text="Nenhuma produto encontrado!" />
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
