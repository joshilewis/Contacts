using Contacts.Domain.AddContact;
using Contacts.Domain.GetContact;
using Contacts.Domain.Model;

namespace Contacts.Domain.UpdateContact
{
    public class UpdateContactHandler
    {
        private IFindContact findContact;
        private IEditContact editContact;
        private IGetContactByNumber getContactByNumber;

        public UpdateContactHandler(IFindContact findContact, IEditContact editContact, IGetContactByNumber getContactByNumber)
        {
            this.findContact = findContact;
            this.editContact = editContact;
            this.getContactByNumber = getContactByNumber;
        }

        public UpdateContactResult Execute(UpdateContactCommand command)
        {
            if (CheckIfNumberAlreadyUsed(command.Number))
                return new UpdateContactResult("The specified number is already in use.");

            ContactNumber? existing = findContact.Execute(Guid.Parse(command.Id));
            if (existing == null) return new UpdateContactResult("The specified contact number does not exist.");

            existing.Edit(command.FirstName, command.LastName, command.Number);

            editContact.Execute(existing);

            return new UpdateContactResult();
        }

        private bool CheckIfNumberAlreadyUsed(string number)
        {
            return getContactByNumber.Execute(number) != null;
        }
    }
}
