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
        public int TipoFornecedorId { get; set; }
        public int OrigemFornecedorId { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string Responsavel { get; set; }
        public bool IndicadorFabricante { get; set; }
        public bool IndicadorReceberEmail { get; set; }
        public string RamoAtividade { get; set; }
        public string InfoAdicional { get; set; }
    }
}