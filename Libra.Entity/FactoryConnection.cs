using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Libra.Entity
{
    internal static class FactoryConnection
    {
        #region Constantes

        private const String STRINGCONNECTION_LIBRA = "DefaultConnection";

        #endregion

        #region Propriedades

        public static string IpAddress
        {
            get
            {
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        #endregion

        public static ConnectionStringSettings GetConnectionString(string stringName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[stringName];
        }

        public static String GetConnectionStringLibra()
        {
            return GetConnectionString(STRINGCONNECTION_LIBRA).ConnectionString;
        }
    }
}
