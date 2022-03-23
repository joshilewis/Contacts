using Contacts.Domain.GetContact;

namespace Contacts.Endpoints
{
    public class GetContactEndpoint: Endpoint<GetContactQuery, GetContactResult>
    {
        private readonly GetContactHandler getContact;

        public GetContactEndpoint(GetContactHandler getContact)
        {
            this.getContact = getContact;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/contacts/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetContactQuery req, CancellationToken ct)
        {
            await SendAsync(getContact.Execute(req));
        }
    }
}
