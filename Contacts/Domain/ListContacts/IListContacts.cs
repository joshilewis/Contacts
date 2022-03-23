using Contacts.Domain.Model;

namespace Contacts.Domain.ListContacts;

public interface IListContacts
{
    IEnumerable<ContactNumber> Execute();
}