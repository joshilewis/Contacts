using Contacts.Domain.Model;

namespace Contacts.Domain.AddContact
{
    public interface ISaveContact
    {
        void Execute(ContactNumber contactNumber);
    }
}
