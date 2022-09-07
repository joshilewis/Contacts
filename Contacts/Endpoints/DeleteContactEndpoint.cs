using Contacts.Domain.DeleteContact;

namespace Contacts.Endpoints
{
    public class DeleteContactEndpoint : Endpoint<DeleteContactCommand, DeleteContactResult>
    {
        private readonly DeleteContactHandler deleteContact;

        public DeleteContactEndpoint(DeleteContactHandler deleteContact)
        {
            this.deleteContact = deleteContact;
        }

        public override void Configure()
        {
            Verbs(Http.DELETE);
            Routes("/contacts/{Id}");
            AllowAnonymous();

        }

        public override async Task HandleAsync(DeleteContactCommand req, CancellationToken ct)
        {
            await SendAsync(deleteContact.Execute(req));
        }
    }
}
