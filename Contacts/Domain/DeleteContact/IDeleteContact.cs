using Contacts.Domain.Model;

namespace Contacts.Domain.DeleteContact;

public interface IDeleteContact
{
    void Execute(ContactNumber contactNumber);
}