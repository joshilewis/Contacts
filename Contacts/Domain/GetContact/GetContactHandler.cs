using Contacts.Domain.Model;

namespace Contacts.Domain.GetContact
{
    public class GetContactHandler
    {
        private readonly IFindContact _findContact;

        public GetContactHandler(IFindContact findContact)
        {
            this._findContact = findContact;
        }

        public GetContactResult Execute(GetContactQuery query)
        {
            ContactNumber contact = _findContact.Execute(Guid.Parse(query.Id));

            return new GetContactResult(contact);
        }
    }
}
