using Libra.Communs;
using Libra.Communs.Enumerators;
using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class UnidadeBll : LibraBusinessLogic<UNIDADE>
    {
        private LibraDataContext dc = new LibraDataContext();

        public UNIDADE GetUnidadeById(int idUnidade)
        {
            return dc.UNIDADEs.Where(u => u.UNIDADEID == idUnidade).FirstOrDefault();
        }

        public List<UNIDADE> GetAllUnidades()
        {
            return dc.UNIDADEs.OrderBy(u => u.APELIDO).ToList();
        }

        public List<UnidadeModelGrid> GetAllUnidadesGrid()
        {
            var result = (from unidade in dc.UNIDADEs
                          orderby unidade.APELIDO
                          select new UnidadeModelGrid
                          {
                              UnidadeId = unidade.UNIDADEID,
                              Apelido = unidade.APELIDO,
                              Cidade = unidade.CIDADE,
                              Bairro = unidade.BAIRRO,
                              Telefone1 = unidade.TEL1,
                              Email1 = unidade.EMAIL1,
                              Ativo = unidade.ATIVO,
                              TipoUnidade = unidade.TIPOUNIDADE
                          });

            return result.Distinct().OrderBy(p => p.Apelido).ToList();
        }

        public List<UnidadeModelGrid> GetAllUnidadesGridFiltro(List<bool> listAtivo, string nomeUnidade, string tipoUnidade)
        {
            var result = (from unidade in dc.UNIDADEs
                          orderby unidade.APELIDO
                          select new
                          {
                              unidade
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.unidade.ATIVO));
            }

            if (!nomeUnidade.Equals(string.Empty))
                result = result.Where(a => a.unidade.APELIDO.ToUpper().Contains(nomeUnidade.ToUpper()));

            if (!tipoUnidade.Equals(string.Empty))
                result = result.Where(a => a.unidade.TIPOUNIDADE.Equals(EnumUtils.ParseEnum<TipoUnidadeEnum>(Convert.ToInt16(tipoUnidade))));

            var x = from p in result
                    select new UnidadeModelGrid
                    {
                        UnidadeId = p.unidade.UNIDADEID,
                        Apelido = p.unidade.APELIDO,
                        Cidade = p.unidade.CIDADE,
                        Bairro = p.unidade.BAIRRO,
                        Telefone1 = p.unidade.TEL1,
                        Email1 = p.unidade.EMAIL1,
                        Ativo = p.unidade.ATIVO,
                        TipoUnidade = p.unidade.TIPOUNIDADE
                    };

            return x.Distinct().OrderBy(p => p.Apelido).ToList();
        }

        public List<UNIDADE> GetAllUnidadeAtivas()
        {
            return dc.UNIDADEs.Where(u => u.ATIVO).OrderBy(u => u.APELIDO).ToList();
        }

        public List<UNIDADE> GetUnidadesByIdUsuario(int idUsuario)
        {
            var result = (from unidade in dc.UNIDADEs
                          join usuarioUnidade in dc.USUARIOUNIDADEs on unidade.UNIDADEID equals usuarioUnidade.UNIDADEID
                          where usuarioUnidade.USUARIOID == idUsuario
                          select unidade);

            return result.OrderBy(p => p.APELIDO).ToList();
        }

        public void Salvar(UNIDADE unidade)
        {
            if (unidade.UNIDADEID > 0)
                Atualizar(unidade);
            else
                Inserir(unidade);
        }
    }

    public class UnidadeModelGrid
    {
        public int UnidadeId { get; set; }
        public string Apelido { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Telefone1 { get; set; }
        public string Email1 { get; set; }
        public bool Ativo { get; set; }
        public Int16 TipoUnidade { get; set; }

    }
}
