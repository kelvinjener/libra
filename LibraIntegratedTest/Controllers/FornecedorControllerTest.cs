using Libra.Controllers.Negocio;
using Libra.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraIntegratedTest.Controllers
{
    [TestClass]
    public class FornecedorControllerTest
    {
        [TestMethod]
        public void DeveListarFornecedores()
        {
            //arrange
            var c = new FornecedoresController();

            //act
            var r = c.RetornaFornecedores();

            //assert
            Assert.AreNotEqual(r, null, "Não foram listados os fornecedores como deveria.");
        }

        [TestMethod]
        public void DeveListarFornecedor()
        {
            //arrange
            int id = 5;
            var c = new FornecedoresController();

            //act
            var r = c.RetornaFornecedor(id);

            //assert
            Assert.AreNotEqual(r, null, "Não foram listados os fornecedores como deveria.");
        }

        [TestMethod]
        public void DeveExcluirFornecedor()
        {
            //arrange
            int id = 5;
            var c = new FornecedoresController();

            //act
            var r = c.ExcluirFornecedor(id);

            //assert
            Assert.AreNotEqual(r, null, "Não foram listados os fornecedores como deveria.");
        }

        [TestMethod]
        public void DeveInserirFornecedor()
        {
            //arrange
            var c = new FornecedoresController();
            var fornecedor = new FORNECEDORE()
            {
                FORNECEDORID = 10,
                TIPOFORNECEDORID = 1,
                ORIGEMFORNECEDORID = 1,
                RAZAOSOCIAL = "",
                NOMEFANTASIA = "",
                CNPJ = "",
                INSCRICAOESTADUAL = "",
                INSCRICAOMUNICIPAL = "",
                RESPONSAVEL = "",
                INDICADORFABRICANTE = true,
                INDICADORRECEBEREMAIL = true,
                RAMOATIVIDADE = "",
                INFOADICIONAL = "",
            };

            //act
            var r = c.Inserir(fornecedor);

            //assert
            Assert.AreNotEqual(r, null, "Não foram listados os fornecedores como deveria.");
        }
    }
}
