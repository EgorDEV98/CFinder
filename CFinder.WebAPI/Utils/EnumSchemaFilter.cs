using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CFinder.WebAPI.Utils;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            model.Enum.Clear();
            var enumNames = Enum.GetNames(context.Type);
            enumNames.ToList().ForEach(name =>
            {
                var enumValue = Enum.Parse(context.Type, name, ignoreCase: true);
                var displayAttribute = context?.Type?.GetField(name)
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .OfType<DisplayAttribute>()
                    .FirstOrDefault();
        
                var displayName = displayAttribute?.Name ?? name;
                var enumString = $"{Convert.ToInt64(enumValue)} - {name} ({displayName})";
                model.Enum.Add(new OpenApiString(enumString));
            });
        }
    }
}