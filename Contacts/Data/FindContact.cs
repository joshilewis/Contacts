using Contacts.Domain.GetContact;
using Contacts.Domain.Model;

namespace Contacts.Data
{
    public class FindContact : IFindContact
    {
        private readonly ContactsContext context;

        public FindContact(ContactsContext context)
        {
            this.context = context;
        }

        public ContactNumber? Execute(Guid contactId)
        {
            return context.Find<ContactNumber>(contactId);
        }
    }
}
