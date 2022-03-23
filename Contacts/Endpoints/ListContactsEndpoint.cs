using Contacts.Domain.ListContacts;

namespace Contacts.Endpoints
{
    public class ListContactsEndpoint : Endpoint<ListContactsQuery, ListContactsResult>
    {
        private readonly ListContactsHandler handler;

        public ListContactsEndpoint(ListContactsHandler handler)
        {
            this.handler = handler;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/contacts/");
            AllowAnonymous();

        }

        public override async Task HandleAsync(ListContactsQuery req, CancellationToken ct)
        {
            await SendAsync(handler.Execute(req));
        }
    }
}
