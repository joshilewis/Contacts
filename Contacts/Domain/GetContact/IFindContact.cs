using Contacts.Domain.Model;

namespace Contacts.Domain.GetContact
{
    public interface IFindContact
    {
        ContactNumber? Execute(Guid contactId);
    }
}
