using System.Linq;
using AirPurity.API.Data;
using API.DTOs.Pagination;
using FluentValidation;

namespace API.DTOs.Validators
{
    public class SensorsDataQueryValidator : AbstractValidator<SensorsDataQuery>
    {
        public SensorsDataQueryValidator(DataContext dbContext)
        {
            RuleFor(q => q.StationId)
                .Must(value => dbContext.Stations.Any(s => s.Id == value))
                .WithMessage(q => $"Station with id [{q.StationId}] does not exist in database");
            RuleFor(q => q.Range).IsInEnum();
        }
    }
}