using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contacts.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using NUnit.Framework;

namespace Specs
{
    [TestFixture]
    public abstract class Spec
    {
        private static IMigrator migrator;

        private static readonly WebApplicationFactory<Program> factory = new();
        public static HttpClient Client { get; } = factory.CreateClient();
        protected static ContactsContext contactsContext;

        static Spec()
        {
            contactsContext = (ContactsContext)factory.Services.GetService(typeof(ContactsContext));
            migrator = contactsContext.Database.GetService<IMigrator>();
            migrator.Migrate("0");
        }

        [SetUp]
        public void SetUp()
        {
            migrator.Migrate();
        }

        [TearDown]
        public void TearDown()
        {
            migrator.Migrate("0");
        }
    }
}
