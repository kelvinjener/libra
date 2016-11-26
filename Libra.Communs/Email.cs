using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs
{
    public class Email
    {
        public static void EnviarEmail(string emailDestinatario, string assunto, string mensagem, string emailRemetente)
        {
            EnviarEmail(emailDestinatario, assunto, mensagem, true, emailRemetente);
        }

        public static void EnviarEmail(string emailDestinatario, string assunto, string mensagem, bool web, string emailRemetente)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(emailRemetente);
                message.IsBodyHtml = true;
                message.Body = mensagem;
                message.Subject = assunto;
                //A Wilsa solicitou que todo processo que envia email seja enviado com CCO.
                message.Bcc.Add(emailDestinatario);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(message);
            }
            catch (System.Exception)
            {
                //throw new System.Exception(string.Format("Não foi possível enviar email. (Erro: {0})", ex.Message));
            }
        }

        public static void EnviarEmailComAnexos(string emailDestinatario, string assunto, string mensagem, string emailRemetente, Dictionary<string, byte[]> anexos)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(emailRemetente);
                message.IsBodyHtml = true;
                message.Body = mensagem;
                message.Subject = assunto;
                //A Wilsa solicitou que todo processo que envia email seja enviado com CCO.
                message.Bcc.Add(emailDestinatario);

                foreach (var item in anexos)
                {
                    Stream s = new MemoryStream(item.Value);
                    Attachment at1 = new Attachment(s, item.Key);
                    message.Attachments.Add(at1);
                }

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Timeout = 7 * 60 * 1000; // 7 minutos (milissegundos).
                smtpClient.Send(message);
            }
            catch (System.Exception)
            {
                //throw new System.Exception(string.Format("Não foi possível enviar email. (Erro: {0})", ex.Message));
            }
        }
    }
}
