using Libra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libra.ViewModels
{
    public class ProdutoViewModels : ProdutoModels
    {
        public string FabricanteDescricao { get; set; }

        public string MarcaDescricao { get; set; }

        public string ModeloDescricao { get; set; }

        public string DimensoesDescricao { get; set; }

        public string CorDescricao { get; set; }
    }
}