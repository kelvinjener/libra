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

        public ESTOQUEPRODUTO GetEstoqueByIdProdutoAndUnidade(int idProduto, int idUnidade)
        {
            return dc.ESTOQUEPRODUTOs.Where(u => u.PRODUTOID == idProduto && u.UNIDADEID == idUnidade).FirstOrDefault();
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
                              QtdDisponivel = Convert.ToInt32(estoque.QTDESTOQUE)
                          });

            return result.Distinct().OrderBy(p => p.DescricaoProduto).ToList();
        }

        public List<EstoqueProdutoModelGrid> GetAllEstoqueProdutosGridFiltro(string codigoEstoqueProduto, string codigoProduto, string nomeProduto, int tipoProduto, int fabricante, int marca, int modelo, int tamanho, int cor)
        {
            var result = (from e in dc.ESTOQUEPRODUTOs
                          orderby e.PRODUTO.DESCRICAO
                          select new
                          { e });

            if (!codigoEstoqueProduto.Equals(string.Empty))
                result = result.Where(a => a.e.CODIGOESTOQUE.ToUpper().Contains(codigoEstoqueProduto.ToUpper()));

            if (!codigoProduto.Equals(string.Empty))
                result = result.Where(a => a.e.PRODUTO.CODIGOPRODUTO.ToUpper().Contains(codigoProduto.ToUpper()));

            if (!nomeProduto.Equals(string.Empty))
                result = result.Where(a => a.e.PRODUTO.DESCRICAO.ToUpper().Contains(nomeProduto.ToUpper()));

            if (!tipoProduto.Equals(null) && tipoProduto > 0)
                result = result.Where(a => a.e.PRODUTO.TIPOPRODUTOID.Equals(tipoProduto));

            if (!fabricante.Equals(null) && fabricante > 0)
                result = result.Where(a => a.e.PRODUTO.FABRICANTEID.Equals(fabricante));

            if (!marca.Equals(null) && marca > 0)
                result = result.Where(a => a.e.PRODUTO.MARCAID.Equals(marca));

            if (!modelo.Equals(null) && modelo > 0)
                result = result.Where(a => a.e.PRODUTO.MODELOID.Equals(modelo));

            if (!tamanho.Equals(null) && cor > 0)
                result = result.Where(a => a.e.PRODUTO.DIMENSOESID.Equals(tamanho));

            if (!cor.Equals(null) && cor > 0)
                result = result.Where(a => a.e.PRODUTO.CORID.Equals(cor));

            var x = from p in result
                    select new EstoqueProdutoModelGrid
                    {
                        EstoqueId = p.e.ESTOQUEID,
                        CodigoEstoque = p.e.CODIGOESTOQUE,
                        ProdutoId = p.e.PRODUTOID,
                        DescricaoProduto = p.e.PRODUTO.DESCRICAO,
                        CodigoDescricaoProduto = p.e.PRODUTO.CODIGOPRODUTO + " - " + p.e.PRODUTO.DESCRICAO,
                        UnidadeId = p.e.UNIDADEID,
                        DescricaoUnidade = p.e.UNIDADE.APELIDO,
                        ValorCompra = Convert.ToDecimal(p.e.VALORCUSTO),
                        ValorVenda = Convert.ToDecimal(p.e.VALORVENDA),
                        QtdDisponivel = Convert.ToInt32(p.e.QTDESTOQUE)
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
