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
        public void DeveConterCNPJ()
        {
            //arrange
            var fornecedorModel = new FornecedoresModel();
            fornecedorModel.Id = 1;
            fornecedorModel.Nome = "jose";
            fornecedorModel.CNPJ = null;

            //act
            var c = new FornecedoresController();
            var resultado = c.ValidarFornecedor(fornecedorModel);

            //asert
            Assert.AreEqual(resultado, false, "O fornecedor informado não possui CNPJ");
        }
    }
}
