using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Entity
{
    internal class LoginUser
    {
        public object IdUser
        {
            get
            {
                if (user == null)
                {
                    LibraDataContext contexto = new LibraDataContext();
                    String usrLogin = string.Empty;

                    string idUsuario = contexto.USUARIOs.Single(u => u.USUARIOID == BaseDataContext.IdUsuario).ASPNETUSERID;
                    AspNetUser usrAdm = contexto.AspNetUsers.Where(a => a.Id == idUsuario).SingleOrDefault();
                    if (usrAdm != null)
                    {

                        usrLogin = usrAdm.UserName;
                    }
                }
                return user;
            }
        }
        private object user = null;
    }

    internal class LogLoginUser
    {
        public LogLoginUser()
        {
            LoginUser loginUser = new LoginUser();

            if (BaseDataContext.Usuario == null || BaseDataContext.IdUsuario == 0)
            {
                UserLocal = DBNull.Value;
                return;
            }

            if (loginUser == null || loginUser.IdUser == null)
                UserLocal = DBNull.Value;
            else
                UserLocal = loginUser.IdUser;

        }

        public object UserLocal { get; set; }
    }

}
