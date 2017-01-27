using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class EstoqueProdutoBll : LibraBusinessLogic<ESTOQUEPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public ESTOQUEPRODUTO GetEstoqueById(int idEstoqueProduto)
        {
            return dc.ESTOQUEPRODUTOs.Where(u => u.ESTOQUEID == idEstoqueProduto).FirstOrDefault();
        }

        public List<ESTOQUEPRODUTO> GetAllProdutos()
        {
            return dc.ESTOQUEPRODUTOs.OrderBy(u => u.PRODUTO.DESCRICAO).ToList();
        }

        public List<EstoqueProdutoModelGrid> GetAllEstoqueProdutosGrid()
        {
            var result = (from estoque in dc.ESTOQUEPRODUTOs
                          orderby estoque.PRODUTO.DESCRICAO
                          select new EstoqueProdutoModelGrid
                          {
                              EstoqueId = estoque.ESTOQUEID,
                              CodigoEstoque = estoque.CODIGOESTOQUE,
                              ProdutoId = estoque.PRODUTOID,
                              DescricaoProduto = estoque.PRODUTO.DESCRICAO,
                              CodigoDescricaoProduto = estoque.PRODUTO.CODIGOPRODUTO + " - " + estoque.PRODUTO.DESCRICAO,
                              UnidadeId = estoque.UNIDADEID,
                              DescricaoUnidade = estoque.UNIDADE.APELIDO,
                              ValorCompra = Convert.ToDecimal(estoque.VALORCUSTO),
                              ValorVenda = Convert.ToDecimal(estoque.VALORVENDA),
                              QtdDisponivel = estoque.QTDENTRADA
                          });

            return result.Distinct().OrderBy(p => p.DescricaoProduto).ToList();
        }

        public List<EstoqueProdutoModelGrid> GetAllEstoqueProdutosGridFiltro(/*string codigoProduto, string nomeProduto, List<bool> disponivel, int tipoProduto, int fabricante, int marca, int modelo, int cor*/)
        {
            var result = (from estoque in dc.ESTOQUEPRODUTOs
                          orderby estoque.PRODUTO.DESCRICAO
                          select new
                          { estoque });

            //if (disponivel.Count > 0)
            //    result = result.Where(a => disponivel.Contains(a.p.DISPONIVELCOMERCIO));

            //if (!codigoProduto.Equals(string.Empty))
            //    result = result.Where(a => a.p.CODIGOPRODUTO.ToUpper().Contains(codigoProduto.ToUpper()));

            //if (!nomeProduto.Equals(string.Empty))
            //    result = result.Where(a => a.p.DESCRICAO.ToUpper().Contains(nomeProduto.ToUpper()));

            //if (!tipoProduto.Equals(null) && tipoProduto > 0)
            //    result = result.Where(a => a.p.TIPOPRODUTOID.Equals(tipoProduto));

            //if (!fabricante.Equals(null) && fabricante > 0)
            //    result = result.Where(a => a.p.FABRICANTEID.Equals(fabricante));

            //if (!marca.Equals(null) && marca > 0)
            //    result = result.Where(a => a.p.MARCAID.Equals(marca));

            //if (!modelo.Equals(null) && modelo > 0)
            //    result = result.Where(a => a.p.MODELOID.Equals(modelo));

            //if (!cor.Equals(null) && cor > 0)
            //    result = result.Where(a => a.p.CORID.Equals(cor));

            var x = from p in result
                    select new EstoqueProdutoModelGrid
                    {
                        EstoqueId = p.estoque.ESTOQUEID,
                        CodigoEstoque = p.estoque.CODIGOESTOQUE,
                        ProdutoId = p.estoque.PRODUTOID,
                        DescricaoProduto = p.estoque.PRODUTO.DESCRICAO,
                        CodigoDescricaoProduto = p.estoque.PRODUTO.CODIGOPRODUTO + " - " + p.estoque.PRODUTO.DESCRICAO,
                        UnidadeId = p.estoque.UNIDADEID,
                        DescricaoUnidade = p.estoque.UNIDADE.APELIDO,
                        ValorCompra = Convert.ToDecimal(p.estoque.VALORCUSTO),
                        ValorVenda = Convert.ToDecimal(p.estoque.VALORVENDA),
                        QtdDisponivel = p.estoque.QTDENTRADA
                    };

            return x.Distinct().OrderBy(p => p.DescricaoProduto).ToList();
        }

        public void Salvar(ESTOQUEPRODUTO estoque)
        {
            if (estoque.ESTOQUEID > 0)
                Atualizar(estoque);
            else
                Inserir(estoque);
        }
    }

    public class EstoqueProdutoModelGrid
    {
        public int EstoqueId { get; set; }
        public string CodigoEstoque { get; set; }
        public int ProdutoId { get; set; }
        public string DescricaoProduto { get; set; }
        public string CodigoDescricaoProduto { get; set; }
        public int UnidadeId { get; set; }
        public string DescricaoUnidade { get; set; }
        public Decimal ValorCompra { get; set; }
        public Decimal ValorVenda { get; set; }
        public int QtdDisponivel { get; set; }
    }
}
