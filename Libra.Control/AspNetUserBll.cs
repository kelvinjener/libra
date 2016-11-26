using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class AspNetUserBll : LibraBusinessLogic<AspNetUser>
    {
        private LibraDataContext dc = new LibraDataContext();

        public AspNetUser GetANUsuerByUsuarioId(int UsuarioId)
        {
            var result = (from usuario in dc.USUARIOs
                          join ANUser in dc.AspNetUsers on usuario.ASPNETUSERID equals ANUser.Id
                          where usuario.USUARIOID == UsuarioId
                          select ANUser).FirstOrDefault();

            return result;
        }

        public AspNetUser GetANUsuerByUserName(string UserName)
        {
            var result = (from ANUser in dc.AspNetUsers
                          where ANUser.UserName.ToUpper() == UserName.ToUpper()
                          select ANUser).FirstOrDefault();

            return result;
        }

        public AspNetUser GetUNUsuerById(string idUsuario)
        {
            return dc.AspNetUsers.Where(u => u.Id == idUsuario).FirstOrDefault();
        }

        public void Salvar(AspNetUser usuario)
        {
            if (string.IsNullOrEmpty(usuario.Id))
                this.Inserir(usuario);
            else
                this.Atualizar(usuario);
        }
    }
}
