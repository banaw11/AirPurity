using System.Linq;
using AirPurity.API.Data;
using API.DTOs.Pagination;
using FluentValidation;

namespace API.DTOs.Validators
{
    public class CityQueryValidator : AbstractValidator<CityQuery>
    {
        public CityQueryValidator(DataContext dbContext)
        {
            RuleFor(q => q.ProvinceName)
                .Must(value => dbContext.Provinces.Any(p => p.ProvinceName == value) || string.IsNullOrEmpty(value) )
                .WithMessage(q =>  $"Provience [{q.ProvinceName}] does not exist in database");
            RuleFor(q => q.DistrictName)
                .Must(value => dbContext.Districts.Any(p => p.DistrictName == value) || string.IsNullOrEmpty(value))
                .WithMessage(q =>  $"District [{q.DistrictName}] does not exist in database");
            RuleFor(q => q.CommuneName)
                .Must(value => dbContext.Communes.Any(p => p.CommuneName == value) || string.IsNullOrEmpty(value) )
                .WithMessage(q =>  $"Commune [{q.CommuneName}] does not exist in database");
        }
    }
}