<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormFechamentoCaixa.ascx.cs" Inherits="Libra.UserControl.Caixa.FormFechamentoCaixa1" %>
<asp:LinkButton ID="lkbOculto" runat="server" Text="" Style="display: none"></asp:LinkButton>
<asp:ModalPopupExtender ID="mpeFechamentoCaixa" TargetControlID="lkbOculto" PopupControlID="pnlFechamentoCaixa"
    BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lblCloseFechamentoCaixaOculto"
    ClientIDMode="AutoID">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlFechamentoCaixa" runat="server" Style="display: none">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="x_panel">
                            <div class="x_title">
                                <div class="row">
                                    <div class="col-md-11 col-sm-11 col-xs-11">
                                        <h2>
                                            <asp:Label runat="server" ID="lblTitulo" Text="ABERTURA CAIXA"></asp:Label></h2>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-xs-1">
                                        <asp:LinkButton runat="server" ID="lblCloseFechamentoCaixa" OnClick="lblCloseFechamentoCaixa_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lblCloseFechamentoCaixaOculto" Style="display: none"><i class="fa fa-close"></i></asp:LinkButton>
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
                                                        <label>Valor Abertura</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox runat="server" ID="txtValorAbertura" CssClass="form-control"></asp:TextBox>
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
                                                    <div class="col-md-4 col-sm-12 col-md-12">
                                                        <div>
                                                            <label>Valor Abertura</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">&nbsp;</div>
                                    <div class="row">
                                        <div class="col-md-offset-8 col-md-2 col-sm-12 col-xs-12">
                                            <asp:LinkButton ID="lkbFechar" runat="server" Width="100%"
                                                CssClass="btn btn-default" OnClick="lkbFechar_Click">Fechar</asp:LinkButton>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12">
                                            <asp:LinkButton runat="server" ID="lkbSalvar"
                                                CssClass="btn btn-primary" Width="100%" OnClick="lkbSalvar_Click"></asp:LinkButton>
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
