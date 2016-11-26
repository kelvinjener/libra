using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class MarcaProdutoBll : LibraBusinessLogic<MARCAPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaProdutoMarcaAssociados(int idMarca)
        {
            bool associado = false;

            List<PRODUTO> produto = (from p in dc.PRODUTOs
                                     where p.MARCAID == idMarca
                                     select p).ToList();

            if (produto.Count > 0)
                associado = true;

            return associado;
        }

        public MARCAPRODUTO GetMarcaProdutoById(int idMarcaProduto)
        {
            return dc.MARCAPRODUTOs.Where(u => u.MARCAPRODUTOID == idMarcaProduto).FirstOrDefault();
        }

        public List<MARCAPRODUTO> GetAllMarcasProdutos()
        {
            return dc.MARCAPRODUTOs.OrderBy(u => u.NOME).ToList();
        }

        public List<MarcaProdutoModelGrid> GetAllMarcasGrid()
        {
            var result = (from MarcaProduto in dc.MARCAPRODUTOs
                          orderby MarcaProduto.NOME
                          select new MarcaProdutoModelGrid
                          {
                              MarcaProdutoId = MarcaProduto.MARCAPRODUTOID,
                              Nome = MarcaProduto.NOME,
                              Ativo = MarcaProduto.ATIVO
                          });

            return result.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<MarcaProdutoModelGrid> GetAllMarcasGridFiltro(List<bool> listAtivo, string nomeMarcaProduto)
        {
            var result = (from MarcaProduto in dc.MARCAPRODUTOs
                          orderby MarcaProduto.NOME
                          select new
                          {
                              MarcaProduto
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.MarcaProduto.ATIVO));
            }

            if (!nomeMarcaProduto.Equals(string.Empty))
                result = result.Where(a => a.MarcaProduto.NOME.ToUpper().Contains(nomeMarcaProduto.ToUpper()));

            var x = from p in result
                    select new MarcaProdutoModelGrid
                    {
                        MarcaProdutoId = p.MarcaProduto.MARCAPRODUTOID,
                        Nome = p.MarcaProduto.NOME,
                        Ativo = p.MarcaProduto.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<MARCAPRODUTO> GetAllAtivos()
        {
            return dc.MARCAPRODUTOs.Where(u => u.ATIVO).OrderBy(u => u.NOME).ToList();
        }


        public void Salvar(MARCAPRODUTO MarcaProduto)
        {
            if (MarcaProduto.MARCAPRODUTOID > 0)
                Atualizar(MarcaProduto);
            else
                Inserir(MarcaProduto);
        }
    }

    public class MarcaProdutoModelGrid
    {
        public int MarcaProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

    }
}
