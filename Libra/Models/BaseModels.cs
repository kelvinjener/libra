using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libra.Models
{
    public class BaseModels
    {
        public bool Ativo { get; set; }

        public DateTime? DataCriacao { get; set; }

        public int CriadoPor { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int AlteradoPor { get; set; }
    }
}