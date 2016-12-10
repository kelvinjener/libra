using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libra.Models
{
    public class FornecedoresModel : BaseModels
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
    }
}