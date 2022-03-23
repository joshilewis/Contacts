using Contacts.Domain.AddContact;
using Contacts.Domain.Model;

namespace Contacts.Data;

public class SaveContact : ISaveContact
{
    private readonly ContactsContext context;

    public SaveContact(ContactsContext context)
    {
        this.context = context;
    }

    public virtual void Execute(ContactNumber contactNumber)
    {
        context.ContactNumbers.Add(contactNumber);
        context.SaveChanges();
    }
}
