namespace Contacts.Domain.DeleteContact
{
    public class DeleteContactValidator : Validator<DeleteContactCommand>
    {
        public DeleteContactValidator()
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
