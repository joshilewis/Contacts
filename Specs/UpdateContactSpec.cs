using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contacts.Domain.AddContact;
using Contacts.Domain.Model;
using Contacts.Domain.UpdateContact;
using FastEndpoints;
using NUnit.Framework;

namespace Specs
{
    [TestFixture]
    public class UpdateContactSpec : Spec
    {
        [Test]
        public void NonExistentContact()
        {
            var updateCommand = new UpdateContactCommand
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "new first name",
                LastName = "new last name",
                Number = "1112223333"
            };

            UpdateContactResult result = this.UpdateContact(updateCommand);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.Message, Is.EqualTo("The specified contact number does not exist."));
        }

        [Test]
        public void NumberAlreadyUsed()
        {
            Guid idOfFirst = this.CreateAndAddContact();
            AddContactCommand second = this.CreateAddContactCommand("first", "last", "9998887777");
            this.AddContact(second);

            var updateCommand = new UpdateContactCommand
            {
                Id = idOfFirst.ToString(),
                FirstName = "new first name",
                LastName = "new last name",
                Number = second.Number
            };

            UpdateContactResult result = this.UpdateContact(updateCommand);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.Message, Is.EqualTo("The specified number is already in use."));

        }

        [Test]
        public void Success()
        {
            Guid contactId = this.CreateAndAddContact();

            var updateCommand = new UpdateContactCommand
            {
                Id = contactId.ToString(),
                FirstName = "new first name",
                LastName = "new last name",
                Number = "1112223333"
            };

            (HttpResponseMessage response, UpdateContactResult result) = this.UpdateContactRaw<UpdateContactResult>(updateCommand);
            Assert.That(response.IsSuccessStatusCode);

            ContactNumber contactNumber = this.GetContact(contactId);

            Assert.That(contactNumber.IsEqualTo(updateCommand));

        }

        //validations
                [Test]
        public void MissingFirstName()
        {
            UpdateContactCommand updateContactCommand = this.CreateUpdateContactCommand(Guid.NewGuid().ToString(), firstName: "");

            (HttpResponseMessage? response, ErrorResponse? result) = this.UpdateContactRaw<ErrorResponse>(updateContactCommand);

            var expected = new Dictionary<string, List<string>>
            {
                { "FirstName", new List<string> { "First name is required." } }
            };

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors, Is.EqualTo(expected));
        }

        [Test]
        public void MissingLastName()
        {
            UpdateContactCommand updateContactCommand = this.CreateUpdateContactCommand(Guid.NewGuid().ToString(), lastName: "");

            (HttpResponseMessage? response, ErrorResponse? result) = this.UpdateContactRaw<ErrorResponse>(updateContactCommand);
            var expected = new Dictionary<string, List<string>>
            {
                { "LastName", new List<string> { "Last name is required." } }
            };

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors, Is.EqualTo(expected));
        }

        [Test]
        public void MissingNumber()
        {
            UpdateContactCommand updateContactCommand = this.CreateUpdateContactCommand(Guid.NewGuid().ToString(), number: "");

            (HttpResponseMessage? response, ErrorResponse? result) = this.UpdateContactRaw<ErrorResponse>(updateContactCommand);
            var expected = new Dictionary<string, List<string>>
            {
                { "Number", new List<string> { "Number is required." } }
            };

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors, Is.EqualTo(expected));
        }

        [TestCase("000", Description = "Too short")]
        [TestCase("0001112222222", Description = "Too long")]
        [TestCase("000111222 ", Description = "Contains whitespace")]
        [TestCase("00011122--", Description = "Contains symbols")]
        [TestCase("000111aaaa", Description = "Contains letters")]
        public void InvalidNumber(string number)
        {
            UpdateContactCommand updateContactCommand = this.CreateUpdateContactCommand(Guid.NewGuid().ToString(), number: number);

            (HttpResponseMessage? response, ErrorResponse? result) = this.UpdateContactRaw<ErrorResponse>(updateContactCommand);
            var expected = new Dictionary<string, List<string>>
            {
                { "Number", new List<string> { "Number must be 10 digits without any other characters." } }
            };

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors, Is.EqualTo(expected));
        }

        [Test]
        public void MalformedId()
        {
            UpdateContactCommand updateContactCommand = this.CreateUpdateContactCommand(id:"invalid uuid");

            (HttpResponseMessage? response, ErrorResponse? result) = this.UpdateContactRaw<ErrorResponse>(updateContactCommand);
            var expected = new Dictionary<string, List<string>>
            {
                { "Id", new List<string> { "Please provide a correctly formatted contact ID." } }
            };

            Assert.That(response.IsSuccessStatusCode, Is.False);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors, Is.EqualTo(expected));

        }

    }
}
