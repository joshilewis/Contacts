using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contacts.Domain.AddContact;
using Contacts.Domain.GetContact;
using Contacts.Domain.Model;
using Contacts.Endpoints;
using FastEndpoints;
using NUnit.Framework;

namespace Specs
{
    [TestFixture]
    public class GetContactSpec : Spec
    {
        [Test]
        public async Task EmptyContactId()
        {
            GetContactQuery query = new GetContactQuery("");

            var expected = new Dictionary<string, List<string>>
            {
                { "Id", new List<string> { "Please provide a contact ID." } }
            };

            (HttpResponseMessage? response, ErrorResponse? result) =
                await Client.GETAsync<GetContactEndpoint, GetContactQuery, ErrorResponse>(query);

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));    
            Assert.That(result.Errors, Is.EqualTo(expected));

        }

        [Test]
        public async Task MalformedId()
        {
            GetContactQuery query = new GetContactQuery("gregrrg");

            var expected = new Dictionary<string, List<string>>
            {
                { "Id", new List<string> { "Please provide a correctly formatted contact ID." } }
            };

            (HttpResponseMessage? response, ErrorResponse? result) =
                await Client.GETAsync<GetContactEndpoint, GetContactQuery, ErrorResponse>(query);

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(result.Errors, Is.EqualTo(expected));

        }

        [Test]
        public async Task NonExistentContact()
        {
            GetContactQuery query = new GetContactQuery(Guid.NewGuid().ToString());

            (HttpResponseMessage? response, GetContactResult? result) =
                await Client.GETAsync<GetContactEndpoint, GetContactQuery, GetContactResult>(query);

            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(result.ContactNumber, Is.Null);

        }

        [Test]
        public void ExistingContact()
        {
            AddContactCommand addContactCommand = this.CreateAddContactCommand();

            Guid contactId = this.AddContact(addContactCommand);

            ContactNumber contactNumber = this.GetContact(contactId);

            Assert.That(contactNumber.Id, Is.EqualTo(contactId));
            contactNumber.IsEqualTo(addContactCommand);

        }
    }
}
