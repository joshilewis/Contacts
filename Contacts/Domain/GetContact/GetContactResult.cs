using Contacts.Domain.Model;

namespace Contacts.Domain.GetContact
{
    public class GetContactResult
    {
        public ContactNumber? ContactNumber { get; set; }

        public GetContactResult(ContactNumber contactNumber)
        {
            ContactNumber = contactNumber;
        }

        public GetContactResult()
        {
            
        }
    }
}
