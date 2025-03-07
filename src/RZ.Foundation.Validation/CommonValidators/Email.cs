using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace RZ.Foundation.Validation.CommonValidators;

[PublicAPI]
public static class Email
{
    static readonly EmailAddressAttribute EmailValidator = new();

    public static bool IsValid(string email) =>
        EmailValidator.IsValid(email) && EmailContainTopDomain(email);

    public static bool EmailContainTopDomain(string email)
    {
        var lastIndexOfDot = email.LastIndexOf('.');
        var dotIsLastIndex = lastIndexOfDot == email.Length - 1;
        return lastIndexOfDot > email.IndexOf('@') && !dotIsLastIndex;
    }
}