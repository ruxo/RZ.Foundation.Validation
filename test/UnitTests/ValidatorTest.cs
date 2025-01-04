using FluentAssertions;
using FluentValidation;
using RZ.Foundation.Validation;

namespace UnitTests;

public class ValidatorTest
{
    [Fact]
    public void SimpleValidation() {
        var f = Validator.OfSingleError<string>(b => b.NotEmpty().WithMessage("Must have value"));

        var result = f(string.Empty);

        result.Should().Be("Must have value");
    }

    [Fact]
    public void MultipleValidation() {
        var f = Validator.Of<int>(b => b.GreaterThan(2).WithMessage(">2")
                                          .Must(x => x % 2 == 0).WithMessage("Be even"));

        var result = f(1);

        result.Should().BeEquivalentTo(">2", "Be even");
    }
}
