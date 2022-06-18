using AirPurity.API.BusinessLogic.External.Models;
using AirPurity.API.BusinessLogic.External.Services;
using AirPurity.API.BusinessLogic.Repositories.Repositories;
using AirPurity.API.Common.Enums;
using AirPurity.API.Common.Resources;
using AirPurity.API.Data.Entities;
using AirPurity.API.DTOs.ClientDTOs;
using AirPurity.API.Interfaces;
using AirPurity.API.Repositories.BusinessLogic.Repositories;
using AirPurity.API.Repositories.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AirPurity.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly GiosHttpClientService _giosHttpClientService;
        private readonly StationRepository _stationRepository;
        private readonly EmailService _emailService;
        private readonly NotificationUserRepository _notificationUserRepository;
        private readonly CityRepository _cityRepository;
        private IConfigurationSection _hostConfig;

        public NotificationService(NotificationRepository notificationRepository, GiosHttpClientService giosHttpClientService,
            StationRepository stationRepository, IConfiguration configuration, EmailService emailService, NotificationUserRepository notificationUserRepository
            ,CityRepository cityRepository)
        {
            _notificationRepository = notificationRepository;
            _giosHttpClientService = giosHttpClientService;
            _stationRepository = stationRepository;
            _emailService = emailService;
            _notificationUserRepository = notificationUserRepository;
            _cityRepository = cityRepository;
            _hostConfig = configuration.GetRequiredSection("HostConfig");
        }

        public bool Add(Notification notification, string email)
        {
            bool isEmailConfirmed = false;

            if(notification != null)
            {
                notification.CityId = _stationRepository.GetById(notification.StationId)?.CityId ?? 0;
                var user = _notificationUserRepository.GetByEmail(email);
                if(user != null)
                {
                    isEmailConfirmed = user.IsEmailConfirmed;
                    var existedNotification = _notificationRepository
                        .FindAll(x => x.NotificationUser.Id == user.Id && x.StationId == notification.StationId, x => x.NotificationSubjects)
                        .FirstOrDefault();

                    if(existedNotification != null)
                    {
                        existedNotification.IndexLevelId = notification.IndexLevelId;
                        foreach (var subject in existedNotification.NotificationSubjects)
                        {
                            _notificationRepository.RemoveNotificationSubject(subject);
                        }

                        foreach (var subject in notification.NotificationSubjects)
                        {
                            subject.NotificationId = existedNotification.Id;
                            _notificationRepository.AddNotificationSubject(subject);
                        }
                    }
                    else
                    {
                        notification.NotificationUserId = user.Id;
                        _notificationRepository.Add(notification);
                    }

                    if(user.IsActive == false)
                    {
                        _notificationUserRepository.ActivateUser(user.UserEmail);
                    }
                }
                else
                {
                    user = new NotificationUser()
                    {
                        UserEmail = email,
                        IsActive = true,
                        IsEmailConfirmed = false,
                        Notifications = new List<Notification>() { notification }
                    };
                    
                    _notificationUserRepository.Add(user);
                }
                _notificationRepository.SaveChanges();
            }
            return isEmailConfirmed;
        }

        public IEnumerable<Notification> GetAll()
        {
            var notifications = _notificationRepository
                .GetAll(x => x.NotificationSubjects, x => x.NotificationUser, x => x.Station)
                .Where(x => x.NotificationUser.IsActive && x.NotificationUser.IsEmailConfirmed);

            return notifications;
        }

        public void Remove(int id)
        {
            if(id > 0)
            {
                _notificationRepository.DeleteById(id);
                _notificationRepository.SaveChanges();
            }
        }

        public void RemoveAllNotification(Guid stopNotificationToken)
        {
            if (!string.IsNullOrEmpty(stopNotificationToken.ToString()))
            {
                var user = _notificationUserRepository.DeactivateUser(stopNotificationToken);
                if(user != null)
                {
                    var notifications = _notificationRepository
                    .FindAll(x => x.NotificationUser.Id == user.Id);

                    foreach (var notification in notifications)
                    {
                        _notificationRepository.DeleteById(notification.Id);
                    }

                    _notificationRepository.SaveChanges();
                }
            }
        }

        public Task StartNotificationThread()
        {
            var notifications = GetAll()
                .GroupBy(x => x.NotificationUser)
                .ToDictionary(x => x.Key, x => x.ToList());

            if (notifications.Any())
            {
                NotificationThread(notifications).Wait();
                _notificationRepository.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public void ResetNotificationLastIndexValues()
        {
            var notifications = GetAll();
            foreach (var notification in notifications)
            {
                notification.LastIndexLevelId = null;
                foreach (var subject in notification.NotificationSubjects)
                {
                    subject.LastIndexLevelId = null;
                }

                _notificationRepository.Update(notification);
            }

            _notificationRepository.SaveChanges();
        }

        private async Task NotificationThread(Dictionary<NotificationUser, List<Notification>> notificationDictionary)
        {
            if(notificationDictionary != null)
            {
                string homeUrl = GetHomeUrl();
                List<EmailTemplateModel> emails = new List<EmailTemplateModel>();

                foreach(var valuePair in notificationDictionary)
                {
                    var userEmail = valuePair.Key.UserEmail;
                    EmailTemplateModel emailTemplateModel = new EmailTemplateModel();
                    foreach (var notification in valuePair.Value)
                    {
                        try
                        {
                            var stationState = await _giosHttpClientService.GetStationState(notification.StationId);
                            CheckIfNotificationIsRequired(notification, stationState, emailTemplateModel);
                        }
                        catch (Exception)
                        {

                        }
                        
                    }

                    if (emailTemplateModel.Notifications.Any())
                    {
                        emailTemplateModel.Subject = NotificationResource.EmailSubject;
                        emailTemplateModel.Email = userEmail;
                        emailTemplateModel.HomeUrl = homeUrl;
                        emailTemplateModel.StopSubscriptionUrl = GetStopNotificationUrl(valuePair.Key.StopNotificationToken.Value);
                        emails.Add(emailTemplateModel);
                    }
                }

                if (emails.Any())
                {
                    Task.Run(() => _emailService.SendEmails(emails));
                }
            }
        }

        private void CheckIfNotificationIsRequired(Notification notification, StationState stationState, EmailTemplateModel emailTemplateModel)
        {
            if(notification != null && stationState != null)
            {
                var notificationTemplateModel = new NotificationTemplateModel() { SubNotificationTemplates = new List<SubNotificationTemplateModel>()};

                if (notification.IndexLevelId.HasValue)
                {
                    if(notification.IndexLevelId.Value <= stationState.StIndexLevel?.Id &&
                        (notification.LastIndexLevelId.HasValue == false || stationState.StIndexLevel?.Id != notification.LastIndexLevelId.Value))
                    {
                        notificationTemplateModel.StationIndexLevelName = stationState.StIndexLevel.IndexLevelName;
                    }

                    notification.LastIndexLevelId = stationState.StIndexLevel?.Id;
                } 

                foreach (var subject in notification.NotificationSubjects)
                {
                    var subjectIndexValue = GetSubNotificationTemplateValue(subject, stationState);
                    if(subjectIndexValue != null)
                    {
                        subject.LastIndexLevelId = subjectIndexValue.Item1;
                        notificationTemplateModel.SubNotificationTemplates.Add(new SubNotificationTemplateModel
                        {
                            ParamCode = subject.ParamCode,
                            ParamIndexLevelName = subjectIndexValue.Item2
                        });
                    }
                }

                if(!string.IsNullOrEmpty(notificationTemplateModel.StationIndexLevelName) || 
                    notificationTemplateModel.SubNotificationTemplates.Any())
                {
                    notificationTemplateModel.StationName = notification.Station.StationName;
                    emailTemplateModel.Notifications.Add(notificationTemplateModel);
                }

                _notificationRepository.Update(notification);
            }
        }

        private Tuple<int, string> GetSubNotificationTemplateValue(NotificationSubject subject, StationState stationState)
        {
            var paramIndexLevel = GetParamIndexLevel(subject.ParamCode, stationState);
            if(paramIndexLevel != null)
            {
                if(paramIndexLevel.Item1 >= subject.IndexLevelId && 
                    (subject.LastIndexLevelId.HasValue == false || paramIndexLevel.Item1 != subject.LastIndexLevelId))
                {
                    return paramIndexLevel;
                }
            }

            return null;
        }

        private Tuple<int, string> GetParamIndexLevel(string paramCode, StationState stationState)
        {
            int? indexLevelId = null;
            string indexLevelName = string.Empty;

            if(Enum.TryParse(paramCode, out ParamCodes param))
            {
                switch (param)
                {
                    case ParamCodes.SO2:
                        if(stationState.So2IndexLevel != null)
                        {
                            indexLevelId = stationState.So2IndexLevel.Id;
                            indexLevelName = stationState.So2IndexLevel.IndexLevelName;
                        }
                        break;
                    case ParamCodes.NO2:
                        if (stationState.No2IndexLevel != null)
                        {
                            indexLevelId = stationState.No2IndexLevel.Id;
                            indexLevelName = stationState.No2IndexLevel.IndexLevelName;
                        }
                        break;
                    case ParamCodes.PM10:
                        if (stationState.Pm10IndexLevel != null)
                        {
                            indexLevelId = stationState.Pm10IndexLevel.Id;
                            indexLevelName = stationState.Pm10IndexLevel.IndexLevelName;
                        }
                        break;
                    case ParamCodes.PM25:
                        if (stationState.Pm25IndexLevel != null)
                        {
                            indexLevelId = stationState.Pm25IndexLevel.Id;
                            indexLevelName = stationState.Pm25IndexLevel.IndexLevelName;
                        }
                        break;
                    case ParamCodes.O3:
                        if (stationState.O3IndexLevel != null)
                        {
                            indexLevelId = stationState.O3IndexLevel.Id;
                            indexLevelName = stationState.O3IndexLevel.IndexLevelName;
                        }
                        break;
                }
            }

            if(indexLevelId.HasValue && !string.IsNullOrEmpty(indexLevelName))
            {
                return new Tuple<int, string>(indexLevelId.Value, indexLevelName);
            }

            return null;
            
        }

        private string GetHomeUrl()
        {
            string scheme = _hostConfig.GetValue<string>("Scheme");
            string host = _hostConfig.GetValue<string>("host");
            int? port = _hostConfig.GetValue<int?>("port");
            
            var uri = new UriBuilder(scheme, host);
            if (port.HasValue)
            {
                uri.Port = (int)port;
            }
            return uri.ToString();
        }

        private string GetStopNotificationUrl(Guid stopNotyficationToken)
        {
            string scheme = _hostConfig.GetValue<string>("Scheme");
            string host = _hostConfig.GetValue<string>("host");
            int? port = _hostConfig.GetValue<int?>("port");

            var uri = new UriBuilder(scheme, host);
            if (port.HasValue)
            {
                uri.Port = (int)port;
            }

            uri.Path = "stop-notification";
            uri.Query = $"token={HttpUtility.UrlEncode(stopNotyficationToken.ToString())}";

            return uri.ToString();
        }

        public async Task<bool> SendConfirmationEmailAsync(string email)
        {
            var user = _notificationUserRepository.GetByEmail(email);
            if(user == null)
            {
                return false;
            }

            user.EmailConfirmationToken = Guid.NewGuid();

            _notificationUserRepository.Update(user);
            _notificationUserRepository.SaveChanges();

            string scheme = _hostConfig.GetValue<string>("Scheme");
            string host = _hostConfig.GetValue<string>("host");
            int? port = _hostConfig.GetValue<int?>("port");

            var uri = new UriBuilder(scheme, host);
            if (port.HasValue)
            {
                uri.Port = (int)port;
            }

            uri.Path = "email-confirmation";
            uri.Query = $"token={HttpUtility.UrlEncode(user.EmailConfirmationToken.ToString())}";

            return await _emailService.SendConfirmationEmail(user.UserEmail, uri.ToString());
        }

        public bool ConfirmEmail(Guid token)
        {
            
            return _notificationUserRepository.ConfirmEmail(token);

        }
    }
}
