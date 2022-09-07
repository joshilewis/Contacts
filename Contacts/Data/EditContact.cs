using Contacts.Domain.Model;
using Contacts.Domain.UpdateContact;

namespace Contacts.Data
{
    public class EditContact : IEditContact
    {
        private readonly ContactsContext context;

        public EditContact(ContactsContext context)
        {
            this.context = context;
        }

        public void Execute(ContactNumber contactNumber)
        {
            context.SaveChanges();
        }
    }
}
