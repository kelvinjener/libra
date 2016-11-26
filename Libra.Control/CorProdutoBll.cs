using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class CorProdutoBll : LibraBusinessLogic<CORPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaProdutoCorAssociados(int idCor)
        {
            bool associado = false;

            List<PRODUTO> produto = (from p in dc.PRODUTOs
                                     where p.CORID == idCor
                                     select p).ToList();

            if (produto.Count > 0)
                associado = true;

            return associado;
        }

        public CORPRODUTO GetCorProdutoById(int idCorProduto)
        {
            return dc.CORPRODUTOs.Where(u => u.CORPRODUTOID == idCorProduto).FirstOrDefault();
        }

        public List<CORPRODUTO> GetAllCoresProdutos()
        {
            return dc.CORPRODUTOs.OrderBy(u => u.NOME).ToList();
        }

        public List<CorProdutoModelGrid> GetAllCoresGrid()
        {
            var result = (from CorProduto in dc.CORPRODUTOs
                          orderby CorProduto.NOME
                          select new CorProdutoModelGrid
                          {
                              CorProdutoId = CorProduto.CORPRODUTOID,
                              Nome = CorProduto.NOME,
                              Ativo = CorProduto.ATIVO
                          });

            return result.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<CorProdutoModelGrid> GetAllCoresGridFiltro(List<bool> listAtivo, string nomeCorProduto)
        {
            var result = (from CorProduto in dc.CORPRODUTOs
                          orderby CorProduto.NOME
                          select new
                          {
                              CorProduto
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.CorProduto.ATIVO));
            }

            if (!nomeCorProduto.Equals(string.Empty))
                result = result.Where(a => a.CorProduto.NOME.ToUpper().Contains(nomeCorProduto.ToUpper()));

            var x = from p in result
                    select new CorProdutoModelGrid
                    {
                        CorProdutoId = p.CorProduto.CORPRODUTOID,
                        Nome = p.CorProduto.NOME,
                        Ativo = p.CorProduto.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<CORPRODUTO> GetAllAtivos()
        {
            return dc.CORPRODUTOs.Where(u => u.ATIVO).OrderBy(u => u.NOME).ToList();
        }


        public void Salvar(CORPRODUTO CorProduto)
        {
            if (CorProduto.CORPRODUTOID > 0)
                Atualizar(CorProduto);
            else
                Inserir(CorProduto);
        }
    }

    public class CorProdutoModelGrid
    {
        public int CorProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

    }
}
