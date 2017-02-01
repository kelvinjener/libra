<%@ Page Title="Venda" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Venda.aspx.cs" Inherits="Libra.Vendas.Venda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnIdVenda" runat="server" />
    <asp:HiddenField ID="hdnIdCliente" runat="server" />

    <div>
        <div class="page-title">
            <div class="title_left">
                <h3>Venda
                </h3>
            </div>
        </div>
        <div class="clearfix"></div>
        <div id="divPrincipal" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <div class="row">
                                <h2><i class="fa fa-bookmark"></i><small>DADOS DA VENDA</small></h2>
                            </div>
                            <div class="row">&nbsp;</div>

                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
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
                                        <div class="col-md-2 col-sm-12 col-xs-12">
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
                                        <div class="col-md-6 col-sm-12 col-xs-12">
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
                                <div class="col-md-6 col-sm-12 col-xs-12">

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
                                                        ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12">
                                                    <asp:LinkButton ID="lkbPesquisarCliente" runat="server" Width="100%"
                                                        CssClass="btn btn-default"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12">
                                                    <asp:LinkButton runat="server" ID="lkbAddCliente"
                                                        CssClass="btn btn-primary" Width="100%"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">

                            <div id="wizard" class="form_wizard wizard_horizontal">

                                <ul class="wizard_steps">
                                    <li>
                                        <a href="#step-1">
                                            <span class="step_no">1º</span>
                                            <span class="step_descr">Produtos<br />
                                                <small>Selecione os produtos a vender</small>
                                            </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#step-2">
                                            <span class="step_no">2º</span>
                                            <span class="step_descr">Pagamento<br />
                                                <small>Informe as formas de pagamento</small>
                                            </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#step-3">
                                            <span class="step_no">3º</span>
                                            <span class="step_descr">Finalizar Venda<br />
                                                <small>Emitir pedido</small>
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                                <div id="step-1">
                                    <h2 class="StepTitle">Produtos</h2>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <%--Tabela--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <asp:LinkButton runat="server" ID="lkbAddItem" CssClass="btn">
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
                                                        <div class="col-md-6 col-sm-12 col-xs-12">
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
                                <div id="step-2">
                                    <h2 class="StepTitle">Pagamento</h2>
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
                                                <asp:LinkButton runat="server" ID="lkbAddNovoPagamento" CssClass="btn">
                                                <i class="fa fa-plus" aria-hidden="true"></i>&nbsp; Inserir Pagamento
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">&nbsp;</div>
                                        <div class="row" id="formAddPagamento">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="x_panel">
                                                    <div class="x_content">
                                                        <div class="row">
                                                            <div class="col-md-5 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>Forma de pagamento</label>
                                                                </div>
                                                                <div>
                                                                    <asp:DropDownList runat="server" ID="ddlFormaPagamento" CssClass="form-control select2_single"></asp:DropDownList>
                                                                    <asp:LinkButton runat="server" ID="lkbAddFormaPagamento" ToolTip="Adicionar forma de pagamento - Parâmetro" CssClass="btn"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:RequiredFieldValidator ID="rfvDdlFormaPagamento" ControlToValidate="ddlFormaPagamento" SetFocusOnError="True" CssClass="requerid"
                                                                        ValidationGroup="GPag" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
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
                                                            <div class="col-md-2 col-sm-12 col-xs-12" id="divParcelas">
                                                                <div>
                                                                    <label>Parcelas</label>
                                                                </div>
                                                                <div>
                                                                    <asp:DropDownList runat="server" ID="ddlParcelas" CssClass="form-control select2_single"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlParcelas" ControlToValidate="ddlParcelas" SetFocusOnError="True" CssClass="requerid"
                                                                        ValidationGroup="GPag" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12" id="divValorParcelas">
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
                                                                    <asp:LinkButton runat="server" ID="lkbAddPagamento" CssClass="btn btn-primary"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <%--Tabela--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="step-3">
                                    <h2 class="StepTitle">Finalizar Venda</h2>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <!-- form wizard -->
    <script type="text/javascript" src="../Scripts/wizard/jquery.smartWizard.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Smart Wizard
            $('#wizard').smartWizard();

            function onFinishCallback() {
                $('#wizard').smartWizard('showMessage', 'Finish Clicked');
                //alert('Finish Clicked');
            }
        });


  </script>

</asp:Content>
