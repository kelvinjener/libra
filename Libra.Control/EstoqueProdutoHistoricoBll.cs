using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class EstoqueProdutoHistoricoBll : LibraBusinessLogic<ESTOQUEPRODUTOSHISTORICO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public ESTOQUEPRODUTOSHISTORICO GetEstoqueHistoricoById(int idEstoqueProdutoHistorico)
        {
            return dc.ESTOQUEPRODUTOSHISTORICOs.Where(u => u.HISTORICOID == idEstoqueProdutoHistorico).FirstOrDefault();
        }

        public List<EstoqueProdutoHistoricoModelGrid> GetAllEstoqueProdutosGridByEstoqueId(int estoqueId)
        {
            var result = (from estoque in dc.ESTOQUEPRODUTOSHISTORICOs
                          where estoque.ESTOQUEID == estoqueId
                          select new EstoqueProdutoHistoricoModelGrid
                          {
                              HistoricoId = estoque.HISTORICOID,
                              EstoqueId = estoque.ESTOQUEID,
                              CodigoEstoque = estoque.ESTOQUEPRODUTO.CODIGOESTOQUE,
                              DescricaoProduto = estoque.ESTOQUEPRODUTO.PRODUTO.DESCRICAO,
                              DescricaoUnidade = estoque.ESTOQUEPRODUTO.UNIDADE.APELIDO,
                              ValorCompra = Convert.ToDecimal(estoque.VALORCUSTO),
                              MargemLucro = Decimal.Round((decimal)estoque.MARGEMLUCRO, 2),
                              ValorVenda = Convert.ToDecimal(estoque.VALORVENDA),
                              QtdDisponivel = Convert.ToInt32(estoque.QTDESTOQUE),
                              QtdEntrada = estoque.QTDENTRADA,
                              AlteradoPor = estoque.USUARIO.NOME,
                              DataAlteracao = estoque.DATAALTERACAO
                          });

            return result.Distinct().OrderByDescending(p => p.DataAlteracao).ToList();
        }

        public void Salvar(ESTOQUEPRODUTOSHISTORICO historico)
        {
            if (historico.HISTORICOID > 0)
                Atualizar(historico);
            else
                Inserir(historico);
        }
    }

    public class EstoqueProdutoHistoricoModelGrid
    {
        public int HistoricoId { get; set; }
        public int EstoqueId { get; set; }
        public string CodigoEstoque { get; set; }
        public string DescricaoProduto { get; set; }
        public string DescricaoUnidade { get; set; }
        public Decimal ValorCompra { get; set; }
        public Decimal MargemLucro { get; set; }
        public Decimal ValorVenda { get; set; }
        public int QtdDisponivel { get; set; }
        public int QtdEntrada { get; set; }
        public string AlteradoPor { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
