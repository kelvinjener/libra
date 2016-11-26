using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class ModeloProdutoBll : LibraBusinessLogic<MODELOPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaProdutoModeloAssociados(int idModelo)
        {
            bool associado = false;

            List<PRODUTO> produto = (from p in dc.PRODUTOs
                                     where p.MODELOID == idModelo
                                     select p).ToList();

            if (produto.Count > 0)
                associado = true;

            return associado;
        }

        public MODELOPRODUTO GetModeloProdutoById(int idModeloProduto)
        {
            return dc.MODELOPRODUTOs.Where(u => u.MODELOPRODUTOID == idModeloProduto).FirstOrDefault();
        }

        public List<MODELOPRODUTO> GetAllModeloProdutos()
        {
            return dc.MODELOPRODUTOs.OrderBy(u => u.NOME).ToList();
        }

        public List<ModeloProdutoModelGrid> GetAllModelosGrid()
        {
            var result = (from ModeloProduto in dc.MODELOPRODUTOs
                          orderby ModeloProduto.NOME
                          select new ModeloProdutoModelGrid
                          {
                              ModeloProdutoId = ModeloProduto.MODELOPRODUTOID,
                              Nome = ModeloProduto.NOME,
                              Ativo = ModeloProduto.ATIVO
                          });

            return result.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<ModeloProdutoModelGrid> GetAllModelosGridFiltro(List<bool> listAtivo, string nomeModeloProduto)
        {
            var result = (from ModeloProduto in dc.MODELOPRODUTOs
                          orderby ModeloProduto.NOME
                          select new
                          {
                              ModeloProduto
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.ModeloProduto.ATIVO));
            }

            if (!nomeModeloProduto.Equals(string.Empty))
                result = result.Where(a => a.ModeloProduto.NOME.ToUpper().Contains(nomeModeloProduto.ToUpper()));

            var x = from p in result
                    select new ModeloProdutoModelGrid
                    {
                        ModeloProdutoId = p.ModeloProduto.MODELOPRODUTOID,
                        Nome = p.ModeloProduto.NOME,
                        Ativo = p.ModeloProduto.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<MODELOPRODUTO> GetAllAtivos()
        {
            return dc.MODELOPRODUTOs.Where(u => u.ATIVO).OrderBy(u => u.NOME).ToList();
        }


        public void Salvar(MODELOPRODUTO ModeloProduto)
        {
            if (ModeloProduto.MODELOPRODUTOID > 0)
                Atualizar(ModeloProduto);
            else
                Inserir(ModeloProduto);
        }
    }

    public class ModeloProdutoModelGrid
    {
        public int ModeloProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

    }
}
