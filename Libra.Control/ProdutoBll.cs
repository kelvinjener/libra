using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class ProdutoBll : LibraBusinessLogic<PRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public PRODUTO GetProdutoById(int idProduto)
        {
            return dc.PRODUTOs.Where(u => u.PRODUTOID == idProduto).FirstOrDefault();
        }

        public List<PRODUTO> GetAllProdutos()
        {
            return dc.PRODUTOs.OrderBy(u => u.DESCRICAO).ToList();
        }

        public List<ProdutoModelGrid> GetAllProdutosGrid()
        {
            var result = (from produto in dc.PRODUTOs
                          orderby produto.DESCRICAO
                          select new ProdutoModelGrid
                          {
                              ProdutoId = produto.PRODUTOID,
                              Descricao = produto.DESCRICAO,
                              Peso = Convert.ToDecimal(produto.PESO),
                              DisponivelComercio = produto.DISPONIVELCOMERCIO
                          });

            return result.Distinct().OrderBy(p => p.Descricao).ToList();
        }

        public List<ProdutoModelGrid> GetAllProdutosGridFiltro(string nomeProduto)
        {
            var result = (from produto in dc.PRODUTOs
                          orderby produto.DESCRICAO
                          select new
                          {
                              produto
                          });

            if (!nomeProduto.Equals(string.Empty))
                result = result.Where(a => a.produto.DESCRICAO.ToUpper().Contains(nomeProduto.ToUpper()));

            //if (!tipoProduto.Equals(string.Empty))
            //    result = result.Where(a => a.produto.TIPOPRODUTO == (TipoProduto)Enum.Parse(typeof(TipoProduto), tipoProduto));

            var x = from p in result
                    select new ProdutoModelGrid
                    {
                        ProdutoId = p.produto.PRODUTOID,
                        Descricao = p.produto.DESCRICAO,
                        Peso = Convert.ToDecimal(p.produto.PESO),
                        DisponivelComercio = p.produto.DISPONIVELCOMERCIO
                    };

            return x.Distinct().OrderBy(p => p.Descricao).ToList();
        }

        public void Salvar(PRODUTO produto)
        {
            if (produto.PRODUTOID > 0)
                Atualizar(produto);
            else
                Inserir(produto);
        }
    }

    public class ProdutoModelGrid
    {
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public Decimal Peso { get; set; }
        public bool DisponivelComercio { get; set; }

    }
}
