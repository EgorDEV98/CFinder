using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CFinder.Application.Utils;


internal static class JsonSearcher
{
    private static JsonDocumentOptions options = new()
    {
        CommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };
    
    public static async Task<string> SearchCycle(string mainText, string startWith, int maxItteration = 1000)
    {
        var indexString = mainText.IndexOf(startWith, StringComparison.OrdinalIgnoreCase);
        if (indexString <= 0)
        {
            return string.Empty;
        }
        
        var substring = mainText.Substring(indexString, maxItteration);
        var endOfSubstring = substring.IndexOf('}') + 1;
        var createdSubstring = substring
            .Substring(0, endOfSubstring)
            .NormilizeJson();
        
        if (CanDeserialize(createdSubstring))
        {
            await Task.CompletedTask;
            return createdSubstring;
        }
        else
        {
            var stringBuilder = new StringBuilder();
             for (int i = 0; i < maxItteration; i++)
             {
                 stringBuilder.Append(substring[i]);
                 
                 var stringBuilderString = stringBuilder.ToString().NormilizeJson();
                 if (CanDeserialize(stringBuilderString))
                 {
                     await Task.CompletedTask;
                     return stringBuilder.ToString();
                 }
             }
        }
        
        await Task.CompletedTask;
        return string.Empty;
    }
    public static async Task<string> SearchRegex(string mainText, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
    {
        var isMatch = Regex.IsMatch(mainText, pattern, options);
        if (isMatch)
        {
            await Task.CompletedTask;
            return Regex.Match(mainText, pattern, options).Groups[1].Value.Replace("\\", "");
        }

        await Task.CompletedTask;
        return string.Empty;
    }
    public static bool CanDeserialize(string inputJson)
    {
        if (string.IsNullOrWhiteSpace(inputJson))
        {
            return false;
        }
        
        try
        {
            JsonDocument.Parse(inputJson, options);
            return true;
        }
        catch (JsonException ex)
        {
            return false;
        }
    }
    public static string NormilizeJson(this string json)
    {
        return  json.Replace("\\\"", "\"")
            .Replace("\"{", "{")
            .Replace("}\"", "}");
    }
    
    public static JsonElement Search(JsonElement element, string key)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (JsonProperty property in element.EnumerateObject())
            {
                if (property.Name.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    return property.Value;
                }
                else if (property.Value.ValueKind == JsonValueKind.Object || property.Value.ValueKind == JsonValueKind.Array)
                {
                    // Рекурсивный вызов для поиска во вложенных объектах или массивах
                    JsonElement foundElement = Search(property.Value, key);
                    if (foundElement.ValueKind != JsonValueKind.Undefined)
                    {
                        return foundElement;
                    }
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (JsonElement arrayElement in element.EnumerateArray())
            {
                if (arrayElement.ValueKind == JsonValueKind.Object || arrayElement.ValueKind == JsonValueKind.Array)
                {
                    // Рекурсивный вызов для поиска во вложенных объектах или массивах
                    JsonElement foundElement = Search(arrayElement, key);
                    if (foundElement.ValueKind != JsonValueKind.Undefined)
                    {
                        return foundElement;
                    }
                }
            }
        }

        return default; // Если не найдено значение
    }
}