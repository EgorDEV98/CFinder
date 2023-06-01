using System.Reflection;
using Microsoft.OpenApi.Attributes;

namespace CFinder.WebAPI.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.Name ?? string.Empty;
    }
}