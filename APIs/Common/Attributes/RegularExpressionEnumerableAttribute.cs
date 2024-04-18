using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyService.APIs.Common.Attributes;

public class RegularExpressionEnumerable : RegularExpressionAttribute
{
    public RegularExpressionEnumerable(string pattern)
        : base(pattern) { }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        if (value is not IEnumerable<string>)
            return false;

        IEnumerable<string> values = value as IEnumerable<string> ?? [];

        foreach (var val in values)
        {
            if (!Regex.IsMatch(val, Pattern))
                return false;
        }

        return true;
    }
}
