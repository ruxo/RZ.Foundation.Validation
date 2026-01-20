using FluentValidation;
using JetBrains.Annotations;
using RZ.Foundation.Types;

namespace RZ.Foundation.Validation;

public interface IHaveValidator<in T>
{
    static abstract IValidator<T> Validator { get; }
}

public static class IHaveValidatorExtensions
{
    extension<T>(T updated) where T : IHaveValidator<T>
    {
        [PublicAPI]
        public Outcome<T> Validate()
            => T.Validator.Validate(updated).Errors.FirstOrDefault() is { } invalid
                   ? new ErrorInfo(StandardErrorCodes.ValidationFailed, invalid.ErrorMessage)
                   : updated;
    }
}