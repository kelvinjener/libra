using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class UsuarioUnidadeBll : LibraBusinessLogic<USUARIOUNIDADE>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaUnidadeAssociadas(int idUnidade)
        {
            bool associado = false;

            List<USUARIOUNIDADE> usuarioUnidades = (from UU in dc.USUARIOUNIDADEs
                                                    where UU.UNIDADEID == idUnidade
                                                    select UU).ToList();

            if (usuarioUnidades.Count > 0)
                associado = true;

            return associado;
        }

        public bool VerificaUnidadeUsuario(string loginUsuario, int idUnidade)
        {
            bool associado = false;

            USUARIO usuario = new UsuarioBll().GetUsuarioByLogin(loginUsuario);
            List<UNIDADE> unidadesUsuario = new UnidadeBll().GetUnidadesByIdUsuario(usuario.USUARIOID);

            foreach (var item in unidadesUsuario)
                if (item.UNIDADEID == idUnidade)
                    associado = true;

            return associado;
        }

        public List<USUARIOUNIDADE> GetListUsuarioUnidadesByUsuarioId(int UsuarioId)
        {
            return dc.USUARIOUNIDADEs.Where(u => u.USUARIOID == UsuarioId).ToList();
        }

        public void DeletarTodosByUsuarioId(int UsuarioId)
        {
            foreach (USUARIOUNIDADE uu in GetListUsuarioUnidadesByUsuarioId(UsuarioId))
            {
                this.Deletar(uu);
            }
        }

        public void Salvar(USUARIOUNIDADE uu)
        {
            this.Inserir(uu);
        }

    }
}
