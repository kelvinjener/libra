using Libra.Communs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Libra.Models
{
    [Table("Unidade")]
    public class UnidadeModel : BaseModels
    {
        public int UnidadeId { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Fax { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Observacao { get; set; }
        public Int16 TipoUnidade { get; set; }
    }
}