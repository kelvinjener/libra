using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libra.Models;

namespace Libra.Controllers.Negocio
{
    public class FornecedoresController
    {
        public bool ValidarFornecedor(FornecedoresModel fornecedorModel)
        {
            if (string.IsNullOrEmpty(fornecedorModel.CNPJ))
            {
                return false;
            }

            return true;
        }
    }
}