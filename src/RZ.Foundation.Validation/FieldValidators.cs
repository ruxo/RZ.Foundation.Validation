using JetBrains.Annotations;

namespace RZ.Foundation.Validation;

[PublicAPI]
public static class FieldValidators
{
    public static Func<string, string?> TextMustNoEmpty(Action<string> setter, string message) => name => {
        var normalized = name.Trim();
        if (normalized != name)
            setter(normalized);
        return string.IsNullOrWhiteSpace(normalized) ? message : null;
    };
}