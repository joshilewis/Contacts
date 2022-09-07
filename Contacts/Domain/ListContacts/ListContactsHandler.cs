namespace Contacts.Domain.ListContacts
{
    public class ListContactsHandler
    {
        private readonly IListContacts listContacts;

        public ListContactsHandler(IListContacts listContacts)
        {
            this.listContacts = listContacts;
        }

        public ListContactsResult Execute(ListContactsQuery query)
        {
            return new ListContactsResult(listContacts.Execute());
        }
    }
}
