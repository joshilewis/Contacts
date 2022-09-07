using Contacts.Domain.Model;

namespace Contacts.Domain.ListContacts
{
    public class ListContactsResult : Result
    {
        public IEnumerable<ContactNumber> ContactNumbers { get; set; }

        public ListContactsResult(string message)
            : base(message)
        {
            ContactNumbers = new List<ContactNumber>();
        }

        public ListContactsResult(IEnumerable<ContactNumber> contactNumbers)
        {
            ContactNumbers = contactNumbers;
        }

        public ListContactsResult()
        {
        }
    }
}
