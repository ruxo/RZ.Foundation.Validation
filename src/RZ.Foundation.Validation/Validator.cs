using FluentValidation;

namespace RZ.Foundation.Validation;

public static class Validator
{
    public static Func<T, string?> OfSingleError<T>(Action<IRuleBuilderInitial<SingleValue<T>,T>> builder)
        => OfSingleError<T,T>(builder, x => x);

    public static Func<A, string?> OfSingleError<A,B>(Action<IRuleBuilderInitial<SingleValue<B>,B>> builder, Func<A,B> mapper) {
        var validator = new SingleValueValidator<B>(builder);
        return x => {
            var errors = validator.Validate(new SingleValue<B>(mapper(x)));
            return errors.IsValid ? null : errors.Errors.First().ErrorMessage;
        };
    }

    public static Func<T, IEnumerable<string>> Of<T>(Action<IRuleBuilderInitial<SingleValue<T>,T>> builder) {
        var validator = new SingleValueValidator<T>(builder);
        return x => {
            var errors = validator.Validate(new SingleValue<T>(x));
            return errors.IsValid ? [] : errors.Errors.Select(e => e.ErrorMessage);
        };
    }

    public static Func<A, IEnumerable<string>> Of<A,B>(Action<IRuleBuilderInitial<SingleValue<B>,B>> builder, Func<A,B> mapper) {
        var validator = new SingleValueValidator<B>(builder);
        return x => {
            var errors = validator.Validate(new SingleValue<B>(mapper(x)));
            return errors.IsValid ? [] : errors.Errors.Select(e => e.ErrorMessage);
        };
    }

    public readonly struct SingleValue<T>(T v)
    {
        public T Value => v;
    }

    sealed class SingleValueValidator<T> : AbstractValidator<SingleValue<T>>
    {
        public SingleValueValidator(Action<IRuleBuilderInitial<SingleValue<T>,T>> builder) {
            builder(RuleFor(x => x.Value));
        }
    }
}
