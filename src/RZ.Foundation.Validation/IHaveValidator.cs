using FluentValidation;
using JetBrains.Annotations;
using RZ.Foundation.Types;

namespace RZ.Foundation.Validation;

public interface IHaveValidator<in T>
{
    static abstract IValidator<T> Validator { get; }
}

[PublicAPI]
public static class IHaveValidatorExtensions
{
    public static T Validate<T>(this T updated) where T: IHaveValidator<T> {
        var invalid = T.Validator.Validate(updated).Errors.FirstOrDefault();
        if (invalid is not null)
            throw new ErrorInfoException(StandardErrorCodes.ValidationFailed, invalid.ErrorMessage);
        return updated;
    }

    public static Outcome<T> TryValidate<T>(this T updated) where T: IHaveValidator<T> {
        var invalid = T.Validator.Validate(updated).Errors.FirstOrDefault();
        if (invalid is not null)
            return new ErrorInfo(StandardErrorCodes.ValidationFailed, invalid.ErrorMessage);
        return updated;
    }
}
