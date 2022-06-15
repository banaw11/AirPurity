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
            RuleFor(x => x.IndexLevelId)
                .Custom((value, context) =>
                {
                    var dto = context.InstanceToValidate;
                    if(dto.NotificationSubjects.Count == 0 && !value.HasValue)
                    {
                        context.AddFailure("notification", "Poziom jakości powietrza lub specyficzny sensor jest wymagany");
                    }
                });
        }
    }
}
