using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SYFramework.Net.Common
{
    public class SendMailHelper
    {
        private static string _FromEmail = string.Empty;
        private static string _Username = string.Empty;
        private static string _Password = string.Empty;
        private static string _Host = string.Empty;

        public void SendMail(string to, string title, string content)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            client.Host = _Host;//指定SMTP服务器
            client.Credentials = new System.Net.NetworkCredential(_Username, _Password);//用户名和密码

            MailMessage message = new MailMessage(_FromEmail, to);
            message.Subject = title;//标题
            message.Body = content;//内容
            message.BodyEncoding = System.Text.Encoding.UTF8;//正文编码
            message.IsBodyHtml = true;//设为HTML格式
            message.Priority = MailPriority.High;//优先级为高

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
