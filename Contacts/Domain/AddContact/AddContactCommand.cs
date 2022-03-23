namespace Contacts.Domain.AddContact
{
    public record AddContactCommand
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Number { get; init; }

    }
}
