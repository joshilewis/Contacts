namespace Contacts.Domain.DeleteContact
{
    public class DeleteContactCommand
    {
        public string Id { get; set; }

        public DeleteContactCommand(string id)
        {
            Id = id;
        }

        public DeleteContactCommand()
        {
            
        }
    }
}
