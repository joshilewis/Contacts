using System.Text.RegularExpressions;

namespace Contacts.Domain.AddContact;

public class AddContactValidator : Validator<AddContactCommand>
{
    public AddContactValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Number is required.")
            .Must(x =>
            {
                if (string.IsNullOrEmpty(x)) return true;
                string cleaned = Regex.Replace(x, @"[^0-9]+", "");
                return cleaned.Length == 10;
            })
            .WithMessage("Number must be 10 digits without any other characters.")
            ;

    }
}