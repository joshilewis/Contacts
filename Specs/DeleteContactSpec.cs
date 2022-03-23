using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contacts.Domain.DeleteContact;
using Contacts.Domain.ListContacts;
using Contacts.Domain.Model;
using FastEndpoints;
using NUnit.Framework;
using static Specs.Helpers;

namespace Specs
{
    public class DeleteContactSpec : Spec
    {
        [Test]
        public void MalformedId()
        {
            DeleteContactCommand deleteContactCommand = new DeleteContactCommand("rgregrgrgrg");

            (HttpResponseMessage? response, ErrorResponse? result) = this.DeleteContactRaw<ErrorResponse>(deleteContactCommand);
            var expected = new Dictionary<string, List<string>>
            {
                { "Id", new List<string> { "Please provide a correctly formatted contact ID." } }
            };

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors, Is.EqualTo(expected));
        }

        [Test]
        public void NonExistentContact()
        {
            DeleteContactCommand deleteContactCommand = new DeleteContactCommand(Guid.NewGuid().ToString());

            DeleteContactResult result = this.DeleteContact(deleteContactCommand);

            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.Message, Is.EqualTo("The specified contact number does not exist."));
        }

        [Test]
        public void Success()
        {
            Guid contactId = this.CreateAndAddContact();
            ContactNumber contactNumber = this.GetContact(contactId);

            DeleteContactResult result = this.DeleteContact(new DeleteContactCommand(contactId.ToString()));

            contactNumber = this.GetContact(contactId);
            Assert.That(contactNumber, Is.Null);

        }

        [Test]
        public void Success2()
        {
            Guid id1 = this.CreateAndAddContact(firstName: "first1", "last1", "1111111111");
            Guid id2 = this.CreateAndAddContact(firstName: "first2", "last2", "2222222222");
            Guid id3 = this.CreateAndAddContact(firstName: "first3", "last3", "3333333333");

            ContactNumber contact1 = this.GetContact(id1);
            ContactNumber contact2 = this.GetContact(id2);
            ContactNumber contact3 = this.GetContact(id3);

            this.DeleteContact(new DeleteContactCommand(id2.ToString()));

            ListContactsResult result = this.ListContacts();
            Assert.That(result.ContactNumbers.Count(), Is.EqualTo(2));

            var expected = new[] { contact1, contact3};
            Assert.That(result.ContactNumbers, Is.EquivalentTo(expected).Using(Comparer));

        }
    }
}
