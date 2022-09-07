using Contacts.Domain.UpdateContact;

namespace Contacts.Endpoints
{
    public class UpdateContactEndpoint : Endpoint<UpdateContactCommand, UpdateContactResult>
    {
        private readonly UpdateContactHandler updateContact;

        public UpdateContactEndpoint(UpdateContactHandler updateContact)
        {
            this.updateContact = updateContact;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/contacts/{Id}");
            AllowAnonymous();

        }

        public override async Task HandleAsync(UpdateContactCommand req, CancellationToken ct)
        {
            await SendAsync(updateContact.Execute(req));
        }
    }
}
