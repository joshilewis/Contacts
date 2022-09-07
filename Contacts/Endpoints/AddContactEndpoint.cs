using Contacts.Domain;
using Contacts.Domain.AddContact;

namespace Contacts.Endpoints
{
    public class AddContactEndpoint : Endpoint<AddContactCommand, AddContactResult>
    {
        private readonly AddContactHandler addContact;

        public AddContactEndpoint(AddContactHandler addContact)
        {
            this.addContact = addContact;
        }

        public override void Configure()
        {
            Verbs(Http.PUT);
            Routes("/contacts/");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AddContactCommand req, CancellationToken ct)
        {
            await SendAsync(addContact.Execute(req));
        }
    }
}
