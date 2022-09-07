using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Domain.ListContacts;
using Contacts.Domain.Model;
using NUnit.Framework;
using static Specs.Helpers;

namespace Specs
{
    public class ListContactsSpec : Spec
    {
        [Test]
        public void NoContacts()
        {
            ListContactsResult result = this.ListContacts();
            Assert.That(result.IsSuccessful);
            Assert.That(result.ContactNumbers, Is.Empty);
        }

        [Test]
        public void Success()
        {
            Guid id1 = this.CreateAndAddContact(firstName: "first1", "last1", "1111111111");
            Guid id2 = this.CreateAndAddContact(firstName: "first2", "last2", "2222222222");
            Guid id3 = this.CreateAndAddContact(firstName: "first3", "last3", "3333333333");
            Guid id4 = this.CreateAndAddContact(firstName: "first4", "last4", "4444444444");

            ContactNumber contact1 = this.GetContact(id1);
            ContactNumber contact2 = this.GetContact(id2);
            ContactNumber contact3 = this.GetContact(id3);
            ContactNumber contact4 = this.GetContact(id4);

            ListContactsResult result = this.ListContacts();
            Assert.That(result.ContactNumbers.Count(), Is.EqualTo(4));

            var expected = new[] { contact1, contact2, contact3, contact4 };
            Assert.That(result.ContactNumbers, Is.EquivalentTo(expected).Using(Comparer));

        }

    }
}
