using Libra.Class;
using Libra.Controllers;
using Libra.Controllers.Core;
using Libra.Controllers.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Libra.Models;
using System.Web.Script.Serialization;

namespace Libra.Fornecedores
{
    public partial class CadastroFornecedores : BasePage
    {
        private FornecedoresController _controller;

        public CadastroFornecedores()
        {
            _controller = new FornecedoresController();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaCRT();
        }

        public void CarregaCRT()
        {
            var instancia = new CRTController();
            var itens = instancia.RetornaTodos();

            ddlCRT.Items.Clear();
            ddlCRT.Items.Add(new ListItem("Selecione...", ""));

            foreach (var item in itens)
            {
                string value = Convert.ToString(item.ID);
                string text = item.DESCRICAO;

                ddlCRT.Items.Add(new ListItem(text, value));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static string RetornaFornecedores()
        {
            var c = new FornecedoresController();
            List<FornecedoresModel> r = c.RetornaFornecedores();

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(r);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static string RetornaFornecedor(int id)
        {
            var c = new FornecedoresController();
            FornecedoresModel r = c.RetornaFornecedor(id);

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(r);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static string ExcluirFornecedor(int id)
        {
            var c = new FornecedoresController();
            Resultado r = c.ExcluirFornecedor(id);

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(r);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Resultado r = new Resultado();

                    var entidade = new FornecedoresModel()
                    {
                        TipoFornecedorId = chkPessoaFisica.Checked ? 1 : 2,
                        OrigemFornecedorId = chkNacional.Checked ? 1 : 2,
                        RazaoSocial = txtRazaoSocial.Text,
                        NomeFantasia = txtNomeFantasia.Text,
                        CNPJ = txtCNPJ.Text,
                        InscricaoEstadual = txtInscricaoEstadual.Text,
                        InscricaoMunicipal = txtInscricaoMunicipal.Text,
                        Responsavel = txtResponsavel.Text,
                        IndicadorFabricante = radioFabricanteSim.Checked ? true : false,
                        IndicadorReceberEmail = radioReceberEmailSim.Checked ? true : false,
                        RamoAtividade = txtRamoAtividade.Text,
                        InfoAdicional = txtInformacaoAdicional.Text
                    };

                    if (entidade.Id == null)
                        r = _controller.InserirFornecedor(entidade);
                    else
                        r = _controller.AtualizarFornecedor(entidade);

                    if (r.IsError)
                        this.MessageBoxError(this.Page, r.Message);
                    else
                        this.MessageBoxSucesso(this.Page, r.Message);
                }
                catch (Exception ex)
                {
                    HandlerException(ex);
                }
            }
        }
    }
}