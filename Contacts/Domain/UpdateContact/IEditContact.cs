using Contacts.Domain.Model;

namespace Contacts.Domain.UpdateContact;

public interface IEditContact
{
    void Execute(ContactNumber contactNumber);
}