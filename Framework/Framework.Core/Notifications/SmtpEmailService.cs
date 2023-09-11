// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmtpEmailService.cs" company="Usama Nada">
//   No Copyright .. Copy, Share, and Evolve.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.Notifications
{
    using System;
    #region usings

    using System.Net.Mail;

    #endregion

    /// <summary>
    ///     The smtp email service.
    /// </summary>
    public class SmtpEmailService : IEmailService
    {
        //private readonly IEmailService emailService;

        public SmtpEmailService()
        {
            //this.emailService = emailService;
        }
        /// <summary>
        /// The send email.
        /// </summary>
        /// <param name="emailMessage">
        /// The email message.
        /// </param>
        /// <param name="notificationSettings">
        /// todo: describe notificationSettings parameter on SendEmail
        /// </param>
        //public void SendEmail(EmailMessage emailMessage
        //    , NotificationSettings notificationSettings

        //    )
        //{
        //    using (var smtpClient = new SmtpClient
        //    {
        //        Host = notificationSettings.SmtpServer,
        //        UseDefaultCredentials = false,
        //        Port = notificationSettings.SmtpPort,
        //        EnableSsl = notificationSettings.SmtpEnableSSL,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        Credentials = new NetworkCredential(
        //                                        notificationSettings.SmtpUserName,
        //                                        notificationSettings.SmtpPassword)
        //    })
        //    {
        //        var mail = emailMessage.ToMailMessage();

        //        ServicePointManager.SecurityProtocol =
        //            SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        //        smtpClient.Send(mail);
        //    }
        //}

        public void SendEmail(EmailMessage emailMessage, NotificationSettings notificationSettings)
        {
            using (SmtpClient smtp = new SmtpClient(notificationSettings.SmtpServer, notificationSettings.SmtpPort))
            {
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = notificationSettings.IsSmtpAuthenticated;
                smtp.Credentials = new System.Net.NetworkCredential(notificationSettings.SmtpUserName, notificationSettings.SmtpPassword);
                var mail = emailMessage.ToMailMessage();

                try
                {

                    smtp.Send(mail);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}