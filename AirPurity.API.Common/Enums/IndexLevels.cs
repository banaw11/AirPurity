using AirPurity.API.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace AirPurity.API.Common.Enums
{
    public enum IndexLevels
    { 
        [Display(Name = nameof(VeryGood), ResourceType = typeof(NotificationResource))]
        VeryGood = 0,
        [Display(Name = nameof(Good), ResourceType = typeof(NotificationResource))]
        Good = 1,
        [Display(Name = nameof(Temperate), ResourceType = typeof(NotificationResource))]
        Temperate = 2,
        [Display(Name = nameof(Sufficient), ResourceType = typeof(NotificationResource))]
        Sufficient = 3,
        [Display(Name = nameof(Bad), ResourceType = typeof(NotificationResource))]
        Bad = 4,
        [Display(Name = nameof(VeryBad), ResourceType = typeof(NotificationResource))]
        VeryBad = 5,
    }
}
