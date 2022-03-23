using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Contacts.Domain.AddContact;
using Contacts.Domain;
using Contacts.Domain.Model;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Specs
{
    [TestFixture]
    public class AddContactSpec : Spec
    {

        [Test]
        public void Success()
        {
            AddContactCommand addContactCommand = this.CreateAddContactCommand();

            (HttpResponseMessage? response, AddContactResult? result) =
                this.AddContactRaw<AddContactResult>(addContactCommand); 

            Assert.That(response.IsSuccessStatusCode);
            Assert.That(result.ContactId, Is.Not.EqualTo(Guid.NewGuid()));

            DbSet<ContactNumber> contactNumbers = contactsContext.ContactNumbers;
            Assert.That(contactNumbers.Count(), Is.EqualTo(1));

            ContactNumber contactNumber = contactNumbers.First();
            Assert.That(contactNumber.Id, Is.EqualTo(result.ContactId));
            contactNumber.IsEqualTo(addContactCommand);

        }

        [Test]
        public void MissingFirstName()
        {
            AddContactCommand addContactCommand = this.CreateAddContactCommand(firstName: "");

            (HttpResponseMessage? response, ErrorResponse? result) = this.AddContactRaw<ErrorResponse>(addContactCommand);

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
            AddContactCommand addContactCommand = this.CreateAddContactCommand(lastName: "");

            (HttpResponseMessage? response, ErrorResponse? result) = this.AddContactRaw<ErrorResponse>(addContactCommand);

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
            AddContactCommand addContactCommand = this.CreateAddContactCommand(number: "");

            (HttpResponseMessage? response, ErrorResponse? result) = this.AddContactRaw<ErrorResponse>(addContactCommand);

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
            AddContactCommand addContactCommand = this.CreateAddContactCommand(number: number);

            (HttpResponseMessage? response, ErrorResponse? result) = this.AddContactRaw<ErrorResponse>(addContactCommand);

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
        public void NumberAlreadyExists()
        {
            AddContactCommand addContactCommand = this.CreateAddContactCommand();

            this.AddContact(addContactCommand);

            (HttpResponseMessage response, AddContactResult result) =
                this.AddContactRaw<AddContactResult>(addContactCommand);

            Assert.That(response.IsSuccessStatusCode);

            Assert.That(result.ResultCode, Is.EqualTo(EResultCode.Error));
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.Message, Is.EqualTo("A contact number with the given number already exists."));

        }
    }
}