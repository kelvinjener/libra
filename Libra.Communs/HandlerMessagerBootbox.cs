using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Libra.Communs
{
    public static class HandlerMessagerBootbox
    {
        private static Random random = new Random();

        private static String GetNameUnique()
        {
            String name = random.Next().ToString();
            return name;
        }

        public static void MessageBoxError(Page page, string message)
        {
            string javascript = "$(document).ready(function(){new PNotify({ " +
                                "title: 'Error!', " +
                                "text: '" + message + "', " +
                                "type: 'dark', " +
                                "animate: { " +
                                "    animate: true, " +
                                "    in_class: 'fadeInRight', " +
                                "    out_class: 'bounceOut' " +
                                "} " +
                                "});});";
            message = message.Replace("\r\n", " < br > ");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), javascript, true);
        }

        public static void MessageBoxAtencao(Page page, string message)
        {
            string javascript = "$(document).ready(function(){new PNotify({ " +
                                "title: 'Atenção!', " +
                                "text: '" + message + "', " +
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

        public static void MessageBoxInfo(Page page, string message)
        {
            string javascript = "$(document).ready(function(){new PNotify({ " +
                                "title: 'Olá!', " +
                                "text: '" + message + "', " +
                                "type: 'info', " +
                                "animate: " +
                                "    { " +
                                "    animate: true, " +
                                "    in_class: 'fadeInRight', " +
                                "    out_class: 'bounceOut' " +
                                "} " +
                                "});});";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), javascript, true);
        }

        public static void MessageBoxSucesso(Page page, string message)
        {
            string javascript = "$(document).ready(function(){new PNotify({ " +
                                "title: 'Sucesso!', " +
                                "text: '" + message + "', " +
                                "type: 'success', " +
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
            string javascript = "redirect = '" + urlRedirect + "'; $(document).ready(function(){NotifyRegular('Atenção!','{0}');});";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }

        public static void MessageBoxError(UpdatePanel updatePanel, string message)
        {
            string javascript = "NotifyError('Erro!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), GetNameUnique(), string.Format(javascript, message), true);

        }

        public static void MessageBoxAtencao(UpdatePanel updatePanel, string message)
        {
            string javascript = "NotifyRegular('Atenção!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), GetNameUnique(), string.Format(javascript, message), true);

        }

        public static void MessageBoxInfo(UpdatePanel updatePanel, string message)
        {
            string javascript = "NotifyInfo('Olá!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), GetNameUnique(), string.Format(javascript, message), true);

        }

        public static void MessageBoxSucesso(UpdatePanel updatePanel, string message)
        {
            string javascript = "NotifySucess('Sucesso!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), GetNameUnique(), string.Format(javascript, message), true);

        }

        public static void MessageBox(UpdatePanel updatePanel, string message, string urlRedirect)
        {
            string javascript = "redirect = '" + urlRedirect + "'; NotifyRegular('Atenção!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }

        public static void MessageTabbedError(Page page, string message)
        {
            string javascript = "TabbedNotifyError('Erro!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }

        public static void MessageTabbedAtencao(Page page, string message)
        {
            string javascript = "TabbedNotifyRegular('Atenção!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }

        public static void MessageTabbedInfo(Page page, string message)
        {
            string javascript = "TabbedNotifyInfo('Olá!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }

        public static void MessageTabbedSucesso(Page page, string message)
        {
            string javascript = "TabbedNotifySucess('Sucesso!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }

        public static void MessageTabbed(Page page, string message, string urlRedirect)
        {
            string javascript = "redirect = '" + urlRedirect + "'; TabbedNotifyRegular('Atenção!','{0}');";
            message = message.Replace("\r\n", "<br>");
            message = message.Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), GetNameUnique(), string.Format(javascript, message), true);
        }
    }
}
