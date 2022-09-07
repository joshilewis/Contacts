using Contacts.Domain.Model;

namespace Contacts.Domain.AddContact
{
    public class AddContactHandler
    {
        private readonly ISaveContact saveContact;
        private readonly IGetContactByNumber getContactByNumber;

        public AddContactHandler(ISaveContact saveContact, IGetContactByNumber getContactByNumber)
        {
            this.saveContact = saveContact;
            this.getContactByNumber = getContactByNumber;
        }

        public AddContactResult Execute(AddContactCommand command)
        {
            //Check if contact already exists
            ContactNumber existing = getContactByNumber.Execute(command.Number);
            if (existing != null) return new AddContactResult("A contact number with the given number already exists.");
            
            Guid newId = Guid.NewGuid();

            var newContact = new ContactNumber(newId, command.FirstName, command.LastName, command.Number);

            saveContact.Execute(newContact);
            return new AddContactResult(newContact.Id);
        }
    }
}
