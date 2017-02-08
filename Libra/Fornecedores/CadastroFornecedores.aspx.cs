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
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string PersistirFornecedor(int id, int tipoFornecedorId, int origemFornecedorId, string razaoSocial, string nomeFantasia, string cnpj, string inscricaoEstadual, string inscricaoMunicipal, string responsavel, bool indFabricante, bool indRecebEmail, string ramoAtividade, string infoAdicional)
        {
            Resultado r = new Resultado();
            var c = new FornecedoresController();

            var entidade = new FornecedoresModel()
            {
                Id = id,
                TipoFornecedorId = tipoFornecedorId,
                OrigemFornecedorId = origemFornecedorId,
                RazaoSocial = razaoSocial,
                NomeFantasia = nomeFantasia,
                CNPJ = cnpj,
                InscricaoEstadual = inscricaoEstadual,
                InscricaoMunicipal = inscricaoMunicipal,
                Responsavel = responsavel,
                IndicadorFabricante = indFabricante,
                IndicadorReceberEmail = indRecebEmail,
                RamoAtividade = ramoAtividade,
                InfoAdicional = infoAdicional
            };

            if (entidade.Id == null)
            {
                r = c.InserirFornecedor(entidade);
            }
            else
            {
                r = c.AtualizarFornecedor(entidade);
            }

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(r);
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
    }
}