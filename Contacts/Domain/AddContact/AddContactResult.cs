namespace Contacts.Domain.AddContact
{
    public class AddContactResult : Result
    {
        public Guid ContactId { get; set; }

        public AddContactResult(Guid contactId)
            : base()
        {
            ContactId = contactId;
        }

        public AddContactResult(string message) 
            : base(message)
        {
        }

        public AddContactResult()
            : base()
        {

        }
    }
}
