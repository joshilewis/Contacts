using System.Text.RegularExpressions;
using Contacts.Domain.UpdateContact;

namespace Contacts.Endpoints
{
    public class UpdateContactValidator : Validator<UpdateContactCommand>
    {
        public UpdateContactValidator()
        {
            RuleFor(x => x.Id)
                .Must(x =>
                {
                    if (string.IsNullOrEmpty(x)) return true;
                    return Guid.TryParse(x, out _);
                })
                .WithMessage("Please provide a correctly formatted contact ID.");

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
}
