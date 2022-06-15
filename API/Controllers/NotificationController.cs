using AirPurity.API.Data.Entities;
using AirPurity.API.DTOs.ClientDTOs;
using AirPurity.API.Interfaces;
using API.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CreateNoticiation([FromBody] NotificationDTO notificationDTO)
        {
            var notification = _mapper.Map<Notification>(notificationDTO);
            _notificationService.Add(notification);
            return Ok();
        }

        [HttpPost("delete")]
        public IActionResult DeleteNoticiation([FromQuery] string email)
        {
            _notificationService.RemoveAllNotification(email);
            //to - do redirect to inform page

            HttpContext.Response.Redirect(string.Empty);
            return Ok();
        }
    }
}
