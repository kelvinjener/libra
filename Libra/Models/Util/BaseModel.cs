using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libra.Models.Util
{
    public class BaseModel
    {
        public bool Ativo { get; set; }

        public int CriadoPor { get; set; }

        public int? AlteradoPor { get; set; }

        public int? ExcluidoPor { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public DateTime? DataExclusao { get; set; }
    }
}