using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Libra.Communs
{
    public static class HandlerMessager
    {
        private static Random random = new Random();

        private static String GetNameUnique()
        {
            String name = random.Next().ToString();
            return name;
        }

        public static void MessageBox(Page page, string message)
        {
            string javascript = "$(document).ready(function(){new PNotify({ " +
                                "title: 'Alguma coisa deu errado!', " +
                                "text: '" + message + "', " +
                                "type: 'dark', " +
                                "animate: { " +
                                "    animate: true, " +
                                "    in_class: 'fadeInRight', " +
                                "    out_class: 'bounceOut' " +
                                "} " +
                                "});});";

            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), javascript, true);
        }

        public static void MessageBox(Page page, string message, string urlRedirect)
        {
            string javascript = "redirect = '" + urlRedirect + "'; alert('{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }

        public static void MessageBox(UpdatePanel updatePanel, string message)
        {
            string javascript = "NotifyDark('Alguma coisa deu errado!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }

        public static void MessageBox(UpdatePanel updatePanel, string message, string urlRedirect)
        {
            string javascript = "redirect = '" + urlRedirect + "'; NotifyDark('Alguma coisa deu errado!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }
    }
}
