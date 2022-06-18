using AirPurity.API.Common.Resources;
using AirPurity.API.DTOs.ClientDTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AirPurity.API.Services
{
    public class EmailService
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;
        private readonly bool _enableSsl;
        private readonly string _rootPath;

        public EmailService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            var smtpConfiguration = configuration.GetSection("SmtpSettings");
            _username = smtpConfiguration.GetValue<string>("Username");
            _password = Environment.GetEnvironmentVariable("SMTP_Password");
            _host = smtpConfiguration.GetValue<string>("Host");
            _port = smtpConfiguration.GetValue<int>("Port");
            _enableSsl = smtpConfiguration.GetValue<bool>("EnableSsl");
            _rootPath = environment.WebRootPath;
        }

        public void SendEmails(List<EmailTemplateModel> emailTemplateModels)
        {
            if (!string.IsNullOrEmpty(_password))
            {
                foreach (var model in emailTemplateModels)
                {
                    string body = CreateNotifiactionBody(model);

                    try
                    {
                        Task.Run(async () => await SendEmail(model.Subject, body, model.Email));
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }

        public async Task<bool> SendConfirmationEmail(string email, string callbackUrl)
        {
            if (!string.IsNullOrEmpty(_password))
            {
                string body = CreateEmailConfirmationBody(callbackUrl);
                string subject = NotificationResource.ConfirmationEmailSubject;

                try
                {
                    await SendEmail(subject, body, email);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
            
        }

        private async Task SendEmail(string subject, string body, string to)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_username);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.To.Add(new MailAddress(to));

                SmtpClient smtpClient = new SmtpClient(_host, _port);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                

                NetworkCredential credential = new NetworkCredential(_username, _password);
                smtpClient.Credentials = credential;
                smtpClient.EnableSsl = true;
                var cred = smtpClient.Credentials.Equals(credential);

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception) { }
                
            }
        }

        private string CreateNotifiactionBody(EmailTemplateModel model)
        {
            var emailTemplatePath = $"{_rootPath}/Templates/Notification_Email_Template.html";
            string emailTemplate = string.Empty;

            using (StreamReader reder = File.OpenText(emailTemplatePath))
            {
                emailTemplate = reder.ReadToEnd();
            }

            List<string> notifications = new List<string>();

            foreach (var stationState in model.Notifications)
            {
                notifications.Add(GenerateStationStateRow(stationState));
            }

            string content = string.Join(Environment.NewLine, notifications);

            string body = string.Format(emailTemplate, content, model.HomeUrl, model.StopSubscriptionUrl);

            return body;

        }

        private string GenerateStationStateRow(NotificationTemplateModel model)
        {
            var stationTemplatePath = $"{_rootPath}/Templates/_Station_Template_Row.html";
            string stationTemplate = string.Empty;

            using (StreamReader reder = File.OpenText(stationTemplatePath))
            {
                stationTemplate = reder.ReadToEnd();
            }

            List<string> paramStates = new List<string>();

            if (!string.IsNullOrEmpty(model.StationIndexLevelName))
            {
                string generalState = string.Format(NotificationResource.StationState, model.StationIndexLevelName);
                paramStates.Add(generalState);
            }

            foreach (var paramState in model.SubNotificationTemplates)
            {
                string paramRow = string.Format(NotificationResource.ParamState, paramState.ParamCode, paramState.ParamIndexLevelName);
                paramStates.Add(paramRow);
            }

            string listValues = string.Join(Environment.NewLine, paramStates);
            string stationRow = string.Format(stationTemplate, model.StationName, listValues);

            return stationRow;
        }

        private string CreateEmailConfirmationBody(string callbackUrl)
        {
            var emailTemplatePath = $"{_rootPath}/Templates/Email_Confirmation_Template.html";
            string emailTemplate = string.Empty;

            using (StreamReader reder = File.OpenText(emailTemplatePath))
            {
                emailTemplate = reder.ReadToEnd();
            }

            return string.Format(emailTemplate, NotificationResource.ConfirmationEmailSubject, callbackUrl);
        }
    }
}
