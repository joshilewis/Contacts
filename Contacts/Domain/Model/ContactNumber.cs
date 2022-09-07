namespace Contacts.Domain.Model;
public class ContactNumber
{
    public  Guid Id { protected set; get; }
    public string FirstName { protected set; get; }
    public string LastName { protected set; get; }
    public string Number { protected set; get; }

    public ContactNumber(Guid id, string firstName, string lastName, string number)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Number = number;
    }

    protected ContactNumber()
    {
        
    }

    public void SetFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    public void SetNumber(string number)
    {
        Number = number;
    }

    public void Edit(string firstName, string lastName, string number)
    {
        FirstName = firstName;
        LastName = lastName;
        Number = number;    
    }

}
