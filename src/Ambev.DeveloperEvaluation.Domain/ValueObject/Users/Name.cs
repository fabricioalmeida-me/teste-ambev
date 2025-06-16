namespace Ambev.DeveloperEvaluation.Domain.ValueObject.Users;

public class Name
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;

    public Name(string firstname, string lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
    }

    public Name() { } 
}