using Contacts.Domain.ListContacts;
using Contacts.Domain.Model;

namespace Contacts.Data
{
    public class ListContacts : IListContacts
    {
        private readonly ContactsContext context;

        public ListContacts(ContactsContext context)
        {
            this.context = context;
        }

        public IEnumerable<ContactNumber> Execute()
        {
            return context.ContactNumbers;
        }
    }
}
