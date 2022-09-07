using Contacts.Domain.DeleteContact;
using Contacts.Domain.Model;

namespace Contacts.Data
{
    public class DeleteContact : IDeleteContact
    {
        private readonly ContactsContext context;

        public DeleteContact(ContactsContext context)
        {
            this.context = context;
        }

        public void Execute(ContactNumber contactNumber)
        {
            context.Remove(contactNumber);
            context.SaveChanges();
        }
    }
}
