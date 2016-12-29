using Libra.Models.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libra.Models
{
    public class FornecedoresModel : BaseModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
    }
}