<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormFechamentoCaixa.ascx.cs" Inherits="Libra.UserControl.Caixa.FormFechamentoCaixa1" %>

<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <div class="row">
                    <div class="col-md-11 col-sm-11 col-xs-11">
                        <h2>
                            <asp:Label runat="server" ID="lblTitulo" Text="ABERTURA CAIXA"></asp:Label></h2>
                    </div>

                </div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="row">
                    <div class="col-md-4 col-sm-12 col-md-12">
                        <div>
                            <label>Unidade</label>
                        </div>
                        <div>
                            <asp:Label runat="server" ID="lblUnidade" CssClass="labelInfo"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12 col-md-12">
                        <div>
                            <label>Usuário Responsável</label>
                        </div>
                        <div>
                            <asp:Label runat="server" ID="lblUsuarioResponsavel" CssClass="labelInfo"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12 col-md-12">
                        <div>
                            <label>Situação Caixa</label>
                        </div>
                        <div>
                            <asp:Label runat="server" ID="lblSituacaoCaixa" CssClass="labelInfo"></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="x_panel">
                        <div class="x_title">
                            <div class="row">
                                <div class="col-md-11 col-sm-11 col-xs-11">
                                    <h2><small>ABERTURA</small>
                                    </h2>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-md-12">
                                    <div>
                                        <label>Data/Hora Abertura</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblDataHoraAbertura" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-md-12">
                                    <div>
                                        <label>Valor Abertura (R$)</label>
                                    </div>
                                    <div>
                                        <asp:TextBox runat="server" ID="txtValorAbertura" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvValorAbertura" ControlToValidate="txtValorAbertura" SetFocusOnError="True" CssClass="requerid"
                                            ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">&nbsp;</div>
                <div class="row" id="divFechamento" runat="server">
                    <div class="x_panel">
                        <div class="x_title">
                            <div class="row">
                                <div class="col-md-11 col-sm-11 col-xs-11">
                                    <h2><small>FECHAMENTO</small>
                                    </h2>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-md-12">
                                    <div>
                                        <label>Valor Total Vendas</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lbValorTotalVendas" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <label>Valor Total em Dinheiro</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblValorDinheiro" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <label>Valor Total em Cartão de Débito</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblCartaoDebito" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <label>Valor Total em Cartão de Crédito</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblCartaoCredito" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <asp:LinkButton ID="lkbDetalharEntradas" runat="server" Width="100%"
                                            CssClass="btn btn-dark" OnClick="lkbFechar_Click">Detalhar Entradas</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <label>Valor Total Sangria</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblValorSangria" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <label>Valor Total Suprimentos</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblValorSuprimentos" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-offset-md-3 col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <asp:LinkButton ID="lkDetalharSaidas" runat="server" Width="100%"
                                            CssClass="btn btn-dark" OnClick="lkbFechar_Click">Detalhar Saídas</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <label>Valor Fechamento Caixa</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblValorFechamento" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-12 col-md-12">
                                    <div>
                                        <label>Data/Hora Fechamento</label>
                                    </div>
                                    <div>
                                        <asp:Label runat="server" ID="lblDataHoraFechamento" CssClass="labelInfo"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-md-offset-8 col-md-2 col-sm-12 col-xs-12" runat="server" id="divBtnFechar">
                        <asp:LinkButton ID="lkbFechar" runat="server" Width="100%"
                            CssClass="btn btn-default" OnClick="lkbFechar_Click">Fechar</asp:LinkButton>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12" runat="server" id="divBtnSalvar">
                        <asp:LinkButton runat="server" ID="lkbSalvar" ValidationGroup="G1"
                            CssClass="btn btn-primary" Width="100%" OnClick="lkbSalvar_Click"></asp:LinkButton>
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>

