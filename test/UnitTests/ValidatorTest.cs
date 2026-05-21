using FluentValidation;
using JetBrains.Annotations;
using RZ.Foundation.Validation;

namespace UnitTests;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class ValidatorTest
{
    [Test]
    public async ValueTask SimpleValidation() {
        var f = Validator.OfSingleError<string>(b => b.NotEmpty().WithMessage("Must have value"));

        var result = f(string.Empty);

        await Assert.That(result).IsEqualTo("Must have value");
    }

    [Test]
    public async ValueTask MultipleValidation() {
        var f = Validator.Of<int>(b => b.GreaterThan(2).WithMessage(">2")
                                          .Must(x => x % 2 == 0).WithMessage("Be even"));

        var result = f(1);

        await Assert.That(result).IsEquivalentTo([">2", "Be even"]);
    }
}
