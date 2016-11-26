using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class UsuarioPerfilBll : LibraBusinessLogic<USUARIOPERFI>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaPerfilAssociadas(int perfilId)
        {
            bool associado = false;

            List<USUARIOPERFI> usuarioPerfis = (from UP in dc.USUARIOPERFIs
                                                where UP.PERFILID == perfilId
                                                select UP).ToList();

            if (usuarioPerfis.Count > 0)
                associado = true;

            return associado;
        }

        public bool VerificaPerfilUsuario(string loginUsuario, int idPerfil)
        {
            bool associado = false;

            USUARIO usuario = new UsuarioBll().GetUsuarioByLogin(loginUsuario);
            List<PERFI> perfisUsuario = new PerfilBll().GetPerfisByIdUsuario(usuario.USUARIOID);

            foreach (var item in perfisUsuario)
                if (item.PERFILID == idPerfil)
                    associado = true;

            return associado;
        }

        public List<USUARIOPERFI> GetListUsuarioPerfilByUsuarioId(int UsuarioId)
        {
            return dc.USUARIOPERFIs.Where(u => u.USUARIOID == UsuarioId).ToList();
        }

        public void DeletarTodosByUsuarioId(int UsuarioId)
        {
            foreach (USUARIOPERFI up in GetListUsuarioPerfilByUsuarioId(UsuarioId))
            {
                this.Deletar(up);
            }
        }

        public void Salvar(USUARIOPERFI up)
        {
            this.Inserir(up);
        }

    }
}
