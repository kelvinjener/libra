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

        public List<PRODUTO> GetAllProdutosDisponveis()
        {
            return dc.PRODUTOs.Where(p => p.DISPONIVELCOMERCIO).OrderBy(u => u.DESCRICAO).ToList();
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
                              DisponivelComercio = produto.DISPONIVELCOMERCIO,
                              CodigoProduto = produto.CODIGOPRODUTO
                          });

            return result.Distinct().OrderBy(p => p.Descricao).ToList();
        }

        public List<ProdutoModelGrid> GetAllProdutosGridFiltro(string codigoProduto, string nomeProduto, List<bool> disponivel, int tipoProduto, int fabricante, int marca, int modelo, int cor)
        {
            var result = (from p in dc.PRODUTOs
                          orderby p.DESCRICAO
                          select new
                          { p });

            if (disponivel.Count > 0)
                result = result.Where(a => disponivel.Contains(a.p.DISPONIVELCOMERCIO));

            if (!codigoProduto.Equals(string.Empty))
                result = result.Where(a => a.p.CODIGOPRODUTO.ToUpper().Contains(codigoProduto.ToUpper()));

            if (!nomeProduto.Equals(string.Empty))
                result = result.Where(a => a.p.DESCRICAO.ToUpper().Contains(nomeProduto.ToUpper()));

            if (!tipoProduto.Equals(null) && tipoProduto > 0)
                result = result.Where(a => a.p.TIPOPRODUTOID.Equals(tipoProduto));

            if (!fabricante.Equals(null) && fabricante > 0)
                result = result.Where(a => a.p.FABRICANTEID.Equals(fabricante));

            if (!marca.Equals(null) && marca > 0)
                result = result.Where(a => a.p.MARCAID.Equals(marca));

            if (!modelo.Equals(null) && modelo > 0)
                result = result.Where(a => a.p.MODELOID.Equals(modelo));

            if (!cor.Equals(null) && cor > 0)
                result = result.Where(a => a.p.CORID.Equals(cor));

            var x = from p in result
                    select new ProdutoModelGrid
                    {
                        ProdutoId = p.p.PRODUTOID,
                        Descricao = p.p.DESCRICAO,
                        Peso = Convert.ToDecimal(p.p.PESO),
                        DisponivelComercio = p.p.DISPONIVELCOMERCIO,
                        CodigoProduto = p.p.CODIGOPRODUTO
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
        public string CodigoProduto { get; set; }

    }
}
