using AirPurity.API.Common.Resources;
using AirPurity.API.Data.Entities;
using AirPurity.API.DTOs;
using AirPurity.API.DTOs.ClientDTOs;
using AirPurity.API.Interfaces;
using API.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AirPurity.API.Controllers
{
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNoticiation([FromBody] NotificationDTO notificationDTO)
        {
            var notification = _mapper.Map<Notification>(notificationDTO);
            var res = _notificationService.Add(notification, notificationDTO.UserEmail);
            if (res)
            {
                return Ok(new ResponseModel(success: true, message: NotificationResource.SubscriptionSuccessMessage));
            }

            var confirmationEmailresult = await _notificationService.SendConfirmationEmailAsync(notificationDTO.UserEmail);

            if (confirmationEmailresult)
            {
                return Ok(new ResponseModel(success: true, message: NotificationResource.ConfirmEmailMessage));
            }
            else
            {
                return Ok(new ResponseModel(success: false, message: NotificationResource.ConfirmationEmailFailed));
            }
        }

        [HttpPost("stop-notification")]
        public IActionResult StopNotification(string token)
        {
            if(Guid.TryParse(token, out Guid guid))
            {
                _notificationService.RemoveAllNotification(guid);
            };
            
            return Ok();
        }

        [HttpPost("email-confirmation")]
        public IActionResult ConfirmEmail(string token)
        {
            if (Guid.TryParse(token, out Guid guid))
            {
                var res = _notificationService.ConfirmEmail(guid);
                if (res)
                {
                    return Ok(new ResponseModel(success: true, message: NotificationResource.ConfirmationEmailSuccess));
                }
            };

            return Ok(new ResponseModel(success: false, message: NotificationResource.ConfirmationEmailFailed));
        }
    }
}
