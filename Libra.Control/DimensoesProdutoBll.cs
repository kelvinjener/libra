using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class DimensoesProdutoBll : LibraBusinessLogic<DIMENSOESPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaProdutoDimensoesAssociados(int idDimensoes)
        {
            bool associado = false;

            List<PRODUTO> produto = (from p in dc.PRODUTOs
                                     where p.DIMENSOESID == idDimensoes
                                     select p).ToList();

            if (produto.Count > 0)
                associado = true;

            return associado;
        }

        public DIMENSOESPRODUTO GetDimensoesProdutoById(int idDimensoesProduto)
        {
            return dc.DIMENSOESPRODUTOs.Where(u => u.DIMENSOESPRODUTOID == idDimensoesProduto).FirstOrDefault();
        }

        public List<DIMENSOESPRODUTO> GetAllDimensoesProdutos()
        {
            return dc.DIMENSOESPRODUTOs.OrderBy(u => u.DESCRICAO).ToList();
        }

        public List<DimensoesProdutoModelGrid> GetAllDimensoesGrid()
        {
            var result = (from DimensoesProduto in dc.DIMENSOESPRODUTOs
                          orderby DimensoesProduto.DESCRICAO
                          select new DimensoesProdutoModelGrid
                          {
                              DimensoesProdutoId = DimensoesProduto.DIMENSOESPRODUTOID,
                              Altura = DimensoesProduto.ALTURA,
                              Lagura = DimensoesProduto.LARGURA,
                              Comprimento = DimensoesProduto.COMPRIMENTO,
                              Descricao = DimensoesProduto.DESCRICAO,
                              Ativo = DimensoesProduto.ATIVO
                          });

            return result.Distinct().OrderBy(p => p.Descricao).ToList();
        }

        public List<DimensoesProdutoModelGrid> GetAllDimensoesGridFiltro(List<bool> listAtivo, string nomeDimensoesProduto)
        {
            var result = (from DimensoesProduto in dc.DIMENSOESPRODUTOs
                          orderby DimensoesProduto.DESCRICAO
                          select new
                          {
                              DimensoesProduto
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.DimensoesProduto.ATIVO));
            }

            if (!nomeDimensoesProduto.Equals(string.Empty))
                result = result.Where(a => a.DimensoesProduto.DESCRICAO.ToUpper().Contains(nomeDimensoesProduto.ToUpper()));

            var x = from p in result
                    select new DimensoesProdutoModelGrid
                    {
                        DimensoesProdutoId = p.DimensoesProduto.DIMENSOESPRODUTOID,
                        Altura = p.DimensoesProduto.ALTURA,
                        Lagura = p.DimensoesProduto.LARGURA,
                        Comprimento = p.DimensoesProduto.COMPRIMENTO,
                        Descricao = p.DimensoesProduto.DESCRICAO,
                        Ativo = p.DimensoesProduto.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Descricao).ToList();
        }

        public List<DIMENSOESPRODUTO> GetAllAtivos()
        {
            return dc.DIMENSOESPRODUTOs.Where(u => u.ATIVO).OrderBy(u => u.DESCRICAO).ToList();
        }


        public void Salvar(DIMENSOESPRODUTO DimensoesProduto)
        {
            if (DimensoesProduto.DIMENSOESPRODUTOID > 0)
                Atualizar(DimensoesProduto);
            else
                Inserir(DimensoesProduto);
        }
    }

    public class DimensoesProdutoModelGrid
    {
        public int DimensoesProdutoId { get; set; }
        public Decimal Altura { get; set; }
        public Decimal Lagura { get; set; }
        public Decimal Comprimento { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

    }
}
