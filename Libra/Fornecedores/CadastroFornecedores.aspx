<%@ Page Title="Fornecedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroFornecedores.aspx.cs" Inherits="Libra.Fornecedores.CadastroFornecedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnIdUsuario" />
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Fornecedores
                </h3>
            </div>
        </div>
        <div class="clearfix"></div>
        <div id="divFiltroETabela" class="div-filtro-tabela" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2><i class="fa fa-bookmark"></i><small>Lista de Fornecedores cadastrados.</small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li>
                                    <asp:LinkButton runat="server" ID="lbFiltroUsuarios" CssClass="fa fa-filter" ToolTip="Filtrar Usuarios"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbVisualizarUsuario" CssClass="fa fa-ellipsis-h" ToolTip="Visualizar Usuario"></asp:LinkButton>
                                </li>
                                <li>
                                    <a id="lbAddUsuarios" class="fa fa-plus add-usuario" title="Adicionar Usuario" style="cursor: pointer" accesskey="a"></a>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbEditUsuarios" CssClass="fa fa-pencil" ToolTip="Editar Usuario"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbDelUsuarios" CssClass="fa fa-trash-o" ToolTip="Deletar Usuarios"></asp:LinkButton>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <table class="table-fornecedor table table-striped responsive-utilities jambo_table gvResults table-bordered dt-responsive nowrap no-footer dtr-inline">
                                <thead>
                                    <tr role="row">
                                        <th class="">Ação</th>
                                        <th>Nome Fantasia</th>
                                        <th>Razao Social</th>
                                        <th>Inscrição Estadual</th>
                                        <th>Ramo Atividade</th>
                                        <th>CNPJ</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="template hide">
                                        <td class="" style="width: 100px">
                                            <div class="btn-group" title="Excluir Fornecedor">
                                                <a class="btn btn-default btn-remove" accesskey="r">
                                                    <i class="fa fa-remove"></i>
                                                </a>
                                            </div>
                                            <div class="btn-group" title="Editar Fornecedor">
                                                <a class="btn btn-default btn-edit" accesskey="e">
                                                    <i class="fa fa-pencil-square-o"></i>
                                                </a>
                                            </div>
                                        </td>
                                        <td class="clicavel" data-property="NomeFantasia"></td>
                                        <td class="clicavel" data-property="RazaoSocial"></td>
                                        <td class="clicavel" data-property="InscricaoEstadual"></td>
                                        <td class="clicavel" data-property="RamoAtividade"></td>
                                        <td class="clicavel" data-property="CNPJ"></td>
                                    </tr>
                                </tbody>
                                <tfoot class="no-data">
                                    <tr>
                                        <td class="text-center" colspan="6">Nenhum registro encontrado
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-panel-fornecedor" role="tabpanel" data-example-id="togglable-tabs" style="display: none">
            <ul id="myTab" class="nav nav-tabs bar_tabs" role="tablist">
                <li role="presentation" class="active" runat="server" id="tabFornecedor">
                    <a href="#tab_content_fornecedor" id="fornecedor-tab" role="tab" data-toggle="tab" aria-expanded="true">Fornecedor</a>
                </li>
                <li role="presentation" class="hide" runat="server" id="tabContatos">
                    <a href="#tab_content_contatos" id="contatos-tab" role="tab" data-toggle="tab" aria-expanded="true">Contatos</a>
                </li>
                <li role="presentation" class="hide" runat="server" id="tabEnderecos">
                    <a href="#tab_content_enderecos" id="enderecos-tab" role="tab" data-toggle="tab" aria-expanded="true">Endereços</a>
                </li>
                <li role="presentation" class="hide" runat="server" id="Li1">
                    <a href="#tab_content_produtos" id="produtos-tab" role="tab" data-toggle="tab" aria-expanded="true">Produtos</a>
                </li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <div role="tabpanel" class="tab-pane fade active in" id="tab_content_fornecedor" aria-labelledby="fornecedor-tab">
                    <div id="divFornecedor" runat="server">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <h2>
                                            <asp:Label ID="lbAddEditUsuario" runat="server" Text="Novo Fornecedor"></asp:Label>
                                        </h2>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <div class="row">
                                            <div class="col-md-2 col-sm-2 col-xs-3">
                                                <div>
                                                    <label>
                                                        Tipo
                                                        <asp:Label runat="server" ID="lblTipo" Text="*" CssClass="requerid" />
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:RadioButton ID="chkPessoaFisica" name="chkTipo" GroupName="chkTipo" runat="server"></asp:RadioButton>&nbsp;<span>Pessoa Fisica</span>
                                                    <asp:RadioButton ID="chkPessoaJuridica" name="chkTipo" GroupName="chkTipo" runat="server"></asp:RadioButton>&nbsp;<span>Pessoa Juridica</span>
                                                </div>
                                            </div>
                                            <div class="col-md-2 col-sm-2 col-xs-3">
                                                <div>
                                                    <label>
                                                        Origem
                                                        <asp:Label runat="server" ID="Label1" Text="*" CssClass="requerid" />
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:RadioButton ID="chkNacional" name="chkOrigem" GroupName="chkOrigem" runat="server"></asp:RadioButton>&nbsp;<span>Nacional</span>
                                                    <asp:RadioButton ID="chkEstrangeira" name="chkOrigem" GroupName="chkOrigem" runat="server"></asp:RadioButton>&nbsp;<span>Estrangeira</span>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-4 col-xs-6">
                                                <div>
                                                    <label>
                                                        Razao Social
                                                        <asp:Label runat="server" ID="lblRazaoSocial" Text="*" CssClass="requerid" />
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtRazaoSocial" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRazaoSocial" ControlToValidate="txtRazaoSocial" SetFocusOnError="True" CssClass="requerid"
                                                        ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-4 col-xs-6">
                                                <div>
                                                    <label>
                                                        Nome Fantasia
                                                        <asp:Label runat="server" ID="lblNomeFantasia" Text="*" CssClass="requerid" />
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtNomeFantasia" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNomeFantasia" ControlToValidate="txtNomeFantasia" SetFocusOnError="True" CssClass="requerid"
                                                        ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">&nbsp;</div>
                                        <div class="row">
                                            <div class="col-md-4 col-sm-4 col-xs-6">
                                                <div>
                                                    <label>
                                                        CNPJ
                                            <asp:Label runat="server" ID="lblCNPJ" Text="*" CssClass="requerid" />
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtCNPJ" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCNPJ" ControlToValidate="txtCNPJ" SetFocusOnError="True" CssClass="requerid"
                                                        ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                <div>
                                                    <label>
                                                        Inscrição Estadual
                                                        <asp:Label runat="server" ID="lblInscricaoEstadual" Text="*" CssClass="requerid" />
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtInscricaoEstadual" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvInscricaoEstadual" ControlToValidate="txtInscricaoEstadual" SetFocusOnError="True" CssClass="requerid"
                                                        ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                <div>
                                                    <label>
                                                        Inscrição Municipal
                                            <asp:Label runat="server" ID="lblInscricaoMunicipal" Text="*" CssClass="requerid" />
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtInscricaoMunicipal" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvInscricaoMunicipal" ControlToValidate="txtInscricaoMunicipal" SetFocusOnError="True" CssClass="requerid"
                                                        ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">&nbsp;</div>
                                        <div class="row">
                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                <div>
                                                    <label>
                                                        Responsável
                                                        <asp:Label runat="server" ID="lblResponsavel" Text="*" CssClass="requerid" />
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtResponsavel" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvResponsavel" ControlToValidate="txtResponsavel" SetFocusOnError="True" CssClass="requerid"
                                                        ValidationGroup="G1" Display="Dynamic" runat="server" ErrorMessage="Atenção! Campo de preenchimento obrigatório."></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-4 col-xs-12">
                                                <div>
                                                    <label>
                                                        CRT
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:DropDownList runat="server" ID="ddlCRT" CssClass="form-control" />
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
                                                                <label>DADOS GERAIS</label>
                                                                <asp:Label runat="server" ID="lblUnidadesReq" Text="*" CssClass="required" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="x_content" style="height: 120px!important;">
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-3 col-xs-3">
                                                                <div>
                                                                    <label>
                                                                        Fabricante
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:RadioButton ID="radioFabricanteSim" name="radioFabricanteSim" GroupName="radioFabricante" runat="server"></asp:RadioButton>&nbsp;<span>Sim</span>
                                                                    <asp:RadioButton ID="radioFabricanteNao" name="radioFabricanteNao" GroupName="radioFabricante" runat="server"></asp:RadioButton>&nbsp;<span>Não</span>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 col-sm-3 col-xs-3">
                                                                <div>
                                                                    <label>
                                                                        Receber Email
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:RadioButton ID="radioReceberEmailSim" name="radioReceberEmailSim" GroupName="radioReceberEmail" runat="server"></asp:RadioButton>&nbsp;<span>Sim</span>
                                                                    <asp:RadioButton ID="radioReceberEmailNao" name="radioReceberEmailNao" GroupName="radioReceberEmail" runat="server"></asp:RadioButton>&nbsp;<span>Não</span>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6 col-sm-6 col-xs-6">
                                                                <div>
                                                                    <label>
                                                                        Ramo de Atividade
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtRamoAtividade" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <div>
                                                                    <label>
                                                                        Informação Adicional
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtInformacaoAdicional" TextMode="multiline" Rows="1" runat="server" CssClass="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="x_panel">
                                                    <div class="x_title">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <label>CONTATOS PRINCIPAIS</label>
                                                                <asp:Label runat="server" ID="lblPerfilReq" Text="*" CssClass="required" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="x_content" style="height: 120px!important;">
                                                        <div class="col-md-6 col-sm-6 col-xs-6">
                                                            <div>
                                                                <label>
                                                                    Email
                                                                </label>
                                                            </div>
                                                            <div>
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 col-sm-6 col-xs-6">
                                                            <div>
                                                                <label>
                                                                    Telefone
                                                                </label>
                                                            </div>
                                                            <div>
                                                                <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
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
                                                    CssClass="btn btn-default" Text="Cancelar" AccessKey="c" />
                                            </div>
                                            <div class="col-md-3 col-sm-3 col-xs-6">
                                                <asp:Button runat="server" ID="btnSalvar" ValidationGroup="G1" CssClass="btn btn-primary" Width="100%" Text="Salvar" AccessKey="s" OnClick="btnSalvar_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div role="tabpanel" class="tab-pane fade" id="tab_content_contatos" aria-labelledby="contatos-tab">
                    <div id="divContato" runat="server">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <h2>
                                            <asp:Label ID="Label3" runat="server" Text="Novo Contato"></asp:Label>
                                        </h2>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton3" CssClass="fa fa-plus" ToolTip="Adicionar Contato"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton4" CssClass="fa fa-pencil" ToolTip="Editar Contato"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton5" CssClass="fa fa-trash-o" ToolTip="Deletar Contato"></asp:LinkButton>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="x_panel">
                                                    <div class="x_title">
                                                        <div class="row">
                                                            <div class="col-md-11 col-sm-11 col-xs-11">
                                                                <label>CONTATO</label>
                                                            </div>
                                                            <div class="col-md-1 col-sm-1 col-xs-1">
                                                                <label>
                                                                    <input type="checkbox" value="">&nbsp;Principal</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="x_content" style="height: 80px!important;">
                                                        <div class="row">
                                                            <div class="col-md-2 col-sm-2 col-xs-2">
                                                                <div>
                                                                    <label>
                                                                        Tipo de Cadastro
                                                                        <asp:Label runat="server" ID="Label5" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:DropDownList runat="server" ID="ddlTipoCadastroContato" CssClass="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 col-sm-2 col-xs-2">
                                                                <div>
                                                                    <label>
                                                                        Tipo de Contato
                                                                        <asp:Label runat="server" ID="Label6" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                                <div>
                                                                    <label>
                                                                        Contato
                                                                        <asp:Label runat="server" ID="lblContato" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtContato" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                                <div>
                                                                    <label>
                                                                        Detalhes
                                                                        <asp:Label runat="server" ID="lblDetalhes" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtDetalhesContato" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div role="tabpanel" class="tab-pane fade" id="tab_content_enderecos" aria-labelledby="enderecos-tab">
                    <div id="divEndereco" runat="server">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <h2>
                                            <asp:Label ID="Label2" runat="server" Text="Novo Endereço"></asp:Label>
                                        </h2>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton1" CssClass="fa fa-plus" ToolTip="Adicionar Endereço"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton2" CssClass="fa fa-plus" ToolTip="Adicionar Endereço"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton10" CssClass="fa fa-plus" ToolTip="Adicionar Endereço"></asp:LinkButton>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="x_panel">
                                                    <div class="x_title">
                                                        <div class="row">
                                                            <div class="col-md-8 col-sm-8 col-xs-8">
                                                                <label>ENDEREÇO</label>
                                                            </div>
                                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                                <ul class="nav navbar-right panel_toolbox">
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="LinkButton7" CssClass="fa fa-check" ToolTip="Tornar Principal"></asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="LinkButton9" CssClass="fa fa-trash-o" ToolTip="Deletar Endereço"></asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="LinkButton8" CssClass="fa fa-pencil" ToolTip="Editar Endereço"></asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="x_content" style="height: 140px!important;">
                                                        <div class="row">
                                                            <div class="col-md-2 col-sm-2 col-xs-2">
                                                                <div>
                                                                    <label>
                                                                        ZIP Code
                                                                        <asp:Label runat="server" ID="lblZipCode" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 col-sm-2 col-xs-2">
                                                                <div>
                                                                    <label>
                                                                        Logradouro
                                                                        <asp:Label runat="server" ID="lblLogradouro" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtLogradouro" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-1 col-sm-1 col-xs-1">
                                                                <div>
                                                                    <label>
                                                                        Número
                                                                        <asp:Label runat="server" ID="lblNumero" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-7 col-sm-7 col-xs-7">
                                                                <div>
                                                                    <label>
                                                                        Complemento
                                                                        <asp:Label runat="server" ID="lblComplemento" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtComplemento" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">&nbsp;</div>
                                                        <div class="row">
                                                            <div class="col-md-2 col-sm-2 col-xs-2">
                                                                <div>
                                                                    <label>
                                                                        Pais
                                                                        <asp:Label runat="server" ID="lblPais" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtPais" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-7 col-sm-7 col-xs-7">
                                                                <div>
                                                                    <label>
                                                                        Referencia
                                                                        <asp:Label runat="server" ID="lblReferencia" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 col-sm-3 col-xs-3">
                                                                <div>
                                                                    <label>
                                                                        Tipo Endereço
                                                                        <asp:Label runat="server" ID="lblTipoEndereco" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:DropDownList runat="server" ID="txtTipoEndereco" CssClass="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div role="tabpanel" class="tab-pane fade" id="tab_content_produtos" aria-labelledby="produtos-tab">
                    <div id="divProduto" runat="server">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <h2>
                                            <asp:Label ID="LBLTESTS" runat="server" Text="Novo Produto"></asp:Label>
                                        </h2>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton6" CssClass="fa fa-plus" ToolTip="Adicionar"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton11" CssClass="fa fa-plus" ToolTip="Adicionar"></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton runat="server" ID="LinkButton12" CssClass="fa fa-plus" ToolTip="Adicionar"></asp:LinkButton>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="x_panel">
                                                    <div class="x_title">
                                                        <div class="row">
                                                            <div class="col-md-8 col-md-8 col-md-8">
                                                                <label>PRODUTO</label>
                                                            </div>
                                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                                <ul class="nav navbar-right panel_toolbox">
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="LinkButton13" CssClass="fa fa-check" ToolTip="Principal"></asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="LinkButton14" CssClass="fa fa-trash-o" ToolTip="Deletar"></asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton runat="server" ID="LinkButton15" CssClass="fa fa-pencil" ToolTip="Editar"></asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="x_content" style="height: 80px!important;">
                                                        <div class="row">
                                                            <div class="col-md-6 col-sm-6 col-xs-6">
                                                                <div>
                                                                    <label>
                                                                        Produto
                                                                        <asp:Label runat="server" ID="lblProduto" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtProduto" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 col-sm-3 col-xs-3">
                                                                <div>
                                                                    <label>
                                                                        Cod. Interno
                                                                        <asp:Label runat="server" ID="lblCodInternoProduto" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtCodInternoProdutp" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 col-sm-3 col-xs-3">
                                                                <div>
                                                                    <label>
                                                                        Em Estoque Revenda
                                                                        <asp:Label runat="server" ID="lblQtdEstoqueRevenda" Text="*" CssClass="requerid" />
                                                                    </label>
                                                                </div>
                                                                <div>
                                                                    <asp:TextBox ID="txtQtdEstoqueRevenda" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
                                            <asp:LinkButton runat="server" ID="lkClose"><i class="fa fa-close"></i></asp:LinkButton>
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
                                                                <asp:Button ID="btnMarcarTodosUnidades" runat="server"
                                                                    Text="Marcar Todos" CausesValidation="false" class="btn btn-default btn-xs button-select" />
                                                                <asp:Button ID="btnDesmarcarTodosUnidades" Visible="false" runat="server"
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
                                                                <asp:Button ID="btnMarcarTodosPerfis" runat="server"
                                                                    Text="Marcar Todos" CausesValidation="false" class="btn btn-default btn-xs button-select" />
                                                                <asp:Button ID="btnDesmarcarTodosPerfis" Visible="false" runat="server"
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
                                            <asp:Button runat="server" ID="btnFiltrar" CssClass="btn btn-primary" Text="Filtrar" />
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

    <div class="modal fade modal-fornecedor" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Fornecedor</h4>
                </div>
                <div class="modal-body">
                    <table class="table table-view-fornecedor table-striped table-bordered jambo_table">
                        <thead>
                            <tr role="row">
                                <th>Campo</th>
                                <th>Descrição</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="template hide">
                                <td>Código</td>
                                <td data-property="Id"></td>
                            </tr>
                            <tr class="template hide">
                                <td>Nome Fantasia</td>
                                <td data-property="NomeFantasia"></td>
                            </tr>
                            <tr class="template hide">
                                <td>Razão Social</td>
                                <td data-property="RazaoSocial"></td>
                            </tr>
                            <tr class="template hide">
                                <td>Inscrição Estadual</td>
                                <td data-property="InscricaoEstadual"></td>
                            </tr>
                            <tr class="template hide">
                                <td>Ramo Atividade</td>
                                <td data-property="RamoAtividade"></td>
                            </tr>
                            <tr class="template hide">
                                <td>CNPJ</td>
                                <td data-property="CNPJ"></td>
                            </tr>
                        </tbody>
                        <tfoot class="no-data">
                            <tr>
                                <td class="text-center" colspan="2">Nenhum registro encontrado
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="lkbOculto" runat="server" Text="" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpeFornecedor" TargetControlID="lkbOculto" PopupControlID="pnlVisualizarFornecedor"
        BackgroundCssClass="modalBackground" runat="server" Enabled="True" CancelControlID="lnbFecharVisualizarFornecedor"
        ClientIDMode="AutoID">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlVisualizarFornecedor" runat="server" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <div class="row">
                                        <div class="col-md-11 col-sm-11 col-xs-11">
                                            <h2>Fornecedor
                                        <asp:Label ID="lblFornecedor" runat="server"></asp:Label></h2>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-xs-1">
                                            <asp:LinkButton runat="server" ID="lkCloseVisualizarFornecedor" CssClass="right"><i class="fa fa-close"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Nome Fantasia:
                                                </label>
                                                <asp:Label ID="lbNomeFornecedor" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">&nbsp;</div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div>
                                                <label>
                                                    Razao Social:
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
                                                    Inscrição Estadual:
                                                </label>
                                                <asp:Label ID="lbSexo" runat="server" CssClass="labelInfo"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-6 col-sm-6 col-xs-12">
                                            <div>
                                                <label>
                                                    Ramo Atividade:
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
                                                    CNPJ:
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
                                            <asp:LinkButton ID="lnbFecharVisualizarFornecedor" runat="server"
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
                        <asp:Button ID="btnExcluirUsuario" runat="server" CausesValidation="false"
                            Text="SIM, EXCLUIR" class="btn btn-primary pull-right" />
                        <asp:Button ID="btnCancelarExclusaoUsuario" runat="server"
                            Text="FECHAR" CausesValidation="false" class="btn btn-default pull-right" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <%--<script type="text/javascript" src="../js/Controllers/FornecedorController.js"></script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>
