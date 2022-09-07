global using FastEndpoints;
global using FastEndpoints.Validation;
using Contacts.Data;
using Contacts.Domain.AddContact;
using Contacts.Domain.DeleteContact;
using Contacts.Domain.GetContact;
using Contacts.Domain.ListContacts;
using Contacts.Domain.UpdateContact;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services.AddScoped<AddContactHandler>();
builder.Services.AddScoped<ISaveContact, SaveContact>();
builder.Services.AddScoped<IGetContactByNumber, GetContactByNumber>();
builder.Services.AddScoped<GetContactHandler>();
builder.Services.AddScoped<IFindContact, FindContact>();
builder.Services.AddScoped<UpdateContactHandler>();
builder.Services.AddScoped<IEditContact, EditContact>();
builder.Services.AddScoped<DeleteContactHandler>();
builder.Services.AddScoped<IDeleteContact, DeleteContact>();
builder.Services.AddScoped<ListContactsHandler>();
builder.Services.AddScoped<IListContacts, ListContacts>();

builder.Services.AddDbContext<ContactsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ContactsContext")));

var app = builder.Build();
//app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi(); 
app.UseSwaggerUi3(c => c.ConfigureDefaults()); 
app.Run();

public partial class Program { }