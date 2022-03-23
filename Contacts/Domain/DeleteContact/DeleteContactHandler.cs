using Contacts.Domain.GetContact;
using Contacts.Domain.Model;

namespace Contacts.Domain.DeleteContact
{
    public class DeleteContactHandler
    {
        private readonly IFindContact findContact;
        private readonly IDeleteContact deleteContact;

        public DeleteContactHandler(IFindContact findContact, IDeleteContact deleteContact)
        {
            this.findContact = findContact;
            this.deleteContact = deleteContact;
        }

        public DeleteContactResult Execute(DeleteContactCommand command)
        {
            ContactNumber? contact = findContact.Execute(Guid.Parse(command.Id));
            if (contact == null) return new DeleteContactResult("The specified contact number does not exist.");

            deleteContact.Execute(contact);

            return new DeleteContactResult();
        }
    }
}
