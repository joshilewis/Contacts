using Contacts.Domain.AddContact;
using Contacts.Domain.Model;

namespace Contacts.Data
{
    public class GetContactByNumber : IGetContactByNumber
    {
        private readonly ContactsContext context;

        public GetContactByNumber(ContactsContext context)
        {
            this.context = context;
        }

        public ContactNumber Execute(string number)
        {
            return context.ContactNumbers.FirstOrDefault(x => x.Number == number);
        }
    }
}
