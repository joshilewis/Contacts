namespace Contacts.Domain.UpdateContact
{
    public class UpdateContactCommand
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
    }
}
