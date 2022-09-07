using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Contacts.Domain.AddContact;
using Contacts.Domain.DeleteContact;
using Contacts.Domain.GetContact;
using Contacts.Domain.ListContacts;
using Contacts.Domain.Model;
using Contacts.Domain.UpdateContact;
using Contacts.Endpoints;
using FastEndpoints;
using NUnit.Framework;

namespace Specs
{
    public static class Helpers
    {
        public static AddContactCommand CreateAddContactCommand(this Spec spec, string firstName = "First", string lastName = "Last",
            string number = "1231231234")
        {
            return new AddContactCommand()
            {
                FirstName = firstName,
                LastName = lastName,
                Number = number
            };
        }

        public static (HttpResponseMessage, TResponse) AddContactRaw<TResponse>(this Spec spec, AddContactCommand command)
        {
            return Spec.Client
                .PUTAsync<AddContactEndpoint, AddContactCommand, TResponse>(command)
                .GetAwaiter()
                .GetResult();
        }

        public static Guid AddContact(this Spec spec, AddContactCommand command)
        {
            return AddContactRaw<AddContactResult>(spec, command).Item2.ContactId;
        }

        public static Guid CreateAndAddContact(this Spec spec, string firstName = "First", string lastName = "Last",
            string number = "1231231234")
        {
            return AddContact(spec, CreateAddContactCommand(spec, firstName, lastName, number));
        }

        public static ContactNumber GetContact(this Spec spec, Guid id)
        {
            (HttpResponseMessage? response, GetContactResult? result) = Spec.Client
                .GETAsync<GetContactEndpoint, GetContactQuery, GetContactResult>(new GetContactQuery(id: id.ToString()))
                .GetAwaiter()
                .GetResult();

            return result.ContactNumber;
        }

        public static void IsEqualTo(this ContactNumber contactNumber, AddContactCommand command)
        {
            Assert.That(contactNumber.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(contactNumber.LastName, Is.EqualTo(command.LastName));
            Assert.That(contactNumber.Number, Is.EqualTo(command.Number));
        }

        public static (HttpResponseMessage?, TResponse?) UpdateContactRaw<TResponse>(this Spec spec, UpdateContactCommand command)
        {
            return Spec.Client
                .POSTAsync<UpdateContactEndpoint, UpdateContactCommand, TResponse>(command)
                .GetAwaiter()
                .GetResult();
        }

        public static UpdateContactResult UpdateContact(this Spec spec, UpdateContactCommand command)
        {
            (HttpResponseMessage? response, UpdateContactResult? result) = UpdateContactRaw<UpdateContactResult>(spec, command);
            Assert.That(response.IsSuccessStatusCode);
            return result;
        }

        public static bool IsEqualTo(this ContactNumber contactNumber, UpdateContactCommand command)
        {
            Assert.That(contactNumber.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(contactNumber.LastName, Is.EqualTo(command.LastName));
            Assert.That(contactNumber.Number, Is.EqualTo(command.Number));

            return true;
        }

        public static bool IsEqualTo(this ContactNumber contactNumber, ContactNumber other)
        {
            Assert.That(contactNumber.FirstName, Is.EqualTo(other.FirstName));
            Assert.That(contactNumber.LastName, Is.EqualTo(other.LastName));
            Assert.That(contactNumber.Number, Is.EqualTo(other.Number));

            return true;
        }

        public static UpdateContactCommand CreateUpdateContactCommand(this Spec spec, string id,
            string firstName = "First", string lastName = "Last", string number = "1231231234")
        {
            return new UpdateContactCommand
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Number = number
            };
        }

        public static (HttpResponseMessage?, TResponse?) DeleteContactRaw<TResponse>(this Spec spec, DeleteContactCommand command)
        {
            string uri = $"/contacts/{command.Id}";
            HttpResponseMessage res = Spec.Client.DeleteAsync(uri).GetAwaiter().GetResult();

            TResponse? body;
            try
            {
                body = res.Content.ReadFromJsonAsync<TResponse>().GetAwaiter().GetResult();
            }
            catch (JsonException)
            {
                var reason = $"[{res.StatusCode}] {res.Content.ReadAsStringAsync().GetAwaiter().GetResult()}";
                throw new InvalidOperationException(
                    $"Unable to deserialize the response body as [{typeof(TResponse).FullName}]. Reason: {reason}");
            }

            return (res, body);

        }

        public static DeleteContactResult DeleteContact(this Spec spec, DeleteContactCommand command)
        {
            (HttpResponseMessage? response, DeleteContactResult? result)  = DeleteContactRaw<DeleteContactResult>(spec, command);
            Assert.That(response.IsSuccessStatusCode);
            return result;
        }

        public static ListContactsResult ListContacts(this Spec spec)
        {
            (HttpResponseMessage? response, ListContactsResult? result) = Spec.Client
                .GETAsync<ListContactsEndpoint, ListContactsQuery, ListContactsResult>(new ListContactsQuery())
                .GetAwaiter()
                .GetResult();

            Assert.That(response.IsSuccessStatusCode);
            return result;
        }

        public static Func<ContactNumber, ContactNumber, bool> Comparer = (x, y) =>
            x.FirstName == y.FirstName && x.LastName == y.LastName && x.Number == y.Number;

    }
}
