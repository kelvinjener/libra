using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class FuncionalidadeBll : LibraBusinessLogic<FUNCIONALIDADE>
    {
        private LibraDataContext dc = new LibraDataContext();

        public FUNCIONALIDADE GetFuncionalidadeById(int idFuncionalidade)
        {
            return dc.FUNCIONALIDADEs.Where(f => f.FUNCIONALIDADEID == idFuncionalidade).FirstOrDefault();
        }

        public FUNCIONALIDADE GetFuncionalidadeByURL(string Url)
        {
            return dc.FUNCIONALIDADEs.Where(f => f.URL == Url).FirstOrDefault();
        }

        public FUNCIONALIDADE GetFuncionalidadeByNome(string Nome)
        {
            return dc.FUNCIONALIDADEs.Where(f => f.NOME == Nome).FirstOrDefault();
        }

        public List<FUNCIONALIDADE> GetAllFuncionalidades()
        {
            return dc.FUNCIONALIDADEs.OrderBy(p => p.NOME).ToList();
        }

        public List<FuncionalidadeModelGrid> GetAllGrid()
        {
            var result = (from funcionalidade in dc.FUNCIONALIDADEs
                          orderby funcionalidade.NOME
                          select new FuncionalidadeModelGrid
                          {
                              FuncionalidadeId = funcionalidade.FUNCIONALIDADEID,
                              Nome = funcionalidade.NOME,
                              NomeArquivo = funcionalidade.NOMEARQUIVO,
                              MenuPaiId = funcionalidade.MENUPAIID,
                              URL = funcionalidade.URL,
                              Ativo = funcionalidade.ATIVO
                          });

            return result.ToList();
        }

        public List<FUNCIONALIDADE> GetAllAtivos()
        {
            return dc.FUNCIONALIDADEs.Where(p => p.ATIVO).OrderBy(p => p.NOME).ToList();
        }

        public List<FUNCIONALIDADE> GetFuncionalidadesByIdPerfis(int idPerfil)
        {
            var result = (from funcionalidades in dc.FUNCIONALIDADEs
                          join funcionalidadePerfil in dc.FUNCIONALIDADEPERFIs on funcionalidades.FUNCIONALIDADEID equals funcionalidadePerfil.FUNCIONALIDADEID
                          where funcionalidadePerfil.PERFILID == idPerfil
                          select funcionalidades);

            return result.OrderBy(p => p.NOME).ToList();
        }

        public List<FuncionalidadesLista> GetAllListaAtivos()
        {
            var result = (from funcionalidade in dc.FUNCIONALIDADEs
                          join menuPai in dc.FUNCIONALIDADEs on funcionalidade.MENUPAIID equals menuPai.FUNCIONALIDADEID
                          where funcionalidade.ATIVO && funcionalidade.MENUPAIID != funcionalidade.FUNCIONALIDADEID
                          select new FuncionalidadesLista
                          {
                              FuncionalidadeId = funcionalidade.FUNCIONALIDADEID,
                              Nome = menuPai.NOME + " > " + funcionalidade.NOME,
                          });

            return result.OrderBy(p => p.Nome).ToList();
        }

        public bool CanAcess(int UsuarioId, string Url)
        {
            bool canAcess = false;

            //TODO: Regra de Acesso

            List<USUARIOPERFI> usuarioPerfis = new UsuarioPerfilBll().GetListUsuarioPerfilByUsuarioId(UsuarioId);
            FUNCIONALIDADE funcionalidade = GetFuncionalidadeByURL(Url);

            if (funcionalidade != null)
            {
                foreach (var up in usuarioPerfis)
                {
                    List<FUNCIONALIDADEPERFI> funcionalidadePerfil = new FuncionalidadePerfilBll().GetListFuncionalidadePerfilByPerfilId(Convert.ToInt32(up.PERFILID));
                    foreach (var fp in funcionalidadePerfil)
                    {
                        if (fp.FUNCIONALIDADEID == funcionalidade.FUNCIONALIDADEID)
                            canAcess = true;
                    }
                }
            }
            else
            {
                //TODO: Verificar regra.
                canAcess = true;
            }

            return canAcess;
        }

        public bool ExibeMenu(int UsuarioId, string NomeMenu)
        {
            bool visivel = false;

            //TOOD: Regra visibilidade.

            List<USUARIOPERFI> usuarioPerfis = new UsuarioPerfilBll().GetListUsuarioPerfilByUsuarioId(UsuarioId);
            FUNCIONALIDADE funcionalidade = GetFuncionalidadeByNome(NomeMenu);

            foreach (var up in usuarioPerfis)
            {
                List<FUNCIONALIDADEPERFI> funcionalidadePerfil = new FuncionalidadePerfilBll().GetListFuncionalidadePerfilByPerfilId(Convert.ToInt32(up.PERFILID));
                foreach (var fp in funcionalidadePerfil)
                {
                    if (fp.FUNCIONALIDADEID == funcionalidade.FUNCIONALIDADEID && funcionalidade.ATIVO)
                        visivel = true;
                }
            }

            return visivel;
        }

        public bool ExibeMenuPai(int UsuarioId, string NomeMenu)
        {
            bool visivel = false;

            //TOOD: Regra visibilidade.

            List<USUARIOPERFI> usuarioPerfis = new UsuarioPerfilBll().GetListUsuarioPerfilByUsuarioId(UsuarioId);
            FUNCIONALIDADE menuPai = GetFuncionalidadeByNome(NomeMenu);

            foreach (var up in usuarioPerfis)
            {
                List<FUNCIONALIDADEPERFI> funcionalidadePerfil = new FuncionalidadePerfilBll().GetListFuncionalidadePerfilByPerfilId(Convert.ToInt32(up.PERFILID));
                foreach (var fp in funcionalidadePerfil)
                {
                    if (fp.FUNCIONALIDADE.MENUPAIID == menuPai.FUNCIONALIDADEID && menuPai.ATIVO)
                        visivel = true;
                }
            }

            return visivel;
        }
    }

    public class FuncionalidadeModelGrid
    {
        public int FuncionalidadeId { get; set; }
        public string Nome { get; set; }
        public string NomeArquivo { get; set; }
        public string URL { get; set; }
        public int MenuPaiId { get; set; }
        public bool Ativo { get; set; }
    }

    public class FuncionalidadesLista
    {
        public int FuncionalidadeId { get; set; }
        public string Nome { get; set; }
    }
}
