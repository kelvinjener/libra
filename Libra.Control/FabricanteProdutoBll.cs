using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class FabricanteProdutoBll : LibraBusinessLogic<FABRICANTEPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaProdutoFabricanteProdutoAssociados(int idFabricante)
        {
            bool associado = false;

            List<PRODUTO> produto = (from p in dc.PRODUTOs
                                     where p.FABRICANTEID == idFabricante
                                     select p).ToList();

            if (produto.Count > 0)
                associado = true;

            return associado;
        }

        public FABRICANTEPRODUTO GetFabricanteProdutoById(int idFabricanteProduto)
        {
            return dc.FABRICANTEPRODUTOs.Where(u => u.FABRICANTEPRODUTOID == idFabricanteProduto).FirstOrDefault();
        }

        public List<FABRICANTEPRODUTO> GetAllFabricantesProdutos()
        {
            return dc.FABRICANTEPRODUTOs.OrderBy(u => u.NOME).ToList();
        }

        public List<FabricanteProdutoModelGrid> GetAllFabricantesProdutosGrid()
        {
            var result = (from FabricanteProduto in dc.FABRICANTEPRODUTOs
                          orderby FabricanteProduto.NOME
                          select new FabricanteProdutoModelGrid
                          {
                              FabricanteProdutoId = FabricanteProduto.FABRICANTEPRODUTOID,
                              Nome = FabricanteProduto.NOME,
                              Ativo = FabricanteProduto.ATIVO
                          });

            return result.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<FabricanteProdutoModelGrid> GetAllFabricantesProdutosGridFiltro(List<bool> listAtivo, string nomeFabricanteProduto)
        {
            var result = (from FabricanteProduto in dc.FABRICANTEPRODUTOs
                          orderby FabricanteProduto.NOME
                          select new
                          {
                              FabricanteProduto
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.FabricanteProduto.ATIVO));
            }

            if (!nomeFabricanteProduto.Equals(string.Empty))
                result = result.Where(a => a.FabricanteProduto.NOME.ToUpper().Contains(nomeFabricanteProduto.ToUpper()));

            var x = from p in result
                    select new FabricanteProdutoModelGrid
                    {
                        FabricanteProdutoId = p.FabricanteProduto.FABRICANTEPRODUTOID,
                        Nome = p.FabricanteProduto.NOME,
                        Ativo = p.FabricanteProduto.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<FABRICANTEPRODUTO> GetAllAtivos()
        {
            return dc.FABRICANTEPRODUTOs.Where(u => u.ATIVO).OrderBy(u => u.NOME).ToList();
        }


        public void Salvar(FABRICANTEPRODUTO FabricanteProduto)
        {
            if (FabricanteProduto.FABRICANTEPRODUTOID > 0)
                Atualizar(FabricanteProduto);
            else
                Inserir(FabricanteProduto);
        }
    }

    public class FabricanteProdutoModelGrid
    {
        public int FabricanteProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

    }
}
