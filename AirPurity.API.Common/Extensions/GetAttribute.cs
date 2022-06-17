using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AirPurity.API.Common.Extensions
{
    public static class Extensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            string name = string.Empty;

            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();
            
            if(displayAttribute != null)
            {
                name = displayAttribute.Name;

                var resourceKeyProperty = displayAttribute.ResourceType.GetProperty(displayAttribute.Name,
                    BindingFlags.Static | BindingFlags.Public, null, typeof(string), new Type[0], null);
                if (resourceKeyProperty != null)
                {
                    name = (string)resourceKeyProperty.GetMethod.Invoke(null, null);
                }
            }

            return name;
        }
    }
}
