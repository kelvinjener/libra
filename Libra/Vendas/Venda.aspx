<%@ Page Title="Venda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Venda.aspx.cs" Inherits="Libra.Vendas.Venda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnStep0Concluida" runat="server" />
    <asp:HiddenField ID="hdnStep1Concluida" runat="server" />
    <asp:HiddenField ID="hdnStep2Concluida" runat="server" />
    <asp:HiddenField ID="hdnIdVenda" runat="server" />
    <asp:HiddenField ID="hdnIdCliente" runat="server" />

    <div class="page-title">
        <div class="title_left">
            <h3>Venda</h3>
        </div>
    </div>
    <div class="clearfix"></div>
    <div id="divPrincipal" runat="server">
        <div class="x_panel">
            <div class="x_title">
                <div class="row">
                    <div class="col-md-offset-2 col-md-8 col-sm-12 col-xs-12">
                        <div class="btn-toolbar ">
                            <div class="btn-group col-md-12 col-sm-12 col-xs-12">
                                <asp:LinkButton runat="server" ID="btnStep0" class="btn btn-default" OnClick="btnStep0_Click" Width="25%">
                                                Cliente<%--<p><small>Selecione o cliente</small></p>--%>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnStep1" class="btn btn-dark" OnClick="btnStep1_Click" Width="25%">
                                                Produtos<%--<p><small>Selecione os produtos a vender</small></p>--%>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnStep2" class="btn btn-dark" OnClick="btnStep2_Click" Width="25%">
                                                Pagamento<%--<p><small>Informe as formas de pagamento</small></p>--%>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnStep3" class="btn btn-dark" OnClick="btnStep3_Click" Width="25%">
                                                Finalizar Venda<%--<p><small>Emitir pedido</small></p>--%>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="x_content">
                <div id="divStep0" runat="server" visible="true">
                    <%-- <h2 class="StepTitle">Cliente</h2>--%>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <h2><i class="fa fa-bookmark"></i><small>DADOS DO PEDIDO</small></h2>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Número do Pedido</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblNumeroPedido" CssClass="labelInfo"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-md-4 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Data/Hora Criação</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblDataHoraCriacao" CssClass="labelInfo"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-md-4 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Situação</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblSituacao" CssClass="labelInfo"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">&nbsp;</div>
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Vendedor</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblVendedor" CssClass="labelInfo"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-md-4 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Unidade</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblUnidade" CssClass="labelInfo"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">&nbsp;</div>
                                        <div class="row">&nbsp;</div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="x_panel">
                                                    <div class="x_title">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <label>Cliente <span class="required">*</span></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="x_content">
                                                        <div class="row">
                                                            <div class="col-md-8 col-sm-12 col-xs-12">
                                                                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control select2_single"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCliente" ControlToValidate="ddlCliente" SetFocusOnError="True" CssClass="requerid"
                                                                    ValidationGroup="GStep0" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="col-md-2 col-sm-12 col-xs-12">
                                                                <asp:LinkButton ID="lkbPesquisarCliente" runat="server" Width="100%" ToolTip="Pesquisar por cliente"
                                                                    CssClass="btn btn-default"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12">
                                                                <asp:LinkButton runat="server" ID="lkbAddCliente" ToolTip="Adicionar novo cliente" ValidationGroup="GCliente"
                                                                    CssClass="btn btn-primary" Width="100%"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-offset-6 col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="lkbCancelar" runat="server" Width="100%"
                                CssClass="btn btn-default" OnClick="lkbCancelar_Click">Cancelar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbSalvarStep0" ValidationGroup="GStep0"
                                CssClass="btn btn-primary" Width="100%" OnClick="lkbSalvarStep0_Click">Salvar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbSalvarAvancarStep0" ValidationGroup="GStep01"
                                CssClass="btn btn-dark" Width="100%" OnClick="lkbSalvarAvancarStep0_Click">Salvar / Avançar</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div id="divStep1" runat="server" visible="false">
                    <%--<h2 class="StepTitle">Produtos</h2>--%>
                    <div class="x_panel">
                        <div class="x_title">
                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <h2><i class="fa fa-bookmark"></i><small>PRODUTOS | CLIENTE:
                                                    <asp:Label ID="lblClienteProdutos" runat="server"></asp:Label></small></h2>
                                </div>
                                <div class="col-md-offset-3 col-md-3 col-sm-12 col-xs-12 right">
                                    <small>NÚMERO PEDIDO:
                                                    <asp:Label ID="lblNumeroPedidoProdutos" runat="server"></asp:Label></small>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="x_content">
                            <div class="row" style="max-height: 40vh; overflow-y: auto;">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <asp:GridView ID="gvResultsProdutos" runat="server" ShowHeader="true" AutoGenerateColumns="false" DataKeyNames="VendaId, VendaProdutosId, ClienteId, VendedorId, UnidadeId"
                                        AllowPaging="false" AllowSorting="true" Width="100%" DataSourceID="ldsFiltroProdutos" CssClass="table table-striped responsive-utilities jambo_table table-bordered dt-responsive nowrap"
                                        OnRowDataBound="gvResultsProdutos_RowDataBound" OnPreRender="gvResultsProdutos_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" CssClass="labelInfo" Text='<%# Eval("Item")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Produto">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlProduto" runat="server" CssClass="select2_single form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProduto_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="40%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantidade">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQtd" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtQtd_TextChanged" Text='<%# Eval("Quantidade") %>'>
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor Unitário">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValorUnitario" runat="server" CssClass="labelInfo" DataFormatString="{0:C}" Text='<%# Eval("ValorUnitario") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desconto (R$)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDesconto" runat="server" CssClass="form-control" DataFormatString="{0:C}" AutoPostBack="true" OnTextChanged="txtDesconto_TextChanged" Text='<%# Eval("DescontoReal") %>'>
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desconto (%)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDescontoPorcentagem" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDescontoPorcentagem_TextChanged" Text='<%# Eval("DescontoPorcentagem") %>'>
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acréscimo (%)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAcrescimoPorcentagem" runat="server" CssClass="form-control" DataFormatString="{0:C}" AutoPostBack="true" OnTextChanged="txtAcrescimoPorcentagem_TextChanged" Text='<%# Eval("Acrescimo") %>'>
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubTotal" runat="server" CssClass="labelInfo" DataFormatString="{0:C}" Text='<%# Eval("SubTotal") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Excluir">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelProdutoVenda" runat="server" CssClass="btn btn-danger" ToolTip="Deletar Item" CommandArgument='<%#Eval("Item")%>' OnCommand="btnDelProdutoVenda_Command"><i class="fa fa-minus" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Label runat="server" ID="lblNoResults" Text="Nenhum item inserido! Clique em 'Adicionar Item' e selecione o produto que desejar." />
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="ldsFiltroProdutos" runat="server" ContextTypeName="Libra.Entity.LibraDataContext"
                                        OnSelecting="ldsFiltroProdutos_Selecting" AutoSort="true" AutoGenerateWhereClause="true">
                                    </asp:LinqDataSource>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <asp:LinkButton runat="server" ID="lkbAddItem" CssClass="btn" OnClick="lkbAddItem_Click">
                                                <i class="fa fa-plus" aria-hidden="true"></i>&nbsp; Adicionar Item
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div class="x_panel" style="height: 205px">
                                        <div class="x_title">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <label>Observações</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="x_content">
                                            <asp:TextBox runat="server" ID="txtObservacao" CssClass="form-control" TextMode="MultiLine" Wrap="true" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div class="x_panel" style="height: 205px">
                                        <div class="x_title">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <label>Composições</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="x_content">
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Desconto (R$)</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox runat="server" ID="txtDescontoReais" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Desconto (%)</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox runat="server" ID="txtDescontoPorcentagem" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">&nbsp;</div>

                                            <div class="row">
                                                <div class="col-md-6 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Acréscimo (R$)</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox runat="server" ID="txtAcrescimo" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Frete</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox runat="server" ID="txtFrete" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div class="x_panel" style="height: 205px">
                                        <div class="x_title">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <label>TOTAL</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="x_content">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div>
                                                        <h1>
                                                            <asp:Label runat="server" ID="lbValorTotal" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>
                                                        </h1>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-offset-4 col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="LinkButton1" runat="server" Width="100%"
                                CssClass="btn btn-default" OnClick="lkbCancelar_Click">Cancelar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbVoltarStep1"
                                CssClass="btn btn-dark" Width="100%" OnClick="lkbVoltarStep1_Click">Voltar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbSalvarStep1" ValidationGroup="GStep1"
                                CssClass="btn btn-primary" Width="100%" OnClick="lkbSalvarStep1_Click">Salvar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbSalvarAvancarStep1" ValidationGroup="GStep1"
                                CssClass="btn btn-dark" Width="100%" OnClick="lkbSalvarAvancarStep1_Click">Salvar / Avançar</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div id="divStep2" runat="server" visible="false">
                    <%--<h2 class="StepTitle">Pagamento</h2>--%>
                    <div class="x_panel">
                        <div class="x_title">
                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <h2><i class="fa fa-bookmark"></i><small>PAGAMENTO | Cliente:
                                                    <asp:Label ID="lblClientePagamento" runat="server"></asp:Label></small></h2>
                                </div>
                                <div class="col-md-offset-3 col-md-3 col-sm-12 col-xs-12 right">
                                    <small>NÚMERO PEDIDO:
                                                    <asp:Label ID="lblNumeroPedidoPagamento" runat="server"></asp:Label></small>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="x_panel">
                                        <div class="x_title">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <label>Valores para Pagamento</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="x_content">
                                            <div class="row">
                                                <div class="col-md-3 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Valor Total dos Produtos</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblPagamentoValorTotalProdutos" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Acréscimos</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblPagamentoAcrescimos" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Descontos</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblPagamentoDescontos" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Frete</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblPagamentoFrete" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">&nbsp;</div>

                                            <div class="row">
                                                <div class="col-md-3 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Valor Total a Faturar</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblPagamentoTotalFaturar" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Valor Pago</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblPagamentoValorPago" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12">
                                                    <div>
                                                        <label>Falta Pagar</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblPagamentoFaltaPagar" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <asp:LinkButton runat="server" ID="lkbAddNovoPagamento" CssClass="btn" OnClick="lkbAddNovoPagamento_Click">
                                                <i class="fa fa-plus" aria-hidden="true"></i>&nbsp; Inserir Pagamento
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">&nbsp;</div>
                                <div class="row" id="formAddPagamento" runat="server" visible="false">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="x_panel">
                                            <div class="x_content">
                                                <div class="row">
                                                    <div class="col-md-2 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>Valor</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox runat="server" ID="txtValorPagamento" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtValorPagamento" ControlToValidate="txtValorPagamento" SetFocusOnError="True" CssClass="requerid"
                                                                ValidationGroup="GPag" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12">

                                                        <div>
                                                            <label>Forma de pagamento</label>
                                                        </div>
                                                        <div>
                                                            <asp:DropDownList runat="server" ID="ddlFormaPagamento" CssClass="form-control select2_single" OnSelectedIndexChanged="ddlFormaPagamento_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <asp:LinkButton runat="server" ID="lkbAddFormaPagamento" ToolTip="Adicionar forma de pagamento - Parâmetro" CssClass="btn"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:RequiredFieldValidator ID="rfvDdlFormaPagamento" ControlToValidate="ddlFormaPagamento" SetFocusOnError="True" CssClass="requerid"
                                                                ValidationGroup="GPag" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2 col-sm-12 col-xs-12" id="divParcelas" runat="server" visible="false">
                                                        <div>
                                                            <label>Parcelas</label>
                                                        </div>
                                                        <div>
                                                            <asp:DropDownList runat="server" ID="ddlParcelas" CssClass="form-control select2_single" OnSelectedIndexChanged="ddlParcelas_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlParcelas" ControlToValidate="ddlParcelas" SetFocusOnError="True" CssClass="requerid"
                                                                ValidationGroup="GPag" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12" id="divValorParcelas" runat="server" visible="false">
                                                        <div>
                                                            <label>Valor parcela</label>
                                                        </div>
                                                        <div>
                                                            <asp:Label runat="server" ID="lblValorParcela" CssClass="labelInfo" Text="R$ 0,00"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-sm-12 col-xs-12">
                                                        <div>
                                                            <label>&nbsp;</label>
                                                        </div>
                                                        <div>
                                                            <asp:LinkButton runat="server" ID="lkbAddPagamento" CssClass="btn btn-primary" OnClick="lkbAddPagamento_Click" ValidationGroup="GPag"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="max-height: 40vh; overflow-y: auto;">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <asp:GridView ID="gvResultsPagamento" runat="server" AutoGenerateColumns="false" DataKeyNames="VendaId, VendaPagamentoId, ClienteId, VendedorId, UnidadeId"
                                            AllowPaging="true" AllowSorting="true" Width="100%" DataSourceID="ldsFiltroPagamento" OnPreRender="gvResultsPagamento_PreRender"
                                            CssClass="table table-striped responsive-utilities jambo_table  table-bordered dt-responsive nowrap">
                                            <Columns>
                                                <asp:BoundField HeaderText="Forma de Pagamento" SortExpression="FormaPagamento" DataField="FormaPagamento" ItemStyle-Width="65%" />
                                                <asp:BoundField HeaderText="Parcelas" SortExpression="Parcelas" DataField="Parcelas" ItemStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Valor Parcela" SortExpression="ValorParcela" DataField="ValorParcela" DataFormatString="{0:C}" ItemStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Total" SortExpression="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-Width="10%" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelPagamento" runat="server" CssClass="btn btn-danger" ToolTip="Deletar Item" CommandArgument='<%#Eval("VendaPagamentoId")%>' OnCommand="btnDelPagamento_Command"><i class="fa fa-minus" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label runat="server" ID="lblNoResults" Text="Nenhum item inserido! Clique em 'Inserir Pagamento' e selecione a forma de pagamento que desejar." />
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <asp:LinqDataSource ID="ldsFiltroPagamento" runat="server" ContextTypeName="Libra.Entity.LibraDataContext"
                                            OnSelecting="ldsFiltroPagamento_Selecting" AutoSort="true" AutoGenerateWhereClause="true">
                                        </asp:LinqDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-offset-4 col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="LinkButton4" runat="server" Width="100%"
                                CssClass="btn btn-default" OnClick="lkbCancelar_Click">Cancelar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbVoltarStep2"
                                CssClass="btn btn-dark" Width="100%" OnClick="lkbVoltarStep2_Click">Voltar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbSalvarStep2" ValidationGroup="GStep2"
                                CssClass="btn btn-primary" Width="100%" OnClick="lkbSalvarStep2_Click">Salvar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbSalvarAvancarStep2" ValidationGroup="GStep2"
                                CssClass="btn btn-dark" Width="100%" OnClick="lkbSalvarAvancarStep2_Click">Salvar / Avançar</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div id="divStep3" runat="server" visible="false">
                    <%--<h2 class="StepTitle">Finalizar Venda</h2>--%>
                    <div class="x_panel">
                        <div class="x_title">
                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <h2><i class="fa fa-bookmark"></i><small>FINALIZAR VENDA | Cliente:
                                                    <asp:Label ID="lblClienteFinalizarVenda" runat="server"></asp:Label></small></h2>
                                </div>
                                <div class="col-md-offset-3 col-md-3 col-sm-12 col-xs-12 right">
                                    <small>NÚMERO PEDIDO:
                                                    <asp:Label ID="lblNumeroPedidoFinalizarVenda" runat="server"></asp:Label></small>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="x_content">
                            <div class="row">&nbsp;</div>

                            <div class="row">&nbsp;</div>

                            <div class="row">&nbsp;</div>

                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="row">
                                        <div class="col-md-6 col-md-offset-3 col-sm-12 col-xs-12">
                                            <asp:Button runat="server" ID="btnEmitirPedito" Text="EMITIR PEDIDO" Width="100%" CssClass="btn btn-primary btn-lg" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-2 col-md-offset-5 col-sm-12 col-xs-12">
                                            <asp:Button runat="server" ID="btnCancelarVenda" Text="Cancelar Venda" Width="100%" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>

                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-offset-8 col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="LinkButton7" runat="server" Width="100%"
                                CssClass="btn btn-default" OnClick="lkbCancelar_Click">Cancelar</asp:LinkButton>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12">
                            <asp:LinkButton runat="server" ID="lkbVoltarStep3"
                                CssClass="btn btn-dark" Width="100%" OnClick="lkbVoltarStep3_Click">Voltar</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <script>
        $('#<%= gvResultsProdutos.ClientID %>').dataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bInfo": false,
            "bAutoWidth": false
        });

        $('#<%= gvResultsPagamento.ClientID %>').dataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bInfo": false,
            "bAutoWidth": false
        });
    </script>
</asp:Content>
