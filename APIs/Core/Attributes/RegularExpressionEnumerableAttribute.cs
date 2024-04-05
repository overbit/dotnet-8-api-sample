using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyService.APIs.Core.Attributes;

public class RegularExpressionEnumerable : RegularExpressionAttribute
{
    public RegularExpressionEnumerable(string pattern)
        : base(pattern) { }

    public override bool IsValid(object value)
    {
        if (value == null)
            return true;

        if (value is not IEnumerable<string>)
            return false;

        foreach (var val in value as IEnumerable<string>)
        {
            if (!Regex.IsMatch(val, Pattern))
                return false;
        }

        return true;
    }
}