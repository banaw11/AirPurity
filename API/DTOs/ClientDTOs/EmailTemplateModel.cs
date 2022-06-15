using System.Collections.Generic;

namespace AirPurity.API.DTOs.ClientDTOs
{
    public class EmailTemplateModel
    {
        public EmailTemplateModel()
        {
            this.Notifications = new List<NotificationTemplateModel>();
        }

        public string Email { get; set; }
        public string Subject { get; set; }
        public string HomeUrl { get; set; }
        public string StopSubscriptionUrl { get; set; }
        public List<NotificationTemplateModel> Notifications { get; set; }
    }

    public class NotificationTemplateModel
    {
        public string StationName { get; set; }
        public string StationIndexLevelName { get; set; }
        public List<SubNotificationTemplateModel> SubNotificationTemplates { get; set; }

    }

    public class SubNotificationTemplateModel
    {
        public string ParamCode { get; set; }
        public string ParamIndexLevelName { get; set; }
    }
}
