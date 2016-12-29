using Libra.Models.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libra.Models
{
    public class ProdutoModels : BaseModel
    {
        public int ProdutoId { get; set; }

        public Int16 TipoProduto { get; set; }

        public int FabricanteId { get; set; }

        public int MarcaId { get; set; }

        public int ModeloId { get; set; }

        public int DimensoesId { get; set; }

        public int CorId { get; set; }

        public string Descricao { get; set; }

        public decimal Peso { get; set; }

        public bool DisponivelComercio { get; set; }
    }
}