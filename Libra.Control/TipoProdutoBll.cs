using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class TipoProdutoBll : LibraBusinessLogic<TIPOPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaProdutoTipoProdutoAssociados(int idTipoProduto)
        {
            bool associado = false;

            List<PRODUTO> produto = (from p in dc.PRODUTOs
                                     where p.TIPOPRODUTOID == idTipoProduto
                                     select p).ToList();

            if (produto.Count > 0)
                associado = true;

            return associado;
        }

        public TIPOPRODUTO GetTipoProdutoById(int idTipoProduto)
        {
            return dc.TIPOPRODUTOs.Where(u => u.TIPOPRODUTOID == idTipoProduto).FirstOrDefault();
        }

        public List<TIPOPRODUTO> GetAllTiposProdutos()
        {
            return dc.TIPOPRODUTOs.OrderBy(u => u.NOME).ToList();
        }

        public List<TipoProdutoModelGrid> GetAllTiposProdutosGrid()
        {
            var result = (from TipoProduto in dc.TIPOPRODUTOs
                          orderby TipoProduto.NOME
                          select new TipoProdutoModelGrid
                          {
                              TipoProdutoId = TipoProduto.TIPOPRODUTOID,
                              Nome = TipoProduto.NOME,
                              Ativo = TipoProduto.ATIVO
                          });

            return result.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<TipoProdutoModelGrid> GetAllTiposProdutosGridFiltro(List<bool> listAtivo, string nomeTipoProduto)
        {
            var result = (from TipoProduto in dc.TIPOPRODUTOs
                          orderby TipoProduto.NOME
                          select new
                          {
                              TipoProduto
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.TipoProduto.ATIVO));
            }

            if (!nomeTipoProduto.Equals(string.Empty))
                result = result.Where(a => a.TipoProduto.NOME.ToUpper().Contains(nomeTipoProduto.ToUpper()));

            var x = from p in result
                    select new TipoProdutoModelGrid
                    {
                        TipoProdutoId = p.TipoProduto.TIPOPRODUTOID,
                        Nome = p.TipoProduto.NOME,
                        Ativo = p.TipoProduto.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<TIPOPRODUTO> GetAllAtivos()
        {
            return dc.TIPOPRODUTOs.Where(u => u.ATIVO).OrderBy(u => u.NOME).ToList();
        }


        public void Salvar(TIPOPRODUTO TipoProduto)
        {
            if (TipoProduto.TIPOPRODUTOID > 0)
                Atualizar(TipoProduto);
            else
                Inserir(TipoProduto);
        }
    }

    public class TipoProdutoModelGrid
    {
        public int TipoProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

    }
}

