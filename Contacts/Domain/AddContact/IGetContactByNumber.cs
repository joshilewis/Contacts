using Contacts.Domain.Model;

namespace Contacts.Domain.AddContact
{
    public interface IGetContactByNumber
    {
        ContactNumber Execute(string number);
    }
}
