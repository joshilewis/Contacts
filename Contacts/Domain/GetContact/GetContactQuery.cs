namespace Contacts.Domain.GetContact
{
    public class GetContactQuery
    {
        public string Id { get; set; }

        public GetContactQuery(string id)
        {
            Id = id;
        }

        public GetContactQuery()
        {
            Id = string.Empty;
        }

    }
}
