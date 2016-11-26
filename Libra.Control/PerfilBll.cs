using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class PerfilBll : LibraBusinessLogic<PERFI>
    {
        private LibraDataContext dc = new LibraDataContext();

        public PERFI GetPerfilById(int idPerfil)
        {
            return dc.PERFIs.Where(p => p.PERFILID == idPerfil).FirstOrDefault();
        }

        public List<PerfilModelGrid> GetAllPerfisGridFiltro(List<bool> listAtivo, string nomePerfil, List<bool> listSomenteLeitura)
        {
            var result = (from perfil in dc.PERFIs
                          orderby perfil.NOME
                          select new
                          {
                              perfil
                          });

            if (listAtivo.Count > 0)
                result = result.Where(a => listAtivo.Contains(a.perfil.ATIVO));

            if (listSomenteLeitura.Count > 0)
                result = result.Where(a => listSomenteLeitura.Contains((bool)a.perfil.SOMENTELEITURA));

            if (!nomePerfil.Equals(string.Empty))
                result = result.Where(a => a.perfil.NOME.ToUpper().Contains(nomePerfil.ToUpper()));

            var x = from p in result
                    select new PerfilModelGrid
                    {
                        PerfilId = p.perfil.PERFILID,
                        Nome = p.perfil.NOME,
                        SomenteLeitura = (bool)p.perfil.SOMENTELEITURA,
                        Ativo = p.perfil.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<PERFI> GetAllPerfis()
        {
            return dc.PERFIs.OrderBy(p => p.NOME).ToList();
        }

        public List<PerfilModelGrid> GetAllPerfisGrid()
        {
            var result = (from perfil in dc.PERFIs
                          orderby perfil.NOME
                          select new PerfilModelGrid
                          {
                              PerfilId = perfil.PERFILID,
                              Nome = perfil.NOME,
                              SomenteLeitura = (bool)perfil.SOMENTELEITURA,
                              Ativo = perfil.ATIVO
                          });

            return result.ToList();
        }

        public List<PERFI> GetAllPerfilAtivos()
        {
            return dc.PERFIs.Where(p => p.ATIVO).OrderBy(p => p.NOME).ToList();
        }

        public List<PERFI> GetPerfisByIdUsuario(int idUsuario)
        {
            var result = (from perfis in dc.PERFIs
                          join usuarioPerfil in dc.USUARIOPERFIs on perfis.PERFILID equals usuarioPerfil.PERFILID
                          where usuarioPerfil.USUARIOID == idUsuario
                          select perfis);

            return result.OrderBy(p => p.NOME).ToList();
        }

        public void Salvar(PERFI perfil)
        {
            if (perfil.PERFILID > 0)
                Atualizar(perfil);
            else
                Inserir(perfil);
        }
    }

    public class PerfilModelGrid
    {
        public int PerfilId { get; set; }
        public string Nome { get; set; }
        public bool SomenteLeitura { get; set; }
        public bool Ativo { get; set; }
    }
}
