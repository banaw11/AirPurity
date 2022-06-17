using AirPurity.API.DTOs.ClientDTOs;
using FluentValidation;

namespace AirPurity.API.DTOs.Validators
{
    public class NotificationDTOValidator : AbstractValidator<NotificationDTO>
    {
        public NotificationDTOValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty()
                .WithMessage("Adres email jst wymagany");
        }
    }
}
