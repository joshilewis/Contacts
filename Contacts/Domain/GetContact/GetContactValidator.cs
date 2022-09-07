namespace Contacts.Domain.GetContact
{
    public class GetContactValidator : Validator<GetContactQuery>
    {
        public GetContactValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Please provide a contact ID.");

            RuleFor(x => x.Id)
                .Must(x =>
                {
                    if (string.IsNullOrEmpty(x)) return true;
                    return Guid.TryParse(x, out _);
                })
                .WithMessage("Please provide a correctly formatted contact ID.");
        }

    }
}
