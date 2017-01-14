using Libra.Controllers.Negocio;
using Libra.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraUnitTest.Controllers
{
    [TestClass]
    public class FornecedoresControllerTest
    {
        [TestMethod]
        public void DeveValidarCampos()
        {
            //arrange
            var entidade = new FornecedoresModel();
            entidade.Id = 1;
            //entidade.TipoFornecedorId = "";
            //entidade.RazaoSocial = "jose";
            //entidade.NomeFantasia = "jose";
            //entidade.CNPJ = null;
            //entidade.InscricaoEstadual = null;
            //entidade.InscricaoMunicipal = null;
            //entidade.Responsavel = null;
            //entidade.CRTId = null;
            //entidade.IndicadorFabricante = "";
            //entidade.ReceberEmail = "";
            //entidade.RamoAtividade = "";
            //entidade.InformacaoAdicional = "";
            //entidade.Ativo = "";

            //act
            var c = new FornecedoresController();

            //asert
            Assert.AreEqual(1, false, "O fornecedor informado não possui CNPJ");
        }
    }
}
